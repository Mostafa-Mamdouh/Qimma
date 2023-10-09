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

    public class WorkersController  : ControllerBase
    {
        private readonly bus.Workers.WorkersManager _manager;
        public WorkersController(bus.Workers.WorkersManager manager)
        {
            _manager = manager;
        }
        #region Workers
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _manager.GetAll());
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(string Id)
        {
            return Ok(await _manager.GetById(Id));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(dto.Workers.DTOWorkers dto)
        {
            return Ok(await _manager.Add(dto));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(dto.Workers.DTOWorkers dto)
        {
            return Ok(await _manager.Update(dto));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            return Ok(await _manager.Delete(Id));
        }
        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }
        #endregion
    }
}
