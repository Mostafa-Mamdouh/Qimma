using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRJ.Domain.Common;

namespace PRJ.Domain.Entities.BillingServiceTrn
{
    public class BillingServiceTrnDetails 
    {
        [Key]
        public int TransactionID { get; set; }
        [Key]
        public int? LineNum { get; set; }
        public int? ServiceItemId { get; set; }//we get it from aman as stageid
        public int? PracticeId { get; set; }
        public int? SectorId { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemPrice { get; set; }
        [MaxLength(500)]
        public string LineRemarks { get; set; }
        public bool VatIncluded { get; set; }
        public decimal VatPcntg { get; set; }
        public decimal VatAmount { get; set; }
        public decimal BillableQty { get; set; }

    }
}
