namespace SummHub.Model;

public enum CustomExceptionType
{
    Warning = 1,
    UserAlert = 2,
    DevError = 3
}

public class CustomException
{
    public CustomExceptionType ExceptionType { get; set; }

    public Exception InnerException { get; set; }

    public CustomException(string message, CustomExceptionType type) 
    {
        ExceptionType = type;
        InnerException = new(message);
    }

    public CustomException(Exception ex, CustomExceptionType type)
    {
        ExceptionType = type;
        InnerException = ex;
    }
}