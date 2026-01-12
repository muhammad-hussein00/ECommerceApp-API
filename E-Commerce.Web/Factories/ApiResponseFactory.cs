using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState.Where(x => x.Value!.Errors.Count > 0)
            .ToDictionary(x => x.Key, x => x.Value!.Errors.Select(x => x.ErrorMessage).ToArray());

            var problem = new ProblemDetails()
            {
                Title = "Validation errors",
                Status = StatusCodes.Status400BadRequest,
                Detail = "One or more validation error occured",
                Extensions = { {"Errors", errors } },
            };

            return new BadRequestObjectResult(problem);
        }
    }
}