using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
    public class InvoiceTypeLookUp : AuditableBasedEntity
    {
        [Key]
        [MaxLength(100)]
        public string InvoiceTypeCode { get; set; }
        [MaxLength(80)]
        public string InvoiceTypeDesc { get; set; }
    }
}
