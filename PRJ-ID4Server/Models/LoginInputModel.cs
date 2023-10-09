using System.ComponentModel.DataAnnotations;

namespace PRJ_ID4Server.Models
{
	public class LoginInputModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public bool RememberLogin { get; set; }
		public string ReturnUrl { get; set; }
	}
}
