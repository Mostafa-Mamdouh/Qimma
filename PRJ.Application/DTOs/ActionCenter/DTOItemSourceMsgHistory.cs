using PRJ.Application.DTOs.Common;
using PRJ.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRJ.Application.DTOs
{
    public class DTOItemSourceMsgHistory : BasedDtoEntity
    {
        public string MsgText { get; set; }
        public string SentBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
