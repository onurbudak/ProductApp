namespace ProductApp.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : this("Validation error occured")
    {

    }

    public ValidationException(string Message) : base(Message)
    {

    }

    public ValidationException(Exception ex) : this(ex.Message)
    {

    }
}
