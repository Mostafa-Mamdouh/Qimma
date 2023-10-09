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
    public class ManufacturerMasterController : ControllerBase
    {
        private readonly bus.ManufacturerMaster.ManufacturerMasterManager _manager;
        public ManufacturerMasterController(bus.ManufacturerMaster.ManufacturerMasterManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _manager.GetAll());
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(dto.DTOManufacturerMaster Body)
        {
            return Ok(await _manager.AddManufacturerMaster(Body));
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dto.DTOManufacturerMaster Body)
        {
            return Ok(await _manager.UpdateManufacturerMaster(Body));
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _manager.DeleteManufacturerMaster(id));
        }
        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }

    }
}
