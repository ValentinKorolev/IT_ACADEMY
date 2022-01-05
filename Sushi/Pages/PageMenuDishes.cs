using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal sealed class PageMenuDishes : PageFather
    {
        private readonly IEnumerable<SauceAndDishes> _dishes;
        private readonly string _goBack = "\nGo back";

        SqlDishesRepository sqlDishes = new SqlDishesRepository();

        public PageMenuDishes()
        {
            _bannerPage = "Menu Sauce And Dishes";

            _dishes = sqlDishes.GetItemList();
            _options = SetOptions(_dishes);
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
                    PageViewingProduct pageViewingProduct = new(_dishes.ElementAt(selectedIndex));
                    _ = pageViewingProduct.Run();
                    break;
            }                
        }

        private string[] SetOptions(IEnumerable<SauceAndDishes> dishes)
        {
            string[] options = new string[dishes.Count() + 1];

            for (int i = 0; i < dishes.Count(); i++)
            {
                options[i] = dishes.ElementAt(i).ToString();
            }
            options[^1] = _goBack;

            return options;
        }
    }
}
