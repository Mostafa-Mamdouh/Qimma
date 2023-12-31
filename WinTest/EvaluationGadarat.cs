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
    
    public partial class EvaluationGadarat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvaluationGadarat()
        {
            this.EvaluationGadarhDegrees = new HashSet<EvaluationGadarhDegree>();
        }
    
        public long Id { get; set; }
        public Nullable<long> OracelSerial { get; set; }
        public Nullable<long> OriginalGadarahCode { get; set; }
        public string GadarahDescription { get; set; }
        public long PlanId { get; set; }
        public int GadarahType { get; set; }
        public byte GadarahLevel { get; set; }
        public int RequiredLevel { get; set; }
        public int GadarahVersion { get; set; }
        public string JobFamilyCode { get; set; }
        public string JobTitleCode { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual EvaluationPlan EvaluationPlan { get; set; }
        public virtual GadaratType GadaratType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluationGadarhDegree> EvaluationGadarhDegrees { get; set; }
    }
}
