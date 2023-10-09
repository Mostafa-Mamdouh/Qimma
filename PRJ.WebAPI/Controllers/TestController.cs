using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;

namespace PRJ.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly bus.ProfileOfUsers.ProfileOfUserManager? _manager;

		public TestController(bus.ProfileOfUsers.ProfileOfUserManager? manager)
		{
			_manager = manager;
		}


        
        //[AllowAnonymous]
        //[HttpPost("GetItem")]
        //public async Task<IActionResult> GetItem(dto.GetByPost? Id)
        //{

        //	if (_manager is not null)
        //	{
        //		var _data = await _manager.GetItem(Id);

        //		if (_data != null)
        //		{
        //			return Ok(_data);
        //		}
        //		else
        //		{
        //			return NotFound();
        //		}
        //	}
        //	else
        //	{
        //		return NotFound();
        //	}
        //}

        //[HttpGet("GetAllAsync")]
        //public async Task<IActionResult> GetAllAsync()
        //{
        //	if (_manager is not null)
        //	{
        //		var _data = await _manager.GetAllAsync();

        //		if (_data != null)
        //		{
        //			return Ok(_data);
        //		}
        //		else
        //		{
        //			return NotFound();
        //		}
        //	}
        //	return NotFound();
        //}

        //[HttpPost("CountAsync")]
        //public async Task<IActionResult> CountAsync()
        //{
        //	if (_manager is not null)
        //	{
        //		var _data = await _manager.CountAsync();

        //		if (_data != null)
        //		{
        //			return Ok(_data);
        //		}
        //		else
        //		{
        //			return NotFound();
        //		}
        //	}
        //	return NotFound();
        //}
    }
}
