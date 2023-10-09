using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ_internal_app.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class InternalRoleController : ControllerBase
    {
        private readonly bus.InternalRole.InternalRoleManager _manager;
        public InternalRoleController(bus.InternalRole.InternalRoleManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _manager.GetAll());
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            return Ok(await _manager.GetInternalRoleById(Id));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(dto.InternalRole.DTOInternalRole dto)
        {
            return Ok(await _manager.AddInternalRole(dto));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(dto.InternalRole.DTOInternalRole dto)
        {
            return Ok(await _manager.UpdateInternalRole(dto));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            return Ok(await _manager.DeleteInternalRole(Id));
        }
        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }
    }
}
