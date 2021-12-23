using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageMenuDishes : PageFather
    {
        private readonly IEnumerable<SauceAndDishes> _dishes;
        private readonly string _goBack = "\nGo back";

        public PageMenuDishes()
        {
            _bannerPage = "Menu Sauce And Dishes";

            _dishes = GetDishes();
            _options = SetDishes(_dishes);
        }

        private string[] SetDishes(IEnumerable<SauceAndDishes> dishes)
        {
            string[] options = new string[dishes.Count() + 1];

            for (int i = 0; i < dishes.Count(); i++)
            {
                options[i] = dishes.ElementAt(i).ToString();
            }
            options[^1] = _goBack;

            return options;
        }

        private IEnumerable<SauceAndDishes> GetDishes()
        {
            try
            {
                using ApplicationContext db = new ApplicationContext();
                return db.SauceAndDishes.ToList();
            }
            catch (Exception ex)
            {
                return GetDishesJson();
            }
        }

        private IEnumerable<SauceAndDishes> GetDishesJson()
        {
            var fileName = File.ReadAllText(Observer.FileNameProduct);

            var jsonObject = JsonConvert.DeserializeObject<ListProducts>(fileName);
            
            return jsonObject.SauceAndDishesMenu;
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
                    break;
            }
                
        }
    }
}
