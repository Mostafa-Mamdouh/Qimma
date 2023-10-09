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
    
    public partial class RoomReservationRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoomReservationRequest()
        {
            this.RoomReservationAttandaceRequests = new HashSet<RoomReservationAttandaceRequest>();
            this.RoomReservationRequestActions = new HashSet<RoomReservationRequestAction>();
            this.RoomReservationRequestTasks = new HashSet<RoomReservationRequestTask>();
            this.RoomReservationSpeekersRequests = new HashSet<RoomReservationSpeekersRequest>();
        }
    
        public int Id { get; set; }
        public string RequestNo { get; set; }
        public long RequsterNo { get; set; }
        public Nullable<int> RequestMainType { get; set; }
        public Nullable<int> RequestNature { get; set; }
        public Nullable<int> RequestStatus { get; set; }
        public string RequestStatusText { get; set; }
        public Nullable<long> AssignedTo { get; set; }
        public Nullable<long> Specialist { get; set; }
        public string SpecialistComments { get; set; }
        public string RoomName { get; set; }
        public Nullable<int> RoomId { get; set; }
        public string RoomNameIfOther { get; set; }
        public string RoomBulidingIfOther { get; set; }
        public string RoomFloorIfOther { get; set; }
        public string SpecialistName { get; set; }
        public Nullable<bool> VisitInteractiveLap { get; set; }
        public Nullable<System.DateTime> DateOfReservation { get; set; }
        public Nullable<System.DateTime> DateOfReservationTo { get; set; }
        public string TimeOfReservation { get; set; }
        public string TimeOfReservationTo { get; set; }
        public Nullable<int> NoOfVisitors { get; set; }
        public Nullable<int> VisitorsType { get; set; }
        public Nullable<bool> SeniorOfficials { get; set; }
        public Nullable<bool> VisitInteractiveExhibition { get; set; }
        public string Comment { get; set; }
        public byte[] Attachment { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentContentType { get; set; }
        public byte[] Attachment2 { get; set; }
        public string AttachmentName2 { get; set; }
        public string AttachmentContentType2 { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomReservationAttandaceRequest> RoomReservationAttandaceRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomReservationRequestAction> RoomReservationRequestActions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomReservationRequestTask> RoomReservationRequestTasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomReservationSpeekersRequest> RoomReservationSpeekersRequests { get; set; }
    }
}
