using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
namespace PRJ.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DssFiscalYearsController : ControllerBase
    {
       
        private readonly bus.DssFiscalYears.DssFiscalYearsManager _manager;

        public DssFiscalYearsController(bus.DssFiscalYears.DssFiscalYearsManager manager)
        {
            _manager = manager;
        }
        [HttpPost("GetYears")]
        public async Task<IActionResult> GetYears()
        {
            return Ok(await _manager.GetAll());
        }

    }
}
