using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PRJ.Domain.Entities
{
    public class RelatedItem : AuditableBasedEntity
    {
        public RelatedItem()
        {
            this.RelatedItemFiles = new HashSet<RelatedItemFiles>();
        }

        [Key]
        [MaxLength(450)]
        public string RelatedItemCode { get; set; }
        [MaxLength(450)]
        public string ManufacturerSerialNom { get; set; }
        public bool IsTechnologyAndSoftware { get; set; }
        [MaxLength(450)]
        public string Description { get; set; }
        public int? EntityId { get; set; }
        [ForeignKey("EntityId")]
        public virtual EntityProfile EntityProfile { get; set; }
        public int? FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public virtual FacilityProfile FacilityProfile { get; set; }
        public int? LicenseId { get; set; }
        [ForeignKey("LicenseId")]
        public virtual LicenseProfile LicenseProfile { get; set; }
        public int? PermitdetailsId { get; set; }
        [ForeignKey("PermitdetailsId")]
        public virtual PermitDetailsProfile PermitDetailsProfile { get; set; }
        public virtual ICollection<RelatedItemFiles> RelatedItemFiles { get; set; }
        public int? StatusID { get; set; }
        [ForeignKey("StatusID")]
        public virtual LookupSetTerm StatusLookup { get; set; }
        public int? ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual LookupSetTerm ManufacturerLookup { get; set; }
        public int? ManufacturerCountryId { get; set; }
        [ForeignKey("ManufacturerCountryId")]
        public virtual LookupSetTerm ManufacturerCountryLookup { get; set; }
        public string HierarchyStructureCode { get; set; }
        [ForeignKey("HierarchyStructureCode")]
        public virtual RelatedItemsHierarchyStructure HierarchyStructure { get; set; }
        public bool NoManufacturerCertificateFlag { get; set; }
        public bool NoCustomImportPermitFlag { get; set; }
        public bool NoShipperImportPermitFlag { get; set; }
        public bool NoCharacterizationCertificateFlag { get; set; }
        public bool NoSourceTagImageFlag { get; set; }
    }
}
