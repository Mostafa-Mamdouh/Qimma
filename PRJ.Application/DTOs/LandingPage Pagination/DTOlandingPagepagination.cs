using PRJ.Application.DTOs.Common;
using PRJ.Application.DTOs.LandingPage_Pagination;

namespace PRJ.Application.DTOs
{
    public class DTOlandingPagepagination : BasedDtoEntity
    {
        public DTOlandingPagepagination()
        {
            this.RadionuclideName = new List<DTOLandingPageRadionuclides>();
        }
        public int TransactionType { get; set; }
        public string TransactionTypeEn { get; set; }

        public string TransactionTypeAr { get; set; }

        public string FacilityEn { get; set; }
        public string FacilityAr { get; set; }
        public double? currentActivity { get; set; } = null;
        public string LicenseNumber { get; set; }
        public List<DTOLandingPageRadionuclides> RadionuclideName { get; set; }
        public string RadioActivity { get; set; }
        public string PermitNumber { get; set; }
        public string SourceTypeEn { get; set; }
        public string SourceTypeAr { get; set; }
        public int SourceType { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public int SourceStatus { get; set; }
        public int TrnSourceId { get; set; }
        public string SourceId { get; set; }
        public string StatusEn { get; set; }
        public string StatusAr { get; set; }

    }
}
