using PRJ.Application.DTOs.Common;

namespace PRJ.Application.DTOs
{
    public class DTOItemSourceStatusHistoryEditor : BasedDtoEntity
    {
        public string StatusHistoryId { get; set; }
        public string Remarks { get; set; }
        public string SourceId { get; set; }
        public string StatusId { get; set; }
        public string ParentStatusId { get; set; }


    }
}
