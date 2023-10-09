using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRJ.Domain.Common;

namespace PRJ.Domain.Entities
{
    public class WorkFlowRejectReasons : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RejectReasonID { get; set; }
        [MaxLength(80)]
        public string RejectReasonDesFrn { get; set; }
        [MaxLength(80)]
        public string RejectReasonDesNtv { get; set; }
    }
}