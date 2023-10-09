using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.AmanDTOs.Billing
{
    public class DTOInvoiceTransactionDetails
    {
        public int LineNo { get; set; }
        public string Stage { get; set; }
        public int Qty { get; set; }
        public string Sector { get; set; }
        public string Practise { get; set; }


    }
}
