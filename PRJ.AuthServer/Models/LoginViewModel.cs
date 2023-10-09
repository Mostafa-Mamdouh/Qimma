using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PRJ.AuthServer.Models
{
	public class LoginViewModel
	{
		[Required]
		public string EmailOrId { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember me")]
		public bool RememberMe { get; set; }
	}
}
