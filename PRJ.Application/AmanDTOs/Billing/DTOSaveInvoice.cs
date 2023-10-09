using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.AmanDTOs.Billing
{
    public class DTOInvoiceTransactionHeader
    {
        public DateTime InvoiceDate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int PaymentTerms { get; set; }
        public string Remarks { get; set; }
        public List<DTOInvoiceTransactionDetails> BillingServiceTrnDetails { get; set; }

    }

}
