using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageMenuDrinks : PageFather
    {
        private readonly IEnumerable<Drinks> _drinks;
        private readonly string _goBack = "\nGo back";

        SqlDrinksRepository sqlDrinks = new SqlDrinksRepository();

        public PageMenuDrinks()
        {
            _bannerPage = "Menu Drinks";

            _drinks = sqlDrinks.GetItemList();
            _options = SetOptions(_drinks);
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "\nGo back":
                    PageMainMenu pageMainMenu = new PageMainMenu("View the menu");
                    _ = pageMainMenu.Run();
                    break;
                default:
                    PageViewingProduct pageViewingProduct = new(_drinks.ElementAt(selectedIndex));
                    _ = pageViewingProduct.Run();
                    break;
            }
        }

        private string[] SetOptions(IEnumerable<Drinks> drinks)
        {
            string[] options = new string[drinks.Count() + 1];

            for (int i = 0; i < drinks.Count(); i++)
            {
                options[i] = drinks.ElementAt(i).ToString();
            }
            options[^1] = _goBack;

            return options;
        }
    }
}
