using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
	public class ManufacturerMaster : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

		public int ManufacturerId { get; set; }
		[MaxLength(80)]
		public string ManufacturerDescAr { get; set; }
		[MaxLength(80)]
		public string ManufacturerDescEn { get; set; }
		[MaxLength(20)]
		public string PhoneNo { get; set; }
		[MaxLength(20)]
		public string MobileNo { get; set; }
		[MaxLength(80)]
		public string EmailId { get; set; }
		[MaxLength(200)]
		public string Location { get; set; }
		[MaxLength(200)]
		public string AddressLine1 { get; set; }
		[MaxLength(200)]
		public string AddressLine2 { get; set; }
		[MaxLength(200)]
		public string AddressLine3 { get; set; }
		public int City { get; set; }
		[MaxLength(20)]
		public string ZipCode { get; set; }
		[MaxLength(20)]
		public string POBox { get; set; }
		[Display(Name = "BasCountries")]
		public virtual int? CountryId { get; set; }

		[ForeignKey("CountryId")]
		public virtual BasCountries BasCountries { get; set; }
	}
}