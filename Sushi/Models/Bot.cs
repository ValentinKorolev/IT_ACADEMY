using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet
{
    public static class Bot
    {
        private static readonly TimeSpan currentTime = DateTime.Now.TimeOfDay;
        private static readonly TimeSpan _timeMorning = new(0, 9, 0, 0);
        private static readonly TimeSpan _timeDay = new(0, 12, 0, 0);
        private static readonly TimeSpan _timeEvening = new(0, 17, 0, 0);
        private static readonly TimeSpan _timeNight = new(0, 22, 0, 0);

        private static ConsoleKeyInfo keyInfo;
        private static ConsoleKey keyPressed;

        public static void SayHello()
        {
            switch (DateTime.Now)
            {
                case DateTime when ((currentTime >= _timeMorning) && (currentTime < _timeDay)):
                    WriteLine("Good morning! Welcom to Sushi Marcet!!!");
                    break;
                case DateTime when ((currentTime >= _timeDay) && (currentTime < _timeEvening)):
                    WriteLine("Good day! Welcom to Sushi Marcet!!!");
                    break;
                case DateTime when ((currentTime >= _timeEvening) && (currentTime < _timeNight)):
                    WriteLine("Good evening! Welcom to Sushi Marcet!!!");
                    break;
                default: 
                    WriteLine("Good Night! Welcom to Sushi Marcet!!!");
                    break;
            }


            Thread.Sleep(1000);
            WriteLine("Please, press any key to continue ...");

            keyInfo = ReadKey();
            keyPressed = keyInfo.Key;

            PageAdminRun(keyPressed);
        }

        public static void AskNameUser()
        {
            Clear();

            WriteLine("What is your name?");

            Observer.nameUser = ReadLine(); 
        }

        public static void ShowMenu()
        {
            PageMainMenu menu = new PageMainMenu();
            _ = menu.Run();
        }

        public static void PageAdminRun(ConsoleKey keyPressed)
        {
            Clear ();

            if (keyPressed == ConsoleKey.F12)
            {
                string _name = "Admin123";
                string _pass = "122345";
                PageAdmin admin = new PageAdmin( _name, _pass);
                _ = admin.Run();

                //WriteLine("name: ");
                //string _name = ReadLine();
                //WriteLine("pass: ");
                //string _pass = ReadLine();

                //PageAdmin admin = new PageAdmin(_name,_pass);
                //_ = admin.Run();
            }
        }
    }
}
