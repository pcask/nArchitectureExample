namespace Core.Exceptions;

public class ValidationException(string message, short statusCode = 400) : Exception(message), IPresentableException
{
    public short StatusCode { get; set; } = statusCode;
}
