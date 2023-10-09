using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs;
using PRJ.Application.Enums;
using PRJ.Application.Warppers;
using PRJ.GlobalComponents.Encryption;
using cns = PRJ.GlobalComponents.Const;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;
namespace PRJ.Business
{
    public class ActionCenterManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public ActionCenterManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<bool>> AddNewStatusHistory(DTOItemSourceStatusHistoryEditor body)
        {

            var data = _mapper.Map<ent.ItemSourceStatusHistory>(body);
            await _manager.ItemSourceStatusHistory.AddAsync(data);

            return new wap.Response<bool>() { Data = true };
        }

        public async Task<wap.Response<bool>> AddNewMsgHistory(DTOItemSourceMsgHistoryEditor body)
        {

            var data = _mapper.Map<ent.ItemSourceMsgHistory>(body);
            await _manager.ItemSourceMsgHistory.AddAsync(data);

            return new wap.Response<bool>() { Data = true };


        }

        public async Task<wap.Response<DTOActionCenter>> GetById(string id)
        {

            var trnId = EncryptQueryURL.Decrypt(id.Replace(" ", "+"));
            var getById = await _manager.ItemSourceProfile.GetByQuery(x => x.SourceId == int.Parse(trnId))
                .Include(x => x.TrnItemSource)
                .Include(x => x.ItemSourceFiles)
                    .Include(x => x.EntityProfile)
                    .Include(x => x.LicenseProfile)
                    .Include(x => x.FacilityProfile)
                    .Include(x => x.SourceTypeLookup)
                    .Include(x => x.TrnItemSource.TransactionsHeader).ThenInclude(x => x.TransactionTypeMaster)
                    .Include(x => x.PermitDetailsProfile)
                    .Include(x => x.TrnItemSource.TrnItemSourceRadionuclides)
                    .ThenInclude(x => x.InitialActivityUnitLookup)
                    .Include(x => x.ItemSourceStatusHistories).ThenInclude(c => c.ItemSourceStatus)
                    .Include(x => x.ItemSourceMsgs)
                    .Include(x => x.ManufacturerLookup)
                    .Include(x => x.StatusLookup)
                    .Include(x => x.AssociatedEquipmentLookup)
                    .Include(x => x.ShieldNuclearMaterial)
                    .Include(x => x.ManufacturerCountryLookup)
                    .Include(x => x.ItemSourceRadionulcides)
                    .ThenInclude(x => x.Radionuclides)
                    .ThenInclude(x => x.HalfLifeUnit)
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


        // Get Paginated, filtered, sorted
        public async Task<wap.PagingResponse<List<DTOActionCenter>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<DTOActionCenter>>()
            {
                Draw = param.Draw
            };

            if (!string.IsNullOrEmpty(param.Id))
            {

                var transaionTypeId = int.Parse(EncryptQueryURL.Decrypt(param.Id.Replace(" ", "+")));

                // Get all
                IQueryable<ent.ItemSourceProfile> dataList = _manager.ItemSourceProfile.GetByQuery(x => (!x.TrnItemSourceHistory.Any() && x.TrnItemSource.TransactionsHeader.TransactionTypeId == transaionTypeId) || (x.TrnItemSourceHistory.OrderByDescending(x => x.TrnHistoryId).Where(x => x.TransactionTypeId == transaionTypeId).Count() > 0))
                    .Include(x => x.EntityProfile).Include(x => x.LicenseProfile)
                    .Include(x => x.FacilityProfile)
                    .Include(x => x.SourceTypeLookup)
                    .Include(x => x.TrnItemSource).ThenInclude(x => x.TransactionsHeader).ThenInclude(x => x.TransactionTypeMaster)
                    .Include(x => x.PermitDetailsProfile)
                    .Include(x => x.ShieldNuclearMaterial)
                    .Include(x => x.ItemSourceRadionulcides).ThenInclude(x => x.InitialActivityUnitLookup)
                    .Include(x => x.ItemSourceStatusHistories).ThenInclude(c => c.ItemSourceStatus)
                    .Include(x => x.ItemSourceMsgs)
                    .Include(x => x.ManufacturerLookup)
                    .Include(x => x.StatusLookup)
                    .Include(x => x.AssociatedEquipmentLookup)
                    .Include(x => x.ManufacturerCountryLookup)
                    .Include(x => x.ItemSourceRadionulcides).ThenInclude(x => x.Radionuclides).ThenInclude(x => x.HalfLifeUnit)
                    ;

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
                IQueryable<ent.ItemSourceProfile> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                       (param.lang == (int)Language.En ?
                        r.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesFrn.Contains(param.Search.Value) :
                        r.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesNtv.Contains(param.Search.Value))
                       ||
                       (param.lang == (int)Language.En ?
                       r.EntityProfile.EntityNameEn.Contains(param.Search.Value) :
                       r.EntityProfile.EntityNameAr.Contains(param.Search.Value))
                       ||
                       (param.lang == (int)Language.En ?
                       r.FacilityProfile.FacilityNameEn.Contains(param.Search.Value) :
                       r.FacilityProfile.FacilityNameAr.Contains(param.Search.Value))
                       ||
                      (param.lang == (int)Language.En ?
                       r.LicenseProfile.LicenseDescEn.Contains(param.Search.Value) :
                       r.LicenseProfile.LicenseDescAr.Contains(param.Search.Value))
                       ||
                       (param.lang == (int)Language.En ?
                       r.SourceTypeLookup.DisplayNameEn.Contains(param.Search.Value) :
                       r.SourceTypeLookup.DisplayNameAr.Contains(param.Search.Value))
                       ||
                       (param.lang == (int)Language.En ?
                       r.ItemSourceStatusHistories.AsQueryable().OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameEn.Contains(param.Search.Value) :
                       r.ItemSourceStatusHistories.AsQueryable().OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameEn.Contains(param.Search.Value)));

                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {

                            switch (search.Data)
                            {
                                case "transactionDate":
                                    query = dataList.Where(u =>
                        u.CreatedOn.ToString().Contains(search.Search.Value));
                                    break;
                                case "transactionTypeEn":
                                    query = dataList.Where(u =>
                        u.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesFrn.Contains(search.Search.Value));
                                    break;
                                case "transactionTypeAr":
                                    query = dataList.Where(u =>
                        u.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesNtv.Contains(search.Search.Value));
                                    break;
                                case "facilityEn":
                                    query = dataList.Where(u =>
                        u.FacilityProfile.FacilityNameEn.Contains(search.Search.Value));
                                    break;
                                case "facilityAr":
                                    query = dataList.Where(u =>
                        u.FacilityProfile.FacilityNameAr.Contains(search.Search.Value));
                                    break;
                                case "entityEn":
                                    query = dataList.Where(u =>
                        u.EntityProfile.EntityNameEn.Contains(search.Search.Value));
                                    break;
                                case "entityAr":
                                    query = dataList.Where(u =>
                        u.EntityProfile.EntityNameAr.Contains(search.Search.Value));
                                    break;
                                case "licenseEn":
                                    query = dataList.Where(u =>
                        u.LicenseProfile.LicenseDescEn.Contains(search.Search.Value));
                                    break;
                                case "licenseAr":
                                    query = dataList.Where(u =>
                        u.LicenseProfile.LicenseDescAr.Contains(search.Search.Value));
                                    break;
                                case "permitNumber":
                                    query = dataList.Where(u =>
                        u.PermitDetailsProfile.PermitNumber.Contains(search.Search.Value));
                                    break;
                                case "sourceTypeEn":
                                    query = dataList.Where(u =>
                        u.SourceTypeLookup.DisplayNameEn.Contains(search.Search.Value));
                                    break;
                                case "sourceTypeAr":
                                    query = dataList.Where(u =>
                        u.SourceTypeLookup.DisplayNameAr.Contains(search.Search.Value));
                                    break;
                                case "transactionStatusEn":
                                    query = dataList.Where(u =>
                       u.ItemSourceStatusHistories.AsQueryable().OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameEn.Contains(param.Search.Value));
                                    break;
                                case "transactionStatusAr":
                                    query = dataList.Where(u =>
                       u.ItemSourceStatusHistories.AsQueryable().OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameAr.Contains(param.Search.Value));
                                    break;


                            }
                        }
                    }
                }
                else
                {
                    query = dataList;
                }

                var colOrder = param.Order[0];

                if (param.lang == (int)Language.En)
                {
                    switch (colOrder.Column)
                    {

                        case 0:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesFrn) :
                                query.OrderByDescending(r => r.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesFrn);
                            break;
                        case 1:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CreatedOn) : query.OrderByDescending(r => r.CreatedOn);
                            break;
                        case 2:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EntityProfile.EntityNameEn) :
                                query.OrderByDescending(r => r.EntityProfile.EntityNameEn);
                            break;
                        case 3:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.FacilityProfile.FacilityNameEn) :
                                query.OrderByDescending(r => r.FacilityProfile.FacilityNameEn);
                            break;
                        case 4:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LicenseProfile.LicenseDescEn) :
                                query.OrderByDescending(r => r.LicenseProfile.LicenseDescEn);
                            break;
                        case 6:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.SourceTypeLookup.DisplayNameEn) :
                                query.OrderByDescending(r => r.SourceTypeLookup.DisplayNameEn);
                            break;
                        case 7:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ItemSourceStatusHistories.AsQueryable().OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameEn) :
                                query.OrderByDescending(r => r.ItemSourceStatusHistories.AsQueryable().OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameEn);
                            break;
                        default:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CreatedOn) : query.OrderByDescending(r => r.CreatedOn);
                            break;
                    }

                }
                else
                {
                    switch (colOrder.Column)
                    {

                        case 0:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesNtv) :
                                query.OrderByDescending(r => r.TrnItemSource.TransactionsHeader.TransactionTypeMaster.TransactionTypeDesNtv);
                            break;
                        case 1:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CreatedOn) : query.OrderByDescending(r => r.CreatedOn);
                            break;
                        case 2:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EntityProfile.EntityNameAr) :
                                query.OrderByDescending(r => r.EntityProfile.EntityNameAr);
                            break;
                        case 3:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.FacilityProfile.FacilityNameAr) :
                                query.OrderByDescending(r => r.FacilityProfile.FacilityNameAr);
                            break;
                        case 4:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LicenseProfile.LicenseDescAr) :
                                query.OrderByDescending(r => r.LicenseProfile.LicenseDescAr);
                            break;
                        case 6:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.SourceTypeLookup.DisplayNameAr) :
                                query.OrderByDescending(r => r.SourceTypeLookup.DisplayNameAr);
                            break;
                        case 7:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ItemSourceStatusHistories.AsQueryable().OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameAr) :
                                query.OrderByDescending(r => r.ItemSourceStatusHistories.AsQueryable().OrderByDescending(x => x.CreatedOn).First().ItemSourceStatus.StatusNameAr);
                            break;
                        default:
                            query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CreatedOn) : query.OrderByDescending(r => r.CreatedOn);
                            break;
                    }
                }
                var data = await query.Skip(param.Start).Take(param.Length).ToListAsync();
                response.Data = _mapper.Map<List<DTOActionCenter>>(data);
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;
            }

            return response;



        }
    }
}
