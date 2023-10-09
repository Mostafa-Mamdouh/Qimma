using PRJ.Application.DTOs.Common;
using PRJ.Application.DTOs.EntityProfile;
namespace PRJ.Application.DTOs.FacilityProfile
{
    public class DTOFacilityProfile : BasedDtoEntity
    {
        public string FacilityNameAr { get; set; }
        public string FacilityNameEn { get; set; }
        public string NationalAddress { get; set; }
        public string FacilityCode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public virtual string EntityId { get; set; }
        public virtual DTOEntityProfile EntityProfile { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }
        public int FacilityId { get; set; }
    }
}