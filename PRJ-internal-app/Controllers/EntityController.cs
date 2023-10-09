using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;

namespace PRJ_internal_app.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly bus.EntityProfile.EntityProfileManager _manager;
        public EntityController(bus.EntityProfile.EntityProfileManager manager)
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
        public async Task<IActionResult> Add(dto.EntityProfile.DTOEntityProfile Body)
        {
            return Ok(await _manager.AddEntityProfile(Body));
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dto.EntityProfile.DTOEntityProfile Body)
        {
            return Ok(await _manager.UpdateEntityProfile(Body));
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _manager.DeleteEntityProfile(id));
        }

        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest body)
        {
            return Ok(await _manager.GetAllFunctional(body));
        }
    }
}
