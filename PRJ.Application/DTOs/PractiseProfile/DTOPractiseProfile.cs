using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.DTOs.PractiseProfile
{
    public class DTOPractiseProfile : BasedDtoEntity
    {
        public string PractiseNameAr { get; set; }
        public string PractiseNameEn { get; set; }
        public DateTime? AmanInsertedOn { get; set; }
        public int? AmmanId { get; set; }
        public string PermitDetailsId { get; set; }
        public ent.PermitDetailsProfile PermitDetailsProfile { get; set; }

        public int LicenseId { get; set; }
    }
}
