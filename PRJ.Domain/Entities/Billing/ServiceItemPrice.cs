using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities.Billing
{
    public class ServiceItemPrice : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceItemPriceId { get; set; }
        public int ServiceItemId { get; set; }
        public ServiceItemProfile ServiceItem { get; set; }
        public int LineNum { get; set; }
        public decimal ItemPrice { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
