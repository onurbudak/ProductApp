namespace ProductApp.Application.Wrappers;

public class PaginatedResponse<T> : ServiceResponse<T> where T : class
{
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public PaginatedResponse(T? data, bool isSuccess, int totalItems, int pageNumber, int pageSize) : base(data, isSuccess)
    {
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public PaginatedResponse(T? data, bool isSuccess, string message, int totalItems, int pageNumber, int pageSize) : base(data, isSuccess, message)
    {
        TotalItems = totalItems;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    public static PaginatedResponse<T> SuccessPaginatedData(T? data, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, true, totalItems, pageNumber, pageSize);
    }

    public static PaginatedResponse<T> SuccessMessageWithPaginatedData(T? data, string message, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, true, message, totalItems, pageNumber, pageSize);
    }

    public static PaginatedResponse<T> ErrorPaginatedData(T? data, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, false, totalItems, pageNumber, pageSize);
    }

    public static PaginatedResponse<T> ErrorMessageWithPaginatedData(T? data, string message, int totalItems, int pageNumber, int pageSize)
    {
        return new PaginatedResponse<T>(data, false, message, totalItems, pageNumber, pageSize);
    }

    public static PaginatedResponse<T> Error()
    {
        return new PaginatedResponse<T>(null, false, 0, 0, 0);
    }

    public static PaginatedResponse<T> ErrorMessage(string message)
    {
        return new PaginatedResponse<T>(null, false, message, 0, 0, 0);
    }

}
