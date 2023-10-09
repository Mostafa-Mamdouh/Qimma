using API.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PRJ.Application.DTOs;
using PRJ_internal_app.Coomon;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using bus = PRJ.Business;
using wap = PRJ.Application.Warppers;


namespace PRJ_internal_app.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly bus.BasCountries.CommonManager _manager;
        private readonly Helper _helper;
        private readonly IConfiguration _config;


        public AccountController(bus.BasCountries.CommonManager manager, Helper helper, IConfiguration config)
        {
            _manager = manager;
            _helper = helper;
            _config = config;

        }

        string ComputeSHA512(string s)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashValue = sha512.ComputeHash(Encoding.UTF8.GetBytes(s));
                return Convert.ToHexString(hashValue);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(DTOLogin userForLoginDto)
        {
           

              
                var users = _config.GetSection("Users").Get<List<DTOHashedLogin>>();

                userForLoginDto.Password = ComputeSHA512(userForLoginDto.Password);

                var userFromRepo = users.Where(u => u.Username == userForLoginDto.Username
                    && u.Password.ToLower() == userForLoginDto.Password.ToLower()).FirstOrDefault();

                if (userFromRepo == null)
                    return Unauthorized(new ApiResponse(401));

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Username.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),

            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(3),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var userDetails = new DTOGetUserDetails
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    Username = userFromRepo.Username,

                };
                return Ok(new wap.Response<DTOGetUserDetails>(userDetails));
          


        }

    }
}
