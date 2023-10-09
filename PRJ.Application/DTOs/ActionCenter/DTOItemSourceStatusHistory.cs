using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTOItemSourceStatusHistory : BasedDtoEntity
    {
        public string Remarks { get; set; }
        public string FromStateEn { get; set; }
        public string ToStateEn { get; set; }
        public string FromStateAr { get; set; }
        public string ToStateAr { get; set; }
        public string EntryUser { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
