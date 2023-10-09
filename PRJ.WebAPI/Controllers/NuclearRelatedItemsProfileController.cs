using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRJ.Domain.Entities;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
namespace PRJ.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]
	[ApiExplorerSettings(GroupName = "NuclearRelatedItemsProfileAPI")]
	public class NuclearRelatedItemsProfileController : ControllerBase
    {
        private readonly bus.NuclearRelatedItemsProfile.NuclearRelatedItemsProfileManager _manager;

        public NuclearRelatedItemsProfileController(bus.NuclearRelatedItemsProfile.NuclearRelatedItemsProfileManager manager)
        {
            _manager = manager;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetNuclearRelatedItemsProfile()
        {
            return Ok(await _manager.GetAll());
        }

        [HttpPost("GetById")]
        public async Task<IActionResult> GetNuclearRelatedItemsProfileById(string Id)
        {
            return Ok(await _manager.GetNuclearRelatedItemsProfileById(Id));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateNuclearRelatedItemsProfile( dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile body)
        {
            return Ok(await _manager.UpdateNuclearRelatedItemsProfile(body));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddNuclearRelatedItemsProfile(dto.NuclearRelatedItemsProfile.DTONuclearRelatedItemsProfile body)
        {
            return Ok(await _manager.AddNuclearRelatedItemsProfile(body));
        }


    }
}
