

namespace PRJ.Application.DTOs
{
    public class DtoBillingIntegeration
    {
        public DateTime TransactionDate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int PaymentTerms { get; set; }
        public List<DtoBillingDetails> BillingDetails { get; set; }

    }
    public class DtoBillingDetails
    {
        public decimal ItemQty { get; set; }
        public decimal ItemPrice { get; set; }

    }
}
