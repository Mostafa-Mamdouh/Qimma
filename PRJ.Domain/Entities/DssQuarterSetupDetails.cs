using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
    public class DssQuarterSetupDetails
    {
       [Key]
       [MaxLength(20)]
       public string CustomerID { get; set; }
       [Key]
       [MaxLength(10)]
       public string CmpNum { get; set; }
       [Key]
       [MaxLength(10)]
       public string QuarterCode { get; set; }
       [Key]
       [MaxLength(10)]
       public string PeriodNum { get; set; }


    }
}
