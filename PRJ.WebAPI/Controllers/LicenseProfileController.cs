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
    [ApiExplorerSettings(GroupName = "LicenseProfilepi")]
    public class LicenseProfileController : ControllerBase
    {
        private readonly bus.LicenseProfile.LicenseProfileManager _manager;

        public LicenseProfileController(bus.LicenseProfile.LicenseProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetLicenseProfile()
        {
            return Ok(await _manager.GetAll());
        }
        [HttpPost("GetLicenseNumber")]
        public async Task<IActionResult> GetLicenseNumber()
        {
            return Ok(await _manager.GetLicenseNumber());
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetLicenseProfileById(string Id)
        {
            return Ok(await _manager.GetLicenseProfileById(Id));
        }
        [HttpPost("GetByFacilityId")]
        public async Task<IActionResult> GetByFacilityId(string Id)
        {
            return Ok(await _manager.GetByFacilityId(Id));
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateLicenseProfile(dto.LicenseProfile.DTOLicenseProfile body)
        {
            return Ok(await _manager.UpdateLicenseProfile(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLicenseProfile(dto.LicenseProfile.DTOLicenseProfile body)
        {
            return Ok(await _manager.AddLicenseProfile(body));
        }

    }
}
