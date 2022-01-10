
class Programm
{
    static void Main(string[] args)
    {
        try
        {

            new Logger().Info("The application is running");

            Bot.SayHello();
            Bot.AskNameUser();
            Bot.ShowMenu();
        }
        catch (Exception ex)
        {
            new Logger().Error("The application is running", ex);

            Clear();
            WriteLine("Program failure, sorry :(");
        }
        finally
        {
            new Logger().Info("The application has shut down");
        }                   
    }
}



