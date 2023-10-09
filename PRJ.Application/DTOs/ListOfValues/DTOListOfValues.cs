using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs.ListOfValues
{
    public class DTOListOfValue : BasedDtoEntity
    {
        public int LovId { get; set; }
        public string LovNameAr { get; set; }
        public string LovNameEn { get; set; }
        public string LovCode { get; set; }
        public string SqlStatement { get; set; }
        public bool CanDelete { get; set; }
    }
}
