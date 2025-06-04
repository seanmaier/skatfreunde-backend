using System.Diagnostics;
using System.Text.Json;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using skat_back.features.alertEmail;
using skat_back.Lib;
using skat_back.utilities.exceptions;

namespace skat_back.utilities.middleware;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger,
    IWebHostEnvironment environment)
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
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;

        var userId = context.User?.FindFirst("sub")?.Value;

        logger.LogError(ex,
            "Unhandled exception in {Method} {Path} for user {UserId}. TraceId: {TraceId}. RequestSize: {RequestSize}",
            context.Request.Method,
            context.Request.Path,
            userId ?? "Anonymous",
            traceId,
            context.Request.ContentLength ?? 0);

        var errorResponse = CreateErrorResponse(ex, traceId);

        context.Response.StatusCode = errorResponse.Status;
        context.Response.ContentType = "application/json";

        try
        {
            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
        catch (Exception serializationEx)
        {
            // Fallback - send minimal response
            logger.LogCritical(serializationEx, "Failed to serialize error response for TraceId: {TraceId}", traceId);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"{{\"error\":\"Internal server error\",\"traceId\":\"{traceId}\"}}");
        }
    }

    private ErrorResponse CreateErrorResponse(Exception ex, string traceId)
    {
        var errorResponse = new ErrorResponse
        {
            TraceId = traceId,
            TimeStamp = DateTime.UtcNow
        };

        switch (ex)
        {
            case ValidationException fluentEx:
                errorResponse.Status = StatusCodes.Status400BadRequest;
                errorResponse.Title = "One or more validation errors occurred";
                errorResponse.ErrorCode = "VALIDATION_ERROR";
                errorResponse.ValidationErrors = fluentEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                break;

            case BusinessLogicException businessEx:
                errorResponse.Status = businessEx.StatusCode;
                errorResponse.Title = businessEx.UserMessage;
                errorResponse.ErrorCode = businessEx.ErrorCode;
                break;

            case UnauthorizedAccessException:
                errorResponse.Status = StatusCodes.Status401Unauthorized;
                errorResponse.Title = "Access denied";
                errorResponse.ErrorCode = "UNAUTHORIZED";
                break;

            case TimeoutException:
            case TaskCanceledException when ex.InnerException is TimeoutException:
                errorResponse.Status = StatusCodes.Status408RequestTimeout;
                errorResponse.Title = "Request timeout";
                errorResponse.ErrorCode = "REQUEST_TIMEOUT";
                break;

            case DbUpdateConcurrencyException:
                errorResponse.Status = StatusCodes.Status409Conflict;
                errorResponse.Title = "The record was modified by another user";
                errorResponse.ErrorCode = "CONCURRENCY_CONFLICT";
                break;

            case DbUpdateException dbEx when IsUniqueConstraintViolation(dbEx):
                errorResponse.Status = StatusCodes.Status409Conflict;
                errorResponse.Title = "A record with this information already exists";
                errorResponse.ErrorCode = "DUPLICATE_RECORD";
                break;

            case DbUpdateException:
                errorResponse.Status = StatusCodes.Status500InternalServerError;
                errorResponse.Title = "Database error occurred";
                errorResponse.ErrorCode = "DATABASE_ERROR";
                break;

            /*case OperationCanceledException when ex.Cancellation:
                // Client disconnected - don't log as error, just return
                return null;*/

            default:
                errorResponse.Status = StatusCodes.Status500InternalServerError;
                errorResponse.Title = "An internal server error occurred";
                errorResponse.ErrorCode = "INTERNAL_SERVER_ERROR";
                errorResponse.Detail = "Please try again later or contact support if the problem persists";
                break;
        }

        if (!environment.IsDevelopment()) return errorResponse;

        errorResponse.DeveloperMessage = ex.Message;
        errorResponse.StackTrace = ex.StackTrace;

        return errorResponse;
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException?.Message.Contains("UNIQUE constraint", StringComparison.OrdinalIgnoreCase) == true;
    }
    
    private static AlertSeverity GetErrorSeverity(Exception ex)
    {
        return ex switch
        {
            ValidationException => AlertSeverity.Info,
            BusinessLogicException businessEx when businessEx.StatusCode < 500 => AlertSeverity.Warning,
            UnauthorizedAccessException => AlertSeverity.Warning,
            DbUpdateConcurrencyException => AlertSeverity.Warning,
            
            // Critical errors that need immediate attention
            DbUpdateException => AlertSeverity.Critical,
            TimeoutException => AlertSeverity.Critical,
            InvalidOperationException => AlertSeverity.Critical,
            ArgumentException => AlertSeverity.Critical,
            
            // Default to critical for unknown exceptions
            _ => AlertSeverity.Critical
        };
    }
}