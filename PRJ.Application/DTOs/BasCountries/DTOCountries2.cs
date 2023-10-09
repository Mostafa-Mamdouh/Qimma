using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.BasCountries
{
    public class DTOCountries2 : BasedDtoEntity
    {
        public string CountryCodeISO { get; set; }
        public string CountryNameAr { get; set; }
        public string CountryNameEn { get; set; }
        public string NationalityNameFrn { get; set; }
        public string NationalityNameNtv { get; set; }
    }

}
