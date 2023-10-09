using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRJ.Domain.Entities;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
namespace PRJ.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
	[ApiExplorerSettings(GroupName = "FacilityProfileAPI")]
	public class FacilityProfileController : ControllerBase
    {
        private readonly bus.FacilityProfile.FacilityProfileManager _manager;

        public FacilityProfileController(bus.FacilityProfile.FacilityProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetFacilityProfile()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetFacilityProfileById(string Id)
        {
            return Ok(await _manager.GetFacilityProfileById(Id));
        }
        [HttpPost("GetByEntityId")]
        public async Task<IActionResult> GetByEntityId(string Id)
        {
            return Ok(await _manager.GetByEntityId(Id));
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateFacilityProfile( dto.FacilityProfile.DTOFacilityProfile body)
        {
            return Ok(await _manager.UpdateFacilityProfile( body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddFacilityProfile(dto.FacilityProfile.DTOFacilityProfile body)
        {
            return Ok(await _manager.AddFacilityProfile(body));
        }


    }
}
