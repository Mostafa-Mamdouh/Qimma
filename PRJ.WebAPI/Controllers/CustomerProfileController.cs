using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "CustomerProfile")]

    public class CustomerProfileController : Controller
    {
        private readonly bus.CustomerProfile.CustomerProfileManager _manager;
        public CustomerProfileController(bus.CustomerProfile.CustomerProfileManager manager)
        {
            _manager = manager;
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _manager.GetAll());
        }
    }
}
