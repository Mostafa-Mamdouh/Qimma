
using System.ComponentModel.DataAnnotations;

namespace PRJ.Application.AmanDTOs
{
    public class DTOLicenseInventory
    {
        public List<DTOLicenseInventorySealed> SealedSources { get; set; }
        public List<DTOLicenseInventoryUnSealed> UnSealedSources { get; set; }
        public List<DTOLicenseInventoryVUnSealed> VUnSealedSources { get; set; }
        public List<DTOLicenseInventoryGenerator> Generators { get; set; }
    }
}
