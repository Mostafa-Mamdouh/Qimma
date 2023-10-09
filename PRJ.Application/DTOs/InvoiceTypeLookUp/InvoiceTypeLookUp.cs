using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.InvoiceTypeLookUp
{
    internal class InvoiceTypeLookUp : BasedDtoEntity
    {
        public string InvoiceTypeCode { get; set; }
        public string InvoiceTypeDesc { get; set; }
    }
}
