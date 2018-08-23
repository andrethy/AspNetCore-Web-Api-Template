using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SolutionName.Common.Exception;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SolutionName.ApiTemplate.Error
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
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
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //Default error will be internal error, unless the error is known
            var exceptionMessage = exception.Message + exception.StackTrace;
            var result = JsonConvert.SerializeObject(new { error = ExceptionType.InternalError, errorMessage = exceptionMessage });
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            if (exception is ApiException apiException)
            {
                //The error is known, and we can change the exceptiontype to what is given in apiException
                exceptionMessage = apiException.Message;
                result = JsonConvert.SerializeObject(new { error = apiException.ErrorType, errorMessage = exceptionMessage });

                if (apiException.ErrorType == ExceptionType.InvalidPropertyValue)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }
            }

            logger.LogError(exceptionMessage);
            return context.Response.WriteAsync(result);
        }
    }
}
