using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
    public class TrnItemSourceHistory : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrnHistoryId { get; set; }
        public string TransactionCode { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Remarks { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; }
        public int TransactionAttribute { get; set; }

        #region Navigation Properties
        public int? ItemSourceProfileId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual ItemSourceProfile ItemSourceProfile { get; set; }
        public int TransactionTypeId { get; set; }

        [ForeignKey("TransactionTypeId")]
        public virtual TransactionTypeMaster TransactionTypeMaster { get; set; }
        #endregion
    }
}
