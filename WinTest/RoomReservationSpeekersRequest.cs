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
    
    public partial class RoomReservationSpeekersRequest
    {
        public int Id { get; set; }
        public Nullable<int> RoomReservationId { get; set; }
        public int SpeekersType { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Extension { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual RoomReservationRequest RoomReservationRequest { get; set; }
    }
}
