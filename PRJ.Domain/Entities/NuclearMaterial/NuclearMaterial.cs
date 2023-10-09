using Microsoft.EntityFrameworkCore;
using PRJ.Domain.Common;
using PRJ.Domain.Entities.AmanIntegrationEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities.NuclearMaterial
{
    [Index(nameof(TrnItemSource.ManufacturerSerialNo), IsUnique = true)]
    public class NuclearMaterial : AuditableBasedEntity
    {
        public NuclearMaterial()
        {
            this.NuclearMaterialFiles = new HashSet<NuclearMaterialFiles>();
            this.NuclearMaterialElements = new HashSet<NuclearMaterialElement>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SourceId { get; set; }
        public string NrrcId { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public string FacilitySerialNo { get; set; }
        public string ChemicalForm { get; set; }
        public bool? IsShield { get; set; }
        public string SourceModel { get; set; }
        public int Quantity { get; set; }
        public bool NoManufacturerCertificateFlag { get; set; }
        public bool NoCustomImportPermitFlag { get; set; }
        public bool NoShipperImportPermitFlag { get; set; }
        public bool NoImagesFlag { get; set; }
        public bool NoCharacterizationCertificateFlag { get; set; }
        public bool NoSourceTagImageFlag { get; set; }

        #region Navigation Properties
        public int? QuantityUnitId { get; set; }
        [ForeignKey("QuantityUnitId")]
        public virtual LookupSetTerm QuantityUnit { get; set; }
        public int? Status { get; set; }
        [ForeignKey("Status")]
        public virtual LookupSetTerm StatusLookup { get; set; }

        public int? PhysicalForm { get; set; }
        [ForeignKey("PhysicalForm")]
        public virtual LookupSetTerm PhysicalFormLookup { get; set; }
        public int? EntityId { get; set; }
        [ForeignKey("EntityId")]
        public virtual EntityProfile EntityProfile { get; set; }
        public int? FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public virtual FacilityProfile FacilityProfile { get; set; }
        public int? LicenseId { get; set; }
        [ForeignKey("LicenseId")]
        public virtual LicenseProfile LicenseProfile { get; set; }
        public int? LicenseInventoryId { get; set; }
        [ForeignKey("LicenseInventoryId")]
        public virtual LicenseInventoryLimits LicenseInventoryLimits { get; set; }
        public int? PermitdetailsId { get; set; }
        [ForeignKey("PermitdetailsId")]
        public virtual PermitDetailsProfile PermitDetailsProfile { get; set; }
        public int? PermitInventoryId { get; set; }
        [ForeignKey("PermitInventoryId")]
        public virtual PermitInventoryLimits PermitInventoryLimits { get; set; }
        public int? PractiseId { get; set; }
        [ForeignKey("PractiseId")]
        public virtual PractiseProfile PractiseProfile { get; set; }
        public int? ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual LookupSetTerm ManufacturerLookup { get; set; }

        public int? ManufacturerCountryId { get; set; }
        [ForeignKey("ManufacturerCountryId")]
        public virtual LookupSetTerm ManufacturerCountryLookup { get; set; }

        public ItemSourceProfile ItemSourceProfile { get; set; }

        public virtual ICollection<NuclearMaterialFiles> NuclearMaterialFiles { get; set; }
        public virtual ICollection<NuclearMaterialElement> NuclearMaterialElements { get; set; }
        #endregion

    }
}
