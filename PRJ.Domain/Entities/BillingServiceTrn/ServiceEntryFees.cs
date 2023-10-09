using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities.BillingServiceTrn
{
    public class ServiceEntryFees : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceEntryFeesId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }

        public int? PaymentTerms { get; set; }
        [ForeignKey("PaymentTerms")]
        public virtual LookupSetTerm PaymentTermsLookup { get; set; }
        public int? StageId { get; set; }
        [ForeignKey("StageId")]
        public virtual LookupSetTerm StageIdLookup { get; set; }
        public int? ProcessId { get; set; }
        [ForeignKey("ProcessId")]
        public virtual LookupSetTerm ProcessIdLookup { get; set; }

    }
}
