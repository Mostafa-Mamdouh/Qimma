using AutoMapper;
using PRJ.Application.Warppers;
using System.Reflection;
using bus = PRJ.Business;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.EntityProfile
{
    public class EntityProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;
        public readonly bus.LookupSet.LookupSetManager lookupManager;

        public EntityProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.EntityProfile.DTOEntityProfile>>> GetAll()
        {
            
                var EntityProfile = await _manager.EntityProfile.GetAllAsync();
                var test = _mapper.Map<List<dto.EntityProfile.DTOEntityProfile>>(EntityProfile);

                return new wap.Response<List<dto.EntityProfile.DTOEntityProfile>>()
                {
                    Data = _mapper.Map<List<dto.EntityProfile.DTOEntityProfile>>(EntityProfile)
                };
            
           
        }


        public async Task<wap.Response<dto.EntityProfile.DTOEntityProfile>> GetEntityProfileById(string Id)
        {
            
                var getById = await _manager.EntityProfile.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.EntityProfile.DTOEntityProfile>()
                    {
                        Data = _mapper.Map<dto.EntityProfile.DTOEntityProfile>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.EntityProfile.DTOEntityProfile>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
            
        }

        public async Task<wap.Response<bool>> UpdateEntityProfile(dto.EntityProfile.DTOEntityProfile body)
        { 
                var entityprofile = await _manager.EntityProfile.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.EntityProfile>(body);
                _manager.EntityProfile.Update(Data);
                _manager.Complete();
                return new wap.Response<bool>() { Data = true };
        }


        public async Task<wap.Response<bool>> AddEntityProfile(dto.EntityProfile.DTOEntityProfile body)
        {
            
                 var Data = _mapper.Map<ent.EntityProfile>(body);
                 await _manager.EntityProfile.AddAsync(Data);
                 return new wap.Response<bool>() { Data = true, Succeeded =true };
        }

        public async Task<wap.Response<bool>> DeleteEntityProfile(string Id)
        {
                var getById = await _manager.EntityProfile.GetEncryptByIdAsync(Id);
                await _manager.EntityProfile.DeleteAsync(getById);
                await _manager.CompleteAsync();
                return new wap.Response<bool>() { Data = true };

        }

        // Get Paginated, filtered, sorted
        public async Task<wap.PagingResponse<List<dto.EntityProfile.DTOEntityProfile>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.EntityProfile>>()
            {
                Draw = param.Draw
            };
           
                // Get all
                IQueryable<ent.EntityProfile> dataList = _manager.EntityProfile.GetAll();

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
                IQueryable<ent.EntityProfile> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.EntityId.ToString().Contains(param.Search.Value) ||
                        r.EntityNameAr.ToString().Contains(param.Search.Value) ||
                        r.EntityNameEn.Contains(param.Search.Value) ||
                        r.PhoneNo.ToString().Contains(param.Search.Value) ||
                        r.MobileNo.ToString().Contains(param.Search.Value) ||
                        r.EmailId.Contains(param.Search.Value) ||
                        r.GovernmentID.ToString().Contains(param.Search.Value) ||
                        r.EntityType.ToString().Contains(param.Search.Value) ||
                        r.AmanInsertedOn.ToString().Contains(param.Search.Value));
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "entityId":
                                    query = dataList.Where(u => u.EntityId.ToString().Contains(search.Search.Value));
                                    break;
                                case "entityNameAr":
                                    query = dataList.Where(u => u.EntityNameAr.Contains(search.Search.Value));
                                    break;
                                case "entityNameEn":
                                    query = dataList.Where(u => u.EntityNameEn.Contains(search.Search.Value));
                                    break;
                                case "phoneNo":
                                    query = dataList.Where(u => u.PhoneNo.Contains(search.Search.Value));
                                    break;
                                case "mobileNo":
                                    query = dataList.Where(u => u.MobileNo.Contains(search.Search.Value));
                                    break;
                                case "emailId":
                                    query = dataList.Where(u => u.EmailId.Contains(search.Search.Value));
                                    break;
                                case "governmentID":
                                    query = dataList.Where(u => u.GovernmentID.Contains(search.Search.Value));
                                    break;
                                case "entityType":
                                    query = dataList.Where(u => u.EntityType.ToString().Contains(search.Search.Value));
                                    break;
                                case "amanInsertedOn":
                                    query = dataList.Where(u => u.AmanInsertedOn.ToString().Contains(search.Search.Value));
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

                switch (colOrder.Column)
                {
                    case 0:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EntityId) : query.OrderByDescending(r => r.EntityId);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EntityNameAr) : query.OrderByDescending(r => r.EntityNameAr);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EntityNameEn) : query.OrderByDescending(r => r.EntityNameEn);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.PhoneNo) : query.OrderByDescending(r => r.PhoneNo);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.MobileNo) : query.OrderByDescending(r => r.MobileNo);
                        break;
                    case 5:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EmailId) : query.OrderByDescending(r => r.EmailId);
                        break;
                    case 6:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.GovernmentID) : query.OrderByDescending(r => r.GovernmentID);
                        break;
                    case 7:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.EntityType) : query.OrderByDescending(r => r.EntityType);
                        break;
                    case 8:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.AmanInsertedOn) : query.OrderByDescending(r => r.AmanInsertedOn);
                        break;
                }

                response.Data = query.Skip(param.Start).Take(param.Length).ToList();
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;

                return new PagingResponse<List<dto.EntityProfile.DTOEntityProfile>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.EntityProfile.DTOEntityProfile>>(response.Data),
                    Draw = response.Draw,
                    RecordsTotal = response.RecordsTotal,
                    RecordsFiltered = response.RecordsFiltered
                };
          

        }
    }
}
