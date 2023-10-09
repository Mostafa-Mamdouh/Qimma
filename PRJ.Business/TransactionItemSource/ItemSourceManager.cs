using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs;
using PRJ.Application.Enums;
using PRJ.Application.Warppers;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Encryption;
using cns = PRJ.GlobalComponents.Const;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business
{
    public class ItemSourceManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        Dictionary<string, dynamic> RadionuclideValues = new Dictionary<string, dynamic>();

        public ItemSourceManager(rep.IUnitOfWork manager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _manager = manager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<wap.Response<bool>> SaveTransactionItemSource(DTOItemSourceEditor body)
        {
            var result = false;


            var trnData = _mapper.Map<TrnItemSource>(body);

            if (!String.IsNullOrEmpty(body.ManufacturerSerialNo) && body.SourceStatus == (int)TrnSourceStatus.Draft)
            {
                if (IsSerialNumberExistInTrn(body.ManufacturerSerialNo))
                {
                    return new Response<bool>
                    {
                        Succeeded = false,
                        Message = "exist"
                    };
                }
            }

            if (String.IsNullOrEmpty(body.Id))
            {
                // trn Header
                var header = new TransactionHeader();
                header.TransactionTypeId = body.TransactionType;
                header.TrnStatus = (int)body.SourceStatus;
                if (body.SourceStatus == (int)TrnSourceStatus.Submit)
                {
                    header.ConfirmedUser = "Hamza Ibrahim";
                    header.ConfirmedDate = DateTime.Now;
                }
                trnData.TransactionsHeader = header;

                // trn Radionuclides
                trnData.TrnItemSourceRadionuclides = _mapper.Map<List<TrnItemSourceRadionuclides>>(body.Radionuclides);

                // trn Attachment
                int sourceAttachCount = 1;
                foreach (var item in body.SourceAttachments)
                {
                    var trnSourceAttachmenData = _mapper.Map<TransactionAttachments>(item);
                    trnSourceAttachmenData.FileNum = sourceAttachCount;
                    trnData.TransactionAttactcments.Add(trnSourceAttachmenData);
                    sourceAttachCount++;
                }
                int shieldAttachCount = 1;
                foreach (var item in body.ShieldAttachments)
                {
                    var trnShieldAttachmentData = _mapper.Map<TransactionAttachments>(item);
                    trnShieldAttachmentData.FileNum = shieldAttachCount;
                    trnShieldAttachmentData.ForShield = true;
                    trnData.TransactionAttactcments.Add(trnShieldAttachmentData);
                    shieldAttachCount++;
                }

                trnData.NrrcId = trnData.TrnSourceId.ToString();


                if ((trnData.SourceType == (int)SourceType.SealedSource) && trnData.IsShieldDU)
                    trnData.NrrcId = "S-SA11" + trnData.TrnSourceId;
                else if (trnData.SourceType == (int)SourceType.UnsealedSource)
                    trnData.NrrcId = "U-SA11" + trnData.TrnSourceId;
                else if (trnData.SourceType == (int)SourceType.VariableUnsealed)
                    trnData.NrrcId = "V-SA11" + trnData.TrnSourceId;

                result = await _manager.TrnItemSource.AddAsync(trnData);


                if ((trnData.SourceType == (int)SourceType.SealedSource) && trnData.IsShieldDU)
                {
                    if (!string.IsNullOrEmpty(trnData.ShieldNuclearMaterialCode))
                    {
                        var shield = await _manager.NuclearMaterial.GetByIdAsync(int.Parse(trnData.ShieldNuclearMaterialCode));
                        shield.IsShield = true;
                    }
                }
            }
            else
            {
                var rads = _manager.TrnItemSourceRadionuclides.GetByQuery(x => x.TrnSourceID == trnData.TrnSourceId).ToList();
                foreach (var rad in rads)
                {
                    await _manager.TrnItemSourceRadionuclides.DeleteAsync(rad);
                }
                trnData.TrnItemSourceRadionuclides = _mapper.Map<List<TrnItemSourceRadionuclides>>(body.Radionuclides);

                //var attachments = _manager.TrnItemSourceFiles.GetByQuery(x => x.TrnSourceID == trnData.TrnSourceId).ToList();

                _manager.TrnItemSourceFiles.DeleteByParentId(x => x.TrnSourceID == trnData.TrnSourceId);

                int sourceAttachCount = 1;
                foreach (var item in body.SourceAttachments)
                {
                    var trnSourceAttachmenData = _mapper.Map<TransactionAttachments>(item);
                    trnSourceAttachmenData.FileNum = sourceAttachCount;
                    trnData.TransactionAttactcments.Add(trnSourceAttachmenData);
                    sourceAttachCount++;
                }
                int shieldAttachCount = 1;
                foreach (var item in body.ShieldAttachments)
                {
                    var trnShieldAttachmentData = _mapper.Map<TransactionAttachments>(item);
                    trnShieldAttachmentData.FileNum = shieldAttachCount;
                    trnShieldAttachmentData.ForShield = true;
                    trnData.TransactionAttactcments.Add(trnShieldAttachmentData);
                    shieldAttachCount++;
                }

                _manager.TrnItemSource.Update(trnData);
                result = true;
            }

            if (body.SourceStatus == (int)TrnSourceStatus.Submit && (body.TransactionType != 2 || String.IsNullOrEmpty(body.Id)))
            {
                var header = _manager.TransactionHeader.GetById(trnData.TransactionHeaderId);
                header.TrnStatus = (int)TrnSourceStatus.Submit;
                _manager.TransactionHeader.Update(header);
                if (!IsSerialNumberExist(trnData.ManufacturerSerialNo))
                {
                    AddItemSourceProfile(trnData);
                }
                else
                {
                    return new Response<bool>
                    {
                        Succeeded = false,
                        Message = "exist"
                    };
                }
            }
            else if (body.SourceStatus == (int)TrnSourceStatus.Submit && body.TransactionType == 2 && !String.IsNullOrEmpty(body.Id))
            {
                var header = _manager.TransactionHeader.GetById(trnData.TransactionHeaderId);
                header.TrnStatus = (int)TrnSourceStatus.Submit;
                header.TransactionTypeId = 2;
                _manager.TransactionHeader.Update(header);
                UpdateItemSourceProfile(trnData);
            }
            await _manager.CompleteAsync();

            return new wap.Response<bool>(result);

        }


        private TrnItemSource AddItemSourceProfile(TrnItemSource trnData)
        {
            var ItemSourceData = _mapper.Map<ent.ItemSourceProfile>(trnData);
            ItemSourceData.CurrentTrnStatus = (int)SourceStatus.InitiatedByFacility;
            // source status History
            var sourcestatusHistory = new ItemSourceStatusHistory();
            sourcestatusHistory.StatusId = (int)SourceStatus.InitiatedByFacility;
            ItemSourceData.ItemSourceStatusHistories.Add(sourcestatusHistory);
            trnData.ItemSourceProfile = ItemSourceData;
            return trnData;
        }

        private TrnItemSource UpdateItemSourceProfile(TrnItemSource trnData)
        {
            var ItemSourceData = _manager.ItemSourceProfile.GetByQuery(x => x.TrnSourceId == trnData.TrnSourceId).FirstOrDefault();
            if (ItemSourceData != null)
            {
                var sourcestatusHistory = new ItemSourceStatusHistory();
                sourcestatusHistory.StatusId = (int)SourceStatus.InitiatedByFacility;
                ItemSourceData.CurrentTrnStatus = (int)SourceStatus.InitiatedByFacility;
                ItemSourceData.ItemSourceStatusHistories.Add(sourcestatusHistory);
                ItemSourceData.FacilitySerialNo = trnData.FacilitySerialNo;
                ItemSourceData.Status = trnData.Status;
                trnData.ItemSourceProfile = ItemSourceData;
                _manager.ItemSourceProfile.Update(ItemSourceData);
            }
            else
            {
                trnData.ItemSourceProfile = ItemSourceData;
                AddItemSourceProfile(trnData);
            }
            return trnData;
        }

        // Check if ManufacturerSerialNo in ItemSourceProfile
        private bool IsSerialNumberExist(string serial)
        {
            return _manager.ItemSourceProfile.GetByQuery(x => x.ManufacturerSerialNo == serial).Any();
        }

        // Check if ManufacturerSerialNo in TrnItemSource
        private bool IsSerialNumberExistInTrn(string serial)
        {
            return _manager.TrnItemSource.GetByQuery(x => x.ManufacturerSerialNo == serial).Any();
        }

        // Get Paginated, filtered, sorted
        public async Task<wap.PagingResponse<List<DTOlandingPagepagination>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<DTOlandingPagepagination>>()
            {
                Draw = param.Draw
            };

            IQueryable<ent.TrnItemSource> dataList = _manager.TrnItemSource.GetAll()
               .Include(x => x.ItemSourceProfile)
               .Include(x => x.LicenseProfile)
               .Include(x => x.FacilityProfile)
               .Include(x => x.PermitDetailsProfile)
               .Include(x => x.StatusLookup)
               .Include(x => x.TrnItemSourceRadionuclides)
               .ThenInclude(x => x.InitialActivityUnitLookup)
               .Include(x => x.SourceTypeLookup)
               .Include(x => x.TransactionsHeader)
               .ThenInclude(x => x.TransactionTypeMaster)
               .Include(x => x.TrnItemSourceRadionuclides)
               .ThenInclude(x => x.Radionuclides)
               .ThenInclude(x => x.HalfLifeUnit);
            // Get all
            if (!String.IsNullOrEmpty(param.ForPage))
            {
                if (param.ForPage.Equals("incident"))
                {
                    dataList =
                   _manager.TrnItemSource.GetByQuery(x => x.StatusLookup.Value == "d" || x.StatusLookup.Value == "l")
                   .Include(x => x.ItemSourceProfile)
                   .Include(x => x.LicenseProfile)
                   .Include(x => x.FacilityProfile)
                   .Include(x => x.PermitDetailsProfile)
                   .Include(x => x.StatusLookup)
                   .Include(x => x.TrnItemSourceRadionuclides)
                   .ThenInclude(x => x.InitialActivityUnitLookup)
                   .Include(x => x.SourceTypeLookup)
                   .Include(x => x.TransactionsHeader)
                   .ThenInclude(x => x.TransactionTypeMaster)
                   .Include(x => x.TrnItemSourceRadionuclides)
                   .ThenInclude(x => x.Radionuclides)
                   .ThenInclude(x => x.HalfLifeUnit);
                }
                else if (param.ForPage.Equals("waste"))
                {
                    dataList =
                   _manager.TrnItemSource.GetByQuery(x => x.StatusLookup.Value == "w")
                   .Include(x => x.ItemSourceProfile)
                   .Include(x => x.LicenseProfile)
                   .Include(x => x.FacilityProfile)
                   .Include(x => x.PermitDetailsProfile)
                   .Include(x => x.StatusLookup)
                   .Include(x => x.TrnItemSourceRadionuclides)
                   .ThenInclude(x => x.InitialActivityUnitLookup)
                   .Include(x => x.SourceTypeLookup)
                   .Include(x => x.TransactionsHeader)
                   .ThenInclude(x => x.TransactionTypeMaster)
                   .Include(x => x.TrnItemSourceRadionuclides)
                   .ThenInclude(x => x.Radionuclides)
                   .ThenInclude(x => x.HalfLifeUnit);
                }
            }


            if (param.data != null && param.data > 0)
            {
                var licenseId = int.Parse(EncryptQueryURL.Decrypt(param.extra));
                dataList = dataList.Where(r => r.TransactionsHeader.TrnStatus == param.data && r.LicenseId == licenseId);
            }

            if (param.extra != null)
            {
                var licenseId = int.Parse(EncryptQueryURL.Decrypt(param.extra));
                dataList = dataList.Where(r => r.LicenseId == licenseId);
            }


            // Check if search is on specific column
            bool searchByColumn = false;
            foreach (var search in param.Columns)
            {
                if (!string.IsNullOrEmpty(search.Search.Value))
                {
                    searchByColumn = true;
                    break;
                }
            }

            // General Search
            IQueryable<ent.TrnItemSource> query = null;
            if (!string.IsNullOrEmpty(param.Search.Value))
            {
                query = dataList.Where(r =>
                (param.lang == (int)Language.En ?
                r.FacilityProfile.FacilityNameEn.ToLower().Contains(param.Search.Value.ToLower().ToString()) :
                r.FacilityProfile.FacilityNameAr.ToLower().Contains(param.Search.Value.ToLower().ToString()))
                ||
                r.TrnItemSourceRadionuclides.Any(c => c.Radionuclides.DisplayName.ToLower().Contains(param.Search.Value.ToLower().ToString()))
                ||
                (r.LicenseProfile.LicenseCode.ToLower().Contains(param.Search.Value.ToLower()))
                ||
                (r.PermitDetailsProfile.PermitNumber.ToString().ToLower().Contains(param.Search.Value.ToLower()))
                ||
                (r.ManufacturerSerialNo.ToLower().Contains(param.Search.Value.ToLower()))
                ||
                (param.lang == (int)Language.En ?
                r.SourceTypeLookup.DisplayNameEn.ToLower().Contains(param.Search.Value.ToLower().ToString()) :
                r.SourceTypeLookup.DisplayNameAr.ToLower().Contains(param.Search.Value.ToLower().ToString())));
            }
            else if (searchByColumn) // search by column
            {
                foreach (var search in param.Columns)
                {
                    if (!string.IsNullOrEmpty(search.Search.Value))
                    {

                        switch (search.Data)
                        {
                            case "radionuclideName":
                                query = dataList.Where(u =>
                    u.TrnItemSourceRadionuclides.Any(x => x.Radionuclides.DisplayName.Contains(search.Search.Value)));
                                break;
                            case "facilityEn":
                                query = dataList.Where(u =>
                    u.FacilityProfile.FacilityNameEn.Contains(search.Search.Value));
                                break;
                            case "facilityAr":
                                query = dataList.Where(u =>
                    u.FacilityProfile.FacilityNameAr.Contains(search.Search.Value));
                                break;
                            case "licenseNumber":
                                query = dataList.Where(u =>
                    u.LicenseProfile.LicenseDescEn.Contains(search.Search.Value));
                                break;
                            case "permitNumber":
                                query = dataList.Where(u =>
                    u.PermitDetailsProfile.PermitNumber.ToString().Contains(search.Search.Value));
                                break;
                            case "manufacturerSerialNo":
                                query = dataList.Where(u =>
                        u.ManufacturerSerialNo.Contains(search.Search.Value));
                                break;
                            case "sourceTypeEn":
                                query = dataList.Where(u =>
                        u.SourceTypeLookup.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "sourceTypeAr":
                                query = dataList.Where(u =>
                        u.SourceTypeLookup.DisplayNameAr.Contains(search.Search.Value));
                                break;
                            case "sourceStatusAr":
                                query = dataList.Where(u => "مسودة".Contains(search.Search.Value) ? u.TransactionsHeader.TrnStatus == 1 : u.TransactionsHeader.TrnStatus == 2);
                                break;
                            case "sourceStatusEn":
                                query = dataList.Where(u => "draft".Contains(search.Search.Value.ToLower()) ? u.TransactionsHeader.TrnStatus == 1 : u.TransactionsHeader.TrnStatus == 2);
                                break;
                            case "statusEn":
                                query = dataList.Where(u => u.StatusLookup.DisplayNameEn.Contains(search.Search.Value.ToLower()));
                                break;
                            case "statusAr":
                                query = dataList.Where(u => u.StatusLookup.DisplayNameAr.Contains(search.Search.Value.ToLower()));
                                break;
                            case "createdOn":
                                query = dataList.Where(u => u.CreatedOn.ToString().Contains(search.Search.Value));
                                break;
                        }
                    }
                }
            }
            else
            {
                query = dataList;
            }

            var colOrder = param.Order[1];

            if (param.lang == (int)Language.En)
            {
                switch (colOrder.Column)
                {

                    case 0:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.SourceTypeLookup.DisplayNameEn) :
                            query.OrderByDescending(r => r.SourceTypeLookup.DisplayNameEn);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.TrnItemSourceRadionuclides.Select(x => x.Radionuclides.DisplayName)) : query.OrderByDescending(r => r.TrnItemSourceRadionuclides.Select(x => x.Radionuclides.DisplayName));
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerSerialNo) :
                        query.OrderByDescending(r => r.ManufacturerSerialNo);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CreatedOn) : query.OrderByDescending(r => r.CreatedOn);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.StatusLookup.DisplayNameEn) : query.OrderByDescending(r => r.StatusLookup.DisplayNameEn);
                        break;
                }

            }
            else
            {
                switch (colOrder.Column)
                {


                    case 0:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.SourceTypeLookup.DisplayNameAr) :
                            query.OrderByDescending(r => r.SourceTypeLookup.DisplayNameAr);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.TrnItemSourceRadionuclides.Select(x => x.Radionuclides.DisplayName)) : query.OrderByDescending(r => r.TrnItemSourceRadionuclides.Select(x => x.Radionuclides.DisplayName));
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerSerialNo) :
                        query.OrderByDescending(r => r.ManufacturerSerialNo);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CreatedOn) : query.OrderByDescending(r => r.CreatedOn);
                        break;
                    case 5:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.StatusLookup.DisplayNameAr) : query.OrderByDescending(r => r.StatusLookup.DisplayNameAr);
                        break;


                }
            }

            var data = await query.Skip(param.Start).Take(param.Length).ToListAsync();
            response.Data = _mapper.Map<List<DTOlandingPagepagination>>(data);
            var recordsTotal = query.Count();
            response.RecordsTotal = recordsTotal;
            response.RecordsFiltered = recordsTotal;
            response.Succeeded = true;


            return response;


        }

        public async Task<wap.Response<DTOActionCenter>> GetById(string id)
        {

            var trnId = EncryptQueryURL.Decrypt(id.Replace(" ", "+"));
            var getById = await _manager.TrnItemSource.GetByQuery(x => x.TrnSourceId == int.Parse(trnId))
                .Include(x => x.ItemSourceProfile)
                .Include(x => x.TransactionAttactcments)
                    .Include(x => x.EntityProfile)
                    .Include(x => x.LicenseProfile)
                    .Include(x => x.FacilityProfile)
                    .Include(x => x.SourceTypeLookup)
                    .Include(x => x.TransactionsHeader).ThenInclude(x => x.TransactionTypeMaster)
                    .Include(x => x.PermitDetailsProfile)
                    .Include(x => x.ManufacturerLookup)
                    .Include(x => x.StatusLookup)
                    .Include(x => x.AssociatedEquipmentLookup)
                    .Include(x => x.ManufacturerCountryLookup)
                    .Include(x => x.TrnItemSourceRadionuclides).ThenInclude(x => x.Radionuclides)
                    .FirstOrDefaultAsync();


            if (getById != null)
            {
                return new wap.Response<DTOActionCenter>()
                {
                    Data = _mapper.Map<DTOActionCenter>(getById),
                };
            }
            else
            {
                return new wap.Response<DTOActionCenter>()
                {
                    Succeeded = true,
                    Message = cns.ConstErrors.NotFound
                };
            }

        }

        public async Task<wap.Response<DTOGetItemSourceTran>> GetByIdForLandingPage(string id)
        {

            var trnId = EncryptQueryURL.Decrypt(id.Replace(" ", "+"));
            var getById = await _manager.TrnItemSource.GetByQuery(x => x.TrnSourceId == int.Parse(trnId))
                .Include(x => x.ItemSourceProfile)
                .Include(x => x.TransactionAttactcments)
                    .Include(x => x.EntityProfile)
                    .Include(x => x.LicenseProfile)
                    .Include(x => x.FacilityProfile)
                    .Include(x => x.SourceTypeLookup)
                    .Include(x => x.TransactionsHeader).ThenInclude(x => x.TransactionTypeMaster)
                    .Include(x => x.PermitDetailsProfile)
                    .Include(x => x.ManufacturerLookup)
                    .Include(x => x.StatusLookup)
                    .Include(x => x.AssociatedEquipmentLookup)
                    .Include(x => x.ManufacturerCountryLookup)
                    .Include(x => x.TrnItemSourceRadionuclides).ThenInclude(x => x.Radionuclides)
                    .FirstOrDefaultAsync();



            if (getById != null)
            {
                return new wap.Response<DTOGetItemSourceTran>()
                {
                    Data = _mapper.Map<DTOGetItemSourceTran>(getById),
                };
            }
            else
            {
                return new wap.Response<DTOGetItemSourceTran>()
                {
                    Succeeded = true,
                    Message = cns.ConstErrors.NotFound
                };
            }

        }

        public async Task<wap.Response<bool>> DeleteDraft(string id)
        {

            var trnId = EncryptQueryURL.Decrypt(id.Replace(" ", "+"));
            var getById = await _manager.TrnItemSource.GetByQuery(x => x.TrnSourceId == int.Parse(trnId)).Include(x => x.TrnItemSourceRadionuclides).Include(x => x.TransactionAttactcments).FirstOrDefaultAsync();

            if (getById != null)
            {
                await _manager.TrnItemSource.DeleteAsync(getById);
                return new wap.Response<bool>()
                {
                    Data = true,
                };
            }
            else
            {
                return new wap.Response<bool>()
                {
                    Data = false,
                    Message = cns.ConstErrors.NotFound,
                };
            };


        }

        #region edit - transaction
        public async Task<wap.Response<DTOEditTrnResponse>> EditItemSource(DTOAddTrnItemSourceHistory body)
        {

            var source = await _manager.ItemSourceProfile.GetEncryptByIdAsync(body.ItemSourceProfileId);
            var sourceTrn = await _manager.TrnItemSource.GetByIdAsync(source.TrnSourceId);
            var trnType = await _manager.TransactionTypeMaster.GetByIdAsync(body.TransactionTypeId);

            var trnData = _mapper.Map<TrnItemSourceHistory>(body);


            trnData.ItemSourceProfile = source;
            trnData.TransactionTypeMaster = trnType;
            trnData.CreatedOn = DateTime.Now;

            if (body.TransactionAttribute != (int)TransactionAttributes.facilitySourceId && body.TransactionAttribute != (int)TransactionAttributes.quantity)
            {
                trnData.OldValue = EncryptQueryURL.Decrypt(body.OldValue.Replace(" ", "+"));
                trnData.NewValue = EncryptQueryURL.Decrypt(body.NewValue.Replace(" ", "+"));
            }


            await _manager.TrnItemSourceHistory.AddAsync(trnData);


            if (body.TransactionAttribute == (int)TransactionAttributes.facilitySourceId)
            {
                source.FacilitySerialNo = trnData.NewValue;
                sourceTrn.FacilitySerialNo = trnData.NewValue;
            }
            else if (body.TransactionAttribute == (int)TransactionAttributes.quantity)
            {
                source.Quantity = int.Parse(trnData.NewValue);
                sourceTrn.Quantity = int.Parse(trnData.NewValue);
            }
            else if (body.TransactionAttribute == (int)TransactionAttributes.associatedEquipment)
            {
                var lookupObj = _manager.LookupSetTerm.GetById(int.Parse(trnData.NewValue));
                source.AssociatedEquipment = lookupObj.LookupSetTermId;
                source.AssociatedEquipmentLookup = lookupObj;
                sourceTrn.AssociatedEquipment = lookupObj.LookupSetTermId;
                sourceTrn.AssociatedEquipmentLookup = lookupObj;
            }
            else if (body.TransactionAttribute == (int)TransactionAttributes.status)
            {
                var lookupObj = _manager.LookupSetTerm.GetById(int.Parse(trnData.NewValue));
                source.Status = lookupObj.LookupSetTermId;
                source.StatusLookup = lookupObj;
                sourceTrn.Status = lookupObj.LookupSetTermId;
                sourceTrn.StatusLookup = lookupObj;
            }
            else if (body.TransactionAttribute == (int)TransactionAttributes.shield)
            {
                var nuclearMaterial = _manager.NuclearMaterial.GetById(int.Parse(trnData.NewValue));
                var oldNuclearMaterial = _manager.NuclearMaterial.GetById(int.Parse(trnData.OldValue));
                source.ShieldNuclearMaterialCode = nuclearMaterial.SourceId;
                sourceTrn.ShieldNuclearMaterialCode = nuclearMaterial.SourceId.ToString();
                oldNuclearMaterial.IsShield = false;
                oldNuclearMaterial.ItemSourceProfile = null;
            }


            await _manager.CompleteAsync();

            return new wap.Response<DTOEditTrnResponse>()
            {
                Succeeded = true,
                Message = $"Your Request Submitted Succussfully With Ref No. {trnData.TrnHistoryId.ToString()}",
                MessageAr = $" :لقد تم إرسال طلبك بنجاح رقم الطلب {trnData.TrnHistoryId.ToString()} "
            };

        }
        #endregion

        #region get-edit-history
        public async Task<wap.Response<List<DTOGetTrnItemSourceHistory>>> GetEditHistory(string id, string trnType)
        {

            var pk = EncryptQueryURL.Decrypt(id.Replace(" ", "+"));
            var trnTypeId = EncryptQueryURL.Decrypt(trnType.Replace(" ", "+"));
            var history = await _manager.TrnItemSourceHistory.GetByQuery(x => (x.ItemSourceProfileId == int.Parse(pk)) && (x.TransactionTypeId == int.Parse(trnTypeId))).ToListAsync();

            if ((int.Parse(trnTypeId) != (int)TransactionType.ChangeFacilityId) && (int.Parse(trnTypeId) != (int)TransactionType.ChangeSourceShield && int.Parse(trnTypeId) != (int)TransactionType.ChangeQuantity))
            {
                foreach (var item in history)
                {
                    item.NewValue = _manager.LookupSetTerm.GetById(int.Parse(item.NewValue)).DisplayNameEn;
                    item.OldValue = _manager.LookupSetTerm.GetById(int.Parse(item.OldValue)).DisplayNameEn;
                }
            }
            else if (int.Parse(trnTypeId) == (int)TransactionType.ChangeSourceShield)
            {
                foreach (var item in history)
                {
                    item.NewValue = _manager.NuclearMaterial.GetById(int.Parse(item.NewValue)).ManufacturerSerialNo;
                    item.OldValue = _manager.NuclearMaterial.GetById(int.Parse(item.OldValue)).ManufacturerSerialNo;
                }
            }
            if (history == null)
            {
                return new wap.Response<List<DTOGetTrnItemSourceHistory>>()
                {
                    Succeeded = true,
                    Message = cns.ConstErrors.NotFound
                };
            }

            var data = _mapper.Map<List<DTOGetTrnItemSourceHistory>>(history);

            return new wap.Response<List<DTOGetTrnItemSourceHistory>>()
            {
                Succeeded = true,
                Data = data,
            };

        }
        #endregion

        #region create-similer
        public async Task<wap.Response<DTOGetItemSourceTran>> CreateSimiler(string id)
        {
            var trnId = EncryptQueryURL.Decrypt(id.Replace(" ", "+"));
            var getById = await _manager.TrnItemSource.GetByQuery(x => x.TrnSourceId == int.Parse(trnId))
                .Include(x => x.TransactionAttactcments)
                    .Include(x => x.EntityProfile)
                    .Include(x => x.LicenseProfile)
                    .Include(x => x.FacilityProfile)
                    .Include(x => x.SourceTypeLookup)
                    .Include(x => x.TransactionsHeader).ThenInclude(x => x.TransactionTypeMaster)
                    .Include(x => x.PermitDetailsProfile)
                    .Include(x => x.ManufacturerLookup)
                    .Include(x => x.StatusLookup)
                    .Include(x => x.AssociatedEquipmentLookup)
                    .Include(x => x.ManufacturerCountryLookup)
                    .Include(x => x.TrnItemSourceRadionuclides).ThenInclude(x => x.Radionuclides)
                    .FirstOrDefaultAsync();

            if (getById != null)
            {
                TrnItemSource copy = _mapper.Map<TrnItemSource>(getById);

                // trn Header
                var header = new TransactionHeader();
                header.TransactionTypeId = copy.TransactionsHeader.TransactionTypeId;
                header.TrnStatus = (int)TrnSourceStatus.Draft;
                copy.TransactionsHeader = header;

                copy.ManufacturerSerialNo = null;
                copy.AssociatedEquipment = null;
                copy.AssociatedEquipmentLookup = null;
                copy.ShieldNuclearMaterialCode = null;
                copy.FacilitySerialNo = null;
                await _manager.TrnItemSource.AddAsync(copy);
                return new wap.Response<DTOGetItemSourceTran>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<DTOGetItemSourceTran>(getById),
                };
            }
            else
            {
                return new wap.Response<DTOGetItemSourceTran>()
                {
                    Succeeded = true,
                    Message = cns.ConstErrors.NotFound
                };
            }
        }
        #endregion

        #region get-sources-by-permit
        public async Task<wap.Response<List<DTOGetSourcesByPermit>>> GetSourcesByPermit(string permitId, int sourceType)
        {
            var _permitId = EncryptQueryURL.Decrypt(permitId.Replace(" ", "+"));
            var sources = _manager.TrnItemSource.GetByQuery(x => x.PermitdetailsId == int.Parse(_permitId) && x.TransactionsHeader.TrnStatus == 2 && x.SourceType == sourceType && x.TransactionsHeader.TransactionTypeId != 2).OrderByDescending(x => x.TrnSourceId);

            if (sources == null)
            {
                return new wap.Response<List<DTOGetSourcesByPermit>>()
                {

                    Message = "-1",
                    Succeeded = true,
                };
            }
            else
            {
                return new wap.Response<List<DTOGetSourcesByPermit>>()
                {

                    Data = _mapper.Map<List<DTOGetSourcesByPermit>>(sources),
                    Succeeded = true,
                };
            }
        }
        #endregion
    }
}

