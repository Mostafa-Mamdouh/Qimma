using Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.Enums;
using dto = PRJ.Application.AmanDTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;

namespace PRJ.Business.BillingServices
{
    public class ServiceEntryFeesManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public ServiceEntryFeesManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<ApiResponse> SaveEntryFees(dto.Billing.DTOServiceEntryFees body)
        {
            ent.LookupSetTerm processId = null;
            ent.LookupSetTerm stageId = null;
            ent.LookupSetTerm paymentTerm = null;



            ent.BillingServiceTrn.ServiceEntryFees serviceEntryFees = null;


            using (var transaction = _manager.BeginTransaction())
            {
                try
                {
                    // Entity Profile
                    #region Entity
                    var data = _mapper.Map<ent.BillingServiceTrn.ServiceEntryFees>(body);
                    processId = await _manager.LookupSetTerm.GetByQuery( x => x.AmanId == data.ProcessId && x.LookupSet.ClassName == LookupSetClass.Process.ToString()).FirstOrDefaultAsync();
                    stageId = await _manager.LookupSetTerm.GetByQuery( x => x.AmanId == data.StageId && x.LookupSet.ClassName == LookupSetClass.Stage.ToString()).FirstOrDefaultAsync();
                    paymentTerm = await _manager.LookupSetTerm.GetByQuery(x => x.AmanId == data.PaymentTerms && x.LookupSet.ClassName == LookupSetClass.PaymentTerm.ToString()).FirstOrDefaultAsync();


                    serviceEntryFees = await _manager.ServiceEntryFees.GetByQuery(x => x.CustomerId == body.CustomerId.ToString()).FirstOrDefaultAsync();
                    if (serviceEntryFees == null)
                    {
                        data.PaymentTerms = paymentTerm.LookupSetTermId;
                        data.ProcessId = processId.LookupSetTermId;
                        data.StageId = stageId.LookupSetTermId;
                        await _manager.ServiceEntryFees.AddAsync(data);
                    }
                    else
                    {
                        serviceEntryFees.CustomerNameAr = data.CustomerNameEn;
                        serviceEntryFees.CustomerNameEn = data.CustomerNameEn;
                        serviceEntryFees.ProcessId = processId.LookupSetTermId;
                        serviceEntryFees.StageId = stageId.LookupSetTermId;
                        serviceEntryFees.InvoiceDate = data.InvoiceDate;
                        serviceEntryFees.PaymentTerms = paymentTerm.LookupSetTermId;
                        _manager.ServiceEntryFees.Update(serviceEntryFees);
                    }
                    await _manager.CompleteAsync();
                    #endregion

                    transaction.Commit();
                    return new ApiResponse(200) { Message = ReponseMsg.success.ToString() };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }
    }
}
