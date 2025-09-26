namespace ProductApp.Application.Wrappers;

public class BaseResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public Error? Error { get; set; }

    public BaseResponse(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public BaseResponse(bool isSuccess, string message, Error? error) : this(isSuccess, error)
    {
        Message = message;
        Error = error;
    }

    public static BaseResponse Success()
    {
        return new BaseResponse(true, null);
    }

    public static BaseResponse SuccessMessage(string message)
    {
        return new BaseResponse(true, message, null);
    }

    public static BaseResponse Failure(Error error)
    {
        return new BaseResponse(false, error);
    }

    public static BaseResponse FailureMessage(string message, Error error)
    {
        return new BaseResponse(false, message, error);
    }
}
