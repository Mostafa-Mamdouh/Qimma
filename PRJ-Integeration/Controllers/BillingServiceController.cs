using AmanAPI.Controllers;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using PRJ.Application.Enums;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities.BillingServiceTrn;
using System.Security.Cryptography.X509Certificates;
using bus = PRJ.Business;
using dto = PRJ.Application.AmanDTOs;
using wap = PRJ.Application.Warppers;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;

using AutoMapper;
using Org.BouncyCastle.Ocsp;

namespace PRJ_internal_app.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]

    public class BillingServiceController : BaseApiController
    {
        private readonly rep.IUnitOfWork _manager2;
        private readonly RepositoryContext _manager;
        BillingServiceTrnHeader invoiceHeader = null;
        public readonly IMapper _mapper;

        public BillingServiceController(RepositoryContext manager)
        {
            _manager = manager;
        }

        [HttpPost("Add")]
        public async Task<ApiResponse> Add(dto.Billing.DTOInvoiceTransactionHeader Body)
        {

            try
            {
                var data = _mapper.Map<ent.BillingServiceTrn.BillingServiceTrnHeader>(Body);
                data.TransactionRefNum = data.TransactionID + DateTime.Now.Year.ToString().Substring(1);
                var result = _manager.BillingServiceTrnHeader.AddAsync(data);

                //   transaction.Commit();
                return new ApiResponse(200) { Message = "TransactionNumber :" + data.TransactionRefNum };
            }


            catch (Exception ex)
            {
                return new ApiResponse(500) { Message = ReponseMsg.failed.ToString(), Error = ex.Message };
            }


        }

        //[HttpPost("Enquire")]
        //public async Task<IActionResult> Enquire([FromQuery] string SadadNo)
        //{
        //    return Ok(new ApiResponse(200) { Message = ReponseMsg.success.ToString() });
        //}

    }


}
