using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PRJ.Domain.Entities;
using bus = PRJ.Business;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;

namespace PRJ.WebAPI.Controllers
{

    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "UserInfoApi")]

    public class UserController : ControllerBase
    {
		private readonly bus.ExternalMaserUser.ExternalMaserUserManager _manager;
		private readonly UserManager<ent.ExternalMaserUser> _userManager;
		private readonly SignInManager<ent.ExternalMaserUser> _signInManager;
		public UserController(bus.ExternalMaserUser.ExternalMaserUserManager manager,

						UserManager<ent.ExternalMaserUser> userManager,
			SignInManager<ent.ExternalMaserUser> signInManager

			)
		{
			this._manager = manager;
			this._userManager = userManager;
			this._signInManager = signInManager;
		}

		[HttpPost("GetUserDetailsById")]
        public async Task<IActionResult> GetUserDetailsById(string Id)
        {

            var UserDetails = new dto.DTOGetUserDetails();
            UserDetails.Id = Id;
            UserDetails.FirstNameAr = "أحمد";
            UserDetails.FirstNameEn = "ahmad";
            UserDetails.SecondNameAr = "أنس";
            UserDetails.SecondNameEn = "anas";
            UserDetails.LastNameAr = "محمود";
            UserDetails.LastNameEn = "mahmoud";
            UserDetails.Email = "ahmad@gmail.com";
            UserDetails.Nationality = "Saudi";
            UserDetails.NationalID = "0122312331";
            UserDetails.PassportNo = "23333333454";
            UserDetails.GregorianBirthDate = DateTime.Now;

            return Ok(UserDetails);
        }
		[HttpPost("Register")]
		public async Task<IActionResult> Register(dto.User.DTORegister register)
		{
			return Ok(await _manager.Register(register));
		}
	


		[HttpPost("LogIn")]
		public async Task<IActionResult> LogIn(dto.DTOLogin login)
		{
			return Ok(await _manager.LogIn(login));
		}
		[HttpPost("LogOut")]
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();

			return Ok("LoggedOut");
		}
		[HttpPost("IsAauth")]
		public IActionResult IsAauth()
		{
			return Ok(User.Identity.IsAuthenticated);
		}
	}
}
