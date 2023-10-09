using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;

namespace PRJ_internal_app.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class InternalScreenFieldController : ControllerBase
    {
        private readonly bus.ScreenField.InternalScreenFieldManager _manager;
        private readonly bus.ScreenField.InternalFieldPermissionManager _permissionManager;

        public InternalScreenFieldController(bus.ScreenField.InternalScreenFieldManager manager, bus.ScreenField.InternalFieldPermissionManager permissionManager)
        {
            _manager = manager;
            _permissionManager = permissionManager;
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetScreenFieldsByScreenId(string Id)
        {
            return Ok(await _manager.GetScreenFieldsByScreenId(Id));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(dto.InternalScreen.DTOInternalScreenField dto)
        {
            return Ok(await _manager.AddScreenField(dto));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(dto.InternalScreen.DTOInternalScreenField dto)
        {
            return Ok(await _manager.UpdateScreenField(dto));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            return Ok(await _manager.DeleteScreenField(Id));
        }

        [HttpPost("save-field-permission")]
        public async Task<IActionResult> SaveFieldsPermission(List<dto.InternalScreen.DTOInternalFieldPermission> dto)
        {
            return Ok(await _permissionManager.SaveFieldsPermission(dto));
        }
        [HttpPost("GetByFieldId")]
        public async Task<IActionResult> GetByPermissionsByFieldId(int Id,string roleId)
        {
            return Ok(await _permissionManager.GetByFieldId(Id, roleId));
        }

    }
}
