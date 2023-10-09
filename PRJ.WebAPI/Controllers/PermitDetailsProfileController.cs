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
    [ApiExplorerSettings(GroupName = "PermitDetailsProfileApi")]
    public class PermitDetailsProfileController : ControllerBase
    {
        private readonly bus.PermitDetailsProfile.PermitDetailsProfileManager _manager;

        public PermitDetailsProfileController(bus.PermitDetailsProfile.PermitDetailsProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetPermitDetailsProfile()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetPermitDetailsProfileById(string Id)
        {
            return Ok(await _manager.GetPermitDetailsProfileById(Id));
        }
        [HttpPost("GetPermitNumberProfileById")]
        public async Task<IActionResult> GetPermitNumberProfileById(string Id)
        {
            return Ok(await _manager.GetPermitNumberProfileById(Id));
        }
        [HttpPost("GetByLicenseId")]
        public async Task<IActionResult> GetByLicenseId(string Id)
        {
            return Ok(await _manager.GetByLicenseId(Id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePermitDetailsProfile(dto.PermitDetailsProfile.DTOPermitDetailsProfile body)
        {
            return Ok(await _manager.UpdatePermitDetailsProfile(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPermitDetailsProfile(dto.PermitDetailsProfile.DTOPermitDetailsProfile body)
        {
            return Ok(await _manager.AddPermitDetailsProfile(body));
        }

    }
}
