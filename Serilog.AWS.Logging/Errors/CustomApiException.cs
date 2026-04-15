namespace Serilog.AWS.Logging.Errors;

public class CustomApiException(int statuscode, string message, string? details)
{
    public int StatusCode { get; set; } = statuscode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
}
