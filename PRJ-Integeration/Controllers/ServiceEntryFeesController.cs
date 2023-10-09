using AmanAPI.Controllers;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using PRJ.Application.Enums;
using System.Security.Cryptography.X509Certificates;
using bus = PRJ.Business;
using dto = PRJ.Application.AmanDTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ_Integeration.Controllers
{
   // [Authorize(AuthenticationSchemes = "Bearer")]

    public class ServiceEntryFeesController : BaseApiController
    {
        private readonly bus.BillingServices.ServiceEntryFeesManager _manager;
        public ServiceEntryFeesController(bus.BillingServices.ServiceEntryFeesManager manager)
        {
            _manager = manager;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(dto.Billing.DTOServiceEntryFees Body)
        {
            return Ok(await _manager.SaveEntryFees(Body));

        }
    }
}
