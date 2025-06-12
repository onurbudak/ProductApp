namespace ProductApp.Application.Wrappers;

public class ErrorResponse
{
    public int StatusCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public override string ToString() => $"{StatusCode} : {Message}";

}
