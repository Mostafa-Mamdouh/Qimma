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
	[ApiExplorerSettings(GroupName = "SafetyResponsibleOfficersProfileAPI")]
	public class SafetyResponsibleOfficersProfileController : ControllerBase
	{
        private readonly bus.SafetyResponsibleOfficersProfile.SafetyResponsibleOfficersProfileManager _manager;

        public SafetyResponsibleOfficersProfileController(bus.SafetyResponsibleOfficersProfile.SafetyResponsibleOfficersProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetSafetyResponsibleOfficersProfile()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetSafetyResponsibleOfficersProfileById(string Id)
        {
            return Ok(await _manager.GetSafetyResponsibleOfficersProfileById(Id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSafetyResponsibleOfficersProfile( dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile body)
        {
            return Ok(await _manager.UpdateSafetyResponsibleOfficersProfile(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddSafetyResponsibleOfficersProfile(dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile body)
        {
            return Ok(await _manager.AddSafetyResponsibleOfficersProfile(body));
        }


    }
}
