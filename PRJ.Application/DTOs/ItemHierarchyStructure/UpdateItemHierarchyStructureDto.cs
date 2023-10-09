namespace PRJ.Application.DTOs.ItemHierarchyStructure
{
    public class UpdateItemHierarchyStructureDto
    {
        public string ItemStructureDesFrn { get; set; }
        public string ItemStructureDesNtv { get; set; }
        public bool StructureGeneralDetailFlag { get; set; }
        public int StructureLevelNum { get; set; }
        public string ParentStructure { get; set; }
        public string DefaultIssueAccountCode { get; set; }
    }
}
