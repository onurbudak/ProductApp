namespace ProductApp.Application.Wrappers;

public class PaginatedResponse<T> : ServiceResponse<T>
{
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public PaginatedResponse(T data, bool isSuccess, int totalItems, int pageNumber, int pageSize) : base(data, isSuccess)
    {
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public PaginatedResponse(T data, bool isSuccess, string message, int totalItems, int pageNumber, int pageSize) : base(data, isSuccess, message)
    {
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public static PaginatedResponse<T> Success(T data, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, true, totalItems, pageNumber, pageSize);
    }
    public static PaginatedResponse<T> SuccessWithMessage(T data, string message, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, true, message, totalItems, pageNumber, pageSize);
    }

    public static PaginatedResponse<T> Error(T data, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, false, totalItems, pageNumber, pageSize);
    }

    public static PaginatedResponse<T> ErrorWithMessage(T data, string message, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, false, message, totalItems, pageNumber, pageSize);
    }
}
