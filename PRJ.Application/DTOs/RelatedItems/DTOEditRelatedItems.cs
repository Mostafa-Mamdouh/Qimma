namespace PRJ.Application.DTOs.RelatedItems
{
    public class DTOEditRelatedItems
    {
        public string ItemStructureCode { get; set; }
        public string ItemStructureDesFrn { get; set; }
        public string ItemStructureDesNtv { get; set; }
        public int StructureLevelNum { get; set; }
        public int StructureGeneralDetailFlag { get; set; }
        public string ParentStructure { get; set; }
        public string ItemStructureLongDes { get; set; }
    }
}
