using Newtonsoft.Json;

namespace PRJ.Application.DTOs.ItemHierarchyStructure
{
    public class ItemHierarchyStructureTreeNode
    {
        public Data Data { get; set; }
        public List<ItemHierarchyStructureTreeNode> Children { get; set; }

    }

    public class Data
    {
        public string ItemStructureCode { get; set; }
        public string ItemStructureDesFrn { get; set; }
        public string ItemStructureDesNtv { get; set; }
        public bool StructureGeneralDetailFlag { get; set; }
        public int StructureLevelNum { get; set; }
        public string ParentStructure { get; set; }
        public string DefaultIssueAccountCode { get; set; }
    }

    public class ParentMenuCode
    {
        [JsonProperty("parents")]
        public List<ParentMenuCode> Parents { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
