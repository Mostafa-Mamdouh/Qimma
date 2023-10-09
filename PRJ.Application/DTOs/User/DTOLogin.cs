using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PRJ.Application.DTOs
{
	public class DTOLogin
	{
		[Required]
		public string Username { get; set; } 
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}


    public class DTOHashedLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
