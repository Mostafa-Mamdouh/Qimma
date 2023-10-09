namespace Application.DTOs
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message="",string error="",string status="")
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Status = status ?? GetDefaultStatus(statusCode);
            Error = error;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string Status { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request",
                401 => "You are not authorized",
                404 => "Resource not found",
                500 => "Internal server error",
                _ => "",
            };
        }
        private string GetDefaultStatus(int statusCode)
        {
            return statusCode switch
            {
                200 => "Succeded",
                400 => "Bad request",
                401 => "You are not authorized",
                404 => "Resource not found",
                500 => "Internal server error",
                _ => "",
            };
        }
    }
}