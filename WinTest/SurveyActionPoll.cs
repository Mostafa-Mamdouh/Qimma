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
    
    public partial class SurveyActionPoll
    {
        public long Id { get; set; }
        public Nullable<long> AnswerId { get; set; }
        public Nullable<long> SurveyId { get; set; }
        public Nullable<long> RelatedUserId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual SurveyAnswer SurveyAnswer { get; set; }
        public virtual SurveyRelatedUser SurveyRelatedUser { get; set; }
    }
}
