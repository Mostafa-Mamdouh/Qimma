using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.LegalRepresentativesProfile
{
    public class DTOLegalRepresentativesProfile : BasedDtoEntity
    {
        public string LegalRepresentativesNameAr { get; set; }
        public string LegalRepresentativesNameEn { get; set; }
        public string Title { get; set; }
        public string NationalID { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Status { get; set; }
        public int CurrentFacilities { get; set; }
        public string Note { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

    }
}
