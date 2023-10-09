using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
	public class ListOfValue : AuditableBasedEntity
	{
        public ListOfValue()
        {
            this.InternalScreenFields = new HashSet<InternalScreenField>();
        }

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LovId { get; set; }
		[MaxLength(80)]
		public string LovNameAr { get; set; }
		[MaxLength(80)]
		public string LovNameEn { get; set; }
		[MaxLength(80)]
		public string LovCode { get; set; }
        [MaxLength(2000)]
        public string SqlStatement { get; set; }

        #region Navigation Properties
        public virtual ICollection<InternalScreenField> InternalScreenFields { get; set; }
        #endregion

    }
}
