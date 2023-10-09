using System.ComponentModel.DataAnnotations;

namespace PRJ.Application.AmanDTOs
{
    public class DTORso
    {

        [Required]
        public Guid RSOID { get; set; }
        [Required]
        public string CertificateNo { get; set; }
        [Required]
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }

    }
}
