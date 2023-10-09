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
    
    public partial class PlansGeneralInformation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlansGeneralInformation()
        {
            this.Memberships = new HashSet<Membership>();
            this.PlanDiscounts = new HashSet<PlanDiscount>();
            this.PlanFeatures = new HashSet<PlanFeature>();
        }
    
        public int Id { get; set; }
        public string PlanGUID { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Sequence { get; set; }
        public int DurationInMonths { get; set; }
        public Nullable<bool> Published { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<bool> IsMigrated { get; set; }
        public Nullable<System.DateTime> MigratedOn { get; set; }
        public string MigratedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membership> Memberships { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanDiscount> PlanDiscounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlanFeature> PlanFeatures { get; set; }
    }
}