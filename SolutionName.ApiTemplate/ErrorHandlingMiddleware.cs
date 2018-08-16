using SolutionName.Common.Exception;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SolutionName.ApiTemplate
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        //This method will get called whenever an exception has been trown asynchronously
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //Default error will be internal error, unless the error is known
            var result = JsonConvert.SerializeObject(new { error = ExceptionType.InternalError });
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            if (exception is ApiException apiException)
            {
                //The error is known, and we can change the exceptiontype to what is given in apiException
                result = JsonConvert.SerializeObject(new { error = apiException.ErrorType });

                if (apiException.ErrorType == ExceptionType.InvalidPropertyValue)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }
            }
            return context.Response.WriteAsync(result);
        }
    }
}
