using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Domain.Entities
{
    public class ItemSourceStatusHistory : AuditableBasedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusHistoryId { get; set; }

        [MaxLength(1000)]
        public string Remarks { get; set; }

        #region Navigation Properties
        public int SourceId { get; set; }

        [ForeignKey("SourceId")]
        public virtual ItemSourceProfile ItemSourcesProfile { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual ItemSourceStatus ItemSourceStatus { get; set; }
        #endregion
    }
}
