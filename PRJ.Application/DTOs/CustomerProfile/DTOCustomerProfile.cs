using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTOCustomerProfile : BasedDtoEntity
    {
        public int CustomerId { get; set; }

        public string? CustomerNameAr { get; set; }

        public string? CustomerNameEn { get; set; }
        
        public string? RefCode { get; set; }
        public bool? ActiveFlag { get; set; }
    }
}
