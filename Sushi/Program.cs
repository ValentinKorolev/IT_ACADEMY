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

        }    
        
        
    }
}



