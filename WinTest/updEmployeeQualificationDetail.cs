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
    
    public partial class updEmployeeQualificationDetail
    {
        public long Id { get; set; }
        public long MasterId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> EducationalLevelId { get; set; }
        public Nullable<int> QualificationId { get; set; }
        public Nullable<bool> QualificationStatus { get; set; }
        public Nullable<int> UniversityId { get; set; }
        public Nullable<int> DegreeId { get; set; }
        public Nullable<int> RateAVG { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<System.DateTime> GraduationDate { get; set; }
        public byte[] QualificationDocument { get; set; }
        public string QualificationDocumentName { get; set; }
        public string QualificationDocumentContentType { get; set; }
        public byte[] Quation { get; set; }
        public string QuationName { get; set; }
        public string QuationContentType { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
    
        public virtual updEmployeeQualificationinfo updEmployeeQualificationinfo { get; set; }
    }
}
