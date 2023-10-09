using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRJ.Domain.Entities.Billing;

namespace PRJ.Domain.Entities.BillingServiceTrn
{
    public class BillingServiceTrnHeader : AuditableBasedEntity
    {
        [Key]
        public int TransactionID { get; set; }
        [MaxLength(50)]
        public string InvoiceType { get; set; }//SINV
        [MaxLength(50)]
        public string InvoiceCode { get; set; }//Generated and send to aman (sadad no) to agree on format SINV-2023-00002
        public Nullable<DateTime> InvoiceDate { get; set; }//Aman will send it
        [MaxLength(30)]
        public string TransactionRefNum { get; set; }//not required
        public int StatusFlag { get; set; }//check if paid , not paid , expired or canceld make it lookup Entry 
        [MaxLength(1000)]
        public string TrnRemarks { get; set; }
        [MaxLength(30)]
        public string CustomerId { get; set; }
        [MaxLength(300)]
        public string CustomerName { get; set; }
        [MaxLength(30)]
        public string CurrencyCode { get; set; }//hard code it
        public decimal ExRate { get; set; }//1
        [MaxLength(30)]
        public string TermsCode { get; set; }//lookup payment term 
        
        [MaxLength(120)]
        public string InvoiceBU { get; set; }//
        [ForeignKey("StatusFlag")]
        public virtual LookupSetTerm LookupSetTerm { get; set; }
        public int? InvoiceSource { get; set; }//aman 5 / 1 manual / 10 training Programm
        [ForeignKey("InvoiceSource")]
        public virtual LookupSetTerm InvoiceSources { get; set; }
        public virtual BillingServiceTrnDetails BillingServiceTrnDetails { get; set; }


    }

}
