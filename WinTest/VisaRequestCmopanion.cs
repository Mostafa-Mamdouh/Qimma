//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WinTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class VisaRequestCmopanion
    {
        public long Id { get; set; }
        public long RequestId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public long NationalityId { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string BirthPlace { get; set; }
        public string WorkPlace { get; set; }
        public string WorkActivity { get; set; }
        public string Gender { get; set; }
        public string Profission { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonEmail { get; set; }
        public string Religion { get; set; }
        public string PassportNumber { get; set; }
        public string PassportType { get; set; }
        public Nullable<System.DateTime> PassportIssueDate { get; set; }
        public Nullable<System.DateTime> PassportEndDate { get; set; }
        public string PassportIssuePlace { get; set; }
        public string PassportIssueImage { get; set; }
        public Nullable<long> ComeFromCountryId { get; set; }
        public Nullable<long> EmbassyPlaceId { get; set; }
        public string TravelType { get; set; }
        public string VisaType { get; set; }
        public Nullable<int> ResidentailDuration { get; set; }
        public Nullable<bool> IsCompanion { get; set; }
        public Nullable<System.DateTime> CommingDate { get; set; }
        public Nullable<System.DateTime> VisaEndDate { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual SASOCountry SASOCountry { get; set; }
        public virtual SASOEmbassyPlace SASOEmbassyPlace { get; set; }
        public virtual VisaRequest VisaRequest { get; set; }
    }
}
