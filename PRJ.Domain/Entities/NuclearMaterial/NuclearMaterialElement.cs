using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities.NuclearMaterial
{
    public class NuclearMaterialElement
    {
        public NuclearMaterialElement()
        {
            NuclearMaterialRadionulcides = new HashSet<NuclearMaterialRadionulcide>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double? ElementMass { get; set; }
        public int? NuclearMaterialType { get; set; }
        [ForeignKey("NuclearMaterialType")]
        public virtual LookupSetTerm NuclearMaterialTypeLookup { get; set; }
        public int? ElementMassUnit { get; set; }
        [ForeignKey("InitialMassUnit")]
        public virtual LookupSetTerm InitialMassLookup { get; set; }
        public virtual ICollection<NuclearMaterialRadionulcide> NuclearMaterialRadionulcides { get; set; }
        public int NuclearMaterialId { get; set; }

        [ForeignKey("NuclearMaterialId")]
        public virtual NuclearMaterial NuclearMaterial { get; set; }
    }
}
