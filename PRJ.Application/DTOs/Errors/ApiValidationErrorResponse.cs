using PRJ.Application.Enums;

namespace Application.DTOs
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400, ReponseMsg.failed.ToString())
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}