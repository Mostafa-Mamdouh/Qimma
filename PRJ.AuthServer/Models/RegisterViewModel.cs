using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PRJ.AuthServer.Models
{
	public class RegisterViewModel
	{
		[Required]
		public string IdNo { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string MobileNo { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password",
			ErrorMessage = "Password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}
