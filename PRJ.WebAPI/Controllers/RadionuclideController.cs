using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RadionuclideApi")]
    public class RadionuclideController : ControllerBase
    {

        private readonly bus.Radionuclide.RadionuclideManager _manager;

        public RadionuclideController(bus.Radionuclide.RadionuclideManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetRadionuclideManager()
        {
            return Ok(await _manager.GetAll());
        }

        //[HttpPost("GetAll")]
        //public async Task<IActionResult> GetRadionuclideManager(int? pageNumber, int pageSize = 2)
        //{
        //    return Ok(await _manager.GetAll(pageNumber, pageSize));
        //}

        [HttpPost("Add")]
        public async Task<IActionResult> AddRadionuclide(dto.Radionuclide.AddRadionuclideDto body)
        {
            return Ok(await _manager.AddRadionuclide(body));
        }


        //[HttpPut("Update")]
        //public async Task<IActionResult> UpdateRadionuclide(dto.Radionuclide.UpdateRadionuclideDto changes)
        //{
        //    return Ok(await _manager.UpdateRadionuclide(changes));
        //}

        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> DeleteRadionuclide(int id)
        //{
        //    return Ok(await _manager.DeleteRadionuclide(id));
        //}

        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }

        [HttpPost("GetByNuclearMaterial")]
        public async Task<IActionResult> GetByNuclearMaterial(string nuclearmaterial)
        {
            return Ok(await _manager.GetByNuclearMaterial(nuclearmaterial));
        }
    }

}
