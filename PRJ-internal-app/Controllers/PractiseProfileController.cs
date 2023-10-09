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
	public class PractiseProfileController : ControllerBase
	{
		private readonly bus.PractiseProfile.PractiseProfileManager _manager;

		public PractiseProfileController(bus.PractiseProfile.PractiseProfileManager manager)
		{
			_manager = manager;
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _manager.GetAll());
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetPractiseProfileById(string Id)
		{
			return Ok(await _manager.GetPractiseProfileById(Id));
		}

		[HttpPut("Update")]
		public async Task<IActionResult> UpdatePractiseProfile(dto.PractiseProfile.DTOPractiseProfile body)
		{
			return Ok(await _manager.UpdatePractiseProfile(body));
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddPractiseProfile(dto.PractiseProfile.DTOPractiseProfile body)
		{
			return Ok(await _manager.AddPractiseProfile(body));
		}
		[HttpPost("GetAllPractisesFunctional")]
		public async Task<IActionResult> GetAllPractisesFunctional(wap.PagingRequest body)
		{
			return Ok(await _manager.GetAllPractisesFunctional(body));
		}
	}
}
