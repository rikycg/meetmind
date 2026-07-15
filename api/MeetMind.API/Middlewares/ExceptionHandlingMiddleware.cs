using System.Net;
using System.Text.Json;
using MeetMind.Domain.Exceptions;

namespace MeetMind.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,           // 404
            ConflictException => HttpStatusCode.Conflict,           // 409
            BadRequestException => HttpStatusCode.BadRequest,       // 400
            ArgumentException => HttpStatusCode.BadRequest,         // 400 (validaciones de Domain)
            _ => HttpStatusCode.InternalServerError                 // 500
        };

        var response = new
        {
            status = (int)statusCode,
            message = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
