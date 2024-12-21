namespace Collaboration.Application.Exceptions;
internal class NotFoundException(string message, int statusCode = 404):
Exception(message)
{
    public int StatusCode { get; } = statusCode;
}