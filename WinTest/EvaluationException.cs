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
    
    public partial class EvaluationException
    {
        public long Id { get; set; }
        public long PlanId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<int> PlanOrgnizationalLevel { get; set; }
        public byte ActionType { get; set; }
        public Nullable<long> DirectManagerId { get; set; }
        public Nullable<long> OldDirectManagerId { get; set; }
        public long PlanIdToShowDegree { get; set; }
        public Nullable<decimal> DegreeToShow { get; set; }
        public Nullable<bool> HasMoreThanOnePlan { get; set; }
        public Nullable<bool> ShowTasks { get; set; }
        public Nullable<bool> ChangeOrganizationalLevel { get; set; }
        public Nullable<bool> AllowToAddPlan { get; set; }
        public Nullable<bool> ChangeDirectMnanager { get; set; }
        public Nullable<bool> ResetTasks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    }
}