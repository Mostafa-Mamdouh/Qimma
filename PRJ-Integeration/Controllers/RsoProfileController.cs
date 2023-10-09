using AmanAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business.AmanBusiness;
using dto = PRJ.Application.AmanDTOs;

namespace PRJ_Integeration.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RsoProfileController : BaseApiController
    {
        private readonly bus.RsoManager _manager;

        public RsoProfileController(bus.RsoManager manager)
        {
            _manager = manager;
        }
        [HttpPost("Save")]
        public async Task<IActionResult> Add(dto.DTORso entity)
        {
            return Ok(await _manager.SaveEntity(entity));
        }

    }

}
