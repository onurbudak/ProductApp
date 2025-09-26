namespace ProductApp.Application.Wrappers;

public class PaginatedResponse<T> : ServiceResponse<T> where T : class
{
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public PaginatedResponse(T? data, bool isSuccess, int totalItems, int pageNumber, int pageSize, Error? error) : base(data, isSuccess, error)
    {
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public PaginatedResponse(T? data, bool isSuccess, string message, int totalItems, int pageNumber, int pageSize, Error? error) : base(data, isSuccess, message, error)
    {
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public static PaginatedResponse<T> SuccessPaginatedData(T? data, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, true, totalItems, pageNumber, pageSize, null);
    }

    public static PaginatedResponse<T> SuccessPaginatedDataWithMessage(T? data, string message, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, true, message, totalItems, pageNumber, pageSize, null);
    }

    public static PaginatedResponse<T> FailurePaginatedData(Error error)
    {
        return new PaginatedResponse<T>(default, false, 0, 1, int.MaxValue, error);
    }

    public static PaginatedResponse<T> FailurePaginatedDataWithMessage(string message,Error error)
    {
        return new PaginatedResponse<T>(default, false, message, 0, 1, int.MaxValue, error);
    }

}
