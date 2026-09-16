using System.Net;
using System.Text.Json;
using ECommerceApp.Api.Responses;

namespace ECommerceApp.Api.Middleware
{
    public class ExceptionsHandlingMeddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsHandlingMeddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public ExceptionsHandlingMeddleware(RequestDelegate next
            ,ILogger<ExceptionsHandlingMeddleware> logger
            ,IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception,"An unhandled exception occured.");
            context.Response.ContentType = "Application/json";
            var statusCode = exception switch
            {
                KeyNotFoundException        => HttpStatusCode.NotFound,
                ArgumentException           => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                _                => HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = (int)statusCode;
            var response = new ApiErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = statusCode switch
                {
                    HttpStatusCode.NotFound => "The request resource was not found.",
                    HttpStatusCode.BadRequest => "The request is invalid.",
                    HttpStatusCode.Unauthorized => "You are not authorized to perform this action.",
                    _ => "An unexpected error occured."
                },
                Details = _environment.IsDevelopment() ? exception.Message : null
            };
           
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}

