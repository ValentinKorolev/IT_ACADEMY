using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal sealed class PageMenuSushi : PageFather/*,IGetSushFromDb<Sushi>*/
    {
        private readonly IEnumerable<Sushi> _sushis;
        private readonly string _goBack = "\nGo back";

        SqlSushiRepository sqlSushi = new SqlSushiRepository();

        public PageMenuSushi(string sushiType)
        {
            _bannerPage = "Sushi Menu";

            switch (sushiType)
            {
                case Observer.Uramaki:
                    _sushis = sqlSushi.GetSushi(Observer.Uramaki);
                    break;
                case Observer.Futomaki:
                    _sushis = sqlSushi.GetSushi(Observer.Futomaki);
                    break;
                case Observer.Nigiri:
                    _sushis = sqlSushi.GetSushi(Observer.Nigiri);
                    break;
                case Observer.BakedSushi:
                    _sushis = sqlSushi.GetSushi(Observer.BakedSushi);
                    break;
            }

            _options = SetOptions(_sushis);
        }

        private string[] SetOptions(IEnumerable<Sushi> sushis)
        {
            string[] options = new string[sushis.Count() + 1];

            for (int i = 0; i < options.Length - 1; i++)
            {
                options[i] = sushis.ElementAt(i).ShowData();
            }
            options[^1] = _goBack;

            return options;
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "\nGo back":
                    PageMainMenu page = new PageMainMenu("View the menu");
                    _ = page.Run();
                    break;
                default:
                    PageViewingProduct pageViewingProduct = new(_sushis.ElementAt(selectedIndex));
                    _ = pageViewingProduct.Run();
                    break;
            }
        }
    }
}
