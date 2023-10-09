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

namespace PRJ.Application.DTOs.SafetyResponsibleOfficersProfile
{
    public class DTOSafetyResponsibleOfficersProfile : BasedDtoEntity
    {
        public string SRONameAr { get; set; }
        public string SRONameEn { get; set; }
        public string NationalID { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string CertificateNo { get; set; }
        public DateTime IssuanceDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }
    }
}
