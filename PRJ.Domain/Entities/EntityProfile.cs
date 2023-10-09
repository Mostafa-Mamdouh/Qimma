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
	public class EntityProfile : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EntityId { get; set; }
		[MaxLength(80)]
		public string EntityNameAr { get; set; }
		[MaxLength(80)]
		public string EntityNameEn { get; set; }
		[MaxLength(20)]
		public string PhoneNo { get; set; }
		[MaxLength(20)]
		public string MobileNo { get; set; }
		[MaxLength(80)]
		public string EmailId { get; set; }
		[MaxLength(20)]
		public string GovernmentID { get; set; }
		[MaxLength(20)]
        public int? EntityType { get; set; }
        [ForeignKey("EntityType")]
        public virtual LookupSetTerm EntityTypeLookup { get; set; }



        #region Aman
        public string AmanId { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

        #endregion
    }
}
