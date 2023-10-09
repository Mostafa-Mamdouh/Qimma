using AmanAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application;

namespace PRJ.WebAPI.Controllers
{
  // [Authorize(AuthenticationSchemes = "Bearer")]

    public class PermitProfileController : BaseApiController
    {
        private readonly bus.AmanBusiness.PermiteManager _manager;

        public PermitProfileController(bus.AmanBusiness.PermiteManager manager)
        {
            _manager = manager;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> AddPermitDetailsProfile(dto.AmanDTOs.DTOPermitMaster body)
        {
            return Ok(await _manager.SavePermite(body));
        }


    }
}
