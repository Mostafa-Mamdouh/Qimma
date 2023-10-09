using System.Threading.Tasks;
using rep = PRJ.Repository;
using ent = PRJ.Domain.Entities;
using lg = PRJ.GlobalComponents.Logger;
using cns = PRJ.GlobalComponents.Const;
using wap = PRJ.Application.Warppers;
using dto = PRJ.Application.DTOs;

using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using PRJ.GlobalComponents.Encryption;
using System.Runtime.InteropServices;
using PRJ.Application.Warppers;
using PRJ.Application.DTOs.Tree;

namespace PRJ.Business.CustomerProfile
{
    public class CustomerProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public CustomerProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            this._manager = manager;

            _mapper = mapper;
        }

        public async Task<wap.Response<List<dto.DTOCustomerProfile>>> GetAll()
        {
            
                var CustomerProfile = await _manager.customerProfile.GetAllAsync();

                return new wap.Response<List<dto.DTOCustomerProfile>>()
                {
                    Data = _mapper.Map<List<dto.DTOCustomerProfile>>(CustomerProfile)
                };
            
            
        }

        public async Task<String> GetMaxCustomerId()
        {
            string nextSerialNum = null;
            
                var customerProfile = _manager.customerProfile.GetByQuery(t => t.CustomerId == t.CustomerId).OrderByDescending(i => i.CustomerId).FirstOrDefault();
                if (customerProfile != null)
                {
                    nextSerialNum = (Convert.ToInt32(customerProfile.CustomerId) + 1).ToString();
                }
                else
                {
                    nextSerialNum = "1";
                }

            
            

            return nextSerialNum;
        }


        public async Task<wap.Response<dto.DTOCustomerProfile>> GetCustomerById(int CustomerId)
        {
            
                var getById = await _manager.customerProfile.GetByIdAsync(CustomerId);

                if (getById != null)
                {
                    return new wap.Response<dto.DTOCustomerProfile>()
                    {
                        Data = _mapper.Map<dto.DTOCustomerProfile>(getById)
                    };
                }
                else
                {
                    return new wap.Response<dto.DTOCustomerProfile>()
                    {
                        Succeeded = true,
                        Message = cns.ConstErrors.NotFound
                    };
                }
            
           
        }


        public async Task<wap.Response<dto.DTOCustomerProfile>> UpdateCustomerProfile(dto.DTOCustomerProfile body)
        {
            
                var CustomerProfile = await _manager.customerProfile.GetByIdAsync(body.Id);
                var Data = _mapper.Map<ent.CustomerProfile>(body);
                _manager.customerProfile.Update(Data);
                _manager.Complete();

                //return new wap.Response<dto.RelatedItems.DTORelatedItems>();
                return new wap.Response<dto.DTOCustomerProfile>() { Succeeded = true };
            
           
        }


        public async Task<wap.Response<dto.DTOCustomerProfile>> AddCustomerProfile(dto.DTOCustomerProfile body)
        {
            
                var nextCustomerCode = await GetMaxCustomerId();
                body.CustomerId = int.Parse(nextCustomerCode);
                var Data = _mapper.Map<ent.CustomerProfile>(body);
                await _manager.customerProfile.AddAsync(Data);
                await _manager.CompleteAsync();


                //return new wap.Response<dto.RelatedItems.DTORelatedItems>();
                return new wap.Response<dto.DTOCustomerProfile>() { Succeeded = true };
            
           
        }

        public async Task<wap.Response<bool>> DeleteCustomerProfile(int customerId)
        {
            
                var getById = await _manager.customerProfile.GetByIdAsync(customerId);

                await _manager.customerProfile.DeleteAsync(getById);
                await _manager.CompleteAsync();
                return new wap.Response<bool>() { Succeeded = true };
            
           
        }

        public async Task<wap.PagingResponse<List<dto.DTOCustomerProfile>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<dto.DTOCustomerProfile>>()
            {
                Draw = param.Draw
            };
            
                // Get all
                IQueryable<ent.CustomerProfile> dataList = _manager.customerProfile.GetAll();

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
                IQueryable<ent.CustomerProfile> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.CustomerNameAr.Contains(param.Search.Value) ||
                        r.CustomerNameEn.Contains(param.Search.Value) ||
                        r.RefCode.Contains(param.Search.Value) );
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "customerNameAr":
                                    query = dataList.Where(u => u.CustomerNameAr.Contains(search.Search.Value));
                                    break;
                                case "customerNameEn":
                                    query = dataList.Where(u => u.CustomerNameEn.Contains(search.Search.Value));
                                    break;
                                case "refCode":
                                    query = dataList.Where(u => u.RefCode.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CustomerNameAr) : query.OrderByDescending(r => r.CustomerNameAr);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CustomerNameEn) : query.OrderByDescending(r => r.CustomerNameEn);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.RefCode) : query.OrderByDescending(r => r.RefCode);
                        break;
            }
                var data = await query.Skip(param.Start).Take(param.Length).ToListAsync();
                response.Data = _mapper.Map<List<dto.DTOCustomerProfile>>(data);
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;
                return response;
            
           

        }

    }

    
}
