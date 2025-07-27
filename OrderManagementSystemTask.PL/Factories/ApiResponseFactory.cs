using Microsoft.AspNetCore.Mvc;
using OrderManagementSystemTask.BLL.Dtos.ErrorDtos;
using System.Net;

namespace OrderManagementSystemTask.PL.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(error => error.Value.Errors.Any()).Select(error =>
            new ValidationError
            {
                Field = error.Key,
                Errors = error.Value.Errors.Select(e => e.ErrorMessage)
            });
            var response = new ValidationErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "Validation error",
                Errors = errors
            };
            return new BadRequestObjectResult(response);
        }
    }
}
