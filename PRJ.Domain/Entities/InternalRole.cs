using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
	public class InternalRole : AuditableBasedEntity
	{
        public InternalRole()
        {
            this.InternalScreenRoles = new HashSet<InternalScreenRole>();
            this.InternalFieldPermissions = new HashSet<InternalFieldPermission>();

        }


        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int RoleId { get; set; }
		[MaxLength(80)]
		public string RoleNameAr { get; set; }
		[MaxLength(80)]
		public string RoleNameEn { get; set; }
		[MaxLength(80)]
		public string RoleCode { get; set; }

        #region Navigation Properties
        public virtual ICollection<InternalScreenRole> InternalScreenRoles { get; set; }
        public virtual ICollection<InternalFieldPermission> InternalFieldPermissions { get; set; }


        
        #endregion


    }
}
