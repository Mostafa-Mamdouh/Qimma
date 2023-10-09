using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTOEnquiryCenter : BasedDtoEntity
    {
        public string SourceId { get; set; }
        public string NrrcId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string SourceTypeEn { get; set; }
        public string SourceTypeAr { get; set; }
        public string ManufacturerEn { get; set; }
        public string ManufacturerAr { get; set; }
        public string StatusEn { get; set; }
        public string StatusAr { get; set; }
        public string ManufacturerSerialNo { get; set; }
        public DateTime? ProductionDate { get; set; }
        public string FacilitySourceID { get; set; }
        public string RadionuclideName { get; set; }


    }
}
