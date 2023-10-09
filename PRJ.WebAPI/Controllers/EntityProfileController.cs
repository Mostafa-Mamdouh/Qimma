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
	[ApiExplorerSettings(GroupName = "EntityProfileAPI")]

    public class EntityProfileController : ControllerBase
    {
        private readonly bus.EntityProfile.EntityProfileManager _manager;

        public EntityProfileController(bus.EntityProfile.EntityProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetEntityProfile()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetEntityProfileById(string Id)
        {
            return Ok(await _manager.GetEntityProfileById(Id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateEntityProfile( dto.EntityProfile.DTOEntityProfile body)
        {
            return Ok(await _manager.UpdateEntityProfile( body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddEntityProfile(dto.EntityProfile.DTOEntityProfile body)
        {
            return Ok(await _manager.AddEntityProfile(body));
        }


    }
}
