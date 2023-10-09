using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTORelatedItem : BasedDtoEntity
    {
        public string RelatedItemCode { get; set; }
        public string ManufacturerSerialNom { get; set; }
        public string Description { get; set; }
        public bool IsTechnologyAndSoftware { get; set; }
        public List<DTORelatedItemsFiles> RelatedItemFiles { get; set; }
        public string EntityId { get; set; }
        public string FacilityId { get; set; }
        public string LicenseId { get; set; }
        public string PermitdetailsId { get; set; }
        public string StatusID { get; set; }
        public string ManufacturerId { get; set; }
        public string ManufacturerCountryId { get; set; }
        public string HierarchyStructureCode { get; set; }
        public bool NoManufacturerCertificateFlag { get; set; }
        public bool NoCustomImportPermitFlag { get; set; }
        public bool NoShipperImportPermitFlag { get; set; }
        public bool NoCharacterizationCertificateFlag { get; set; }
        public bool NoSourceTagImageFlag { get; set; }
    }
}
