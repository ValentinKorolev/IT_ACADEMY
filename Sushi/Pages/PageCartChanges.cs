

using System.Reflection;

namespace SushiMarcet.Pages
{
    internal sealed class PageCartChanges : PageFather
    {
        private const string _goBack = "\nGo back";
        private readonly object? _currentProduct;
        public PageCartChanges()
        {
            _bannerPage = "Select the product you want to make changes to";
            _options = SetOptions();            
        }

        public PageCartChanges(object product)
        {
            _currentProduct = product;
            _bannerPage = product.ToString();
            _options = new string[] { "Change number of servings", "Remove from the cart", _goBack};
        }

        private string[] SetOptions()
        {
            string[] option = new string [Cart.cartList.Count + 1];

            for (int i = 0; i < Cart.cartList.Count; i++)
            {
                option[i] = Cart.cartList.ElementAt(i).ToString();
            }

            option[^1] = _goBack;

            return option;
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case _goBack:
                    PageCart pageCart = new ();
                    _ = pageCart.Run();
                    break;
                case "Change number of servings":
                    ChangesServings(_currentProduct);                 
                    break;
                case "Remove from the cart":
                    RemoveFromCart(_currentProduct);
                    break;
                default:
                    PageCartChanges page = new(Cart.cartList.ElementAt(selectedIndex));
                    _ = page.Run();
                    break;
            }
        }

        private void ChangesServings(object product)
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
            
            Type type = product.GetType();
            PropertyInfo info = type.GetProperty("Servings");
            info?.SetValue(product, numServings);

            Clear();
            WriteLine($"The number of servings has been changed to {numServings}");
            Thread.Sleep(2000);

            PageCartChanges pageCartChanges = new PageCartChanges(_currentProduct);
            _ = pageCartChanges.Run();
        }

        private void RemoveFromCart(object product)
        {
            Cart.cartList.Remove(product);

            Clear();
            WriteLine("The product has been removed from the cart");
            Thread.Sleep(2000);

            PageCartChanges pageCartChanges = new();
            _ = pageCartChanges.Run();
        }
    }
}
