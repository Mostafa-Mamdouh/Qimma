using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
    public class ItemSourceRadionulcides : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemSourceRadionuclideId { get; set; }
        public Nullable<DateTime> InitialActivityDate { get; set; }

        #region Navigation Properties
        public float InitialActivity { get; set; }
        public int? InitialActivityUnit { get; set; }
        [ForeignKey("InitialActivityUnit")]
        public virtual LookupSetTerm InitialActivityUnitLookup { get; set; }
        public int RadionulcideId { get; set; }

        [ForeignKey("RadionulcideId")]
        public virtual Radionuclides Radionuclides { get; set; }
        public int SourceId { get; set; }

        [ForeignKey("SourceId")]
        public virtual ItemSourceProfile ItemSourceProfile { get; set; }

        #endregion
    }
}
