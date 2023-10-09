using System;
using System.ComponentModel.DataAnnotations;
using PRJ.Domain.Common;

namespace PRJ.Domain.Entities
{
    public class WFTransactionsFlow : AuditableBasedEntity
    {
        [Key]
        public int WorkFlowSeq { get; set; }
        [Key]
        public string WorkFlowLineNum { get; set; }
        public int TransactionID { get; set; }
        public Nullable<DateTime> CreatedDate  { get; set; }
        [MaxLength(20)]
        public string CreatedUser { get; set; }
        [MaxLength(20)]
        public string FromUser { get; set; }
        [MaxLength(20)]
        public string ToUser { get; set; }
        public int FromWFRoleID { get; set; }
        public int ToWFRoleID { get; set; }
        public int StatusFlag { get; set; }
        [MaxLength(255)]
        public string WFRemarks { get; set; }
        [MaxLength(20)]
        public string ConfirmUser { get; set; }
        [MaxLength(20)]
        public string RejectReasonID { get; set; }


    }
}

