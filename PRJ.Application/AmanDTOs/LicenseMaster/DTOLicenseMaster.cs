using System.ComponentModel.DataAnnotations;

namespace PRJ.Application.AmanDTOs
{
    public class DTOLicenseMaster
    {
        [Required]
        public Guid LicenseMasterId { get; set; }
        [Required]
        public Guid EntityId { get; set; }
        [Required]
        public Guid FacilityId { get; set; }
        [Required]
        public string LicenseCode { get; set; }
        public string LicenseVersionNumber { get; set; }
        [Required]
        public DateTime EffectiveDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        public AmanLookupDto PractiseSector { get; set; }
        public List<AmanLookupDto> LicensePractices { get; set; }
        public DateTime? AmanInsertedOn { get; set; }

        public DTOLicenseInventory LicenseInventory { get; set; }

    }
}
