using PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialRadionuclide;

namespace PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialElement
{
    public class DTONuclearMaterialElement
    {
        public string Id { get; set; } // Main id
        public double? ElementMass { get; set; }
        public string ElementMassUnit { get; set; }
        public string NuclearMaterialType { get; set; }
        public virtual ICollection<DTONuclearMaterialRadionuclide> NuclearMaterialRadionulcides { get; set; }
        public string NuclearMaterialId { get; set; }
        public string Element { get; set; }
    }
}
