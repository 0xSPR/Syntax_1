namespace Syntax_1;

public class Error : Exception
{
    public Error(string message, StreamWriter log) : base(message)
    {
        log.WriteLine("Error " + message);
    }

    public Error(string message) : base(message)
    {
    }
}