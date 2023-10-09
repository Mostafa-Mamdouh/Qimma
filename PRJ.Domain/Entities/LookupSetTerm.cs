using Microsoft.EntityFrameworkCore;
using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{

    public class LookupSetTerm : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LookupSetTermId { get; set; }
        [MaxLength(80)]
        public string Value { get; set; }
        [MaxLength(80)]
        public string DisplayNameAr { get; set; }
        [MaxLength(80)]
        public string DisplayNameEn { get; set; }
        [MaxLength(80)]
        public string ExtraData1 { get; set; }
        [MaxLength(80)]
        public string ExtraData2 { get; set; }
		public bool IsActive { get; set; }

		#region Navigation Properties
		public int LookupSetId { get; set; }
		[ForeignKey("LookupSetId")]
		public virtual LookupSet LookupSet { get; set; }

        #endregion


        #region Aman
        public int? AmanId { get; set; }
        #endregion

    }
}
