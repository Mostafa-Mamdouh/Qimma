using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs.InternalRole;
using PRJ.Application.Warppers;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.InternalRole
{
    public class InternalRoleManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public InternalRoleManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.InternalRole.DTOInternalRole>>> GetAll()
        {
            
                var InternalRole = await _manager.InternalRole.GetAllAsync();

                return new wap.Response<List<dto.InternalRole.DTOInternalRole>>()
                {
                    Data = _mapper.Map<List<dto.InternalRole.DTOInternalRole>>(InternalRole)
                };
            
            
        }

        public async Task<wap.Response<dto.InternalRole.DTOInternalRole>> GetInternalRoleById(string Id)
        {
            
                var getById = await _manager.InternalRole.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.InternalRole.DTOInternalRole>()
                    {
                        Data = _mapper.Map<dto.InternalRole.DTOInternalRole>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.InternalRole.DTOInternalRole>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }

        public async Task<wap.Response<DTOInternalRole>> UpdateInternalRole(dto.InternalRole.DTOInternalRole body)
        {
            
                var InternalRole = await _manager.InternalRole.GetEncryptByIdAsync(body.Id);
                if (InternalRole != null)
                {
                    InternalRole.RoleNameEn = body.RoleNameEn;
                    InternalRole.RoleNameAr = body.RoleNameAr;
                    InternalRole.RoleCode = body.RoleCode;
                }

                _manager.InternalRole.Update(InternalRole);
                _manager.Complete();
                return new wap.Response<DTOInternalRole>() { Data = _mapper.Map<dto.InternalRole.DTOInternalRole>(InternalRole) };

            
           
        }

        public async Task<wap.Response<dto.InternalRole.DTOInternalRole>> AddInternalRole(dto.InternalRole.DTOInternalRole body)
        {
            
                var data = _mapper.Map<ent.InternalRole>(body);
                await _manager.InternalRole.AddAsync(data);
                return new wap.Response<DTOInternalRole>() { Data = _mapper.Map<dto.InternalRole.DTOInternalRole>(data) };
            
           
        }

        public async Task<wap.Response<bool>> DeleteInternalRole(string Id)
        {
            
                var getById = await _manager.InternalRole.GetEncryptByIdAsync(Id);

                await _manager.InternalRole.DeleteAsync(getById);
                await _manager.CompleteAsync();
                return new wap.Response<bool>() { Data = true };
            
            
        }

        // Get Paginated, filtered, sorted
        public async Task<wap.PagingResponse<List<dto.InternalRole.DTOInternalRole>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<dto.InternalRole.DTOInternalRole>>()
            {
                Draw = param.Draw
            };
            
                // Get all
                IQueryable<ent.InternalRole> dataList = _manager.InternalRole.GetAll();

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
                IQueryable<ent.InternalRole> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.RoleNameAr.Contains(param.Search.Value) ||
                        r.RoleNameEn.Contains(param.Search.Value) ||
                        r.RoleCode.Contains(param.Search.Value));
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "roleNameAr":
                                    query = dataList.Where(u => u.RoleNameAr.Contains(search.Search.Value));
                                    break;
                                case "roleNameEn":
                                    query = dataList.Where(u => u.RoleNameEn.Contains(search.Search.Value));
                                    break;
                                case "roleCode":
                                    query = dataList.Where(u => u.RoleCode.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.RoleNameAr) : query.OrderByDescending(r => r.RoleNameAr);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.RoleNameEn) : query.OrderByDescending(r => r.RoleNameEn);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.RoleCode) : query.OrderByDescending(r => r.RoleCode);
                        break;
                    default:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.RoleId) : query.OrderByDescending(r => r.RoleId);
                        break;
                }
                var data = await query.Skip(param.Start).Take(param.Length).ToListAsync();
                response.Data = _mapper.Map<List<dto.InternalRole.DTOInternalRole>>(data);
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;
                return response;
            
           

        }
    }
}
