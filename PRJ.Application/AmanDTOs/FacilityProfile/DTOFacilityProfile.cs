using System.ComponentModel.DataAnnotations;

namespace PRJ.Application.AmanDTOs
{
    public class DTOFacilityProfile
    {
        [Required]
        public Guid FacilityId { get; set; }
        [Required]
        public Guid EntityID { get; set; }
        [Required]
        public string FacilityNameAr { get; set; }
        [Required]
        public string FacilityNameEn { get; set; }
        public string FacilityCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string FullAddress { get; set; }
        public string Location { get; set; }
        public DateTime? AmanInsertedOn { get; set; }

        public AmanLookupDto Province { get; set; }
    }

}
