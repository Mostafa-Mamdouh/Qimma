using PRJ.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRJ.Domain.Entities
{
    public class ItemSourceStatus : AuditableBasedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ItemSourceStatusId { get; set; }
        public string StatusCode { get; set; }
        public string StatusNameEn { get; set; }
        public string StatusNameAr { get; set; }
    }
}
