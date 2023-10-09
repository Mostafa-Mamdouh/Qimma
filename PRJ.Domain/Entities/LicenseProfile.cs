using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
	public class LicenseProfile:AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LicenseId { get; set; }
		[MaxLength(80)]
		public string LicenseDescAr { get; set; }
		[MaxLength(80)]
		public string LicenseDescEn { get; set; }
		[MaxLength(30)]
		public string LicenseCode { get; set; }
		[MaxLength(20)]
		public string LicenseVersionNumber { get; set; }
		public DateTime EffectiveDate { get; set; }
		public DateTime ExpirationDate { get; set; }

		[MaxLength(4000)]
		public string LicenseActivities { get; set; }


		public virtual ICollection<LicenseProfilePractice> LicensePractices { get; set; }

        public int? PractiseSector { get; set; }
        [ForeignKey("PractiseSector")]
        public virtual LookupSetTerm PractiseSectorLookup { get; set; }

        [Display(Name = "EntityProfile")]
		public virtual int? EntityId { get; set; }

		[ForeignKey("EntityId")]
		public virtual EntityProfile EntityProfile { get; set; }

		[Display(Name = "FacilityProfile")]
		public virtual int? FacilityId { get; set; }

		[ForeignKey("FacilityId")]
		public virtual FacilityProfile FacilityProfile { get; set; }



        #region Aman

        public string AmanId { get; set; }

        public string AmanEntityId { get; set; }

        public string AmanFacilityId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion
	}
}
