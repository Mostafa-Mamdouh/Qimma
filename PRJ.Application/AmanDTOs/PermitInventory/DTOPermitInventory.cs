

namespace PRJ.Application.AmanDTOs.LicenseInventory
{
    public class DTOPermitInventory
    {
        public List<DTOPermitInventorySealed> SealedSources { get; set; }
        public List<DTOPermitInventoryUnSealed> UnSealedSources { get; set; }
        public List<DTOPermitInventoryNuclearMaterial> NuclearSources { get; set; }
        public List<DTOPermitInventoryGenerator> Generators { get; set; }
    }
}
