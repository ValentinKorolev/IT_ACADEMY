using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet
{
    public static class Bot
    {
        public static TimeSpan currentTime = DateTime.Now.TimeOfDay;
        static TimeSpan _timeMorning = new(0, 9, 0, 0);
        static TimeSpan _timeDay = new(0, 12, 0, 0);
        static TimeSpan _timeEvening = new(0, 17, 0, 0);
        static TimeSpan _timeNight = new(0, 22, 0, 0);

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
            Thread.Sleep(2000);
        }

        public static void AskNameUser()
        {
            Clear();

            WriteLine("What is your name?");

            Observer.nameUser = ReadLine();            
        }

        public static void ShowMenu()
        {
            Menu menu = new Menu();

            int _selectedIndex = menu.Run(); 
        }

        //private static void ExitText(string str)
        //{
        //    char[] charString = str.ToCharArray();

        //    for (int i = 0; i <= charString.Length-1; i++)
        //    {
        //        Write(charString[i]);
        //        Thread.Sleep(50);
        //    }
        //}
    }
}
