using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRJ.Domain.Entities
{
    public class ItemSourceMsgHistory: AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MsgId { get; set; }
		public string MsgText { get; set; }

        #region Navigation Properties
        public int SourceId { get; set; }

        [ForeignKey("SourceId")]
        public virtual ItemSourceProfile ItemSourceProfile { get; set; }
      
        #endregion
    }
}
