using PRJ.Domain.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
	public class InternalScreenRole : AuditableBasedEntity
	{

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ScreenRoleId { get; set; }
        public int ScreenOrder { get; set; }
        [DefaultValue(false)]
        public bool Insert { get; set; }
        [DefaultValue(false)]
        public bool Update { get; set; }
        [DefaultValue(false)]
        public bool Delete { get; set; }
        [DefaultValue(false)]
        public bool Query { get; set; }

        #region Navigation Properties
        public int ScreenId { get; set; }
        [ForeignKey("ScreenId")]
        public virtual InternalScreen InternalScreen { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual InternalRole Role { get; set; }
        #endregion
    }
}

