using Auto.API.Models;
using System.Text.Json;
using System.Net;
using FluentValidation;

namespace Auto.API.Middleware
{
    /// <summary>
    /// Handles all unhandled exceptions in the application and returns a ApiResponse response with the error details.
    /// If the exception is a ValidationException, the response will include validation errors.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // Call the next middleware in the pipeline
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ApiResponse<object> response;

            if (exception is ValidationException validationException)
            {
                // If the exception is a ValidationException, return validation errors
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // Collect validation errors, grouped by property name
                var validationErrors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToList());

                response = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Validation failed",
                    Data = null,
                    ValidationErrors = validationErrors
                };
            }
            else
            {
                // exception, return a generic internal server error
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                {
                    var errorId = Guid.NewGuid().ToString();
                    //TODO: Log the exceptionwith the error ID
                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = $"An unexpected error occurred. Error ID: {errorId}",
                        Data = null,
                        Errors = new List<string> { "An unexpected error occurred. Please contact support with the error ID." }
                    };
                }
                else
                {
                    response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = "An unexpected error occurred",
                        Data = null,
                        Errors = new List<string> { exception.Message }
                    };
                }
            }

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}