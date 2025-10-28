using System.Net;
using System.Text.Json;

namespace OrdersService.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                context.Response.ContentType = "application/json";

                object responseObj;
                int statusCode;
                statusCode = (int)HttpStatusCode.InternalServerError;
                responseObj = new { message = $"An unexpected error occurred, {ex.Message}" };
                

                context.Response.StatusCode = statusCode;
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(responseObj, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
