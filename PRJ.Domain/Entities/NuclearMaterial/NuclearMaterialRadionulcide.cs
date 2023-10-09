using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities.NuclearMaterial
{
    public class NuclearMaterialRadionulcide : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NuclearMaterialRadionulcideId { get; set; }

        public float? Enrichment { get; set; }

        public string Element { get; set; }
        #region Navigation Properties
        public int RadionulcideId { get; set; }

        [ForeignKey("RadionulcideId")]
        public virtual Radionuclides Radionuclides { get; set; }
        public int NuclearMaterialElementId { get; set; }

        [ForeignKey("NuclearMaterialElementId")]
        public virtual NuclearMaterialElement NuclearMaterialElement { get; set; }
        #endregion
    }
}
