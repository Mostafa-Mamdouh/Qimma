using System.Threading.Tasks;
using rep = PRJ.Repository;
using ent = PRJ.Domain.Entities;
using lg = PRJ.GlobalComponents.Logger;
using cns = PRJ.GlobalComponents.Const;
using wap = PRJ.Application.Warppers;
using dto = PRJ.Application.DTOs;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using PRJ.GlobalComponents.Encryption;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Tracing;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using PRJ.Application.Warppers;

namespace PRJ.Business.ExternalMaserUser
{
	public class ExternalMaserUserManager 
	{
		private readonly rep.IUnitOfWork _manager;
		public readonly IMapper _mapper;
		private readonly UserManager<ent.ExternalMaserUser> _userManager;
		private readonly SignInManager<ent.ExternalMaserUser> _signInManager;
		private readonly IHttpContextAccessor _accessor;
		private readonly IConfiguration _configuration;

		public ExternalMaserUserManager(
			rep.IUnitOfWork manager, 
			IMapper mapper, 
			UserManager<ent.ExternalMaserUser> userManager, 
			SignInManager<ent.ExternalMaserUser> signInManager,
			 IHttpContextAccessor accessor,
			 IConfiguration configuration
			)
		{
			this._manager = manager;
			this._mapper = mapper;
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._accessor = accessor;
			this._configuration = configuration;
		}
		
		public async Task<wap.Response<bool>> Register(dto.User.DTORegister model)
		{
			
				var userExists = await _userManager.FindByEmailAsync(model.Email);
				if (userExists != null)
					return new Response<bool>() { Data = false, Message = "Exist" };

				ent.ExternalMaserUser user = new()
				{
					Email = model.Email,
					SecurityStamp = Guid.NewGuid().ToString(),
					UserName = model.Email
				};
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
					return new Response<bool>() { Data = true };
				else
					return new Response<bool>() { Data = false, Message = "Error" };
			
			
		}
		public async Task<wap.Response<bool>> LogIn(dto.DTOLogin model)
		{
			
				var user = await _userManager.FindByEmailAsync(model.Username);

				if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
				{
					var userRoles = await _userManager.GetRolesAsync(user);

					var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				};

					foreach (var userRole in userRoles)
					{
						authClaims.Add(new Claim(ClaimTypes.Role, userRole));
					}

					var token = GetToken(authClaims);

					//token = new JwtSecurityTokenHandler().WriteToken(token),
					//expiration = token.ValidTo

					
				}
				return new wap.Response<bool>()
				{
					Data = true
				};

			
		
		}
		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddHours(3),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
				);

			return token;
		}
	}
}
