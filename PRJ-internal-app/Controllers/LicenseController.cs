using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
namespace PRJ_internal_app.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class LegalRepProfileController : ControllerBase
    {
        private readonly bus.LegalRepresentativesProfile.LegalRepresentativesProfileManager _manager;
        public LegalRepProfileController(bus.LegalRepresentativesProfile.LegalRepresentativesProfileManager manager)
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
        public async Task<IActionResult> Add(dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile Body)
        {
            return Ok(await _manager.AddLegalRepresentativesProfile(Body));
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile Body)
        {
            return Ok(await _manager.UpdateLegalRepresentativesProfile(Body));
        }
    }
}
