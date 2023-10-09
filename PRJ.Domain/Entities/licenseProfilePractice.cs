using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Domain.Entities
{
    public class LicenseProfilePractice
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PracticeLookup { get; set; }
        [ForeignKey("PracticeLookup")]
        public LookupSetTerm Practice { get; set; }
        [ForeignKey("LicenseProfileId")]
        public LicenseProfile LicenseProfile { get; set; }
    }
}
