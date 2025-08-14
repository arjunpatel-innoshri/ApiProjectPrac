using ApiProjectPrac.Helpers;
using System.Net;

namespace ApiProjectPrac.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // continue the pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>
                {
                    Success = false,
                    Message = "An unexpected error occurred.",
                    Error = ex.Message,
                    Data = null
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }

}
