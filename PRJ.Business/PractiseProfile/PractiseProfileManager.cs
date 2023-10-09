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

namespace PRJ.Business.PractiseProfile
{
    public class PractiseProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public PractiseProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }
        public async Task<wap.Response<List<dto.PractiseProfile.DTOPractiseProfile>>> GetAll()
        {
           
                var PractiseProfile = await _manager.PractiseProfile.GetAll().Include(x=>x.PermitDetailsProfile).ToListAsync();

                return new wap.Response<List<dto.PractiseProfile.DTOPractiseProfile>>()
                {
                    Data = _mapper.Map<List<dto.PractiseProfile.DTOPractiseProfile>>(PractiseProfile)
                };
           
        }


        public async Task<wap.Response<List<dto.PractiseProfile.DTOPractiseProfile>>> GetPractiseByLicenseId(string LicId)
        {

            var LicenseId = int.Parse(EncryptQueryURL.Decrypt(LicId));

            var PractiseProfile = await _manager.PractiseProfile.GetByQuery(_ => _.LicenseId == LicenseId).Include(x => x.PermitDetailsProfile).ToListAsync();

            return new wap.Response<List<dto.PractiseProfile.DTOPractiseProfile>>()
            {
                Data = _mapper.Map<List<dto.PractiseProfile.DTOPractiseProfile>>(PractiseProfile)
            };
            ;

        }


        public async Task<wap.Response<dto.PractiseProfile.DTOPractiseProfile>> GetPractiseProfileById(string Id)
        {

            var PractiseId = int.Parse(EncryptQueryURL.Decrypt(Id));
            var PractiseProfile = await _manager.PractiseProfile.GetByQuery(_ => _.PractiseId == PractiseId).Include(x => x.PermitDetailsProfile).ToListAsync();

            return new wap.Response<dto.PractiseProfile.DTOPractiseProfile>()
            {
                Data = _mapper.Map<dto.PractiseProfile.DTOPractiseProfile>(PractiseProfile)
            };
            ;

        }

        public async Task<wap.Response<dto.PractiseProfile.DTOPractiseProfile>> UpdatePractiseProfile(dto.PractiseProfile.DTOPractiseProfile body)
        {
            
                var PractiseProfile = await _manager.PractiseProfile.GetEncryptByIdAsync(body.Id);
                var Data = _mapper.Map<ent.PractiseProfile>(body);
                _manager.PractiseProfile.Update(Data);
                _manager.Complete();

                return new wap.Response<dto.PractiseProfile.DTOPractiseProfile>();
            
            
        }


        public async Task<wap.Response<dto.PractiseProfile.DTOPractiseProfile>> AddPractiseProfile(dto.PractiseProfile.DTOPractiseProfile body)
        {
            
                var Data = _mapper.Map<ent.PractiseProfile>(body);
                await _manager.PractiseProfile.AddAsync(Data);
                return new wap.Response<dto.PractiseProfile.DTOPractiseProfile>();
            
           
        }

        public async Task<wap.PagingResponse<List<dto.PractiseProfile.DTOPractiseProfile>>> GetAllPractisesFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.PractiseProfile>>()
            {
                Draw = param.Draw
            };
           
                IQueryable<ent.PractiseProfile> dataList = _manager.PractiseProfile.GetAll();

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
                IQueryable<ent.PractiseProfile> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.AmmanId.ToString().Contains(param.Search.Value) ||
                        r.CreatedOn.ToString().Contains(param.Search.Value) ||
                        r.AmanInsertedOn.ToString().Contains(param.Search.Value) ||
                        r.PractiseNameAr.Contains(param.Search.Value) ||
                        r.PractiseNameEn.Contains(param.Search.Value) ||
                        r.PractiseId.ToString().Contains(param.Search.Value) ||
                        r.PermitDetailsId.ToString().Contains(param.Search.Value));
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
                                    query = dataList.Where(u => u.AmanInsertedOn.ToString().Contains(search.Search.Value));
                                    break;
                                case "permitDetailsId":
                                    query = dataList.Where(u => u.PermitDetailsId.ToString().Contains(search.Search.Value));
                                    break;
                                case "practiseNameAr":
                                    query = dataList.Where(u => u.PractiseNameAr.Contains(search.Search.Value));
                                    break;
                                case "practiseNameEn":
                                    query = dataList.Where(u => u.PractiseNameEn.Contains(search.Search.Value));
                                    break;
                                case "amanInsertedOn":
                                    query = dataList.Where(u => u.AmanInsertedOn.ToString().Contains(search.Search.Value));
                                    break;
                                case "ammanId":
                                    query = dataList.Where(u => u.AmmanId.ToString().Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.PractiseNameAr) : query.OrderByDescending(r => r.PractiseNameAr);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.PractiseNameEn) : query.OrderByDescending(r => r.PractiseNameEn);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.PermitDetailsId) : query.OrderByDescending(r => r.PermitDetailsId);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.AmanInsertedOn) : query.OrderByDescending(r => r.AmanInsertedOn);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.AmmanId) : query.OrderByDescending(r => r.AmmanId);
                        break;
                }

                response.Data = query.Skip(param.Start).Take(param.Length).OrderByDescending(_ => _.CreatedOn).ToList();
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;

                return new PagingResponse<List<dto.PractiseProfile.DTOPractiseProfile>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.PractiseProfile.DTOPractiseProfile>>(response.Data),
                    Draw = response.Draw,
                    RecordsTotal = response.RecordsTotal,
                    RecordsFiltered = response.RecordsFiltered
                };
           
        }
    }
}
