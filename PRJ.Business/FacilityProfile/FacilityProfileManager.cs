using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Warppers;
using PRJ.GlobalComponents.Encryption;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.FacilityProfile
{

    public class FacilityProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public FacilityProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.FacilityProfile.DTOFacilityProfile>>> GetAll()
        {
            
                var FacilityProfile = await _manager.FacilityProfile.GetByQuery(_ => _.FacilityNameAr != null).Include(_ => _.EntityProfile).ToListAsync();

                return new wap.Response<List<dto.FacilityProfile.DTOFacilityProfile>>()
                {
                    Data = _mapper.Map<List<dto.FacilityProfile.DTOFacilityProfile>>(FacilityProfile)
                };
            
         
        }


        public async Task<wap.Response<List<dto.FacilityProfile.DTOFacilityProfile>>> GetByEntityId(string Id)
        {
            

                var EntityId = int.Parse(EncryptQueryURL.Decrypt(Id));
                var Facility = await _manager.FacilityProfile.GetByQuery(_ => _.EntityId == EntityId).ToListAsync();
                if (Facility != null)
                {
                    return new wap.Response<List<dto.FacilityProfile.DTOFacilityProfile>>()
                    {
                        Data = _mapper.Map<List<dto.FacilityProfile.DTOFacilityProfile>>(Facility)
                    };
                }
                else
                {
                    return new wap.Response<List<dto.FacilityProfile.DTOFacilityProfile>>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
          
        }

        public async Task<wap.Response<dto.FacilityProfile.DTOFacilityProfile>> GetFacilityProfileById(string Id)
        {
            
                var getById = await _manager.FacilityProfile.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.FacilityProfile.DTOFacilityProfile>()
                    {
                        Data = _mapper.Map<dto.FacilityProfile.DTOFacilityProfile>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.FacilityProfile.DTOFacilityProfile>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }

        public async Task<wap.Response<dto.FacilityProfile.DTOFacilityProfile>> UpdateFacilityProfile(dto.FacilityProfile.DTOFacilityProfile body)
        {
            
                var FacilityProfile = await _manager.FacilityProfile.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.FacilityProfile>(body);
                _manager.FacilityProfile.Update(Data);
                _manager.Complete();

                return new wap.Response<dto.FacilityProfile.DTOFacilityProfile>();
            
            
        }


        public async Task<wap.Response<dto.FacilityProfile.DTOFacilityProfile>> AddFacilityProfile(dto.FacilityProfile.DTOFacilityProfile body)
        {
                var Data = _mapper.Map<ent.FacilityProfile>(body);
                await _manager.FacilityProfile.AddAsync(Data);
                return new wap.Response<dto.FacilityProfile.DTOFacilityProfile>();     
        }

        public async Task<wap.PagingResponse<List<dto.FacilityProfile.DTOFacilityProfile>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.FacilityProfile>>()
            {
                Draw = param.Draw
            };
            
                IQueryable<ent.FacilityProfile> dataList = _manager.FacilityProfile.GetAll();

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
                IQueryable<ent.FacilityProfile> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.AmanInsertedOn.ToString().Contains(param.Search.Value) ||
                        r.FacilityCode.Contains(param.Search.Value) ||
                        r.FacilityNameAr.Contains(param.Search.Value) ||
                        r.FacilityNameEn.Contains(param.Search.Value));
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "amanInsertedOn":
                                    query = dataList.Where(u => u.AmanInsertedOn.ToString().Contains(search.Search.Value));
                                    break;
                                case "facilityCode":
                                    query = dataList.Where(u => u.FacilityCode.Contains(search.Search.Value));
                                    break;
                                case "facilityNameAr":
                                    query = dataList.Where(u => u.FacilityNameAr.Contains(search.Search.Value));
                                    break;
                                case "facilityNameEn":
                                    query = dataList.Where(u => u.FacilityNameEn.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.FacilityNameAr) : query.OrderByDescending(r => r.FacilityNameAr);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.FacilityNameEn) : query.OrderByDescending(r => r.FacilityNameEn);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.FacilityCode) : query.OrderByDescending(r => r.FacilityCode);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.AmanInsertedOn) : query.OrderByDescending(r => r.AmanInsertedOn);
                        break;
                }

                response.Data = query.Skip(param.Start).Take(param.Length).OrderByDescending(_ => _.CreatedOn).ToList();
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;

                return new PagingResponse<List<dto.FacilityProfile.DTOFacilityProfile>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.FacilityProfile.DTOFacilityProfile>>(response.Data),
                    Draw = response.Draw,
                    RecordsTotal = response.RecordsTotal,
                    RecordsFiltered = response.RecordsFiltered
                };
            
          
        }

    }
}
