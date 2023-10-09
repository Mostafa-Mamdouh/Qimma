using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.ProfileOfUser
{
	public class BasicInfoUsers : BasedDtoEntity
    {
        public long EmployeeNo { get; set; }
        public string? Username { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmailId { get; set; }
        public string? Department { get; set; }
        public string? JobTitle { get; set; }
        public string? WorkPhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? Office { get; set; }
    }
}
