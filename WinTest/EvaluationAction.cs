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
    
    public partial class EvaluationAction
    {
        public int Id { get; set; }
        public Nullable<long> OracelSerial { get; set; }
        public long PlanId { get; set; }
        public long ActionTakerId { get; set; }
        public string ActionTakerName { get; set; }
        public string GeneralNote { get; set; }
        public string RejectNote { get; set; }
        public int StatusId { get; set; }
        public Nullable<bool> IsHr { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedById { get; set; }
        public string ModifiedByName { get; set; }
    
        public virtual EvaluationPlan EvaluationPlan { get; set; }
        public virtual EvaluationStatus EvaluationStatus { get; set; }
    }
}