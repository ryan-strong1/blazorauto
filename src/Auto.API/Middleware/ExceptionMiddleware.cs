using System.Text.Json;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Azure;

namespace Auto.API.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions and return a consistent error response
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ExceptionMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
        {
            _next = next;
            _problemDetailsFactory = problemDetailsFactory;
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
            context.Response.Headers["Cache-Control"] = "no-cache";

            if (exception is ValidationException validationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var validationErrors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToList());

                var modelStateDictionary = new ModelStateDictionary();
                foreach (var error in validationErrors)
                {
                    modelStateDictionary.AddModelError(error.Key, error.Value.First());
                }

                var response = _problemDetailsFactory.CreateValidationProblemDetails(
                     context,
                     statusCode: (int)HttpStatusCode.BadRequest,
                     title: "Validation failed",
                     detail: "One or more validation errors occurred.",
                     modelStateDictionary: modelStateDictionary);

                var jsonResponse = JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(jsonResponse);
            }
            else
            {
                ProblemDetails response = new ProblemDetails();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                {
                    var errorId = Guid.NewGuid().ToString();
                    //TODO: Log the exception with the error ID

                    response = _problemDetailsFactory.CreateProblemDetails(
                    context,
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    title: "An unexpected error occurred",
                    detail: "An unexpected error occurred. Please use the error ID for support: " + errorId);
                }
                else
                {
                    response = _problemDetailsFactory.CreateProblemDetails(
                        context,
                        statusCode: (int)HttpStatusCode.InternalServerError,
                        title: "An unexpected error occurred",
                        detail: exception.Message);
                }

                var jsonResponse = JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}