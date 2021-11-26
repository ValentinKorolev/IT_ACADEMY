
using SushiMarcet;


class Programm
{
    
    
    static void Main(string[] args)
    {
        try
        {
            Logger<Programm>.Info("Программа запущенна");

            PageWelcom pageWelcom = new();
            int selectedIndex = pageWelcom.Run();
        }
        catch (Exception ex)
        {
            Logger<Programm>.Error("Программа не запустилась",ex);
        }
        
    }
}



