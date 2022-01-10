using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    public sealed class PageMainMenu : PageFather
    {
        public PageMainMenu()
        {
            if (Observer.nameUser != string.Empty)
                _bannerPage = $"{Observer.nameUser} what do you want?";
            else
                _bannerPage = "What do you want?";

            _options = new string[] { "View the menu", "Cart","Change color", "Go out" };
        }

        public PageMainMenu(string viewSushiMenu)
        {
            _bannerPage = "Menu Sushi Marcet";
            _options = new string[] { $"{Observer.Uramaki}",
                                      $"{Observer.Futomaki}", 
                                      $"{Observer.Nigiri}",
                                      $"{Observer.BakedSushi}",
                                      "Drinks",
                                      "Sauces and side dishes",
                                      "\nGo Back" };
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "View the menu":                    
                    PageMainMenu page = new("View the menu");
                    _ = page.Run();
                    break;
                case Observer.Uramaki:
                    PageMenuSushi pageUramaki = new(Observer.Uramaki);
                    _ = pageUramaki.Run();
                    break;
                case Observer.Futomaki:
                    PageMenuSushi pageFutomaki = new(Observer.Futomaki);
                    _ = pageFutomaki.Run();
                    break;
                case Observer.Nigiri:
                    PageMenuSushi pageNigiri = new(Observer.Nigiri);
                    _ = pageNigiri.Run();
                    break;
                case Observer.BakedSushi:
                    PageMenuSushi pageBakedSushi = new(Observer.BakedSushi);
                    _ = pageBakedSushi.Run();
                    break;
                case "Sauces and side dishes":
                    PageMenuDishes pageDishes = new();
                    _ = pageDishes.Run();
                    break;
                case "Drinks":
                    PageMenuDrinks pageMenuDrinks = new();
                    _ = pageMenuDrinks.Run();
                    break;
                case "\nGo Back":
                    PageMainMenu pageMainMenu = new();
                    _ = pageMainMenu.Run();
                    break;
                case "Cart":
                    PageCart pageBasket = new();
                    _ = pageBasket.Run();
                    break;
                case "Go out":
                    Clear();
                    WriteLine("<<Good Bye :)>>");
                    break;
                case "Change color":
                    PageChangeColor pageChangeColor = new();
                    _ = pageChangeColor.Run();
                    break;
            }
        }
    }
}
