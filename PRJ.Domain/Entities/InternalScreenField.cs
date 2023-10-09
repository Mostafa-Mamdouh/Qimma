using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
	public class InternalScreenField : AuditableBasedEntity
	{
        public InternalScreenField()
        {
            this.InternalFieldPermissions = new HashSet<InternalFieldPermission>();
        }

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int FieldId { get; set; }
        [MaxLength(80)]
        public string FieldDescAr { get; set; }
        [MaxLength(80)]
        public string FieldDescEn { get; set; }
        public int FieldType { get; set; }
		[MaxLength(80)]
		public string FieldFormat { get; set; }

        #region Navigation Properties
        public int ScreenId { get; set; }
        [ForeignKey("ScreenId")]
        public virtual InternalScreen Screen { get; set; }
        public int? LookupSetId { get; set; }
        [ForeignKey("LookupSetId")]
        public virtual LookupSet LookupSet { get; set; }
        public int? LovId { get; set; }
        [ForeignKey("LovId")]
        public virtual ListOfValue ListOfValue { get; set; }
        public virtual ICollection<InternalFieldPermission> InternalFieldPermissions { get; set; }

        #endregion
    }
}
