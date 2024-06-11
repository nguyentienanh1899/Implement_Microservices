using Microsoft.AspNetCore.Http;
using Serilog;
using Shared.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Middlewares
{
    public class ErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorWrappingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            var errorMessage = string.Empty;
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex) when (ex.GetType().Name == "ValidationException")
            {
                _logger.Error(ex, "Validation exception occurred.");

                errorMessage = "Validation failed"; // Error notification settings
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var apiResult = new ApiResult<object>(false, null, errorMessage);

                await context.Response.WriteAsJsonAsync(apiResult);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An unhandled exception occurred.");

                errorMessage = "An unexpected error occurred."; // Error notification settings
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var apiResult = new ApiResult<object>(false, null, errorMessage);

                await context.Response.WriteAsJsonAsync(apiResult);
            }
        }
    }
}
