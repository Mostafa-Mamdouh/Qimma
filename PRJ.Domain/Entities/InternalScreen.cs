using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
	public class InternalScreen : AuditableBasedEntity
	{
        public InternalScreen()
        {
            this.InternalScreenFields = new HashSet<InternalScreenField>();
            this.InternalScreenRoles = new HashSet<InternalScreenRole>();
        }

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ScreenId { get; set; }
		[MaxLength(80)]
		public string ScreenNameAr { get; set; }
		[MaxLength(80)]
		public string ScreenNameEn { get; set; }
		[MaxLength(80)]
		public string ScreenCode { get; set; }

        #region Navigation Properties
        public virtual ICollection<InternalScreenField> InternalScreenFields { get; set; }
        public virtual ICollection<InternalScreenRole> InternalScreenRoles { get; set; }
        #endregion

    }
}
