using System;
using System.Collections.Generic;
using Cln.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cln.Web.Filters.ExceptionFilters
{
    public static class ExceptionToStateCodeMapper
    {
        public static IActionResult ToStatusCode(this Exception exception, ModelStateDictionary modelState)
        {
            if (exception is ModelValidationException)
            {
                modelState.AddModelError((ModelValidationException)exception);

                return new BadRequestObjectResult(modelState);
            }
            else if (exception is KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            else if (exception is ArgumentNullException)
            {
                return new BadRequestObjectResult("Argument cannot be null");
            }
            else if (exception is InvalidOperationException)
            {
                return new BadRequestObjectResult("Invlid Operation");
            }
            else
            {
                throw exception;
            }
        }
    }
}
