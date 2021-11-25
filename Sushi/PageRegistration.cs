using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SushiMarcet
{
    internal class PageRegistration : Page
    {
        public PageRegistration()
        {
            _bannerPage = "Registration";
            _options = new string[] 
            {   "First name: ", 
                "Last name: ",
                "Email: ", 
                "Adress: ", 
                "Phone number: "
            };
        }

        public override void DisplayOptions()
        {
            for (int i = 0; i < _options.Length; i++)
            {
                string currentOption = _options[i];

                SetCursorPosition(65, 10 + i);

                WriteLine($"{currentOption}");

                SetCursorPosition(82,10+i);

                GetInfoUser(i, _options[i]);
                
            }
            Console.ResetColor();
        }

        void GetInfoUser(int i, string _option)
        {
            string info;

            info = ReadLine();

            if (info == String.Empty)
            {
                SetCursorPosition(65, 5 + i);
                WriteLine("Error");                
                Thread.Sleep(2000);
                Run();
                WriteLine();               
            }

            if(_option.Contains("First name: "))
            {
                FirstName = info;
            }        
        }
    }
}
