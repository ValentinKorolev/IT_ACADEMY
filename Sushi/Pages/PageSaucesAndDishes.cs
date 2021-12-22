using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageSaucesAndDishes : PageFather
    {
        IEnumerable<SauceAndDishes> _dishes;
        public PageSaucesAndDishes()
        {
            _bannerPage = "Sauces and side dishes";
        }
    }
}
