using System;
using System.ComponentModel.DataAnnotations;
using PRJ.Domain.Common;

namespace PRJ.Domain.Entities
{
    public class WorkFlowStatus : AuditableBasedEntity
    {
        [Key]
        public int StatusClass { get; set; }
        [Key]
        public int StatusFlag { get; set; }
        [MaxLength(80)]
        public string StatusDesFrn { get; set; }
        [MaxLength(80)]
        public string StatusDesNtv { get; set; }
    }
}

