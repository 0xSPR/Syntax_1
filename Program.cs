namespace Syntax_1;

class Program
{
    // ReSharper disable once UnusedParameter.Local
    static void Main()
    {
        try
        {
            using Language l = new Language();
            l.Program();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error " + e.Message);
        }
    }
}