using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PRJ.Application.Warppers;
using PRJ.AuthServer.Models;
using PRJ.Domain.Entities;
using PRJ.GlobalComponents.Logger;

namespace PRJ.AuthServer.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<ExternalMaserUser> signInManager;
		private readonly UserManager<ExternalMaserUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ILogger<AccountController> logger;
		private readonly Common common;
		public AccountController(SignInManager<ExternalMaserUser> signInManager, UserManager<ExternalMaserUser> userManager, ILogger<AccountController> logger, RoleManager<IdentityRole> roleManager, Common common)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.logger = logger;
			this.roleManager = roleManager;
			this.common = common;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login(string ReturnUrl)
		{
			
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.EmailOrId);

				Int64 num;
				
				if (Int64.TryParse(model.EmailOrId, out num))
				{
					user = await userManager.FindByNameAsync(model.EmailOrId);
				}

				if (user != null && !user.EmailConfirmed &&
							(await userManager.CheckPasswordAsync(user, model.Password)))
				{
					//ModelState.AddModelError(string.Empty, "Your account not verified yet, please check your email to activate");

					var token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
					var confirmationEmail = Url.Action("Confirmation", "Account", new { userId = user.Id, token = token }, Request.Scheme);

					LoggerManager.Log(confirmationEmail);

					//ViewBag.HaveError = true;
					//return View(model);

					ViewData["NextAction"] = "Activation";

					return View(model);
				}

				var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, true);

				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, isPersistent: false);
					//&& Url.IsLocalUrl(returnUrl))

					if (!string.IsNullOrEmpty(ReturnUrl))
					{
						return Redirect(ReturnUrl);
					}
					else
					{
						return RedirectToAction("Main", "Account");
					}
				}
				else if (result.IsLockedOut)
				{
					ViewBag.HaveError = true;
					
					ModelState.AddModelError("", "The account is locked out, after invalid login for 5 times, please reset your password or wait for 15 min");
					return View();
				}
				else if (result.RequiresTwoFactor)
				{
					var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

					LoggerManager.Log(token);

					ViewData["NextAction"] = "OTP";
					string _mobile = user.PhoneNumber.Substring(user.PhoneNumber.Length - 3);
					ViewData["MobileNo"] = "*******" + _mobile;
					return View(model);

				}
				ViewBag.HaveError = true;
				ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
			}

			return View(model);
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<Response<string>>> Verification([FromBody] VerificationViewModel twoFactor)
		{
			if(string.IsNullOrEmpty(twoFactor.Id1) 
				|| string.IsNullOrEmpty(twoFactor.Id2) 
				|| string.IsNullOrEmpty(twoFactor.Id3) 
				|| string.IsNullOrEmpty(twoFactor.Id4) 
				|| string.IsNullOrEmpty(twoFactor.Id5) 
				|| string.IsNullOrEmpty(twoFactor.Id6))
			{
				return new Response<string>()
				{
					Succeeded = false,
					Message = "Please fill verification"
				};
			}

			string code = twoFactor.Id1 + twoFactor.Id2 + twoFactor.Id3 + twoFactor.Id4 + twoFactor.Id5 + twoFactor.Id6;

			var result = await signInManager.TwoFactorSignInAsync("Email", code, false, false);

			if (result.Succeeded)
			{
				//return RedirectToAction("Main", "Account");

				return new Response<string>()
				{
					Succeeded = true,
					Data = common.GetAuthURL()
				};
			}
			else
			{
				return new Response<string>()
				{
					Succeeded = false,
					Message = "Invalid verification code"
				};
			}
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<Response<string>>> ForgetPassword([FromBody] ForgetPasswordViewModel model)
		{
			if(string.IsNullOrEmpty(model.UserName))
			{
				return new Response<string>()
				{
					Succeeded = false,
					Message = "Please fill Email or Id number"
				};
			}
			var user = await userManager.FindByEmailAsync(model.UserName);

			Int64 num;

			if (Int64.TryParse(model.UserName, out num))
			{
				user = await userManager.FindByNameAsync(model.UserName);
			}

			if(user == null)
			{
				return new Response<string>()
				{
					Succeeded = false,
					Message = "Invalid Email / Id Number"
				};
			}

			if (await userManager.IsEmailConfirmedAsync(user))
			{
				var token = await userManager.GeneratePasswordResetTokenAsync(user);

				var _resetPasswordLink = Url.Action("ResetPassword", "Account", new { email = model.UserName, token = token }, Request.Scheme);

				LoggerManager.Log(_resetPasswordLink);

				return new Response<string>()
				{
					Succeeded = true,
					Data = "Reset",
					Message = "Please check your email to reset password"
				};
			}
			else
			{
				var token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
				var confirmationEmail = Url.Action("Confirmation", "Account", new { userId = user.Id, token = token }, Request.Scheme);

				LoggerManager.Log(confirmationEmail);

				return new Response<string>()
				{
					Succeeded = false,
					Message = "Your account not verified yet, please check your email to activate"
				};
			}
		}


		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				if(await userManager.FindByEmailAsync(model.Email) == null)
				{
					var user = new ExternalMaserUser
					{
						UserName = model.IdNo,
						Email = model.Email,
						TwoFactorEnabled = true,
						NationalID = model.IdNo,
						PhoneNumber = model.MobileNo
					};

					var results = await userManager.CreateAsync(user, model.Password);

					if (results.Succeeded)
					{
						var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
						var confirmationEmail = Url.Action("Confirmation", "Account", new { userId = user.Id, token = token }, Request.Scheme);

						LoggerManager.Log(confirmationEmail);

						//await signInManager.SignInAsync(user, isPersistent: false);
						string RoleId = await CheckRole();
						bool InsertRole = await AddReomveUsersInRole(new UserRoleViewModel { IsAddToRole = true, UserName = user.Email, UserId = user.Id }, RoleId);

						return RedirectToAction("RegisterSuccessfuly", "Account");
					}

					foreach (var error in results.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, string.Format("This email {0} is already taken", model.Email));
				}

			}

			return View(model);
		}
		[HttpGet]
		[AllowAnonymous]
		public IActionResult RegisterSuccessfuly()
		{
			return View();
		}
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Confirmation(string userId, string token)
		{
			if(userId == null || token == null)
			{
				return RedirectToAction("Index", "Home");
			}

			var user = await userManager.FindByIdAsync(userId);
			

			if (user == null)
			{
				ViewBag.ErrorMessage = "the user not found";
				return View("NotFound");
			}

			var results = await userManager.ConfirmEmailAsync(user, token);
			if(results.Succeeded)
			{
				return View();
			}

			ViewBag.ErrorTitle = "Connot be confirm";
			return View("Error");
		}
		[HttpGet]
		[AllowAnonymous]
		public IActionResult ResetPassword(string token, string	email)
		{
			if(token == null || email == null)
			{
				ModelState.AddModelError("", "Invalid Password Reset Information");
			}
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Find the user by email
				var user = await userManager.FindByEmailAsync(model.Email);

				if (user != null)
				{
					// reset the user password
					var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
					if (result.Succeeded)
					{
						return View("ResetPasswordConfirmation");
					}

					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
					ViewBag.HaveError = true;
					return View(model);
				}

				return View("ResetPasswordConfirmation");
			}
			// Display validation errors if model state is not valid
			return View(model);
		}
		[Authorize(Roles = "Facility")]
		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}
		[Authorize(Roles = "Facility")] 
		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.GetUserAsync(User);
				if (user == null)
				{
					return RedirectToAction("Login");
				}

				// ChangePasswordAsync changes the user password
				var result = await userManager.ChangePasswordAsync(user,
					model.CurrentPassword, model.NewPassword);

				// The new password did not meet the complexity rules or
				// the current password is incorrect. Add these errors to
				// the ModelState and rerender ChangePassword view
				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return View();
				}

				// Upon successfully changing the password refresh sign-in cookie
				await signInManager.RefreshSignInAsync(user);
				return View("ChangePasswordConfirmation");
			}

			return View(model);
		}
		[Authorize(Roles = "Facility")]
		[HttpGet]
		public IActionResult ChangePasswordConfirmation()
		{
			return View();
		}
		[Authorize (Roles = "Facility")]
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();

			return RedirectToAction("LogIn", "Account");
		}
		[Authorize(Roles = "Facility")]
		[HttpGet]
		public IActionResult Main()
		{
			return View();
		}

		#region Role Manager
		public async Task<string> CheckRole()
		{
			var _getFacilityRole = GetRoles().Where(_ => _.Name == "Facility").FirstOrDefault();

			if (_getFacilityRole != null)
			{
				IdentityRole identityRole = new IdentityRole { Name = "Facility" };
				IdentityResult result = await roleManager.CreateAsync(identityRole);
				if (result.Succeeded)
				{
					return GetRoles().Where(_ => _.Name == "Facility").FirstOrDefault().Id;
				}
			}
			return _getFacilityRole.Id;
		}
		public IEnumerable<IdentityRole> GetRoles()
		{
			return roleManager.Roles;
		}
		public async Task<bool> AddReomveUsersInRole(UserRoleViewModel model, string roleId)
		{
			var role = await roleManager.FindByIdAsync(roleId);

			if (role == null)
			{
				return false;
			}

			var user = await userManager.FindByIdAsync(model.UserId);

			IdentityResult result = null;

			if (model.IsAddToRole && !(await userManager.IsInRoleAsync(user, role.Name)))
			{
				result = await userManager.AddToRoleAsync(user, role.Name);
			}
			else if (!model.IsAddToRole && await userManager.IsInRoleAsync(user, role.Name))
			{
				result = await userManager.RemoveFromRoleAsync(user, role.Name);
			}

			if (result.Succeeded)
			{
				return true;

			}

			return false;
			
		}

		#endregion

		//public PartialViewResult OTP()
		//{
		//	return PartialView("~/Views/Account/Verification.cshtml", null);
		//}
		//[HttpGet]
		//[AllowAnonymous]
		//public IActionResult ForgetPassword()
		//{
		//	return View();
		//}
		//[HttpPost]
		//[AllowAnonymous]
		//public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var user = await userManager.FindByEmailAsync(model.Email);
		//		if (user != null && await userManager.IsEmailConfirmedAsync(user))
		//		{
		//			var token = await userManager.GeneratePasswordResetTokenAsync(user);

		//			var _resetPasswordLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

		//			LoggerManager.Log(_resetPasswordLink);

		//			return View("ForgetPasswordConfirmation");
		//		}
		//		return View("ForgetPasswordConfirmation");
		//	}
		//	return View(model);
		//}
	}
}
