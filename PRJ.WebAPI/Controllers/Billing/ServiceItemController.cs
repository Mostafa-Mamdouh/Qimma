using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using wap = PRJ.Application.Warppers;

namespace PRJ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceItemController : Controller
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
