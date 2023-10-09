using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
    public class Radionuclides : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RadionuclideId { get; set; }
        [MaxLength(80)]
        public string Isotop { get; set; }
        [MaxLength(80)]
        public string DisplayName { get; set; }
        public float? HalfLife { get; set; }
        public int? HalfLifeUnitId { get; set; }
        [ForeignKey("HalfLifeUnitId")]
        public LookupSetTerm HalfLifeUnit { get; set; }
        public float? ExemptionValue { get; set; }
        public int? ExemptionValueUnitId { get; set; }
        [ForeignKey("ExemptionValueUnitId")]
        public LookupSetTerm ExemptionValueUnit { get; set; }
        public bool IsActive { get; set; }
    }
}
