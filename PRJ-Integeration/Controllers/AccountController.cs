using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PRJ.Application.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace AmanAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AccountController : BaseApiController
    {
        private readonly IConfiguration _config;


        public AccountController( IConfiguration config)
        {
         
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
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                access_token = tokenHandler.WriteToken(token),
                userFromRepo.Username,
                exp = token.ValidTo
            });
        }
        }

    }
