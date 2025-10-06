namespace ProductApp.Application.Exceptions;

public class UnauthorizedException : UnauthorizedAccessException
{

    public int StatusCode { get; set; } = 401;


    public UnauthorizedException() : base()
    {

    }

    public UnauthorizedException(string? message) : base(message)
    {

    }

    public UnauthorizedException(string? message, Exception? inner) : base(message, inner)
    {

    }
}
