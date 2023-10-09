using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs;
using PRJ.Application.Enums;
using PRJ.Application.Warppers;
using PRJ.GlobalComponents.Encryption;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business
{
    public class EnquiryCenterManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public EnquiryCenterManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }



        // Get Paginated, filtered, sorted
        public async Task<wap.EnquiryPagingResponse<List<DTOEnquiryCenter>>> GetAllFunctional(wap.EnquiryPagingRequest param)
        {
            // Initialize response
            var response = new EnquiryPagingResponse<List<DTOEnquiryCenter>>()
            {
                Draw = param.Draw
            };


            List<int> entities = new List<int>();
            List<int> facilities = new List<int>();
            List<int> licenses = new List<int>();
            List<int> permits = new List<int>();
            List<int> practises = new List<int>();
            List<int> ids = new List<int>();

            if (param.Entities != null && param.Entities.Any())
            {
                foreach (var item in param.Entities)
                {
                    entities.Add(int.Parse(EncryptQueryURL.Decrypt(item.Replace(" ", "+"))));
                }
            }

            if (param.Facilities != null && param.Facilities.Any())
            {
                foreach (var item in param.Facilities)
                {
                    facilities.Add(int.Parse(EncryptQueryURL.Decrypt(item.Replace(" ", "+"))));
                }
            }
            if (param.Licenses != null && param.Licenses.Any())
            {
                foreach (var item in param.Licenses)
                {
                    licenses.Add(int.Parse(EncryptQueryURL.Decrypt(item.Replace(" ", "+"))));
                }
            }
            if (param.Permits != null && param.Permits.Any())
            {
                foreach (var item in param.Permits)
                {
                    permits.Add(int.Parse(EncryptQueryURL.Decrypt(item.Replace(" ", "+"))));
                }
            }
            if (param.Practises != null && param.Practises.Any())
            {
                foreach (var item in param.Practises)
                {
                    practises.Add(int.Parse(EncryptQueryURL.Decrypt(item.Replace(" ", "+"))));
                }
            }
            if (param.Ids != null && param.Ids.Any())
            {
                foreach (var item in param.Ids)
                {
                    ids.Add(int.Parse(EncryptQueryURL.Decrypt(item.Replace(" ", "+"))));
                }
            }






            // Get all
            IQueryable<ent.TrnItemSource> dataList = _manager.TrnItemSource.GetByQuery(x =>
            (param.Ids == null || !param.Ids.Any() || ids.Contains(x.TrnSourceId)) &&
            (param.Entities == null || !param.Entities.Any() || entities.Contains((int)x.EntityId)) &&
            (param.Facilities == null || !param.Facilities.Any() || facilities.Contains((int)x.FacilityId)) &&
            (param.Licenses == null || !param.Licenses.Any() || licenses.Contains((int)x.LicenseId)) &&
            (param.Permits == null || !param.Permits.Any() || permits.Contains((int)x.PermitdetailsId)) &&
            (param.Practises == null || !param.Practises.Any() || practises.Contains((int)x.PractiseId))
            ).Include(x => x.ManufacturerLookup).Include(x => x.SourceTypeLookup).Include(x => x.StatusLookup)
            .Include(x => x.TrnItemSourceRadionuclides)
                .ThenInclude(x => x.InitialActivityUnitLookup)
             .Include(x => x.TrnItemSourceRadionuclides).ThenInclude(x => x.Radionuclides).ThenInclude(x => x.HalfLifeUnit);



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
                query = dataList
                    .Where(r =>
                    r.NrrcId.Contains(param.Search.Value)
                   ||
                   r.ManufacturerSerialNo.Contains(param.Search.Value)
                   ||
                   (param.lang == (int)Language.En ?
                   r.StatusLookup.DisplayNameEn.Contains(param.Search.Value) :
                   r.StatusLookup.DisplayNameAr.Contains(param.Search.Value))
                   ||
                   r.TrnItemSourceRadionuclides.Any(c => c.Radionuclides.DisplayName.ToLower().Contains(param.Search.Value.ToLower().ToString()))
                   ||
                   (param.lang == (int)Language.En ?
                   r.ManufacturerLookup.DisplayNameEn.Contains(param.Search.Value) :
                   r.ManufacturerLookup.DisplayNameAr.Contains(param.Search.Value))
                   //||
                   //r.InitialActivityDate.HasValue ? r.InitialActivityDate.Value.Year.ToString().Contains(param.Search.Value) : true
                   ||
                   (param.lang == (int)Language.En ?
                   r.SourceTypeLookup.DisplayNameEn.Contains(param.Search.Value) :
                   r.SourceTypeLookup.DisplayNameAr.Contains(param.Search.Value)));

            }
            else if (searchByColumn) // search by column
            {
                foreach (var search in param.Columns)
                {
                    if (!string.IsNullOrEmpty(search.Search.Value))
                    {

                        switch (search.Data)
                        {
                            case "nrrcId":
                                query = dataList.Where(u =>
                    u.NrrcId.ToString().Contains(search.Search.Value));
                                break;
                            case "manufacturerSerialNo":
                                query = dataList.Where(u =>
                    u.ManufacturerSerialNo.Contains(search.Search.Value));
                                break;
                            case "statusEn":
                                query = dataList.Where(u =>
                    u.StatusLookup.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "statusAr":
                                query = dataList.Where(u =>
                    u.StatusLookup.DisplayNameAr.Contains(search.Search.Value));
                                break;
                            case "sourceTypeEn":
                                query = dataList.Where(u =>
                    u.SourceTypeLookup.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "sourceTypeAr":
                                query = dataList.Where(u =>
                    u.SourceTypeLookup.DisplayNameAr.Contains(search.Search.Value));
                                break;
                            case "manufacturerEn":
                                query = dataList.Where(u =>
                    u.ManufacturerLookup.DisplayNameEn.Contains(search.Search.Value));
                                break;
                            case "manufacturerAr":
                                query = dataList.Where(u =>
                    u.ManufacturerLookup.DisplayNameAr.Contains(search.Search.Value));
                                break;
                            case "radionuclideName":
                                query = dataList.Where(u =>
                   u.TrnItemSourceRadionuclides.Any(x => x.Radionuclides.DisplayName.Contains(param.Search.Value)));
                                break;
                                //         case "productionDate":
                                //             query = dataList.Where(u =>
                                //u.InitialActivityDate.HasValue?u.InitialActivityDate.Value.Year.ToString().Contains(param.Search.Value):true);
                                //             break;
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.NrrcId) :
                            query.OrderByDescending(r => r.NrrcId);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerSerialNo) : query.OrderByDescending(r => r.ManufacturerSerialNo);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.StatusLookup.DisplayNameEn) :
                            query.OrderByDescending(r => r.StatusLookup.DisplayNameEn);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.TrnItemSourceRadionuclides.SelectMany(x => x.Radionuclides.DisplayName)) :
                            query.OrderByDescending(r => r.TrnItemSourceRadionuclides.SelectMany(x => x.Radionuclides.DisplayName));
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerLookup.DisplayNameEn) :
                            query.OrderByDescending(r => r.ManufacturerLookup.DisplayNameEn);
                        break;
                    //case 5:
                    //    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.InitialActivityDate) :
                    //        query.OrderByDescending(r => r.InitialActivityDate);
                    //    break;
                    case 6:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.SourceTypeLookup.DisplayNameEn) :
                            query.OrderByDescending(r => r.SourceTypeLookup.DisplayNameEn);
                        break;
                    case 7:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.TrnSourceId) :
                            query.OrderByDescending(r => r.TrnSourceId);
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.NrrcId) :
                            query.OrderByDescending(r => r.NrrcId);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerSerialNo) : query.OrderByDescending(r => r.ManufacturerSerialNo);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.StatusLookup.DisplayNameAr) :
                            query.OrderByDescending(r => r.StatusLookup.DisplayNameAr);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.TrnItemSourceRadionuclides.SelectMany(x => x.Radionuclides.DisplayName)) :
                            query.OrderByDescending(r => r.TrnItemSourceRadionuclides.SelectMany(x => x.Radionuclides.DisplayName));
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ManufacturerLookup.DisplayNameAr) :
                            query.OrderByDescending(r => r.ManufacturerLookup.DisplayNameAr);
                        break;
                    //case 5:
                    //    query = colOrder.Dir == "asc" ? query.OrderBy(r => r.InitialActivityDate) :
                    //        query.OrderByDescending(r => r.InitialActivityDate);
                    //    break;
                    case 6:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.SourceTypeLookup.DisplayNameAr) :
                            query.OrderByDescending(r => r.SourceTypeLookup.DisplayNameAr);
                        break;
                    case 7:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.TrnSourceId) :
                            query.OrderByDescending(r => r.TrnSourceId);
                        break;
                    default:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CreatedOn) : query.OrderByDescending(r => r.CreatedOn);
                        break;
                }
            }
            var data = await query.Skip(param.Start).Take(param.Length).ToListAsync();
            response.Data = _mapper.Map<List<DTOEnquiryCenter>>(data);
            response.SealedIds = query.Where(x => x.SourceType == (int)SourceType.SealedSource).Select(x => EncryptQueryURL.Encrypt(x.TrnSourceId.ToString())).ToList();
            response.UnSealedIds = query.Where(x => x.SourceType == (int)SourceType.UnsealedSource).Select(x => EncryptQueryURL.Encrypt(x.TrnSourceId.ToString())).ToList();
            response.VariableUnSealedIds = query.Where(x => x.SourceType == (int)SourceType.VariableUnsealed).Select(x => EncryptQueryURL.Encrypt(x.TrnSourceId.ToString())).ToList();

            var recordsTotal = query.Count();
            response.RecordsTotal = recordsTotal;
            response.RecordsFiltered = recordsTotal;
            response.Succeeded = true;

            return response;


        }
    }
}
