using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
	public class BasCities : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CityId { get; set; }
		[Key]
		public int CountryId { get; set; }
		[MaxLength(80)]
		[Required]
		public string NameAr { get; set; }
		[MaxLength(80)]
		[Required]
		public string NameEn { get; set; }
		[MaxLength(7)]
		public string CityAbbrCode { get; set; }
	}
}
