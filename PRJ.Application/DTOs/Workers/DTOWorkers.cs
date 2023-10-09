using PRJ.Application.DTOs.EntityProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;
using PRJ.Application.DTOs.Common;
using PRJ.Application.DTOs.FacilityProfile;
using PRJ.Application.DTOs.LookupSet;
using dto = PRJ.Application.DTOs;
using System.ComponentModel.DataAnnotations.Schema;
using PRJ.Domain.Entities;

namespace PRJ.Application.DTOs.Workers
{
    public class DTOWorkers : BasedDtoEntity
    {
        public string WorkerNameAr { get; set; }
        public string WorkerNameEn { get; set; }
        public DateTime BirthDate { get; set; }
        public string JobPosition { get; set; }
        public virtual ent.LookupSetTerm JobPositionLookup { get; set; }
        public virtual string FacilityId { get; set; }
        public virtual DTOFacilityProfile FacilityProfile { get; set; }
        public virtual string Nationality { get; set; }
        public virtual dto.BasCountries.DTOCountries BasCountries { get; set; }
        public string NationalityId { get; set; }
        public virtual string Status { get; set; }
        public virtual ent.LookupSetTerm LookupSetTerm { get; set; }
        public string PassportNo { get; set; }
        public string CurrentDosimeterType { get; set; }
        public string CurrentDosimeterID { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public virtual ent.LookupSetTerm GenderLookup { get; set; }
        public string CurrentLicense { get; set; }
        public string CurrentPractise { get; set; }
        public virtual ICollection<DTOWorkersExposuresDoses> WorkersExposuresDoses { get; set; }
        public virtual WorkersExposuresDoses LastWorkersExposuresDose { get; set; }


    }
}
