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
	public class PractiseProfile : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PractiseId { get; set; }
		[MaxLength(80)]
		public string PractiseNameAr { get; set; }
		[MaxLength(80)]
		public string PractiseNameEn { get; set; }

		[Display(Name = "PermitDetailsProfile")]
		public virtual int? PermitDetailsId { get; set; }

		[ForeignKey("PermitDetailsId")]
		public virtual PermitDetailsProfile PermitDetailsProfile { get; set; }

		public DateTime? AmanInsertedOn { get; set; }
		public int? AmmanId { get; set; }
        public int LicenseId{ get; set; }

    }
}
