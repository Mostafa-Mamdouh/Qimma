using PRJ.Application.DTOs.Common;
using PRJ.Application.DTOs.LicenseProfile;
namespace PRJ.Application.DTOs.PermitDetailsProfile
{
    public class DTOPermitDetailsProfile : BasedDtoEntity
    {
        public string PermitDetailsDescAr { get; set; }
        public string PermitDetailsDescEn { get; set; }
        public string PermitNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual string? LicenseId { get; set; }
        public virtual DTOLicenseProfile LicenseProfile { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }
        public int PermitDetailsId { get; set; }

    }
}
