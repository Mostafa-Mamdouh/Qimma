using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Warppers;
using System.Reflection;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;
using enc = PRJ.GlobalComponents.Encryption;

namespace PRJ.Business.BillingServiceTrnHeader
{
    public class BillingServiceTrnHeaderManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public BillingServiceTrnHeaderManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<wap.Response<List<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>>> GetAll()
        {
            
                return new wap.Response<List<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>>()
                {
                    Data = _mapper.Map<List<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>>(await _manager.BillingServiceTrnHeader.GetByQuery(_ => _.TransactionID != null).OrderBy(_ => _.TransactionID).ToListAsync()),

                };
            
         
        }
        public async Task<wap.Response<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>> GetWithId(string Id)
        {
            
                
                var _id = int.Parse(enc.EncryptQueryURL.Decrypt(Id));
                var data = _manager.BillingServiceTrnHeader.GetByQuery(_ => _.TransactionID == _id).Include(_ => _.BillingServiceTrnDetails).FirstOrDefault();
                var body = _mapper.Map<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>(data);
                
                return new wap.Response<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>()
                {
                    Data = _mapper.Map<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>(data),
                };
            
           
        }
        public async Task<wap.Response<List<dto.BillingServiceItemProfileDtos.ServiceItemProfileDto>>> GetServiceItemByHierarchyCode(string Code)
        {
            
                var BillingServiceTrnHeader = await _manager.ServiceItemProfile.GetByQuery(_ => _.ItemStructureCode == Code).ToListAsync();

                return new wap.Response<List<dto.BillingServiceItemProfileDtos.ServiceItemProfileDto>>()
                {
                    Data = _mapper.Map<List<dto.BillingServiceItemProfileDtos.ServiceItemProfileDto>>(BillingServiceTrnHeader)
                };
            
          
        }

