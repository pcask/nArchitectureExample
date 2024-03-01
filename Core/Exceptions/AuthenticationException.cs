using Microsoft.AspNetCore.Http;

namespace Core.Exceptions;

public class AuthenticationException(string message, short statusCode = 403) : Exception(message), IPresentableException
{
    public short StatusCode { get; set; } = statusCode;
}
