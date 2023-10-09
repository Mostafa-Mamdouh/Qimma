using PRJ.Application.DTOs.Common;
using PRJ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.DTOs.LicenseInventoryLimits
{
    public class DTOLicenseInventoryLimits : BasedDtoEntity
    {
        public string SourceType { get; set; }
        public string Radionuclide { get; set; }
        public int MaximumRadioactivity { get; set; }
        public int NumberofSources { get; set; }
        public virtual int? LicenseId { get; set; }
        public virtual ent.LicenseProfile LicenseProfile { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }
    }
}
