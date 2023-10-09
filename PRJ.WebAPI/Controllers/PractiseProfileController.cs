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
    [ApiExplorerSettings(GroupName = "PractiseProfileApi")]
    public class PractiseProfileController : ControllerBase
    {
        private readonly bus.PractiseProfile.PractiseProfileManager _manager;

        public PractiseProfileController(bus.PractiseProfile.PractiseProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetPractiseProfile()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetPractiseProfileById(string Id)
        {
            return Ok(await _manager.GetPractiseProfileById(Id));
        }
        [HttpPost("GetPractiseByLicenseId")]
        public async Task<IActionResult> GetPractiseByLicenseId(string Id)
        {
            return Ok(await _manager.GetPractiseByLicenseId(Id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePractiseProfile(dto.PractiseProfile.DTOPractiseProfile body)
        {
            return Ok(await _manager.UpdatePractiseProfile(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPractiseProfile(dto.PractiseProfile.DTOPractiseProfile body)
        {
            return Ok(await _manager.AddPractiseProfile(body));
        }



    }
}
