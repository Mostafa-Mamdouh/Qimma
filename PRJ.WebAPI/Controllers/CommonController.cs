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
	[ApiExplorerSettings(GroupName = "CommonAPI")]
	public class CommonController : ControllerBase
	{
		private readonly bus.BasCountries.CommonManager _manager;

		public CommonController(bus.BasCountries.CommonManager manager)
		{
			_manager = manager;
		}
		#region Counries
		[HttpPost("GetCountries")]
		public async Task<IActionResult> GetCountries()
		{
			return Ok(await _manager.GetCountries());
		}
		[HttpPost("GetNationalites")]
		public async Task<IActionResult> GetNationalites()
		{
			return Ok(await _manager.GetNationalites());
		}
		[HttpPost("GetCountryById")]
		public async Task<IActionResult> GetCountryById(string Id)
		{
			return Ok(await _manager.GetCountryById(Id));
		}
		[HttpPost("GetNationalityById")]
		public async Task<IActionResult> GetNationalityById(string Id)
		{
			return Ok(await _manager.GetNationalityById(Id));
		}
		[HttpPost("GetCountryByCode")]
		public async Task<IActionResult> GetCountryByCode(string Code)
		{
			return Ok(await _manager.GetCountryByCode(Code));
		}
		[HttpPost("GetNationalityByCode")]
		public async Task<IActionResult> GetNationalityByCode(string Code)
		{
			return Ok(await _manager.GetNationalityByCode(Code));
		}
		#endregion

		#region Cites
		[HttpPost("GetCitesByCountryCode")]
		public async Task<IActionResult> GetCitesByCountryCode(string Code)
		{
			return Ok(await _manager.GetCitesByCountryCode(Code));
		}
		[HttpPost("GetCityById")]
		public async Task<IActionResult> GetCityById(string Id)
		{
			return Ok(await _manager.GetCityById(Id));
		}
        #endregion
        #region TransactionTypes
        //[HttpPost("GetAllTransactionTypes")]
        //public async Task<IActionResult> GetAllTransactionTypes()
        //{
        //    return Ok(await _manager.GetAllTransactionTypes());
        //}
        #endregion
    }
}
