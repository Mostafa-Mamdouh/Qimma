using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.InternalMasterUser
{
	public class DTOInternalMasterUserBasic : BasedDtoEntity
	{
		public int EmployeeNo { get; set; }
		public string Username { get; set; }
		public string EmployeeName { get; set; }
		public string EmailId { get; set; }
		public string Department { get; set; }
		public string JobTitle { get; set; }
		public string WorkPhoneNo { get; set; }
		public string Office { get; set; }
		public string MobileNo { get; set; }
	}
}
