using PRJ.Application.DTOs.Common;
using PRJ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRJ.Domain.Common;
using dto = PRJ.Application.DTOs;
using PRJ.Application.DTOs.LookupSetTerm;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.DTOs.Workers
{
    public class DTOWorkersExposuresDoses : BasedDtoEntity
    {
        public string Id { get; set; }
        public int LineNum { get; set; }
        public string DosimeterType { get; set; }
        public string DosimeterID { get; set; }
        public decimal DeepDose { get; set; }
        public int? DeepDoseUOM { get; set; }
        public decimal DeepTotalAccumulatedDose1Yr { get; set; }
        public decimal DeepTotalAccumulatedDose5Yr { get; set; }
        public decimal ShallowDose { get; set; }
        public int? ShallowDoseUOM { get; set; }
        public decimal ShallowTotalAccumulatedDose1Yr { get; set; }
        public string QuarterCode { get; set; }
        public string FiscalYear { get; set; }
        public virtual ent.LookupSetTerm DosimeterTypeList { get; set; }
    }
}
