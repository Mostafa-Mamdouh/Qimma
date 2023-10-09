using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRJ.Domain.Common;

namespace PRJ.Domain.Entities
{
    public class TransactionTypeMaster : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionTypeId { get; set; }
        public string TCode { get; set; }
        public string TransactionTypeDesFrn { get; set; }
        public string TransactionTypeDesNtv { get; set; }
    }
}


