using Microsoft.Data.SqlClient;

class Programm
{
    static void Main(string[] args)
    {
        try
        {
            Logger<Programm>.Info("The application is running");

            Bot.SayHello();
            Bot.AskNameUser();
            Bot.ShowMenu();
        }
        catch (Exception ex)
        {
            Logger<Programm>.Error(" ", ex.InnerException);
        }
        finally
        {
            Logger<Programm>.Info("The application has shut down");
        }                   
    }
}



