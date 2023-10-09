using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PRJ.Application.DTOs;
using PRJ.Application.Warppers;
using PRJ_internal_app.Coomon;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using wap = PRJ.Application.Warppers;


namespace PRJ_internal_app.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly bus.BasCountries.CommonManager _manager;
        private readonly Helper _helper;

        public CommonController(bus.BasCountries.CommonManager manager, Helper helper)
        {
            _manager = manager;
            _helper = helper;
        }
        [HttpGet]
        [Route("GetCureentUser")]
        public IActionResult GetCureentUser()
        {
            return Ok(_helper.GetCurrentUser());
        }

        #region Countries
        [HttpPost]
        [Route("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _manager.GetCountries());
        }
        [HttpPost]
        [Route("GetCountriesClear")]
        public async Task<IActionResult> GetCountriesClear()
        {
            return Ok(await _manager.GetCountriesClear());
        }
        [HttpPost]
        [Route("GetCountryByIdClear")]
        public async Task<IActionResult> GetCountryByIdClear(int Id)
        {
            return Ok(await _manager.GetCountryByIdClear(Id));
        }
        [HttpPost]
        [Route("GetCountryByCode")]
        public async Task<IActionResult> GetCountryByCode(string CountryCode)
        {
            return Ok(await _manager.GetCountryByCode(CountryCode));
        }
        [HttpPost]
        [Route("GetCountryById")]
        public async Task<IActionResult> GetCountryById(string Id)
        {
            return Ok(await _manager.GetCountryById(Id));
        }
        [HttpPost]
        [Route("AddCountries")]
        public async Task<IActionResult> AddCountries(dto.BasCountries.DTOCountries2 Body)
        {
            return Ok(await _manager.AddCountries(Body));
        }
        [HttpPost]
        [Route("UpdateCountries")]
        public async Task<IActionResult> UpdateCountries(dto.BasCountries.DTOCountries2 Body)
        {
            return Ok(await _manager.UpdateCountries(Body));
        }
        [HttpPost]
        [Route("DeleteCountries")]
        public async Task<IActionResult> DeleteCountries(string id)
        {
            return Ok(await _manager.DeleteCountries(id));
        }

        // countries
        [HttpPost("GetAllFuncational")]
        public async Task<IActionResult> GetAllFunctional(wap.PagingRequest body)
        {
            return Ok(await _manager.GetAllFunctional(body));
        }
        //cities
        [HttpPost("GetAllCitiesFuncational")]
        public async Task<IActionResult> GetAllCitiesFunctional(wap.PagingRequest body)
        {
            return Ok(await _manager.GetAllCitiesFunctional(body));
        }
        #endregion

        #region Cites
        [HttpPost]
        [Route("GetCitesByCountryId")]
        public async Task<IActionResult> GetCitesByCountryId(string Id)
        {
            return Ok(await _manager.GetCitesByCountryId(Id));
        }
        [HttpPost]
        [Route("GetCitesByCountryIdClear")]
        public async Task<IActionResult> GetCitesByCountryIdClear(int Id)
        {
            return Ok(await _manager.GetCitesByCountryIdClear(Id));
        }
        [HttpPost]
        [Route("GetCitesByCountryCode")]
        public async Task<IActionResult> GetCitesByCountryCode(string Code)
        {
            return Ok(await _manager.GetCitesByCountryCode(Code));
        }
        [HttpPost]
        [Route("AddCity")]
        public async Task<IActionResult> AddCity(dto.BasCites.DTOCites Body)
        {
            return Ok(await _manager.AddCity(Body));
        }
        [HttpPost]
        [Route("UpdateCity")]
        public async Task<IActionResult> UpdateCity(dto.BasCites.DTOCites Body)
        {
            return Ok(await _manager.UpdateCity(Body));
        }
        [HttpPost]
        [Route("DeleteCity")]
        public async Task<IActionResult> DeleteCity(string id)
        {
            return Ok(await _manager.DeleteCity(id));
        }
        #endregion

        #region Action Center
        [HttpPost]
        [Route("GetTransactionTypes")]
        public async Task<IActionResult> GetTransactionTypes()
        {

            return Ok(await _manager.GetAllTransactionTypes());
        }
        [HttpPost]
        [Route("GetItemSourceStatus")]
        public async Task<IActionResult> GetItemSourceStatus()
        {

            return Ok(await _manager.GetAllItemSourceStatus());
        }
        #endregion

    }
}
