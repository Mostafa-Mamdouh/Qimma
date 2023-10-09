using API.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PRJ.Application.DTOs;
using PRJ.Application.DTOs.NafathUser;
using PRJ_internal_app.Coomon;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using bus = PRJ.Business;
using wap = PRJ.Application.Warppers;
namespace PRJ.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "CommonAPI")]
    public class NafathController : ControllerBase
    {
        private readonly bus.BasCountries.CommonManager _manager;
        private readonly IConfiguration _config;
        string userData;


        public NafathController(bus.BasCountries.CommonManager manager, IConfiguration config)
        {
            _manager = manager;
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
        public async Task<IActionResult> LoginAsync(string authCode)
        {
            NafathUser Nafathuser =new NafathUser();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent("application/json");
                    try
                    {

                        string endpoint = "http://cas.nrrc.gov.sa/IAM/GetClaims/" + authCode;

                        using (var Response = await client.GetAsync(endpoint))
                        {
                            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                if(Response.Content.ReadAsStringAsync().Result.IsNullOrEmpty())
                                {

                                }
                                userData = Response.Content.ReadAsStringAsync().Result;
                            }
                            else
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
    
                JObject json = JObject.Parse(userData);

                Nafathuser.Userid = ComputeSHA512(json.Root["userid"].ToString());
                Nafathuser.EnglishFirstName = ComputeSHA512(json.Root["englishName"].ToString());
                Nafathuser.ArabicFirstName = ComputeSHA512(json.Root["arabicName"].ToString());
                Nafathuser.Gender = ComputeSHA512(json.Root["gender"].ToString());

                if (userData == null)
                    return Unauthorized(new ApiResponse(401));

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Nafathuser.Userid),
                new Claim(ClaimTypes.Name, Nafathuser.EnglishFirstName),
                new Claim(ClaimTypes.Surname, Nafathuser.ArabicFirstName),
                new Claim(ClaimTypes.Gender, Nafathuser.Gender),

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

                var userDetails = new DTONafathResponse
                {
                    Token = tokenHandler.WriteToken(token),
                    

                };

                return Ok(new wap.Response<DTONafathResponse>(userDetails));
            }
            catch (Exception exp)
            {
                List<string> _error = new List<string>() { this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name, exp.Message };

                return Ok(new wap.Response<DTONafathResponse>(string.Join("=>", _error))
                {
                    Succeeded = false,
                    Errors = _error
                });
            }

        }
    }
}
