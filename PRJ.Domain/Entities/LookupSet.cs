using PRJ.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
	public class LookupSet : AuditableBasedEntity
	{
		public LookupSet()
		{
			this.LookupSetTerms = new HashSet<LookupSetTerm>();
            this.InternalScreenFields = new HashSet<InternalScreenField>();
        }

        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LookupSetId { get; set; }
        [MaxLength(80)]
        public string ClassName { get; set; }
        [MaxLength(80)]
        public string DisplayNameAr { get; set; }
        [MaxLength(80)]
        public string DisplayNameEn { get; set; }
        [MaxLength(80)]
        public string ExtraData1 { get; set; }
        [MaxLength(80)]
        public string ExtraData2 { get; set; }
        public bool IsActive { get; set; }
		public virtual ICollection<LookupSetTerm> LookupSetTerms { get; set; }
        public virtual ICollection<InternalScreenField> InternalScreenFields { get; set; }

    }
}
