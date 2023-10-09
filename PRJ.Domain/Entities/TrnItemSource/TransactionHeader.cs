using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PRJ.Domain.Common;

namespace PRJ.Domain.Entities
{
    public class TransactionHeader : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrnHeaderId { get; set; }
        public string ConfirmedUser { get; set; }
        public Nullable<DateTime> ConfirmedDate { get; set; }
        public int TrnStatus { get; set; }  

        #region Navigation Properties
        public int TransactionTypeId { get; set; }

        [ForeignKey("TransactionTypeId")]
        public virtual TransactionTypeMaster TransactionTypeMaster { get; set; }
        #endregion

    }
}

