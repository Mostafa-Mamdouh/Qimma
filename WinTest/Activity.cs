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
    
    public partial class Activity
    {
        public long Id { get; set; }
        public int ActivityKey { get; set; }
        public string ActivityCode { get; set; }
        public long ChapterNo { get; set; }
        public string ActivityDescriptionAr { get; set; }
        public string ActivityDescriptionEn { get; set; }
        public int ActivityLevel { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> ActivityRequestId { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual Chapter Chapter { get; set; }
    }
}