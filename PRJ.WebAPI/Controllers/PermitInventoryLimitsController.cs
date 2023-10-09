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
    [ApiExplorerSettings(GroupName = "PermitInventoryLimitsApi")]
    public class PermitInventoryLimitsController : ControllerBase
    {
        private readonly bus.PermitInventoryLimits.PermitInventoryLimitsManager _manager;

        public PermitInventoryLimitsController(bus.PermitInventoryLimits.PermitInventoryLimitsManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetPermitInventoryLimits()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetPermitInventoryLimitsById(string Id)
        {
            return Ok(await _manager.GetPermitInventoryLimitsById(Id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePermitInventoryLimits(dto.PermitInventoryLimits.DTOPermitInventoryLimits body)
        {
            return Ok(await _manager.UpdatePermitInventoryLimits(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPermitInventoryLimits(dto.PermitInventoryLimits.DTOPermitInventoryLimits body)
        {
            return Ok(await _manager.AddPermitInventoryLimits(body));
        }

    }
}
