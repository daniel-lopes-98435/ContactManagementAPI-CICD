using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ContactManagement.Application.Dtos;

namespace ContactManagement.Api.Filters;

public class ValidateModelStateFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors
                    .Select(e => $"Invalid parameter for field '{x.Key}': {e.ErrorMessage}"))
                .ToList();
            var errorResponse = new ErrorResponse
            {
                StatusCode = 400,
                Errors = errors
            };
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}