using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
	public class InternalFieldPermission : AuditableBasedEntity
	{

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int FieldPermissionId { get; set; }
        public int AttributeId { get; set; }

        #region Navigation Properties
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual InternalRole InternalRole { get; set; }
        public int FieldId { get; set; }
        [ForeignKey("FieldId")]
        public virtual InternalScreenField InternalScreenField { get; set; }
        #endregion
    }
}
