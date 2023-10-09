using AmanAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business.AmanBusiness;
using dto = PRJ.Application.AmanDTOs;

namespace PRJ_Integeration.Controllers
{

   [Authorize(AuthenticationSchemes = "Bearer")]

    public class LegalRepresentiveProfileController : BaseApiController
    {
        private readonly bus.LegalRepresentiveManager _manager;
        public LegalRepresentiveProfileController(bus.LegalRepresentiveManager manager)
        {
            _manager = manager;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Add(dto.DTOLegalRepresentative entity)
        {
            return  Ok(await _manager.SaveEntity(entity));
        }
    }
}
