using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WorkersExposuresDoses")]
    public class WorkersExposuresDosesController : ControllerBase
    {

        private readonly bus.Workers.WorkersExposuresDosesManager _manager;

        public WorkersExposuresDosesController(bus.Workers.WorkersExposuresDosesManager manager)
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
        public async Task<IActionResult> Add(dto.Workers.DTOWorkersExposuresDoses dto)
        {
            return Ok(await _manager.Add(dto));
        }
        [HttpPost("GetLastReading")]
        public async Task<IActionResult> GetLastReading(string Id)
        {
            return Ok(await _manager.GetLastReading(Id));
        }

        [HttpPost("AddReadingFromMassPage")]
        public async Task<IActionResult> AddReadingFromMassPage(dto.Workers.DTOMassUpdate_2[] workers , int year , string quarter)
        {
            return Ok(await _manager.AddReadingFromMassPage(workers , year, quarter));
        }

        [HttpPost("GetQuarterByMonth")]
        public async Task<IActionResult> GetQuarterByMonth(string month)
        {
            return Ok(await _manager.GetQuarterByMonth(month));
        }
    }
}
