using PRJ.Application.DTOs.Common;
using PRJ.Domain.Entities.BillingServiceTrn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.BillingServiceTrn
{
    public class DTOUpdateBillingServiceTrn : DTOAddBillingServiceTrnHeader
    {
        public string TransActionID { get; set; }

        
    }
     
}
