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
    public class ListOfValueController : ControllerBase
    {
        private readonly bus.Lov.LovManager _manager;
        public ListOfValueController(bus.Lov.LovManager manager)
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
            return Ok(await _manager.GetLovById(Id));
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(dto.ListOfValues.DTOListOfValue dto)
        {
            return Ok(await _manager.AddLov(dto));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(dto.ListOfValues.DTOListOfValue dto)
        {
            return Ok(await _manager.UpdateLov(dto));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            return Ok(await _manager.DeleteLov(Id));
        }
        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
        {
            return Ok(await _manager.GetAllFunctional(param));
        }
    }
}
