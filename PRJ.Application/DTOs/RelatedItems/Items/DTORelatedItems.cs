using PRJ.Domain.Entities;

namespace PRJ.Application.DTOs.RelatedItems.Items
{
    public class DTORelatedItems
    {
        public DTORelatedItems()
        {
            this.RelatedItemFiles = new HashSet<RelatedItemFiles>();
        }

        public string RelatedItemCode { get; set; }
        public string ManufacturerSerialNom { get; set; }
        public virtual string FacilityRelatedItemID { get; set; }
        public bool IsTechnologyAndSoftware { get; set; }
        public string Description { get; set; }
        public string EntityId { get; set; }
        public string FacilityId { get; set; }
        public string LicenseId { get; set; }
        public string PermitdetailsId { get; set; }
        public virtual ICollection<RelatedItemFiles> RelatedItemFiles { get; set; }
        public string StatusID { get; set; }
        public string ManufacturerId { get; set; }
        public string ManufacturerCountryId { get; set; }
        public string HierarchyStructureCode { get; set; }
        public virtual RelatedItemsHierarchyStructure HierarchyStructure { get; set; }
    }
}
