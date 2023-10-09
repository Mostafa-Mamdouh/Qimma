using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PRJ.Application.DTOs
{
    public class DTOGetUserDetails : BasedDtoEntity
    {
        public string Username { get; set; }
        public string FirstNameAr { get; set; }
        public string SecondNameAr { get; set; }
        public string LastNameAr { get; set; }
        public string FirstNameEn { get; set; }
        public string SecondNameEn { get; set; }
        public string LastNameEn { get; set; }
        public string Nationality { get; set; }
        public string NationalID { get; set; }
        public string Email { get; set; }
        public DateTime GregorianBirthDate { get; set; }
        public byte[] Picture { get; set; }
        public string PassportNo { get; set; }
        public string AccessToken { get; set; }


    }



}
