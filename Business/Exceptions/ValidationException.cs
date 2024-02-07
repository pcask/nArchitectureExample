namespace Business.Exceptions;

public class ValidationException(string message, short statusCode = 400) : Exception(message)
{
    public short StatusCode { get; set; } = statusCode;
}
