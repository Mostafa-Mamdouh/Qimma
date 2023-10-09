using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs.InternalScreen;
using PRJ.Application.Warppers;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.Screen
{
    public class InternalScreenManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public InternalScreenManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.InternalScreen.DTOInternalScreen>>> GetAll()
        {
            
                var Screen = await _manager.InternalScreen.GetAllAsync();

                return new wap.Response<List<dto.InternalScreen.DTOInternalScreen>>()
                {
                    Data = _mapper.Map<List<dto.InternalScreen.DTOInternalScreen>>(Screen)
                };
            
          
        }

        public async Task<wap.Response<dto.InternalScreen.DTOInternalScreen>> GetScreenById(string Id)
        {
            
                var getById = await _manager.InternalScreen.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.InternalScreen.DTOInternalScreen>()
                    {
                        Data = _mapper.Map<dto.InternalScreen.DTOInternalScreen>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.InternalScreen.DTOInternalScreen>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }

        public async Task<wap.Response<DTOInternalScreen>> UpdateScreen(dto.InternalScreen.DTOInternalScreen body)
        {
            
                var screen = await _manager.InternalScreen.GetEncryptByIdAsync(body.Id);
                if (screen != null) {
                    screen.ScreenNameEn = body.ScreenNameEn;
                    screen.ScreenNameAr = body.ScreenNameAr;
                    screen.ScreenCode = body.ScreenCode;
                }
                _manager.InternalScreen.Update(screen);
                _manager.Complete();
                return new wap.Response<DTOInternalScreen>() { Data = _mapper.Map<dto.InternalScreen.DTOInternalScreen>(screen) };

            
            
        }

        public async Task<wap.Response<DTOInternalScreen>> AddScreen(dto.InternalScreen.DTOInternalScreen body)
        {
            
                var data = _mapper.Map<ent.InternalScreen>(body);
                await _manager.InternalScreen.AddAsync(data);

                return new wap.Response<DTOInternalScreen>() { Data = _mapper.Map<dto.InternalScreen.DTOInternalScreen>(data) };
            
           
        }

        public async Task<wap.Response<bool>> DeleteScreen(string Id)
        {
            
                var getById = await _manager.InternalScreen.GetEncryptByIdAsync(Id);

                await _manager.InternalScreen.DeleteAsync(getById);
                await _manager.CompleteAsync();
                return new wap.Response<bool>() { Data = true };
            
           
        }

        // Get Paginated, filtered, sorted
        public async Task<wap.PagingResponse<List<DTOInternalScreen>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<dto.InternalScreen.DTOInternalScreen>>()
            {
                Draw = param.Draw
            };
            
                // Get all
                IQueryable<ent.InternalScreen> dataList = _manager.InternalScreen.GetAll();

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
                IQueryable<ent.InternalScreen> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.ScreenNameAr.Contains(param.Search.Value) ||
                        r.ScreenNameEn.Contains(param.Search.Value) ||
                        r.ScreenCode.Contains(param.Search.Value));
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "screenNameAr":
                                    query = dataList.Where(u => u.ScreenNameAr.Contains(search.Search.Value));
                                    break;
                                case "screenNameEn":
                                    query = dataList.Where(u => u.ScreenNameEn.Contains(search.Search.Value));
                                    break;
                                case "screenCode":
                                    query = dataList.Where(u => u.ScreenCode.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ScreenNameAr) : query.OrderByDescending(r => r.ScreenNameAr);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ScreenNameEn) : query.OrderByDescending(r => r.ScreenNameEn);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ScreenCode) : query.OrderByDescending(r => r.ScreenCode);
                        break;
                    default:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ScreenId) : query.OrderByDescending(r => r.ScreenId);
                        break;
                }
                var data = await query.Skip(param.Start).Take(param.Length).Include(x=>x.InternalScreenFields).ThenInclude(x=>x.InternalFieldPermissions).ToListAsync();
                response.Data = _mapper.Map<List<dto.InternalScreen.DTOInternalScreen>>(data);
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;
                return response;
            
           

        }
    }
}
