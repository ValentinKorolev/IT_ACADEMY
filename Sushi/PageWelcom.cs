using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SushiMarcet
{
    internal class PageWelcom : Page
    {
        //string _welcom = "Welcom Sushi Master";
        string _menu = "Menu";
        string _registration = "Registration";
        string _exit = "Exit";
        public PageWelcom()
        {
            _options = new string[] { _menu, _registration, _exit };
            _bannerPage = "Welcom Sushi Master";
        }
    }
}
