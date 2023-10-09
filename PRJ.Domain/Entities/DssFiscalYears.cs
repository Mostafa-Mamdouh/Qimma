using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
    public class DssFiscalYears
    {
       [MaxLength(10)]
       public string FiscalYear { get; set; }

    }
}
