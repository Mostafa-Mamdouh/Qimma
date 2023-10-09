using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;
namespace PRJ_internal_app.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly bus.FacilityProfile.FacilityProfileManager _manager;
        public FacilityController(bus.FacilityProfile.FacilityProfileManager manager)
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
        public async Task<IActionResult> Add(dto.FacilityProfile.DTOFacilityProfile Body)
        {
            return Ok(await _manager.AddFacilityProfile(Body));
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dto.FacilityProfile.DTOFacilityProfile Body)
        {
            return Ok(await _manager.UpdateFacilityProfile(Body));
        }
        [HttpPost]
        [Route("GetAllFunctional")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }

        [HttpPost]
        [Route("GetByEntityId")]
        public async Task<IActionResult> GetByEntityId(string Id)
        {
            return Ok(await _manager.GetByEntityId(Id));
        }
    }
}
