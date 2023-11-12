namespace Syntax_1;

class Program
{
    static void Main(string[] args)
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