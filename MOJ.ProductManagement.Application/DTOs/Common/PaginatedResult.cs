namespace MOJ.ProductManagement.Application.DTOs.Common
{
    public class PaginatedResult<T> : Result<T>
    {
        public PaginatedResult()
        {
        }

        public PaginatedResult(List<T> data)
        {
            Data = data;
        }

        public PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int pageNumber = 1, int pageSize = 10, object detail = null)
        {
            Data = data;
            CurrentPage = pageNumber;
            Succeeded = succeeded;
            Messages = messages;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Detail = detail;
        }

        public new List<T> Data { get; set; }
        public object Detail { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public bool? IsNormalUser { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public static PaginatedResult<T> Create(List<T> data, int count, int pageNumber, int pageSize)
        {
            return new PaginatedResult<T>(true, data, null, count, pageNumber, pageSize);
        }

        public static PaginatedResult<T> Failure(List<string> messages, int code = 400)
        {
            return new PaginatedResult<T>
            {
                Succeeded = false,
                Messages = messages,
                Code = code 
            };
        }
    }
}
