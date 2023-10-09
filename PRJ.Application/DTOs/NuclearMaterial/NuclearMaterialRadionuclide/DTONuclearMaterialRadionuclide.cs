using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.NuclearMaterial.NuclearMaterialRadionuclide
{
    public class DTONuclearMaterialRadionuclide : BasedDtoEntity
    {
        public string NuclearMaterialRadionulcideId { get; set; }
        public float? Enrichment { get; set; }
        public string RadionulcideId { get; set; }
        public string MaterialId { get; set; }
        public string Element { get; set; }
        public string NuclearMaterialElementId { get; set; }

    }
}
