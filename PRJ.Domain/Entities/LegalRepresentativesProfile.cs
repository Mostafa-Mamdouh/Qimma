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
	public class LegalRepresentativesProfile : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LegalRepresentativesId { get; set; }
		[MaxLength(80)]
		public string LegalRepresentativesNameAr { get; set; }
		[MaxLength(80)]
		public string LegalRepresentativesNameEn { get; set; }
		[MaxLength(80)]
		public string Title { get; set; }
		[MaxLength(20)]
		public string NationalID { get; set; }
		[MaxLength(20)]
		public string PhoneNo { get; set; }
		[MaxLength(20)]
		public string MobileNo { get; set; }
		[MaxLength(80)]
		public string EmailId { get; set; }
		[MaxLength(30)]
		public string Status { get; set; }
		public int CurrentFacilities { get; set; }
		[MaxLength(4000)]
		public string Note { get; set; }
		public string UserIdentity { get; set; }
        public int? UserType { get; set; }
        [ForeignKey("UserType")]
        public virtual LookupSetTerm UserTypeLookup { get; set; }

        #region Aman

        public string AmanId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion
	}
}
