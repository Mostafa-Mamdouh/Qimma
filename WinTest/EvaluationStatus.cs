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
    
    public partial class EvaluationStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvaluationStatus()
        {
            this.EvaluationActions = new HashSet<EvaluationAction>();
            this.EvaluationPlans = new HashSet<EvaluationPlan>();
        }
    
        public int Id { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluationAction> EvaluationActions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluationPlan> EvaluationPlans { get; set; }
    }
}
