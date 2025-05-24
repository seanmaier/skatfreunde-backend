using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using skat_back.Lib;

namespace skat_back.utilities.middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);

        var (statusCode, message) = ex switch
        {
            ValidationException => (StatusCodes.Status400BadRequest, "Validation error"),
            HttpException httpEx => (httpEx.StatusCode, httpEx.Message),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Not found"),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error")
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = statusCode,
            Title = message,
            Detail = ex.Message
        });
    }
}