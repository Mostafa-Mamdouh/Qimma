using lg = PRJ.GlobalComponents.Logger;

namespace PRJ.Application.Warppers
{
    public class Response<T>
    {
        public Response(string? LogMessage = null)
        {
            Succeeded = true;
            if (LogMessage != null) { lg.LoggerManager.Log(LogMessage); }
        }

        public Response(T data, string? message = null, string? LogMessage = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            if (LogMessage != null) { lg.LoggerManager.Log(LogMessage); }
        }
        public Response(string? message, string? LogMessage = null)
        {
            Succeeded = false;
            Message = message;
            if (LogMessage != null) { lg.LoggerManager.Log(LogMessage); }
        }

        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public string? MessageAr { get; set; }
        public string? LogMessage { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }


    }
}
