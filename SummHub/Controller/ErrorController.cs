/*********************************************************************************************************************/
// von Laszlo

using SummHub.Model;

namespace SummHub.Controller;

public class ErrorController
{
    public event EventHandler? ErrorReceived;

    private CustomException? _exception;
    
    // Raise event on setting Exception to display in UI
    public CustomException? Exception
    {
        get { return _exception; }
        set
        {
            _exception = value;
            switch (Exception.ExceptionType)
            {
                case CustomExceptionType.UserAlert:
                    Console.WriteLine($"UserAlert: {Exception.InnerException.Message}");
                    ErrorReceived.Invoke(this, EventArgs.Empty);
                    break;
                case CustomExceptionType.Warning:
                    Console.WriteLine($"Warning: {Exception.InnerException.Message}");
                    break;
                case CustomExceptionType.DevError:
                    Console.WriteLine($"DevError: {Exception.InnerException.Message}");
                    // Logging could be added here
                    break;
            }
        }
    }
}
/*********************************************************************************************************************/