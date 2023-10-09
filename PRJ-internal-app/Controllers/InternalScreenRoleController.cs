using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;

namespace PRJ_internal_app.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class InternalScreenRoleController : ControllerBase
    {
        private readonly bus.InternalScreenRole.InternalScreenRoleManager _manager;
        public InternalScreenRoleController(bus.InternalScreenRole.InternalScreenRoleManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAllScreens")]
        public async Task<IActionResult> GetScreensByRoleId(string Id)
        {
            return Ok(await _manager.GetInternalScreens(Id));
        }
       
        [HttpPost("Update")]
        public async Task<IActionResult> Update(List< dto.InternalRole.DTOInternalScreenRole> dto)
        {
            return Ok(await _manager.UpdateInternalRoleScreens(dto));
        }
    }
}
