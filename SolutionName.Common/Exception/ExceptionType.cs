namespace SolutionName.Common.Exception
{
    //An enum with all the exceptions to throw.
    //Those accessing the API need to have a list of all the integers and their meaning
    public enum ExceptionType
    {
        InternalError = 1,
        InvalidPropertyValue = 10,
        AuthInvalidUsername = 20,
        AuthInvalidPassword = 21,
    }
}
