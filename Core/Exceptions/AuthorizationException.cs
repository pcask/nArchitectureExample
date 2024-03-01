using Microsoft.AspNetCore.Http;

namespace Core.Exceptions;

public class AuthorizationException(string role, string message, short statusCode = 401) : Exception(message), IPresentableException
{
    public string Role { get; private set; } = role;
    public short StatusCode { get; set; } = statusCode;
}