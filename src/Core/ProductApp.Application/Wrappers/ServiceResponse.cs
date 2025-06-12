namespace ProductApp.Application.Wrappers;

public class ServiceResponse<T> : BaseResponse
{
    public T? Data { get; set; }

    public ServiceResponse(bool isSuccess) : base(isSuccess)
    {

    }

    public ServiceResponse(bool isSuccess, string message) : base(isSuccess, message)
    {

    }

    public ServiceResponse(T data, bool isSuccess) : this(isSuccess)
    {
        Data = data;
    }

    public ServiceResponse(T data, bool isSuccess, string message) : this(isSuccess, message)
    {
        Data = data;
    }

    public static ServiceResponse<T> Success()
    {
        return new ServiceResponse<T>(true);
    }

    public static ServiceResponse<T> SuccessWithMessage(string message)
    {
        return new ServiceResponse<T>(true, message);
    }

    public static ServiceResponse<T> SuccessData(T data)
    {
        return new ServiceResponse<T>(data, true);
    }

    public static ServiceResponse<T> SuccessDataWithMessage(T data, string message)
    {
        return new ServiceResponse<T>(data, true, message);
    }

    public static ServiceResponse<T> Error()
    {
        return new ServiceResponse<T>(false);
    }

    public static ServiceResponse<T> ErrorWithMessage(string message)
    {
        return new ServiceResponse<T>(false, message);
    }

    public static ServiceResponse<T> ErrorData(T data)
    {
        return new ServiceResponse<T>(data, false);
    }

    public static ServiceResponse<T> ErrorDataWithMessage(T data, string message)
    {
        return new ServiceResponse<T>(data, false, message);
    }
}
