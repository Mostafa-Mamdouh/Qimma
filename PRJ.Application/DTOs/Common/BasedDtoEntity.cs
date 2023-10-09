using System.ComponentModel.DataAnnotations;

namespace PRJ.Application.DTOs.Common
{
	public class BasedDtoEntity
	{
		
		public string Id { get; set; }
        public string CreatedBy { get; set; } = "Hamza Ibrahim";
        [MaxLength(50)]
        public string ModifiedBy { get; set; } = "Hamza Ibrahim";
        public Nullable<DateTime> CreatedOn { get; set; } = DateTime.Now;
        public Nullable<DateTime> ModifiedOn { get; set; } = DateTime.Now;
    }
}
