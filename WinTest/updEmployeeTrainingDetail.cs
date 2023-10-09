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
    
    public partial class updEmployeeTrainingDetail
    {
        public long Id { get; set; }
        public long MasterId { get; set; }
        public int EmployeeId { get; set; }
        public string CourseName { get; set; }
        public string TrainingCenterName { get; set; }
        public string TrainingCenterCode { get; set; }
        public string CourseNumber { get; set; }
        public Nullable<int> CourseCountryId { get; set; }
        public Nullable<int> CourseCityId { get; set; }
        public string CourseTypeId { get; set; }
        public Nullable<int> CourseNatureId { get; set; }
        public Nullable<int> FromSASOAuthority { get; set; }
        public Nullable<System.DateTime> CourseStartDate { get; set; }
        public Nullable<System.DateTime> CourseEndDate { get; set; }
        public Nullable<int> CourseDuration { get; set; }
        public string CourseCertificateContentType { get; set; }
        public string CourseCertificateName { get; set; }
        public byte[] CourseCertificate { get; set; }
        public Nullable<int> CourseHours { get; set; }
        public string TrainingProgramCode { get; set; }
        public string MajorCode { get; set; }
        public string ProgramType { get; set; }
        public string ProgramGroup { get; set; }
        public Nullable<int> Cost { get; set; }
        public string Currency { get; set; }
        public Nullable<int> PlanYear { get; set; }
        public Nullable<int> PlanCode { get; set; }
        public Nullable<int> PlanType { get; set; }
        public Nullable<bool> IsUpdated { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
    
        public virtual updEmployeeTrainingInfo updEmployeeTrainingInfo { get; set; }
    }
}
