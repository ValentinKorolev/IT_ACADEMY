using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal sealed class PageMenuSushi : PageFather,IGetSushi<Sushi>
    {
        private readonly IEnumerable<Sushi> _sushis;
        private readonly string _goBack = "\nGo back";

        public PageMenuSushi(string sushiType)
        {
            _bannerPage = "Sushi Menu";

            switch (sushiType)
            {
                case Observer.Uramaki:
                    _sushis = GetAllUramaki();
                    break;
                case Observer.Futomaki:
                    _sushis = GetAllFutomaki();
                    break;
                case Observer.Nigiri:
                    _sushis = GetAllNigiri();
                    break;
                case Observer.BakedSushi:
                    _sushis = GetAllBakedSushi();
                    break;
            }

            _options = SetOptions(_sushis);
        }

        public IEnumerable<Sushi> GetAllUramaki()
        {
            try
            {
                var sushi = GetSushiJson(Observer.Uramaki);
                return sushi;
                
            }
            catch (Exception ex)
            {
                using ApplicationContext db = new ApplicationContext();
                return db.Sushi.Where(_ => _.Type.Contains(Observer.Uramaki)).ToList();
            }            
        }

        public IEnumerable<Sushi> GetAllFutomaki()
        {

            try
            {
                var sushi = GetSushiJson(Observer.Futomaki);
                return sushi;

            }
            catch (Exception ex)
            {
                using ApplicationContext db = new ApplicationContext();
                return db.Sushi.Where(_ => _.Type.Contains(Observer.Futomaki)).ToList();
            }
        }

        public IEnumerable<Sushi> GetAllNigiri()
        {
            try
            {
                var sushi = GetSushiJson(Observer.Nigiri);
                return sushi;

            }
            catch (Exception ex)
            {
                using ApplicationContext db = new ApplicationContext();
                return db.Sushi.Where(_ => _.Type.Contains(Observer.Nigiri)).ToList();
            }
        }

        public IEnumerable<Sushi> GetAllBakedSushi()
        {
            try
            {
                var sushi = GetSushiJson(Observer.BakedSushi);
                return sushi;

            }
            catch (Exception ex)
            {
                using ApplicationContext db = new ApplicationContext();
                return db.Sushi.Where(_ => _.Type.Contains(Observer.BakedSushi)).ToList();
            }
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
                    PageMainMenu pageMainMenu = new();
                    _ = pageMainMenu.Run();
                    break;
                default:
                    PageViewingProduct pageViewingProduct = new(_sushis.ElementAt(selectedIndex));
                    _ = pageViewingProduct.Run();
                    break;
            }
        }

        private IEnumerable<Sushi> GetSushiJson(string typeSushi)
        {
            var fileName = File.ReadAllText(Observer.FileNameProduct);
            var sushis = JsonConvert.DeserializeObject<ListProducts>(fileName);
            var currentSushiType = sushis.SushiMenu.FindAll(_ => _.Type == typeSushi);
                         
            return currentSushiType;
        }
    }
}
