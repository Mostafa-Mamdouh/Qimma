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
    
    public partial class updEmployeeExperienceDetail
    {
        public long Id { get; set; }
        public long MasterId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> ExperienceType { get; set; }
        public string Place { get; set; }
        public string JobTitle { get; set; }
        public Nullable<long> SalaryGrade { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> LeaveDate { get; set; }
        public byte[] Letter { get; set; }
        public string LetterName { get; set; }
        public string LetterContentType { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
    
        public virtual updEmployeeExperience updEmployeeExperience { get; set; }
    }
}