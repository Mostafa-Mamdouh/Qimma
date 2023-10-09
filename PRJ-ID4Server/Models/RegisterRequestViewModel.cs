using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PRJ_ID4Server.Models
{
	public class RegisterRequestViewModel
	{

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
