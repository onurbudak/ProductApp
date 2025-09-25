namespace ProductApp.Application.Wrappers;

public class BaseResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public bool IsSuccess { get; set; }

    public string Message { get; set; } = string.Empty;

    public BaseResponse(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public BaseResponse(bool isSuccess, string message) : this(isSuccess)
    {
        Message = message;
    }

    public static BaseResponse Success()
    {
        return new BaseResponse(true);
    }

    public static BaseResponse SuccessMessage(string message)
    {
        return new BaseResponse(true, message);
    }

    public static BaseResponse Error()
    {
        return new BaseResponse(false);
    }

    public static BaseResponse ErrorMessage(string message)
    {
        return new BaseResponse(false, message);
    }
}
