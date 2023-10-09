using PRJ.Application.DTOs.Common;
using PRJ.Domain.Entities.BillingServiceTrn;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;
using dto = PRJ.Application.DTOs;

namespace PRJ.Application.DTOs.BillingServiceTrn
{
    public class DTOBillingServiceTrnHeader : BasedDtoEntity
    {
        public int TransactionID { get; set; }
        public string InvoiceCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string TransactionRefNum { get; set; }
        public string StatusFlag { get; set; }
        public string TrnRemarks { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExRate { get; set; }
        public string TermsCode { get; set; }
        public int InvoiceSource { get; set; }
        public string InvoiceBU { get; set; }
        public virtual ent.Billing.ItemHierarchyStructure ItemHierarchyStructure { get; set; }
        public virtual ent.LookupSetTerm LookupSetTerm { get; set; }
        public virtual ent.LookupSetTerm InvoiceSources { get; set; }
        public ICollection<dto.BillingServiceTrn.DTOBillingServiceTrnDetails> BillingServiceTrnDetails { get; set; }

    }




}
