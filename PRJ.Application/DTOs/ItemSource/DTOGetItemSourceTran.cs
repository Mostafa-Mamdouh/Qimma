using PRJ.Application.DTOs.EntityProfile;
using PRJ.Application.DTOs.FacilityProfile;

namespace PRJ.Application.DTOs
{
    public class DTOGetItemSourceTran
    {
        public DTOGetItemSourceTran()
        {
            this.TransactionAttactcments = new HashSet<DTOGetTransactionAttachments>();
            this.ShieldAttachments = new HashSet<DTOGetTransactionAttachments>();
            this.TrnItemSourceRadionuclides = new HashSet<DTOGetTrnItemSourceRadionuclides>();
        }

        public int TrnSourceId { get; set; }
        public string? NrrcId { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public string FacilitySerialNo { get; set; }
        public Nullable<DateTime> InitialActivityDate { get; set; }
        public int? Quantity { get; set; }
        public string ShieldNuclearMaterialCode { get; set; }
        public string Purpose { get; set; }
        public int? NumberOfAcquiredSources { get; set; }
        public int? NumberOfConsumedSources { get; set; }
        public double? InitialMass { get; set; }
        public string EnrichmentOfUranium { get; set; }
        public double? SourceActivity { get; set; }
        public int? SourceActivityUnit { get; set; }
        public bool IsShieldDU { get; set; }
        public string ShieldPhysicalForm { get; set; }
        public string ShieldChemicalForm { get; set; }
        public int? SourceType { get; set; }
        public int? Status { get; set; }
        public int? PhysicalForm { get; set; }
        public int? AssociatedEquipment { get; set; }
        public int? TransactionTypeId { get; set; }
        public int? NuclearMaterial { get; set; }
        public int? InitialMassUnit { get; set; }
        public int? SourceCategory { get; set; }
        public string TransactionHeaderId { get; set; }

        public int? EntityId { get; set; }
        public DTOEntityProfile EntityProfile { get; set; }

        public int? FacilityId { get; set; }
        public DTOFacilityProfile FacilityProfile { get; set; }

        public int? LicenseId { get; set; }

        public int? LicenseInventoryId { get; set; }

        public int? PermitdetailsId { get; set; }

        public int? PermitInventoryId { get; set; }

        public int? PractiseId { get; set; }

        public int? SROId { get; set; }

        public int? LegalRepresentativesId { get; set; }

        public int? ManufacturerId { get; set; }

        public int? ManufacturerCountryId { get; set; }

        public string SourceId { get; set; }
        public string SourceModel { get; set; }
        public bool NoManufacturerCertificateFlag { get; set; }
        public bool NoCustomImportPermitFlag { get; set; }
        public bool NoShipperImportPermitFlag { get; set; }
        public bool NoImagesFlag { get; set; }
        public bool NoCharacterizationCertificateFlag { get; set; }
        public bool NoSourceTagImageFlag { get; set; }

        public virtual ICollection<DTOGetTransactionAttachments> TransactionAttactcments { get; set; }
        public virtual ICollection<DTOGetTransactionAttachments> ShieldAttachments { get; set; }
        public virtual ICollection<DTOGetTrnItemSourceRadionuclides> TrnItemSourceRadionuclides { get; set; }
    }
}
