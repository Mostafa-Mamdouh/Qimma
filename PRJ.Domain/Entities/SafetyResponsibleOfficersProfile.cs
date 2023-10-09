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
	public class SafetyResponsibleOfficersProfile : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SROId { get; set; }
		[MaxLength(80)]
		public string SRONameAr { get; set; }
		[MaxLength(80)]
		public string SRONameEn { get; set; }
		[MaxLength(20)]
		public string NationalID { get; set; }
		[MaxLength(20)]
		public string PhoneNo { get; set; }
		[MaxLength(20)]
		public string MobileNo { get; set; }
		[MaxLength(80)]
		public string EmailId { get; set; }
		[MaxLength(30)]
		public string CertificateNo { get; set; }
		public DateTime IssuanceDate { get; set; }
		public DateTime ExpiryDate { get; set; }

        #region Aman

        public string AmanId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion

    }
}
