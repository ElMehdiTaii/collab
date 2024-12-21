namespace Collaboration.Application.Exceptions;

public class BadRequestException(string message, int statusCode = 400) :
    Exception(message)
{
    public int StatusCode { get; } = statusCode;
}