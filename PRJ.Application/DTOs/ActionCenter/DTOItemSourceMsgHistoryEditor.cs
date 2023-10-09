using PRJ.Application.DTOs.Common;
using PRJ.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Application.DTOs
{
    public class DTOItemSourceMsgHistoryEditor : BasedDtoEntity
    {
        public string MsgText { get; set; }
        public string MsgType { get; set; }
        public string SourceId { get; set; }


    }
}
