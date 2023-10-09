using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PRJ.Domain.Common
{
   // [Index(nameof(AuditableBasedEntity.AmanOrgId), IsUnique = true)]
    public class AuditableBasedEntity
	{
		[MaxLength(50)]
		public string CreatedBy { get; set; } = "Hamza Ibrahim";
        [MaxLength(50)]
		public string ModifiedBy { get; set; } = "Hamza Ibrahim";
		public Nullable<DateTime> CreatedOn { get; set; } = DateTime.Now;
        public Nullable<DateTime> ModifiedOn { get; set; } = DateTime.Now;

	}
}
