using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using wap = PRJ.Application.Warppers;

namespace PRJ_internal_app.Controllers.Billing
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]
    public class ServiceItemController : ControllerBase
    {
        private readonly bus.BillingServices.ServiceItemProfileManager _manager;

        public ServiceItemController(bus.BillingServices.ServiceItemProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(wap.PagingRequest body)
        {
            var res = await _manager.GetAllFunctional(body);
            return Ok(res);
        }
    }
}
