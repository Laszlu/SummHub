namespace SummHub.Controller;

public class ErrorController
{
    public event EventHandler ErrorReceived;

    private Exception? _exception;
    
    public Exception? Exception
    {
        get { return _exception; }
        set
        {
            _exception = value;
            Console.WriteLine("error set");
            //ErrorReceived.Invoke(this, EventArgs.Empty);
        }
    }
}