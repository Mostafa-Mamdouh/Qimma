using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace PRJ.Domain.Entities.Billing
{
    public class ItemHierarchyStructure : AuditableBasedEntity
    {
        [Key]
        [MaxLength(120)]
        public string ItemStructureCode { get; set; }
        [MaxLength(120)]
        public string ItemStructureDesFrn { get; set; }
        [MaxLength(120)]
        public string ItemStructureDesNtv { get; set; }
        public bool StructureGeneralDetailFlag { get; set; }
        public int StructureLevelNum { get; set; }
        public string ParentStructure { get; set; }
        public string DefaultIssueAccountCode { get; set; }
        public List<ServiceItemProfile> serviceItemProfiles { get; set; }
    }
}
