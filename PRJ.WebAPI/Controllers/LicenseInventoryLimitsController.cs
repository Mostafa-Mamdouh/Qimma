using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;

namespace PRJ.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "LicenseInventoryLimitsApi")]
    public class LicenseInventoryLimitsController : ControllerBase
    {
        private readonly bus.LicenseInventoryLimits.LicenseInventoryLimitsManager _manager;

        public LicenseInventoryLimitsController(bus.LicenseInventoryLimits.LicenseInventoryLimitsManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetLicenseInventoryLimits()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetLicenseInventoryLimitsById(string Id)
        {
            return Ok(await _manager.GetLicenseInventoryLimitsById(Id));
        }

        [HttpPut("Updates")]
        public async Task<IActionResult> UpdateLicenseInventoryLimits(dto.LicenseInventoryLimits.DTOLicenseInventoryLimits body)
        {
            return Ok(await _manager.UpdateLicenseInventoryLimits(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLicenseInventoryLimits(dto.LicenseInventoryLimits.DTOLicenseInventoryLimits body)
        {
            return Ok(await _manager.AddLicenseInventoryLimits(body));
        }

    }
}
