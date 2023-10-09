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
    public class InternalScreenController : ControllerBase
    {
        private readonly bus.Screen.InternalScreenManager _manager;
        public InternalScreenController(bus.Screen.InternalScreenManager manager)
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
            return Ok(await _manager.GetScreenById(Id));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(dto.InternalScreen.DTOInternalScreen dto)
        {
            return Ok(await _manager.AddScreen(dto));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(dto.InternalScreen.DTOInternalScreen dto)
        {
            return Ok(await _manager.UpdateScreen(dto));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            return Ok(await _manager.DeleteScreen(Id));
        }
        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }
    }
}
