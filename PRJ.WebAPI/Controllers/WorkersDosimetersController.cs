using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ.WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WorkersDosimeters")]
    public class WorkersDosimetersController : ControllerBase
    {
        private readonly bus.Workers.WorkersDosimetersManager _manager;

        public WorkersDosimetersController(bus.Workers.WorkersDosimetersManager manager)
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
            return Ok(await _manager.GetById(Id));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(dto.Workers.DTOWorkersDosimeters obj)
        {
            return Ok(await _manager.Add(obj));
        }
    }
}
