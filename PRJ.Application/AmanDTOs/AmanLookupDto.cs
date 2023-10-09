using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.AmanDTOs
{
    public class AmanLookupDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        [Required]
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
    }

    public class HelperRespnse { 
        public int Id { get; set; }
        public string AmanId { get; set; }
        public int? OtherId { get; set; }
        public string OtherAmanId { get; set; }
        public string Message { get; set; }

    }
}
