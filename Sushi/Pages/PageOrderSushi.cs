using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageOrderSushi : PageFather
    {
        public PageOrderSushi(Sushi sushi)
        {
            _bannerPage = sushi.ToString();

            _options = new string[] { "Add Basket", "Go back" };
        }
    }
}
