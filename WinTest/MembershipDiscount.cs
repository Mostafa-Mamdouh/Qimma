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
    
    public partial class MembershipDiscount
    {
        public int Id { get; set; }
        public int PlanDiscountId { get; set; }
        public int MembershipId { get; set; }
        public string OrgNameOfDiscountAR { get; set; }
        public string OrgNameOfDiscountEN { get; set; }
        public string OrgStaticDiscountCode { get; set; }
        public double OrgValueOfDiscount { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<bool> IsMigrated { get; set; }
        public Nullable<System.DateTime> MigratedOn { get; set; }
        public string MigratedBy { get; set; }
        public Nullable<bool> Published { get; set; }
    
        public virtual Membership Membership { get; set; }
        public virtual PlanDiscount PlanDiscount { get; set; }
    }
}
