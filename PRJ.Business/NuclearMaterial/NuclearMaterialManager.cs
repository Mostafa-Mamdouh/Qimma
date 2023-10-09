using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs.NuclearMaterial;
using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialRadionuclide;
using PRJ.Application.DTOs.NuclearMaterial.Shield;
using PRJ.Application.Enums;
using PRJ.Application.Warppers;
using PRJ.Domain.Entities.NuclearMaterial;
using PRJ.GlobalComponents.Encryption;
using PRJ.Repository;
using System.Diagnostics.CodeAnalysis;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.NuclearMaterial
{
    class NuclearElementsComparer : IEqualityComparer<NuclearMaterialElement>
    {
        public bool Equals(NuclearMaterialElement x, NuclearMaterialElement y)
        {
            return x.Id != y.Id;
        }

        public int GetHashCode([DisallowNull] NuclearMaterialElement obj)
        {
            throw new NotImplementedException();
        }
    }

    public class NuclearMaterialManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public NuclearMaterialManager(IUnitOfWork manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        #region Add Nuclear Material
        public async Task<wap.Response<bool>> AddNuclearMaterial(DTONuclearMaterial body)
        {
            var result = false;


            var data = _mapper.Map<ent.NuclearMaterial.NuclearMaterial>(body);

            if (data == null)
            {
                return new wap.Response<bool>
                {
                    Succeeded = false,
                };
            }

            //data.NuclearMaterialElements = _mapper.Map<List<ent.NuclearMaterial.NuclearMaterialRadionulcide>>(body.NuclearMaterialElements);

            await _manager.NuclearMaterial.AddAsync(data);

            data.NrrcId = "N-SA11" + data.SourceId;

            await _manager.CompleteAsync();

            return new wap.Response<bool>(result);

        }
        #endregion

        #region Update Nuclear Material
        public async Task<wap.Response<bool>> UpdateNuclearMaterial(DTONuclearMaterial body)
        {
            var result = false;


            var Id = EncryptQueryURL.Decrypt(body.SourceId.Replace(" ", "+"));

            var data = _mapper.Map<ent.NuclearMaterial.NuclearMaterial>(body);

            if (data == null)
            {
                return new wap.Response<bool>
                {
                    Succeeded = false,
                };
            }

            _manager.NuclearMaterial.Update(data);

            // Files
            _manager.NuclearMaterialFiles.DeleteByParentId(x => x.NuclearMaterialId == data.SourceId);

            foreach (var file in body.NuclearMaterialFiles)
            {
                _mapper.Map<NuclearMaterialFiles>(file);
            }
            // ************

            // Radionuclides
            // Add using the automapper
            var rads = _manager.NuclearMaterialRadionuclide.GetByQuery(x => x.NuclearMaterialElement.NuclearMaterialId == int.Parse(Id)).Select(x => x.NuclearMaterialRadionulcideId).ToList();
            var bodyRads = data.NuclearMaterialElements.SelectMany(x => x.NuclearMaterialRadionulcides).Select(x => x.NuclearMaterialRadionulcideId).ToList();

            List<int> deleted_rads = rads.Except(bodyRads).ToList();

            // Delete
            var toDeleteRads = await _manager.NuclearMaterialRadionuclide.GetByQuery(x => (x.NuclearMaterialElement.NuclearMaterialId == int.Parse(Id)) && (deleted_rads.Contains(x.NuclearMaterialRadionulcideId))).ToListAsync();

            if (toDeleteRads.Any())
            {
                foreach (var rad in toDeleteRads)
                {
                    await _manager.NuclearMaterialRadionuclide.DeleteAsync(rad);
                }
            }


            // Elements
            // Add using the automapper
            var elements = _manager.NuclearElements.GetByQuery(x => x.NuclearMaterialId == int.Parse(Id)).Select(x => x.Id).ToList();
            var bodyElements = data.NuclearMaterialElements.Select(x => x.Id).ToList();

            List<int> deleted = elements.Except(bodyElements).ToList();

            // Delete
            var toDeleteElements = await _manager.NuclearElements.GetByQuery(x => (x.NuclearMaterialId == int.Parse(Id)) && (deleted.Contains(x.Id))).ToListAsync();

            if (toDeleteElements.Any())
            {
                foreach (var ele in toDeleteElements)
                {
                    await _manager.NuclearElements.DeleteAsync(ele);
                }
            }

            //Update
            List<int> updated = elements.Intersect(bodyElements).ToList();

            var toUpdateElements = await _manager.NuclearElements.GetByQuery(x => (x.NuclearMaterialId == int.Parse(Id)) && (updated.Contains(x.Id))).ToListAsync();

            if (toUpdateElements.Any())
            {
                foreach (var ele in toUpdateElements)
                {
                    _manager.NuclearElements.Update(ele);
                }
            }
            // **********


            await _manager.CompleteAsync();

            return new wap.Response<bool>(result);

        }
        #endregion

        #region Get All Nuclear Materials
        public async Task<wap.Response<List<DTONuclearMaterial>>> GetNuclearMaterials()
        {


            var data = await _manager.NuclearMaterial.GetAllAsync();

            if (data == null)
            {
                return new wap.Response<List<DTONuclearMaterial>>
                {
                    Succeeded = false,
                };
            }

            return new wap.Response<List<DTONuclearMaterial>>()
            {
                Succeeded = true,
                Data = _mapper.Map<List<DTONuclearMaterial>>(data)
            };

        }
        #endregion


        #region Get Nuclear Material By (Encrypted) Id
        public async Task<wap.Response<DTOUpdateNuclearMaterial>> GetNuclearMaterialById(string id)
        {


            var decryptedId = EncryptQueryURL.Decrypt(id.Replace(" ", "+"));
            var data = await _manager.NuclearMaterial.GetByQuery(x => x.SourceId == int.Parse(decryptedId))
                .Include(x => x.EntityProfile)
                .Include(x => x.FacilityProfile)
                .Include(x => x.LicenseProfile)
                .Include(x => x.PermitDetailsProfile)
                .Include(x => x.NuclearMaterialFiles)
                .Include(x => x.NuclearMaterialElements)
                .FirstOrDefaultAsync();

            var rads = _manager.NuclearMaterialRadionuclide.GetByQuery(x => x.NuclearMaterialElement.NuclearMaterialId == data.SourceId);

            var res = _mapper.Map<DTOUpdateNuclearMaterial>(data);

            res.NuclearMaterialRadionulcides = _mapper.Map<List<DTONuclearMaterialRadionuclide>>(rads);

            if (data == null)
            {
                return new wap.Response<DTOUpdateNuclearMaterial>
                {
                    Succeeded = false,
                };
            }

            return new wap.Response<DTOUpdateNuclearMaterial>()
            {
                Succeeded = true,
                Data = res
            };
        }


        #endregion

        #region Delete Nuclear Material 
        public async Task<wap.Response<bool>> DeleteNuclearMaterial(string id)
        {


            var decryptedId = EncryptQueryURL.Decrypt(id.Replace(" ", "+"));
            var data = await _manager.NuclearMaterial.GetByIdAsync(int.Parse(decryptedId));

            var check = data.IsShield ?? false;

            if (check)
            {
                return new wap.Response<bool>()
                {
                    Succeeded = true,
                    Data = false,
                    Message = "This Nuclear Material is used as a shield for sealed source",
                    MessageAr = "هذه المادة النووية مستخدمة كدرع لمصدر مختوم",
                };
            }
            await _manager.NuclearMaterial.DeleteAsync(data);

            return new wap.Response<bool>()
            {
                Succeeded = true,
                Data = true,
            };

        }
        #endregion

        #region Get All Nuclear Materials Functional
        public async Task<wap.PagingResponse<List<DTOPaginatedNuclearMaterials>>> GetAllNuclearMaterialFunctional(wap.PagingRequest param)
        {
            var response = new PagingResponse<List<ent.NuclearMaterial.NuclearMaterial>>()
            {
                Draw = param.Draw
            };

            IQueryable<ent.NuclearMaterial.NuclearMaterial> dataList = _manager.NuclearMaterial.GetAll()
                .Include(x => x.EntityProfile)
                .Include(x => x.StatusLookup)
                .Include(x => x.PhysicalFormLookup)
                .Include(x => x.FacilityProfile)
                .Include(x => x.LicenseProfile)
                .Include(x => x.PermitDetailsProfile)
                .Include(x => x.ManufacturerLookup)
                .Include(x => x.ManufacturerCountryLookup)
                .Include(x => x.NuclearMaterialElements).ThenInclude(x => x.NuclearMaterialRadionulcides);

            bool searchByColumn = false;
            foreach (var search in param.Columns)
            {
                if (!string.IsNullOrEmpty(search.Search.Value))
                {
                    searchByColumn = true;
                    break;
                }
            }

            IQueryable<ent.NuclearMaterial.NuclearMaterial> query = null;
            if (!string.IsNullOrEmpty(param.Search.Value))
            {
                query = dataList
                    .Where(r =>
                    r.ChemicalForm.Contains(param.Search.Value) ||
                    r.CreatedOn.ToString().Contains(param.Search.Value) ||
                    r.StatusLookup.DisplayNameEn.Contains(param.Search.Value) ||
                    r.StatusLookup.DisplayNameAr.Contains(param.Search.Value) ||
                    r.PhysicalFormLookup.DisplayNameAr.Contains(param.Search.Value) ||
                    r.PhysicalFormLookup.DisplayNameEn.Contains(param.Search.Value) ||
                    r.PhysicalFormLookup.DisplayNameAr.Contains(param.Search.Value) ||
                    r.PhysicalFormLookup.DisplayNameEn.Contains(param.Search.Value) ||
                    r.FacilityProfile.FacilityNameAr.Contains(param.Search.Value) ||
                    r.FacilityProfile.FacilityNameEn.Contains(param.Search.Value) ||
                    r.ManufacturerLookup.DisplayNameAr.Contains(param.Search.Value) ||
                    r.ManufacturerLookup.DisplayNameEn.Contains(param.Search.Value) ||
                    r.ManufacturerCountryLookup.DisplayNameAr.Contains(param.Search.Value) ||
                    r.ManufacturerCountryLookup.DisplayNameEn.Contains(param.Search.Value) ||
                    r.ManufacturerSerialNo.Contains(param.Search.Value) ||
                    r.NrrcId.Contains(param.Search.Value) ||
                    r.SourceModel.Contains(param.Search.Value) ||
                    r.CreatedOn.ToString().Contains(param.Search.Value) ||
                    r.NrrcId.Contains(param.Search.Value));
            }
            else if (searchByColumn) // search by column
            {
                foreach (var search in param.Columns)
                {
                    if (!string.IsNullOrEmpty(search.Search.Value))
                    {
                        switch (search.Data)
                        {
                            case "createdOn":
                                query = dataList.Where(u => u.CreatedOn.ToString().Contains(search.Search.Value));
                                break;
                            case "manufacturerSerialNo":
                                query = dataList.Where(u => u.ManufacturerSerialNo.Contains(search.Search.Value));
                                break;
                            case "sourceModel":
                                query = dataList.Where(u => u.SourceModel.Contains(search.Search.Value));
                                break;
                            case "chemicalForm":
                                query = dataList.Where(u => u.ChemicalForm.Contains(search.Search.Value));
                                break;
                            case "statusAr":
                                query = dataList.Where(u => u.StatusLookup.DisplayNameAr.Contains(search.Search.Value));
                                break;
                            case "statusEn":
                                query = dataList.Where(u => u.StatusLookup.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "entityAr":
                                query = dataList.Where(u => u.EntityProfile.EntityNameAr.Contains(search.Search.Value));
                                break;
                            case "entityEn":
                                query = dataList.Where(u => u.EntityProfile.EntityNameEn.Contains(search.Search.Value));
                                break;
                            case "facilityAr":
                                query = dataList.Where(u => u.FacilityProfile.FacilityNameAr.Contains(search.Search.Value));
                                break;
                            case "facilityEn":
                                query = dataList.Where(u => u.FacilityProfile.FacilityNameEn.Contains(search.Search.Value));
                                break;
                            case "physicalFormAr":
                                query = dataList.Where(u => u.PhysicalFormLookup.DisplayNameAr.Contains(search.Search.Value));
                                break;
                            case "physicalFormEn":
                                query = dataList.Where(u => u.PhysicalFormLookup.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "nuclearMaterialTypeAr":
                                query = dataList.Where(u => u.NuclearMaterialElements.Any(x => x.NuclearMaterialRadionulcides.Any(a => a.Element.Contains(search.Search.Value))));
                                break;
                            case "nuclearMaterialTypeEn":
                                query = dataList.Where(u => u.NuclearMaterialElements.Any(x => x.NuclearMaterialRadionulcides.Any(a => a.Element.Contains(search.Search.Value))));
                                break;
                            case "license":
                                query = dataList.Where(u => u.LicenseProfile.LicenseCode.Contains(search.Search.Value));
                                break;
                            case "permit":
                                query = dataList.Where(u => u.PermitDetailsProfile.PermitNumber.ToString().Contains(search.Search.Value));
                                break;
                            case "nrrcId":
                                query = dataList.Where(u => u.NrrcId.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.SourceId) : query.OrderByDescending(r => r.SourceId);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerSerialNo) : query.OrderByDescending(r => r.ManufacturerSerialNo);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EntityProfile.EntityNameEn) : query.OrderByDescending(r => r.EntityProfile.EntityNameEn);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.FacilityProfile.FacilityNameEn) : query.OrderByDescending(r => r.FacilityProfile.FacilityNameEn);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LicenseProfile.LicenseCode) : query.OrderByDescending(r => r.LicenseProfile.LicenseCode);
                        break;
                    case 5:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.PermitDetailsProfile.PermitNumber) : query.OrderByDescending(r => r.PermitDetailsProfile.PermitNumber);
                        break;
                    case 6:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.PhysicalFormLookup.DisplayNameEn) : query.OrderByDescending(r => r.PhysicalFormLookup.DisplayNameEn);
                        break;
                    //case 7:
                    //    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.NuclearMaterialTypeLookup.DisplayNameEn) : query.OrderByDescending(r => r.NuclearMaterialTypeLookup.DisplayNameEn);
                    //    break;
                    case 8:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.StatusLookup.DisplayNameEn) : query.OrderByDescending(r => r.StatusLookup.DisplayNameEn);
                        break;
                }
            }
            else
            {
                switch (colOrder.Column)
                {
                    case 0:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.SourceId) : query.OrderByDescending(r => r.SourceId);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerSerialNo) : query.OrderByDescending(r => r.ManufacturerSerialNo);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EntityProfile.EntityNameAr) : query.OrderByDescending(r => r.EntityProfile.EntityNameAr);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.FacilityProfile.FacilityNameAr) : query.OrderByDescending(r => r.FacilityProfile.FacilityNameAr);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LicenseProfile.LicenseCode) : query.OrderByDescending(r => r.LicenseProfile.LicenseCode);
                        break;
                    case 5:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.PermitDetailsProfile.PermitNumber) : query.OrderByDescending(r => r.PermitDetailsProfile.PermitNumber);
                        break;
                    case 6:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.PhysicalFormLookup.DisplayNameAr) : query.OrderByDescending(r => r.PhysicalFormLookup.DisplayNameAr);
                        break;
                    //case 7:
                    //    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.NuclearMaterialTypeLookup.DisplayNameAr) : query.OrderByDescending(r => r.NuclearMaterialTypeLookup.DisplayNameAr);
                    //    break;
                    case 8:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.StatusLookup.DisplayNameAr) : query.OrderByDescending(r => r.StatusLookup.DisplayNameAr);
                        break;
                }
            }

            if (colOrder.Column > 0)
            {
                response.Data = await query.Skip(param.Start).Take(param.Length).ToListAsync();
            }
            else
            {
                response.Data = await query.OrderByDescending(x => x.SourceId).Skip(param.Start).Take(param.Length).ToListAsync();
            }
            var recordsTotal = query.Count();
            response.RecordsTotal = recordsTotal;
            response.RecordsFiltered = recordsTotal;
            response.Succeeded = true;

            return new PagingResponse<List<DTOPaginatedNuclearMaterials>>()
            {
                Succeeded = true,
                Data = _mapper.Map<List<DTOPaginatedNuclearMaterials>>(response.Data),
                Draw = response.Draw,
                RecordsTotal = response.RecordsTotal,
                RecordsFiltered = response.RecordsFiltered
            };

        }
        #endregion

        #region Add Nuclear Shield
        public async Task<wap.Response<DTOGetNuclearMaterialsAsLookup>> AddNuclearShield(DTOShield body)
        {

            var files = _mapper.Map<List<ent.NuclearMaterial.NuclearMaterialFiles>>(body.NuclearMaterialFiles);

            var type = await _manager.LookupSetTerm.GetByQuery(x => x.Value == "d").FirstOrDefaultAsync();

            var ph = await _manager.LookupSetTerm.GetByQuery(x => x.Value == "solid").FirstOrDefaultAsync();

            var status = await _manager.LookupSetTerm.GetByQuery(x => x.LookupSetTermId == 26).FirstOrDefaultAsync();

            var unit = int.Parse(EncryptQueryURL.Decrypt(body.InitialMassUnit));

            var nuclearMaterial = new ent.NuclearMaterial.NuclearMaterial()
            {
                EntityId = int.Parse(EncryptQueryURL.Decrypt(body.EntityId)),
                FacilityId = int.Parse(EncryptQueryURL.Decrypt(body.FacilityId)),
                FacilitySerialNo = "",
                IsShield = true,
                LicenseId = int.Parse(EncryptQueryURL.Decrypt(body.LicenseId)),
                PermitdetailsId = int.Parse(EncryptQueryURL.Decrypt(body.PermitdetailsId)),
                ManufacturerSerialNo = body.ManufacturerSerialNo,
                NuclearMaterialFiles = files,
                PhysicalForm = ph.LookupSetTermId,
                Status = status.LookupSetTermId,
                SourceModel = body.SourceModel,
            };
            await _manager.NuclearMaterial.AddAsync(nuclearMaterial);

            var element = new NuclearMaterialElement()
            {
                ElementMass = body.InitialMass,
                ElementMassUnit = unit,
                NuclearMaterialType = type.LookupSetTermId,
                NuclearMaterialTypeLookup = type,
                NuclearMaterialId = nuclearMaterial.SourceId
            };

            await _manager.NuclearElements.AddAsync(element);

            var rad = await _manager.Radionuclides.GetByQuery(x => x.Isotop == "U-235" || x.Isotop == "u-235").FirstOrDefaultAsync();

            var radionuclide = new NuclearMaterialRadionulcide()
            {
                RadionulcideId = rad.RadionuclideId,
                Radionuclides = rad,
                Element = "d",
                NuclearMaterialElementId = element.Id
            };

            await _manager.NuclearMaterialRadionuclide.AddAsync(radionuclide);

            var obj = nuclearMaterial;

            nuclearMaterial.NrrcId = "N-SA11" + nuclearMaterial.SourceId;

            await _manager.CompleteAsync();

            var res = new DTOGetNuclearMaterialsAsLookup()
            {
                FacilitySerialNo = obj.FacilitySerialNo,
                ManufacturerSerialNo = obj.ManufacturerSerialNo,
                NrrcId = obj.NrrcId,
                SourceId = EncryptQueryURL.Encrypt(nuclearMaterial.SourceId.ToString()),
            };

            return new wap.Response<DTOGetNuclearMaterialsAsLookup>()
            {
                Succeeded = true,
                Data = res
            };

        }
        #endregion

        #region Get Nuclear Materials as Lookup
        public async Task<wap.Response<List<DTOGetNuclearMaterialsAsLookup>>> GetNuclearMaterialsAsLookUp(string licenseId)
        {


            var decryptedId = EncryptQueryURL.Decrypt(licenseId.Replace(" ", "+"));

            var data = _manager.NuclearMaterial.GetByQuery(x => (x.LicenseId == int.Parse(decryptedId)) && ((x.IsShield == null || x.IsShield == false) || x.ItemSourceProfile == null));

            if (data == null)
            {
                return new wap.Response<List<DTOGetNuclearMaterialsAsLookup>>
                {
                    Succeeded = false,
                };
            }

            return new wap.Response<List<DTOGetNuclearMaterialsAsLookup>>()
            {
                Succeeded = true,
                Data = _mapper.Map<List<DTOGetNuclearMaterialsAsLookup>>(data)
            };

        }
        #endregion

        #region Get Nuclear Materials as Lookup Form Transaction Update
        public async Task<wap.Response<List<DTOGetNuclearMaterialsAsLookup>>> GetNuclearMaterialsForTransaction(string facilityId, string sourceId)
        {


            var decryptedId = EncryptQueryURL.Decrypt(facilityId.Replace(" ", "+"));
            var decryptedSourceId = EncryptQueryURL.Decrypt(sourceId.Replace(" ", "+"));

            var data = _manager.NuclearMaterial.GetByQuery(x => (x.FacilityId == int.Parse(decryptedId)) && ((x.IsShield == null || x.IsShield == false) || (x.ItemSourceProfile.SourceId == int.Parse(decryptedSourceId))));

            if (data == null)
            {
                return new wap.Response<List<DTOGetNuclearMaterialsAsLookup>>
                {
                    Succeeded = false,
                };
            }

            return new wap.Response<List<DTOGetNuclearMaterialsAsLookup>>()
            {
                Succeeded = true,
                Data = _mapper.Map<List<DTOGetNuclearMaterialsAsLookup>>(data)
            };

        }
        #endregion
    }
}
