using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal sealed class PageCart : PageFather
    {
      
        public PageCart()
        {
            if(Cart.cartList.Count == 0)
            {
                _bannerPage = "Cart is empty";
                _options = new string[] {"Go back"};
            }
            else
            {
                _bannerPage = "Cart\n\n" + Cart.ShowTheContents() + Cart.ReturnOrderAmount();
                _options = new string[] {"Make an order", "Make changes", "Go back" };
            }            
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "Go back":
                    PageMainMenu pageMainMenu = new();
                    _ = pageMainMenu.Run();
                    break;
                case "Make an order":
                    PageOrder pageOrder = new();
                    _ = pageOrder.Run();
                    break;
                case "Make changes":
                    PageCartChanges pageCartChanges = new();
                    _ = pageCartChanges.Run();
                    break;
            }
        }
    }
}
