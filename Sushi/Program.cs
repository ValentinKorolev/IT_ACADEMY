using SushiMarcet.Exceptions;

class Programm
{
    static void Main(string[] args)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            new Logger().Info("The application is running");

            Bot.SayHello();
            Bot.AskNameUser();
            Bot.ShowMenu();
        }
        catch (ApplicationMyException ex)
        {
            ex = new("Program failure",ex);
            new Logger().Error("An unexpected situation has occurred", ex);

            Clear();
            WriteLine("Program failure, sorry :(");
        }
        finally
        {
            new Logger().Info("The application has shut down");
        }                   
    }
}



