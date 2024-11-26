namespace ProductApp.Application.Wrappers;

public class BaseResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public bool IsSuccess { get; set; }

    public string Message { get; set; }

    public BaseResponse(bool isSuccess, string message) : this(isSuccess)
    {
        Message = message;
    }

    public BaseResponse(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}
