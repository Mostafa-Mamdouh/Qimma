using PRJ.Application.DTOs.Common;
using PRJ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;
using PRJ.Application.DTOs.FacilityProfile;
using PRJ.Application.DTOs.LookupSet;
using dto = PRJ.Application.DTOs;
using System.ComponentModel.DataAnnotations.Schema;
using PRJ.Domain.Entities;
namespace PRJ.Application.DTOs.Workers
{
    public class DTOGetMassUpdate : BasedDtoEntity
    {
        public string WorkerNameAr { get; set; }
        public string WorkerNameEn { get; set; }
        public DateTime BirthDate { get; set; }
        public string JobPositionAr { get; set; }
        public string JobPositionEn { get; set; }
        public string NationalityId { get; set; }
        public virtual string StatusAr { get; set; }
        public virtual string StatusEn { get; set; }
        public string GenderAr { get; set; }
        public string GenderEn { get; set; }
        public decimal q1Value { get; set; }
        public decimal q2Value { get; set; }
        public decimal q3Value { get; set; }
        public decimal q4Value { get; set; }

    }
}
