using AmanAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business.AmanBusiness;
using dto = PRJ.Application.AmanDTOs;
namespace PRJ_Integeration.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class EntityProfileController : BaseApiController
    {
        private readonly bus.EntityManager _manager;
        public EntityProfileController(bus.EntityManager manager)
        {
            _manager = manager;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(dto.DTOEntityProfile entity)
        {
            return Ok( await _manager.SaveEntity(entity));
        }
    }
}
