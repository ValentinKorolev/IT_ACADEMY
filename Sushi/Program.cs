using Microsoft.Data.SqlClient;

class Programm
{
    static void Main(string[] args)
    {
            Logger<Programm>.Info("The application is running");

           // DataBase.OpenningConnection();

            Bot.SayHello();
            Bot.AskNameUser();
            Bot.ShowMenu();
       
    }
}



