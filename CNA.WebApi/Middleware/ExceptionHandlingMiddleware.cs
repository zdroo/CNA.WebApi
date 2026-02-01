using CNA.Contracts.Common;
using CNA.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace CNA.WebApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                await HandleException(context, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await HandleException(
                    context,
                    ex,
                    HttpStatusCode.InternalServerError,
                    "Unexpected server error");
            }
        }

        private static async Task HandleException(
            HttpContext context,
            Exception exception,
            HttpStatusCode statusCode,
            string? messageOverride = null)
        {
            var response = new ApiErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = messageOverride ?? exception.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
