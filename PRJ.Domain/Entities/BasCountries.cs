using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRJ.Domain.Common;

namespace PRJ.Domain.Entities
{
	public class BasCountries : AuditableBasedEntity
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CountryId { get; set; }
		[MaxLength(80)]
		[Required]
		public string CountryNameAr { get; set; }

		[MaxLength(80)]
		[Required]
		public string CountryNameEn { get; set; }

		[MaxLength(80)]
		[Required]
		public string NationalityNameFrn { get; set; }

		[MaxLength(80)]
		[Required]
		public string NationalityNameNtv { get; set; }

		[MaxLength(3)]
		[Required]
		public string CountryCodeISO { get; set; }
	}
}
