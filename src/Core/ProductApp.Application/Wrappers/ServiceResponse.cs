namespace ProductApp.Application.Wrappers;

public class ServiceResponse<T> : BaseResponse
{
    public T? Data { get; set; }

    public ServiceResponse(T? data, bool isSuccess) : base(isSuccess)
    {
        Data = data;
    }

    public ServiceResponse(T? data, bool isSuccess, string message) : base(isSuccess, message)
    {
        Data = data;
    }

    public static ServiceResponse<T> SuccessData(T? data)
    {
        return new ServiceResponse<T>(data, true);
    }

    public static ServiceResponse<T> SuccessMessageWithData(T? data, string message)
    {
        return new ServiceResponse<T>(data, true, message);
    }

    public static ServiceResponse<T> ErrorData(T? data)
    {
        return new ServiceResponse<T>(data, false);
    }

    public static ServiceResponse<T> ErrorMessageWithData(T? data, string message)
    {
        return new ServiceResponse<T>(data, false, message);
    }
}
