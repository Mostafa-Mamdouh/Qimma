using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs.ListOfValues;
using PRJ.Application.DTOs.InternalScreen;
using PRJ.Application.Warppers;
using System.Reflection;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.Lov
{
    public class LovManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public LovManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.ListOfValues.DTOListOfValue>>> GetAll()
        {
            
                var Lov = await _manager.ListOfValues.GetAllAsync();

                return new wap.Response<List<dto.ListOfValues.DTOListOfValue>>()
                {
                    Data = _mapper.Map<List<dto.ListOfValues.DTOListOfValue>>(Lov)
                };
            
           
        }

        public async Task<wap.Response<dto.ListOfValues.DTOListOfValue>> GetLovById(string Id)
        {
            
                var getById = await _manager.ListOfValues.GetEncryptByIdAsync(Id);

                if (getById != null)
                {
                    return new wap.Response<dto.ListOfValues.DTOListOfValue>()
                    {
                        Data = _mapper.Map<dto.ListOfValues.DTOListOfValue>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.ListOfValues.DTOListOfValue>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }

        public async Task<wap.Response<DTOListOfValue>> UpdateLov(dto.ListOfValues.DTOListOfValue body)
        {
            
                var lov = await _manager.ListOfValues.GetEncryptByIdAsync(body.Id);
                if (lov != null)
                {
                    lov.LovNameEn = body.LovNameEn;
                    lov.LovNameAr = body.LovNameAr;
                    lov.LovCode = body.LovCode;
                    lov.SqlStatement = body.SqlStatement;
                }

                _manager.ListOfValues.Update(lov);
                _manager.Complete();
                return new wap.Response<DTOListOfValue>() { Data = _mapper.Map<dto.ListOfValues.DTOListOfValue>(lov) };

            
            
        }

        public async Task<wap.Response<dto.ListOfValues.DTOListOfValue>> AddLov(dto.ListOfValues.DTOListOfValue body)
        {
            
                var data = _mapper.Map<ent.ListOfValue>(body);
                await _manager.ListOfValues.AddAsync(data);
                return new wap.Response<DTOListOfValue>() { Data = _mapper.Map<dto.ListOfValues.DTOListOfValue>(data) };
            
         
        }

        public async Task<wap.Response<bool>> DeleteLov(string Id)
        {
            
                var getById = await _manager.ListOfValues.GetEncryptByIdAsync(Id);

                await _manager.ListOfValues.DeleteAsync(getById);
                await _manager.CompleteAsync();
                return new wap.Response<bool>() { Data = true };
            
           
        }

        // Get Paginated, filtered, sorted
        public async Task<wap.PagingResponse<List<dto.ListOfValues.DTOListOfValue>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<dto.ListOfValues.DTOListOfValue>>()
            {
                Draw = param.Draw
            };
            
                // Get all
                IQueryable<ent.ListOfValue> dataList = _manager.ListOfValues.GetAll();

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
                IQueryable<ent.ListOfValue> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.LovNameAr.Contains(param.Search.Value) ||
                        r.LovNameEn.Contains(param.Search.Value) ||
                        r.LovCode.Contains(param.Search.Value));
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "lovNameAr":
                                    query = dataList.Where(u => u.LovNameAr.Contains(search.Search.Value));
                                    break;
                                case "lovNameEn":
                                    query = dataList.Where(u => u.LovNameEn.Contains(search.Search.Value));
                                    break;
                                case "lovCode":
                                    query = dataList.Where(u => u.LovCode.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LovNameAr) : query.OrderByDescending(r => r.LovNameAr);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LovNameEn) : query.OrderByDescending(r => r.LovNameEn);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LovCode) : query.OrderByDescending(r => r.LovCode);
                        break;
                    default:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.LovId) : query.OrderByDescending(r => r.LovId);
                        break;
                }
                var data = await query.Skip(param.Start).Take(param.Length).Include(x=>x.InternalScreenFields.Take(1)).ToListAsync();
                response.Data = _mapper.Map<List<dto.ListOfValues.DTOListOfValue>>(data);
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;
                return response;
           

        }
    }
}
