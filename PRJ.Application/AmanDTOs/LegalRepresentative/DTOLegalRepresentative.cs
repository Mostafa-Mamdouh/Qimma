using System.ComponentModel.DataAnnotations;

namespace PRJ.Application.AmanDTOs
{
    public class DTOLegalRepresentative
    {
        [Required]
        public Guid LegalRepID { get; set; }
        public string Title { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int CurrentFacilities { get; set; }
        public string Note { get; set; }
        public string UserIdentity { get; set; }
        public AmanLookupDto UserType { get; set; }
        public DateTime? AmanInsertedOn { get; set; }
    }
}