        public async Task<wap.PagingResponse<List<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>>> GetAllFunctional(wap.PagingRequest param)
        {
            // Initialize response
            var response = new PagingResponse<List<ent.BillingServiceTrn.BillingServiceTrnHeader>>()
            {
                Draw = param.Draw
            };
            
                IQueryable<ent.BillingServiceTrn.BillingServiceTrnHeader> dataList = _manager.BillingServiceTrnHeader
                    .GetByQuery(_ => _.TransactionID != null)
                    //.Include(_ => _.ItemHierarchyStructure)
                    .Include(_ => _.BillingServiceTrnDetails);

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
                IQueryable<ent.BillingServiceTrn.BillingServiceTrnHeader> query = null;
                if (!string.IsNullOrEmpty(param.Search.Value))
                {
                    query = dataList
                        .Where(r =>
                        r.TransactionRefNum.Contains(param.Search.Value) ||
                        r.InvoiceDate.ToString().Contains(param.Search.Value) ||
                        r.InvoiceBU.Contains(param.Search.Value) ||
                        r.CustomerName.Contains(param.Search.Value) ||
                        r.LookupSetTerm.Value.Contains(param.Search.Value) ||
                        r.InvoiceSource.ToString().Contains(param.Search.Value));
                    

                }
                else if (searchByColumn) // search by column
                {
                    foreach (var search in param.Columns)
                    {
                        if (!string.IsNullOrEmpty(search.Search.Value))
                        {
                            switch (search.Data)
                            {

                                case "transactionRefNum":
                                    query = dataList.Where(u => u.TransactionRefNum.Contains(search.Search.Value));
                                    break;
                                case "invoiceDate":
                                    query = dataList.Where(u => u.InvoiceDate.ToString().Contains(search.Search.Value));
                                    break;
                                case "invoiceBU":
                                    query = dataList.Where(u => u.InvoiceBU.Contains(search.Search.Value));
                                    break;
                                case "customerName":
                                    query = dataList.Where(u => u.CustomerName.Contains(search.Search.Value));
                                    break;
                                case "invoiceSource":
                                    query = dataList.Where(u => u.InvoiceSource.ToString().Contains(search.Search.Value));
                                    break;
                                case "statusFlag":
                                    query = dataList.Where(u => u.LookupSetTerm.Value.Contains(search.Search.Value));
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
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.TransactionRefNum) : query.OrderByDescending(r => r.TransactionRefNum);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.InvoiceDate) : query.OrderByDescending(r => r.InvoiceDate);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.InvoiceBU) : query.OrderByDescending(r => r.InvoiceBU);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.CustomerName) : query.OrderByDescending(r => r.CustomerName);
                        break;
                    case 4:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.InvoiceSource) : query.OrderByDescending(r => r.InvoiceSource);
                        break;
                    case 5:
                        query = colOrder.Dir == "asc" ? query.OrderBy(r => r.StatusFlag) : query.OrderByDescending(r => r.StatusFlag);
                        break;
                }

                query = query.Include(_ => _.LookupSetTerm).Include(_ => _.InvoiceSources).OrderByDescending(_ => _.InvoiceDate);
                response.Data = query.Skip(param.Start).Take(param.Length).ToList();
                var recordsTotal = query.Count();
                response.RecordsTotal = recordsTotal;
                response.RecordsFiltered = recordsTotal;
                response.Succeeded = true;
                var Data = _mapper.Map<List<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>>(response.Data);

                return new PagingResponse<List<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>>()
                {
                    Succeeded = true,
                    Data = _mapper.Map<List<dto.BillingServiceTrn.DTOBillingServiceTrnHeader>>(response.Data),
                    Draw = response.Draw,
                    RecordsTotal = response.RecordsTotal,
                    RecordsFiltered = response.RecordsFiltered
                };
            
            
        }

        public async Task<wap.Response<bool>> Add(dto.BillingServiceTrn.DTOAddBillingServiceTrnHeader body)
        {
         
                var invoiceLookup = _manager.LookupSetTerm.GetByQuery(_ => _.LookupSetId == 58 && _.Value == "1").ToList();
                var _invoiceSource = invoiceLookup[0].LookupSetTermId;
                var _statusFlag = enc.EncryptQueryURL.Decrypt(body.header.StatusFlag);
                body.header.StatusFlag = _statusFlag;
                body.header.InvoiceSource = _invoiceSource;
                var x = _mapper.Map<ent.BillingServiceTrn.BillingServiceTrnHeader>(body.header);
                await _manager.BillingServiceTrnHeader.AddAsync(x);
                await _manager.CompleteAsync();
                return new wap.Response<bool>();
        }


        public async Task<wap.Response<bool>> AddFromExternalSource(dto.BillingServiceTrn.DTOAddBillingServiceTrnHeader body)
        {

            var invoiceLookup = _manager.LookupSetTerm.GetByQuery(_ => _.LookupSetId == 58 && _.Value == "2").ToList();
            var _invoiceSource = invoiceLookup[0].LookupSetTermId;
            body.header.InvoiceSource = _invoiceSource;
            var x = _mapper.Map<ent.BillingServiceTrn.BillingServiceTrnHeader>(body.header);
            await _manager.BillingServiceTrnHeader.AddAsync(x);
            await _manager.CompleteAsync();
            return new wap.Response<bool>();
        }

        public async Task<wap.Response<bool>> Update(dto.BillingServiceTrn.DTOUpdateBillingServiceTrn body)
        {
            
                var _TrnId = int.Parse(enc.EncryptQueryURL.Decrypt(body.TransActionID));
                var TrnId = body.TransActionID;
                if (!string.IsNullOrEmpty(TrnId))
                {
                    var _statusFlag = enc.EncryptQueryURL.Decrypt(body.header.StatusFlag);
                    body.header.StatusFlag = _statusFlag;
                    var x = _mapper.Map<ent.BillingServiceTrn.BillingServiceTrnHeader>(body.header);
                    x.TransactionID = _TrnId;
                    var items = _manager.BillingServiceTrnDetails.GetByQuery(_ => _.TransactionID == _TrnId).ToList();
                    _manager.BillingServiceTrnDetails.Delete(items);
                    await _manager.CompleteAsync();
                    _manager.BillingServiceTrnHeader.Update(x);
                    await _manager.CompleteAsync();
                }
                return new wap.Response<bool>(); 
            
           
        }
    }
}
