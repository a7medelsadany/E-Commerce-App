using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.ErrorModels;

namespace E_Commerce.Factories
{
    public static class APIResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext Context)
        {
            
                var Errors = Context.ModelState.Where(e => e.Value.Errors.Any())
                .Select(e => new ValidationError()
                {
                    Filed = e.Key,
                    Errors = e.Value.Errors.Select(x => x.ErrorMessage)
                });
                var Response = new validationErrorToReturn()
                {
                    ValidationErrors = Errors
                };
                return new BadRequestObjectResult(Response);
            
        }
    }
}
