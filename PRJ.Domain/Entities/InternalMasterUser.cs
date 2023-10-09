using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PRJ.Domain.Common;

namespace PRJ.Domain.Entities
{
	public class InternalMasterUser : AuditableBasedEntity
	{
		public int Id { get; set; }
        public int EmployeeNo { get; set; }
		[MaxLength(80)]
		public string Username { get; set; }
		[MaxLength(80)]
		public string EmployeeName { get; set; }
		[MaxLength(80)]
		public string EmailId { get; set; }
		[MaxLength(80)]
		public string Department { get; set; }
		[MaxLength(80)]
		public string JobTitle { get; set; }
		[MaxLength(20)]
		public string WorkPhoneNo { get; set; }
		[MaxLength(80)]
		public string Office { get; set; }
		[MaxLength(20)]
		public string MobileNo { get; set; }
	}
}
