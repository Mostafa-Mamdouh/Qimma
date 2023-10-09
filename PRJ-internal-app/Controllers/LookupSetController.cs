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
	public class LookupSetController : ControllerBase
	{
		private readonly bus.LookupSet.LookupSetManager _manager;
		public LookupSetController(bus.LookupSet.LookupSetManager manager)
		{
			_manager = manager;
		}
		#region LookUps
		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _manager.GetAll());
		}
		[HttpPost("GetById")]
		public async Task<IActionResult> GetById(string Id)
		{
			return Ok(await _manager.GetById(Id));
		}
		[HttpPost("Add")]
		public async Task<IActionResult> Add(dto.LookupSet.DTOLookUpAdd dto)
		{
			return Ok(await _manager.Add(dto));
		}
		[HttpPost("Update")]
		public async Task<IActionResult> Update(dto.LookupSet.DTOLookUpAdd dto)
		{
			return Ok(await _manager.Update(dto));
		}
		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(string Id)
		{
			return Ok(await _manager.Delete(Id));
		}
		[HttpPost("GetAllFuncational")]
		public async Task<IActionResult> GetAllFunctional(wap.PagingRequest param)
		{
			return Ok(await _manager.GetAllFunctional(param));
		}
		#endregion

		#region LookUp-Term
		[HttpPost("GetLookupsByClassName")]
		public async Task<IActionResult> GetLookupsByClassName(string ClassName)
		{
			return Ok(await _manager.GetLookupsByClassName(ClassName));
		}
		[HttpPost("GetLookupsBySetId")]
		public async Task<IActionResult> GetLookupsBySetId(string Id)
		{
			return Ok(await _manager.GetLookupsBySetId(Id));
		}
		[HttpPost("AddLookUp")]
		public async Task<IActionResult> AddLookUp(dto.LookupSetTerm.DTOLookupSetTermAction lookUp)
		{
			return Ok(await _manager.AddLookUp(lookUp));
		}
		[HttpPost("UpdateLookUp")]
		public async Task<IActionResult> UpdateLookUp(dto.LookupSetTerm.DTOLookupSetTermAction lookUp)
		{
			return Ok(await _manager.UpdateLookUp(lookUp));
		}
		[HttpPost("DeleteLookUp")]
		public async Task<IActionResult> DeleteLookUp(string Id)
		{
			return Ok(await _manager.DeleteLookUp(Id));
		}
		[HttpPost("GetAllLookUpsTermFunctional")]
		public async Task<IActionResult> GetAllLookUpsTermFunctional(wap.PagingRequest param)
		{
			return Ok(await _manager.GetAllLookUpsTermFunctional(param));
		}
		#endregion

		[HttpPost("GetSourceFormsLookup")]
		public async Task<IActionResult> GetSourceFormsLookup()
		{
			return Ok(await _manager.GetSourceFormsLookup());
		}
		[HttpPost("GetRelatedItemsSetupLookup")]
		public async Task<IActionResult> GetRelatedItemsSetupLookup()
		{
			return Ok(await _manager.GetRelatedItemsSetupLookup());
		}
	}
}
