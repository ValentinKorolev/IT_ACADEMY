using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SushiMarcet
{
    internal class PageMenu : Page
    {
        public PageMenu()
        {
            _bannerPage = "Menu";
            _options = new string[] { "test" };
        }
    }
}
