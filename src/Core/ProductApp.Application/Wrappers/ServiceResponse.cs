namespace ProductApp.Application.Wrappers;

public class ServiceResponse<T> : BaseResponse
{
    public T? Data { get; set; }

    public ServiceResponse(T? data, bool isSuccess, Error? error) : base(isSuccess, error)
    {
        Data = data;
    }

    public ServiceResponse(T? data, bool isSuccess, string message, Error? error) : base(isSuccess, message, error)
    {
        Data = data;
    }

    public static ServiceResponse<T> SuccessData(T? data)
    {
        return new ServiceResponse<T>(data, true, null);
    }

    public static ServiceResponse<T> SuccessDataWithMessage(T? data, string message)
    {
        return new ServiceResponse<T>(data, true, message, null);
    }

    public static ServiceResponse<T> FailureData(Error error)
    {
        return new ServiceResponse<T>(default, false, error);
    }

    public static ServiceResponse<T> FailureDataWithMessage(string message, Error error)
    {
        return new ServiceResponse<T>(default, false, message, error);
    }
}
