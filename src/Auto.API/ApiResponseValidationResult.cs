using Auto.API.Models;
using Microsoft.AspNetCore.Mvc;

//Replace the default validation response with our custom response model for consistency
//https://learn.microsoft.com/en-us/answers/questions/1337147/how-i-can-adjust-which-fields-i-will-have-in-the-r
public class ApiResponseValidationResult : IActionResult
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var errors = context.ModelState
            .Where(kvp => kvp.Value?.Errors?.Any() == true)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
            );

        var apiResult = new ApiResponse<string>("Validation failed", false)
        {
            ValidationErrors = errors
        };

        var objectResult = new ObjectResult(apiResult) { StatusCode = 400 };
        await objectResult.ExecuteResultAsync(context);
    }
}