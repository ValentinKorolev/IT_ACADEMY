using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal sealed class PageViewingProduct : PageFather
    {
        Logger Logger = new Logger();

        private SauceAndDishes? _currentDishes;
        private Sushi? _currentSushi;
        private Drinks? _currentDrinks;

        public PageViewingProduct(Sushi sushi)
        {
            _currentSushi = sushi;
            _bannerPage = _currentSushi.ShowData();
            _options = new string[] { "Number of servings", "Add to cart", "Go back" };
        }

        public PageViewingProduct(SauceAndDishes dishes)
        {
            _currentDishes = dishes;
            _bannerPage = _currentDishes.ShowData();
            _options = new string[] { "Number of servings", "Add to cart", "Go back" };
        }

        public PageViewingProduct(Drinks drinks)
        {
            _currentDrinks = drinks;
            _bannerPage = _currentDrinks.ShowData();
            _options = new string[] { "Number of servings", "Add to cart", "Go back" };
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "Number of servings":
                    int numServings = FindOutNumberOfServings();
                    PageViewingProductWithNewServings(numServings);
                    break;
                case "Add to cart":
                    AddingProductToCart();
                    break;
                case "Go back":
                    GoBack();
                    break;
            }
        }

        private static int FindOutNumberOfServings()
        {
            bool _isTrue;
            int numServings;

            do
            {
                Clear();
                WriteLine("Enter the desired number of servings from the keyboard (use numbers):");
                _isTrue = int.TryParse(ReadLine(), out numServings);
            }
            while (_isTrue == false || numServings == default);

            return numServings;
        }

        private void PageViewingProductWithNewServings(int numServings)
        {
            if(_currentSushi is not null)
            {
                _currentSushi.Servings = numServings;

                PageViewingProduct pageViewingProduct = new(_currentSushi);
                _currentSushi = null;
                _ = pageViewingProduct.Run();
            }
            else if (_currentDishes is not null)
            {
                _currentDishes.Servings = numServings;

                PageViewingProduct pageViewingProduct = new(_currentDishes);
                _currentDishes = null;
                _ = pageViewingProduct.Run();
            }
            else if (_currentDrinks is not null)
            {
                _currentDrinks.Servings = numServings;

                PageViewingProduct pageViewingProduct = new(_currentDrinks);

                _currentDrinks = null;

                _ = pageViewingProduct.Run();
            }
        }

        private void AddingProductToCart()
        {
            if(_currentSushi is not null)
            {
                Cart.Add(_currentSushi);

                Logger.Debug($"The user {Observer.nameUser} added to cart  {_currentSushi}");

                Clear();
                WriteLine($"{_currentSushi.Name} added to cart");
                Thread.Sleep(2000);

                PageMenuSushiRun(_currentSushi.Type);
            }
            else if(_currentDishes is not null)
            {
                Cart.Add(_currentDishes);

                Logger.Debug($"The user {Observer.nameUser} added to cart  {_currentDishes}");

                Clear();
                WriteLine($"{_currentDishes.Name} added to cart");
                Thread.Sleep(2000);

                PageMenuDishesRun();
            }
            else if (_currentDrinks is not null)
            {
                Cart.Add(_currentDrinks);

                Logger.Debug($"The user {Observer.nameUser} added to cart  {_currentDrinks}");

                Clear();
                WriteLine($"{_currentDrinks.Name} added to cart");
                Thread.Sleep(2000);

                PageMenuDrinksRun();
            }
        }

        private void GoBack()
        {
            if(_currentSushi is not null)
            {
                PageMenuSushiRun(_currentSushi.Type);
            }
            else if (_currentDishes is not null)
            {
                PageMenuDishesRun();
            }
            else if (_currentDrinks is not null)
            {
                PageMenuDrinksRun();
            }
        }

        private void PageMenuSushiRun(string typeSushi)
        {
            PageMenuSushi pageMenuSushi = new(typeSushi);
            _ = pageMenuSushi.Run();
        }

        private void PageMenuDishesRun()
        {
            PageMenuDishes pageMenuDishes = new();
            _ = pageMenuDishes.Run();
        }

        private void PageMenuDrinksRun()
        {
            PageMenuDrinks pageMenuDrinks = new();
            _ = pageMenuDrinks.Run();
        }

    }
}
