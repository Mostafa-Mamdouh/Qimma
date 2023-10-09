using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.DTOs.PermitInventoryLimits
{
    public class DTOPermitInventoryLimits : BasedDtoEntity
    {
        public string SourceSerialNo { get; set; }
        public string ManufactureName { get; set; }
        public string Radionuclide { get; set; }
        public string ModelMaximumRadioactivity { get; set; }
        public string Unit { get; set; }
        public virtual int? PermitDetailsId { get; set; }
        public virtual ent.PermitDetailsProfile PermitDetailsProfile { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }
    }
}
