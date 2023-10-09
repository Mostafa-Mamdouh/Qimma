using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
	public class NRRCLog 
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Message { get; set; }
		public string Level { get; set; }
		public DateTime? Timestamp { get; set; }
		public string Exception { get; set; }
		public string LogEvent { get; set; }
	}
}
