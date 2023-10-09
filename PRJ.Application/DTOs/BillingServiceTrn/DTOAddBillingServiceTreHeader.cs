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
    public class DTOAddBillingServiceTrnHeader : BasedDtoEntity
    {
        public DTOBillingServiceTrnHeader header { get; set; }
    }
 
}
