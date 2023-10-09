using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.EntityProfile
{
    public class DTOEntityProfile : BasedDtoEntity
    {
        public string EntityNameAr { get; set; }
        public string EntityNameEn { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string GovernmentID { get; set; }
        public int EntityType { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }
        
    }
}
