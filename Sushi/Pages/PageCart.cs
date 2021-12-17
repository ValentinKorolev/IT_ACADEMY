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
            if(Cart._cartList.Count == 1)//the basket is initially not empty (contains the string for _bannerPage = "Cart\n")
            {
                _bannerPage = "Cart is empty";
                _options = new string[] {"Go back"};
            }
            else
            {
                _bannerPage = Cart.ShowTheContents();
                _options = new string[] {"Make an order", "Go back"};
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
            }
        }
    }
}
