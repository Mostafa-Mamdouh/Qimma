namespace PRJ.Application.Warppers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}

//using Microsoft.EntityFrameworkCore;

//namespace PRJ.Application.Warppers
//{
//    public class PagedResponse<T>
//    {
//        public bool Succcess { get; set; }
//        public string? Message { get; set; }
//        public string? LogMessage { get; set; }
//        public List<string>? Errors { get; set; }
//        public List<T> Data { get; set; } = null;
//        public int PageIndex { get; private set; }
//        public int TotalPages { get; private set; }
//        public int TotalRows { get; private set; }

//        public PagedResponse(List<T> items, int count, int totalRows, int pageIndex, int pageSize, bool success, string msg, string? logMsg, List<string>? errors)
//        {
//            PageIndex = pageIndex;
//            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
//            TotalRows = totalRows;
//            Data = items;
//            Succcess = success;
//            Message = msg;
//            LogMessage = logMsg;
//            Errors = errors;
//            this.Data.AddRange(items);
//        }

//        public bool HasPreviousPage => PageIndex > 1;

//        public bool HasNextPage => PageIndex < TotalPages;

//        public static async Task<PagedResponse<T>> CreateAsync(IQueryable<T> source, int totalRows, int pageIndex, int pageSize, bool success, string msg, string? logMsg, List<string>? errors)
//        {
//            var count = await source.CountAsync();
//            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
//            return new PagedResponse<T>(items, count, totalRows, pageIndex, pageSize, success, msg, logMsg, errors);
//        }
//    }
//}

