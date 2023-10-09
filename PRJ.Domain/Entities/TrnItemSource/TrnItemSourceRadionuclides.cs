using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
    public class TrnItemSourceRadionuclides : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrnRadionuclideId { get; set; }
        public float? InitialActivity { get; set; }
        public Nullable<DateTime> InitialActivityDate { get; set; }
        public int? InitialActivityUnit { get; set; }
        [ForeignKey("InitialActivityUnit")]
        public LookupSetTerm InitialActivityUnitLookup { get; set; }

        #region Navigation Properties
        public int? RadionulcideId { get; set; }

        [ForeignKey("RadionulcideId")]
        public virtual Radionuclides Radionuclides { get; set; }
        public int? TrnSourceID { get; set; }

        [ForeignKey("TrnSourceID")]
        public virtual TrnItemSource TrnItemSource { get; set; }

        #endregion

    }
}

