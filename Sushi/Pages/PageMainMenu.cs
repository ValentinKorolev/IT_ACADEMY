using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    public class PageMainMenu : PageFather
    {
        public PageMainMenu()
        {
            _bannerPage = $"{Observer.nameUser} what do you want?";
            _options = new string[] { "View the menu", "Basket", "Go out" };
        }

        public PageMainMenu(string viewSushiMenu)
        {
            _bannerPage = "Menu Sushi Marcet";
            _options = new string[] { "Uramaki", "Futomaki", "Nigiri", "Baked sushi",
                                      "Sets", "Sauces and side dishes","Go Back" };
        }

        protected override void TransferPage(ConsoleKey keyPressed, string[] options, int selectedIndex)
        {
            if (keyPressed == ConsoleKey.Enter && options[selectedIndex] == "View the menu")
            {
                Clear();
                PageMainMenu page = new("View the menu");
                _ = page.Run();
            }
            else if (keyPressed == ConsoleKey.Enter && options[selectedIndex] == "Basket")
            {
                Clear();
            }
            else if (keyPressed == ConsoleKey.Enter && options[selectedIndex] == "Go out")
            {
                Clear();
            }
        }
    }
}
