using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal sealed class PageViewingSushi : PageFather
    {
        private readonly Sushi _currentSushi;

        public PageViewingSushi(Sushi sushi)
        {
            Observer.currentTypeSushi = sushi.Type;
            _currentSushi = sushi;
            _bannerPage = sushi.ShowData();

            _options = new string[] { "Number of servings", "Add to cart", "Go back" };
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "Number of servings":

                    bool _isTrue;
                    int numServings;

                    do
                    {
                        Clear();
                        WriteLine("Enter the desired number of servings from the keyboard (use numbers):");
                        _isTrue = int.TryParse(ReadLine(), out numServings);
                    }
                    while (_isTrue == false || numServings == default);

                    _currentSushi.Servings = numServings;

                    PageViewingSushi pageOrder = new(_currentSushi);
                    _ = pageOrder.Run();

                    break;

                case "Add to cart":

                    Cart.Add(_currentSushi);

                    Clear();
                    WriteLine("Sushi added to cart");
                    Thread.Sleep(2000);

                    PageMenuSushi pageMenuSushi = new(Observer.currentTypeSushi);
                    _ = pageMenuSushi.Run();

                    break;

                case "Go back":
                    PageMenuSushi pageMenuSushi1 = new(Observer.currentTypeSushi);
                    _ = pageMenuSushi1.Run();
                    break;
            }
        }
    }
}
