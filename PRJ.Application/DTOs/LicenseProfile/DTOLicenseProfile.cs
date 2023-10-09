using PRJ.Application.DTOs.Common;
using PRJ.Application.DTOs.EntityProfile;
using PRJ.Application.DTOs.FacilityProfile;
namespace PRJ.Application.DTOs.LicenseProfile
{
    public class DTOLicenseProfile : BasedDtoEntity
    {
        public string LicenseDescAr { get; set; }
        public string LicenseDescEn { get; set; }
        public string LicenseCode { get; set; }
        public string LicenseVersionNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string PractiseSector { get; set; }
        public string LicenseActivities { get; set; }
        public virtual string? EntityId { get; set; }
        public virtual DTOEntityProfile EntityProfile { get; set; }
        public virtual string? FacilityId { get; set; }
        public virtual DTOFacilityProfile FacilityProfile { get; set; }
        public Nullable<DateTime> AmanInsertedOn { get; set; }
        public int LicenseId { get; set; }
        public int? NumberOfActiveSources { get; set; }
        public int? NumberOfMonitoredWorkers { get; set; }
        public int? NumberOfSealedSources { get; set; }
        public int? NumberOfUnsealedSources { get; set; }
        public int? NumberOfShortLivedSources { get; set; }
    }
}
