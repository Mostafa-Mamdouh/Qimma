using AmanAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business.AmanBusiness;
using dto = PRJ.Application.AmanDTOs;
using wap = PRJ.Application.Warppers;
namespace PRJ_Integeration.Controllers
{
   [Authorize(AuthenticationSchemes = "Bearer")]

    public class FacilityProfileController : BaseApiController
    {
        private readonly bus.FacilityManager _manager;
        public FacilityProfileController(bus.FacilityManager manager)
        {
            _manager = manager;
        }
        [HttpPost("Save")]
        public async Task<IActionResult> Save(dto.DTOFacilityProfile facility)
        {
            return Ok(await _manager.SaveFacility(facility));
        }
    }
}
