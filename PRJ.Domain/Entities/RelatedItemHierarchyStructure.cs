using Microsoft.EntityFrameworkCore;
using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace PRJ.Domain.Entities
{
    [Index(nameof(RelatedItemsHierarchyStructure.ItemNo), IsUnique = true)]
    public class RelatedItemsHierarchyStructure : AuditableBasedEntity
    {
        public RelatedItemsHierarchyStructure()
        {
            this.RelatedItems = new HashSet<RelatedItem>();
        }

        [Key]
        [MaxLength(256)]
        public string ItemStructureCode { get; set; }
        [MaxLength(1000)]
        public string ItemStructureDesFrn { get; set; }
        [MaxLength(1000)]
        public string ItemStructureDesNtv { get; set; }
        public int StructureLevelNum { get; set; }
        public int StructureGeneralDetailFlag { get; set; }
        [MaxLength(256)]
        public string ParentStructure { get; set; }
        [MaxLength(256)]
        public string ItemStructureLongDes { get; set; }
        [MaxLength(256)]
        public string ItemNo { get; set; }
        [MaxLength(256)]
        public string HSCode { get; set; }
        public ICollection<RelatedItem> RelatedItems { get; set; }
    }
}
