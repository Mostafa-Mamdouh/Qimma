using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTOManufacturerMaster : BasedDtoEntity
    {
        public int ManufacturerId { get; set; }
        public string ManufacturerDescAr { get; set; }
        public string ManufacturerDescEn { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Location { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public int City { get; set; }
        public string ZipCode { get; set; }
        public string POBox { get; set; }
        public int CountryId { get; set; }
    }
}
