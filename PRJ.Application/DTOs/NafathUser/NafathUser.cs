using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.NafathUser
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class NafathUser : BasedDtoEntity
    {
        public string EnglishName { get; set; }
        public string ArabicFatherName { get; set; }
        public string EnglishFatherName { get; set; }
        public string Gender { get; set; }
        public string CardIssueDateGregorian { get; set; }
        public string EnglishGrandFatherName { get; set; }
        public string Userid { get; set; }
        public string IdVersionNo { get; set; }
        public string ArabicNationality { get; set; }
        public string ArabicName { get; set; }
        public string ArabicFirstName { get; set; }
        public string NationalityCode { get; set; }
        public string IqamaExpiryDateHijri { get; set; }
        public string IqamaExpiryDateGregorian { get; set; }
        public string IdExpiryDateGregorian { get; set; }
        public string IssueLocationAr { get; set; }
        public string DobHijri { get; set; }
        public string CardIssueDateHijri { get; set; }
        public string EnglishFirstName { get; set; }
        public string IssueLocationEn { get; set; }
        public string ArabicGrandFatherName { get; set; }
        public string Nationality { get; set; }
        public string Dob { get; set; }
        public string EnglishFamilyName { get; set; }
        public string IdExpiryDateHijri { get; set; }
        public string ArabicFamilyName { get; set; }
    }


}
