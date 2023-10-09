using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.AmanDTOs.Billing
{
    public class DTOServiceEntryFees
    {
        public DateTime InvoiceDate { get; set; }
        [Required]
        public string CustomerId { get; set; }
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }

        public int PaymentTerms { get; set; }
        public int StageId { get; set; }
        public int ProcessId { get; set; }
    }
}
