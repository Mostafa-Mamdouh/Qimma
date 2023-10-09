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
	public class RadionuclideController : ControllerBase
	{

		private readonly bus.Radionuclide.RadionuclideManager _manager;

		public RadionuclideController(bus.Radionuclide.RadionuclideManager manager)
		{
			_manager = manager;
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetRadionuclideManager()
		{
			return Ok(await _manager.GetAll());
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddRadionuclide(dto.Radionuclide.AddRadionuclideDto body)
		{
			return Ok(await _manager.AddRadionuclide(body));
		}


		[HttpPost("Update")]
		public async Task<IActionResult> UpdateRadionuclide(dto.Radionuclide.UpdateRadionuclideDto changes)
		{
			return Ok(await _manager.UpdateRadionuclide(changes));
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> DeleteRadionuclide(string Id)
		{
			return Ok(await _manager.DeleteRadionuclide(Id));
		}

		[HttpPost("GetAllFuncational")]
		public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
		{
			return Ok(await _manager.GetAllFunctional(param));
		}
		[HttpPost("GetByNuclearMaterial")]
		public async Task<IActionResult> GetByNuclearMaterial(string nuclearmaterial)
		{
			return Ok(await _manager.GetByNuclearMaterial(nuclearmaterial));
		}
	}
}
