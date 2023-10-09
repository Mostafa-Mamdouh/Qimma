using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Warppers;
using System.Reflection;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;

namespace PRJ.Business.BillingServiceTrnDetails
{
    public class BillingServiceTrnDetailsManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public BillingServiceTrnDetailsManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
          public async Task<wap.Response<List<dto.EntityProfile.DTOEntityProfile>>> GetAll()
        {
           
                var EntityProfile = await _manager.EntityProfile.GetAllAsync();

                return new wap.Response<List<dto.EntityProfile.DTOEntityProfile>>()
                {
                    Data = _mapper.Map<List<dto.EntityProfile.DTOEntityProfile>>(EntityProfile)
                };
            
           
        }



        public async Task<wap.PagingResponse<List<dto.BillingServiceItemProfileDtos.ServiceItemProfileDto>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.Billing.ServiceItemProfile>>()
            {
                Draw = param.Draw
            };
           
                ent.Billing.ItemHierarchyStructure item;
                IQueryable<ent.Billing.ServiceItemProfile> dataList;
                // Get ItemHierarchy
                if (!string.IsNullOrEmpty(param.extra))
                {
                    item = await _manager.ItemHierarchyStructure.GetByQuery(_ => _.ItemStructureCode == param.extra).FirstOrDefaultAsync();
                    dataList = _manager.ServiceItemProfile.GetByQuery(_ => _.ItemStructureCode == item.ItemStructureCode);
                }
                else
                {
                    dataList = _manager.ServiceItemProfile.GetAll();
                }
                //var item = await _manager.ItemHierarchyStructure.GetByQuery(_ => _.ItemStructureCode == param.extra).FirstOrDefaultAsync();
                // Get all


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
                IQueryable<ent.Billing.ServiceItemProfile> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.CurrentPrice.ToString().Contains(param.Search.Value) ||
                        r.IssueAccountCode.Contains(param.Search.Value) ||
                        r.ItemDesFrn.Contains(param.Search.Value) ||
                        r.ItemDesNtv.Contains(param.Search.Value) ||
                        r.ItemRefCode.Contains(param.Search.Value) ||
                        r.ItemStructureCode.Contains(param.Search.Value));
                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {
                                case "currentPrice":
                                    query = dataList.Where(u => u.CurrentPrice.ToString().Contains(search.Search.Value));
                                    break;
                                case "issueAccountCode":
                                    query = dataList.Where(u => u.IssueAccountCode.Contains(search.Search.Value));
                                    break;
                                case "itemDesFrn":
                                    query = dataList.Where(u => u.ItemDesFrn.Contains(search.Search.Value));
                                    break;
                                case "itemDesNtv":
                                    query = dataList.Where(u => u.ItemDesNtv.Contains(search.Search.Value));
                                    break;
                                case "itemRefCode":
                                    query = dataList.Where(u => u.ItemRefCode.Contains(search.Search.Value));
                                    break;
                                case "itemStructureCode":
                                    query = dataList.Where(u => u.ItemStructureCode.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ItemRefCode) : query.OrderByDescending(r => r.ItemRefCode);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.IssueAccountCode) : query.OrderByDescending(r => r.IssueAccountCode);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ItemDesFrn) : query.OrderByDescending(r => r.ItemDesFrn);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.ItemDesNtv) : query.OrderByDescending(r => r.ItemDesNtv);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CurrentPrice) : query.OrderByDescending(r => r.CurrentPrice);
                        break;
                }

                response.Data = query.Skip(param.Start).Take(param.Length).ToList();
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;

                return new PagingResponse<List<dto.BillingServiceItemProfileDtos.ServiceItemProfileDto>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.BillingServiceItemProfileDtos.ServiceItemProfileDto>>(response.Data),
                    Draw = response.Draw,
                    RecordsTotal = response.RecordsTotal,
                    RecordsFiltered = response.RecordsFiltered
                };
            
           

        }
    
    }
}
