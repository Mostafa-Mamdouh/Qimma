using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;

namespace PRJ.WebAPI.Controllers
{
	[Authorize(AuthenticationSchemes = "Bearer")]

	[Route("api/[controller]")]
	[ApiController]
	[ApiExplorerSettings(GroupName = "LookupApi")]
	public class LookupSetController : ControllerBase
	{
		private readonly bus.LookupSet.LookupSetManager _manager;
		public LookupSetController(bus.LookupSet.LookupSetManager manager)
		{
			_manager = manager;
		}

		[HttpPost("GetLookups")]
		public async Task<IActionResult> GetLookups()
		{
			return Ok(await _manager.GetAll());
		}
		[HttpPost("GetSourceFormsLookup")]
		public async Task<IActionResult> GetSourceFormsLookup()
		{
			return Ok(await _manager.GetSourceFormsLookup());
		}
		[HttpPost("GetLookupsByClassName")]
		public async Task<IActionResult> GetLookupsByClassName(string ClassName)
		{
			return Ok(await _manager.GetLookupsByClassName(ClassName));
		}

		[HttpPost("AddLookUp")]
		public async Task<IActionResult> AddLookUp(dto.LookupSetTerm.DTOLookupSetTermAction lookUp)
		{
			return Ok(await _manager.AddLookUp(lookUp));
		}

		[HttpPost("UpdateLookUp")]
		public async Task<IActionResult> AddLookUps(dto.LookupSetTerm.DTOLookupSetTermAction lookUp)
		{
			return Ok(await _manager.UpdateLookUp(lookUp));
		}
	}
}
