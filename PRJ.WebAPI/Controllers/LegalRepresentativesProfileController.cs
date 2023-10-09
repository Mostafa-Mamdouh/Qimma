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
    [ApiExplorerSettings(GroupName = "LegalRepresentativesProfileApi")]

    public class LegalRepresentativesProfileController : ControllerBase
    {
        private readonly bus.LegalRepresentativesProfile.LegalRepresentativesProfileManager _manager;

        public LegalRepresentativesProfileController(bus.LegalRepresentativesProfile.LegalRepresentativesProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetLegalRepresentativesProfile()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetLegalRepresentativesProfileById(string Id)
        {
            return Ok(await _manager.GetLegalRepresentativesProfileById(Id));
        }

        [HttpPut("Updatee")]
        public async Task<IActionResult> UpdateLegalRepresentativesProfile(dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile body)
        {
            return Ok(await _manager.UpdateLegalRepresentativesProfile(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLegalRepresentativesProfile(dto.LegalRepresentativesProfile.DTOLegalRepresentativesProfile body)
        {
            return Ok(await _manager.AddLegalRepresentativesProfile(body));
        }

    }
}
