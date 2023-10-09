using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities.Billing
{
    public class ServiceItemProfile : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceItemId { get; set; }
        [MaxLength(200)]
        [Required]
        public string ItemDesFrn { get; set; }
        [MaxLength(200)]
        [Required]
        public string ItemDesNtv { get; set; }
        [MaxLength(200)]
        [Required]
        public string ItemStructureCode { get; set; }
        public ItemHierarchyStructure ItemStructure { get; set; }
        public decimal CurrentPrice { get; set; }
        public string IssueAccountCode { get; set; }
        public string ItemType { get; set; }
        public string ItemRefCode { get; set; }
        public bool ActiveFlag { get; set; }
        public decimal ItmQty { get; set; }
        public string AmanId { get; set; }
        public bool MultiStage { get; set; }
        [MaxLength(50)]
        public string LicenseTerm { get; set; }
    }
}
