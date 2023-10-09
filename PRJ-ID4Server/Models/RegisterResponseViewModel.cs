using ent = PRJ.Domain.Entities;
namespace PRJ_ID4Server.Models
{
	public class RegisterResponseViewModel
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }

		public RegisterResponseViewModel(ent.ExternalMaserUser user)
		{
			Id = user.Id;
			UserName = user.UserName;
			Email = user.Email;
		}
	}
}
