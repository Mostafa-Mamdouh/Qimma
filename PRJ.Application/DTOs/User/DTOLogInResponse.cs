using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.User
{
	public class DTOLogInResponse
	{
		public string Token { get; set; }
		public DateTime LogInDate { get; set; }
	}
}
