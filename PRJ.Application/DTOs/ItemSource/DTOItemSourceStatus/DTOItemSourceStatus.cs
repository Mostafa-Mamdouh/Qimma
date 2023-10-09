using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTOItemSourceStatus : BasedDtoEntity
    {
        public string StatusCode { get; set; }
        public string StatusNameEn { get; set; }
        public string StatusNameAr { get; set; }
    }
}
