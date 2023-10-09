using AmanAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business.AmanBusiness;
using dto = PRJ.Application.AmanDTOs;

namespace PRJ.WebAPI.Controllers
{
  [Authorize(AuthenticationSchemes = "Bearer")]

    public class LicenseProfileController : BaseApiController
    {
        private readonly bus.LicenseManager _manager;

        public LicenseProfileController(bus.LicenseManager manager)
        {
            _manager = manager;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(dto.DTOLicenseMaster body)
        {
         
            return Ok(await _manager.SaveLicense(body));
        }

    }
}
