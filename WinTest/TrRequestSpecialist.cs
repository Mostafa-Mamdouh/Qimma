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
    
    public partial class TrRequestSpecialist
    {
        public long Id { get; set; }
        public long SpecialistNumber { get; set; }
        public string SpecialistName { get; set; }
        public string EmailId { get; set; }
        public string Extention { get; set; }
        public Nullable<long> RequestId { get; set; }
        public bool IsCoordinator { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual TranslationRequest TranslationRequest { get; set; }
    }
}
