using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.AmanDTOs
{
    public class DTOEntityProfile
    {
        [Required]
        public Guid EntityId { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public string NameAr { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string EntityNumber { get; set; }
        public DateTime? AmanInsertedOn { get; set; }
        public AmanLookupDto EntityType { get; set; }
     }
}
