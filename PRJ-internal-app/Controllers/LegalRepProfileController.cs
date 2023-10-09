using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;
namespace PRJ_internal_app.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly bus.LicenseProfile.LicenseProfileManager _manager;
        public LicenseController(bus.LicenseProfile.LicenseProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _manager.GetAll());
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(dto.LicenseProfile.DTOLicenseProfile Body)
        {
            return Ok(await _manager.AddLicenseProfile(Body));
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dto.LicenseProfile.DTOLicenseProfile Body)
        {
            return Ok(await _manager.UpdateLicenseProfile(Body));
        }
        [HttpPost]
        [Route("GetAllLicensesFuncational")]
        public async Task<IActionResult> GetAllLicensesFuncational(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllLicensesFuncational(param));
        }
        [HttpPost]
        [Route("GetByFacilityId")]
        public async Task<IActionResult> GetByFacilityId(string Id)
        {
            return Ok(await _manager.GetByFacilityId(Id));
        }
    }
}
