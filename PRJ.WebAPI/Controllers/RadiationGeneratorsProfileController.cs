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
	[ApiExplorerSettings(GroupName = "RadiationGeneratorsProfileAPI")]
	public class RadiationGeneratorsProfileController : ControllerBase
    {
      
            private readonly bus.RadiationGeneratorsProfile.RadiationGeneratorsProfileManager _manager;

            public RadiationGeneratorsProfileController(bus.RadiationGeneratorsProfile.RadiationGeneratorsProfileManager manager)
            {
                _manager = manager;
            }

            [HttpPost("GetAll")]
            public async Task<IActionResult> GetRadiationGeneratorsProfile()
            {
                return Ok(await _manager.GetAll());
            }

            [HttpPost("GetById")]
            public async Task<IActionResult> GetRadiationGeneratorsProfileById(string Id)
            {
                return Ok(await _manager.GetRadiationGeneratorsProfileById(Id));
            }

            [HttpPut("Update")]
            public async Task<IActionResult> UpdateRadiationGeneratorsProfile(dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile body)
            {
                return Ok(await _manager.UpdateRadiationGeneratorsProfile( body));
            }

            [HttpPost("Add")]
            public async Task<IActionResult> AddNuclearRelatedItemsProfile(dto.RadiationGeneratorsProfile.RadiationGeneratorsProfile body)
            {
                return Ok(await _manager.AddRadiationGeneratorsProfile(body));
            }
       }
}
