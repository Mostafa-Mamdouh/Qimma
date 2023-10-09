using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ_internal_app.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "CustomerProfileApi")]
    public class CustomerProfileController : Controller
    {
        private readonly bus.CustomerProfile.CustomerProfileManager _manager;
        public CustomerProfileController(bus.CustomerProfile.CustomerProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> getCustomerProfile()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetCustomerProfileById(int customerId)
        {
            return Ok(await _manager.GetCustomerById(customerId));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCustomerProfile(dto.DTOCustomerProfile body)
        {
            return Ok(await _manager.UpdateCustomerProfile(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddCustomerProfile(dto.DTOCustomerProfile body)
        {
            return Ok(await _manager.AddCustomerProfile(body));
        }

        [HttpPost("NextCustomerId")]
        public async Task<IActionResult> GetMaxCustomerId()
        {
            return Ok(await _manager.GetMaxCustomerId());
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteCustomerProfile([FromBody]int customerId)
        {
            return Ok(await _manager.DeleteCustomerProfile(customerId));
        }

        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }
    }
}
