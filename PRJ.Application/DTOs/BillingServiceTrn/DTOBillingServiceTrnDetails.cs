using PRJ.Domain.Entities.BillingServiceTrn;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Application.DTOs.BillingServiceTrn
{
    public class DTOBillingServiceTrnDetails
    {
        public int TransactionID { get; set; }
        public int LineNum { get; set; }
        public int? serviceItemId { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemPrice { get; set; }
        public string LineRemarks { get; set; }
        public bool VatIncluded { get; set; }
        public decimal VatPcntg { get; set; }
        public decimal VatAmount { get; set; }
        public decimal BillableQty { get; set; }

        [ForeignKey("TransactionID")]
        public virtual BillingServiceTrnHeader BillingServiceTrnHeader { get; set; }
    }
}
