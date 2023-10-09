using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRJ_internal_app.Coomon;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
namespace PRJ_internal_app.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class SafetyResponsibleOfficersController : ControllerBase
    {
        private readonly bus.SafetyResponsibleOfficersProfile.SafetyResponsibleOfficersProfileManager _manager;
        public SafetyResponsibleOfficersController(bus.SafetyResponsibleOfficersProfile.SafetyResponsibleOfficersProfileManager manager)
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
        public async Task<IActionResult> Add(dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile Body)
        {
            return Ok(await _manager.AddSafetyResponsibleOfficersProfile(Body));
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dto.SafetyResponsibleOfficersProfile.DTOSafetyResponsibleOfficersProfile Body)
        {
            return Ok(await _manager.UpdateSafetyResponsibleOfficersProfile(Body));
        }
    }
}
