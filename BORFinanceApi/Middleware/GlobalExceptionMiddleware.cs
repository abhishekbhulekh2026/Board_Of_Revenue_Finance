using BORFinanceCommon.Exceptions;
using BORFinanceCommon.Models;
using System.Net;
using System.Text.Json;

namespace BORFinanceApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
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

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            switch (exception)
            {
                case BusinessException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Unauthorized access.";
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;

                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            _logger.LogError(
                exception,
                "Exception occurred. Method: {Method}, Path: {Path}, TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path,
                context.TraceIdentifier);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new ApiResponse<object>
            {
                Success = false,
                StatusCode = (int)statusCode,
                Message = message,
                Errors = _environment.IsDevelopment()
                    ? new[] { exception.Message }
                    : null,
                TraceId = context.TraceIdentifier
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response, options));
        }
    }  
}
