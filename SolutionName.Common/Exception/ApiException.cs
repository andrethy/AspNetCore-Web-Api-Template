using System;

namespace SolutionName.Common.Exception
{
    //A custom made exception to support the ExceptionType enum.
    public class ApiException : ArgumentException
    {
        public ExceptionType ErrorType;
        public new string Message;

        public ApiException(ExceptionType errorType, string message = "")
        {
            ErrorType = errorType;
            Message = message;
        }
    }
}
