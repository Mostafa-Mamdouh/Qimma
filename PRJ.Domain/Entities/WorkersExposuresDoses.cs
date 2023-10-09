using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
    public class WorkersExposuresDoses : AuditableBasedEntity
    {
        [Key]
        public int Id { get; set; }
        [Key]
        public int LineNum { get; set; }
        public DateTime MeasurementDate { get; set; }
        [MaxLength(100)]
        public string DosimeterID { get; set; }
        public decimal DeepDose { get; set; }
        public int? DeepDoseUOM { get; set; }
        public decimal DeepTotalAccumulatedDose1Yr { get; set; }
        public decimal DeepTotalAccumulatedDose5Yr { get; set; }

        public decimal ShallowDose { get; set; }
        public int? ShallowDoseUOM { get; set; }
        public decimal ShallowTotalAccumulatedDose1Yr { get; set; }

        public decimal EyeDose { get; set; }
        public int? EyeDoseUOM { get; set; }
        public decimal EyeTotalAccumulatedDose1Yr { get; set; }
        public decimal EyeTotalAccumulatedDose5Yr { get; set; }

        public decimal NeutronDose { get; set; }
        public int? NeutronDoseUOM { get; set; }

        public DateTime InitialDayofMonitoring { get; set; }
        public DateTime LastDayofMonitoring { get; set; }
        [MaxLength(10)]
        public string QuarterCode { get; set; }
        public int FiscalYear { get; set; }
        [ForeignKey("Id")]
        public Workers Worker { get; set; }
        public int DosimeterType { get; set; }

        [ForeignKey("DosimeterType")]
        public virtual LookupSetTerm DosimeterTypeList { get; set; }
    }
}
