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
	public class FacilityProfile : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int FacilityId { get; set; }
		[MaxLength(80)]
		public string FacilityNameAr { get; set; }
		[MaxLength(80)]
		public string FacilityNameEn { get; set; }
        [MaxLength(80)]
        public string NationalAddress { get; set; }
        [MaxLength(80)]
		public string FacilityCode { get; set; }
		[MaxLength(100)]
		public string Location { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        public int? Province { get; set; }
        [ForeignKey("Province")]
        public virtual LookupSetTerm ProvinceLookup { get; set; }

        [Display(Name = "EntityProfile")]
		public virtual int? EntityId { get; set; }

		[ForeignKey("EntityId")]
		public virtual EntityProfile EntityProfile { get; set; }


        #region Aman
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public string AmanId { get; set; }

        public string AmanFacilityId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion

    }
}
