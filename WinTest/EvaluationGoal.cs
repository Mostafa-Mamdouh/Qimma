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
    
    public partial class EvaluationGoal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvaluationGoal()
        {
            this.EvaluationGoalDegrees = new HashSet<EvaluationGoalDegree>();
            this.EvaluationGoalNotes = new HashSet<EvaluationGoalNote>();
        }
    
        public long Id { get; set; }
        public Nullable<long> OracelSerial { get; set; }
        public Nullable<long> PlanId { get; set; }
        public Nullable<int> GoalType { get; set; }
        public string GoalDescription { get; set; }
        public string MeasurementStandard { get; set; }
        public int RelativeWeight { get; set; }
        public int TargetOutput { get; set; }
        public int GoalsVersion { get; set; }
        public Nullable<int> IndicatorId { get; set; }
        public string IndicatorDescription { get; set; }
        public Nullable<bool> IsOtherIndicator { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluationGoalDegree> EvaluationGoalDegrees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluationGoalNote> EvaluationGoalNotes { get; set; }
        public virtual EvaluationPlan EvaluationPlan { get; set; }
        public virtual GoalType GoalType1 { get; set; }
    }
}
