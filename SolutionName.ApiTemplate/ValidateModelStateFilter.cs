using SolutionName.Common.Exception;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionName.ApiTemplate
{
    public class ValidateModelStateFilter : IAsyncActionFilter
    {
        //This filter method will get executed on each controller action.
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                //Creates a string stating which properties were invalid, and what their error was
                //message example value:
                //The field CauseId must be between 1 and 2147483647. | The field ActivityId must be between 1 and 2147483647.
                var message = string.Join(" | ", context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => !string.IsNullOrEmpty(e.ErrorMessage) ? e.ErrorMessage : e.Exception.Message));

                //Exception message thrown exampple:
                //Model error in action: NovoSevenAtHome.API.Controllers.BleedingController.CreateBleeding (NovoSevenAtHome.API). Error was: The field CauseId must be between 1 and 2147483647. | The field ActivityId must be between 1 and 2147483647.
                throw new ApiException(ExceptionType.InvalidPropertyValue, "Model error in action: " + context.ActionDescriptor.DisplayName + ". Error was: " + message);
            }
            await next();
        }
    }
}
