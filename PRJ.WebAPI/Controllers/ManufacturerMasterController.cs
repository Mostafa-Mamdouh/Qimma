using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRJ.Domain.Entities;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
namespace PRJ.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]
	[ApiExplorerSettings(GroupName = "ManufacturerMasterAPI")]
	public class ManufacturerMasterController : ControllerBase
	{

        private readonly bus.ManufacturerMaster.ManufacturerMasterManager _manager;

        public ManufacturerMasterController(bus.ManufacturerMaster.ManufacturerMasterManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetManufacturerMaster()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetManufacturerMasterById(string Id)
        {
            return Ok(await _manager.GetManufacturerMasterById(Id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateFacilityProfile(dto.DTOManufacturerMaster body)
        {
            return Ok(await _manager.UpdateManufacturerMaster(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddManufacturerMaster(dto.DTOManufacturerMaster body)
        {
            return Ok(await _manager.AddManufacturerMaster(body));
        }


    }
}