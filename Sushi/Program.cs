

using SushiMarcet.MyLogger;
using System.Diagnostics;
using System.Reflection;

class Programm
{
    static void Main(string[] args)
    {
        MyLogger<Programm> MyLogger = new();

        try
        {
            MyLogger.Info("The application is running");

            Bot.SayHello();
            Bot.AskNameUser();
            Bot.ShowMenu();
        }
        catch (Exception ex)
        {
            Clear();
            WriteLine("Program failure, sorry :(");

            MyLogger.Error("Program failure", ex);
        }
        finally
        {
            MyLogger.Info("The application has shut down");
        }                   
    }
}



