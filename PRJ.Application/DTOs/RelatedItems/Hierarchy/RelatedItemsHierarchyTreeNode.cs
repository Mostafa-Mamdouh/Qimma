namespace PRJ.Application.DTOs.RelatedItems.Hierarchy
{
    public class RelatedItemsHierarchyTreeNode
    {
        public Data Data { get; set; }
        public List<RelatedItemsHierarchyTreeNode> Children { get; set; }
    }

    public class Data
    {
        public string ItemStructureCode { get; set; }
        public string ItemStructureDesFrn { get; set; }
        public string ItemStructureDesNtv { get; set; }
        public int StructureLevelNum { get; set; }
        public int StructureGeneralDetailFlag { get; set; }
        public string ParentStructure { get; set; }
        public string ItemStructureLongDes { get; set; }
        public string ItemNo { get; set; }
        public string HSCode { get; set; }

        /* public string RelatedItemCode { get; set; }
         public string ManufacturerSerialNom { get; set; }
         public virtual string FacilityRelatedItemID { get; set; }
         public int? StatusID { get; set; }
         public int? ItemCategory { get; set; }
         public string Description { get; set; }
         public string Purpose { get; set; }
         public double? PermitinitialQty { get; set; }
         public DateTime? DateOfManufacturing { get; set; }
         public int? Manufacturer { get; set; }
         public string Model { get; set; }
         public string ItemTypeNumber { get; set; }
         public string ItemTypeName { get; set; }
         public string HSCode { get; set; }
         public bool? EndUserCertificate { get; set; }
         public bool? GovernmentCommitments { get; set; }
         public int? Attachments { get; set; }
         public virtual ICollection<RelatedItemFiles> RelatedItemFiles { get; set; }
         public int? StructureLevelNum { get; set; }
         public bool? GeneralDetailFlag { get; set; }
         public string ParentStructure { get; set; }
         public virtual ICollection<DTOLookupSetTerm> Status { get; set; }
         public virtual ICollection<DTOLookupSetTerm> ItemsCategory { get; set; }
         public virtual ICollection<DTOLookupSetTerm> Manufacturers { get; set; }*/

    }
    public class ParentMenuCode
    {
        public List<ParentMenuCode> Parents { get; set; }
        public string code { get; set; }
    }
}

