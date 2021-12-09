using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageMenuSushi : PageFather,IRepository<Sushi>
    {
        private IEnumerable<Sushi> _items;

        public PageMenuSushi()
        {
            _bannerPage = "Sushi Menu";
            _items = GetAll();
            _options = GetOptions(_items);
        }

        public  IEnumerable<Sushi> GetAll()
        {
            using  (ApplicationContext db = new ApplicationContext())
            {
                return db.Sushi.ToList();
            }
        }

        private string[] GetOptions(IEnumerable<Sushi> sushis)
        {
            string[] options = new string[sushis.Count()];

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = sushis.ElementAt(i).ToString();
            }
            return options;
        }

        protected override void TransferPage(ConsoleKey keyPressed, string[] options, int selectedIndex)
        {
            if (keyPressed == ConsoleKey.Enter)
            {
                PageOrderSushi pageOrderSushi = new PageOrderSushi(_items.ElementAt(selectedIndex));
                int _selectedIndex = pageOrderSushi.Run();
            }
        }
    }
}
