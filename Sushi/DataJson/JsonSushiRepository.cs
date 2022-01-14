using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal sealed class JsonSushiRepository : IRepository<Sushi>
    {
        Logger Logger = new Logger();

        public void Create(Sushi item)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = objectJson.SushiMenu;
                model.DrinksMenu = objectJson.DrinksMenu;
                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;
                model.SushiMenu.Add(item);

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }
            else
            {
                model.SushiMenu.Add(item);

                string _jsonObject = JsonConvert.SerializeObject(model);

                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }

            Logger.Debug($"Product added to Json {item.ShowDataForAdmin()}");
        }

        public void Delete(int id)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = objectJson.SushiMenu;

                Sushi deleteSushi = model.SushiMenu.FirstOrDefault(_ => _.Id == id);

                model.SushiMenu.Remove(deleteSushi);
                model.DrinksMenu = objectJson.DrinksMenu;
                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                Logger.Debug($"Product deleted to Json {deleteSushi.ShowDataForAdmin()}");
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
            }
        }

        public Sushi GetItem(int id)
        {
            if (File.Exists(Observer.FileNameProduct))
            {
                ListProducts model = new ListProducts();

                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = objectJson.SushiMenu;

                return model.SushiMenu.FirstOrDefault(_ => _.Id == id);
            }
            else
            {
                Clear();
                WriteLine("File Products.json not found");
                Thread.Sleep(4000);

                return null;
            }
        }

        public IEnumerable<Sushi> GetItemList()
        {
            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var jsonObject = JsonConvert.DeserializeObject<ListProducts>(fileName);

                return jsonObject.SushiMenu.ToList();
            }
            else
            {
                Clear();
                WriteLine("File Products.json not found");
                Thread.Sleep(4000);

                return null;
            }
        }

        public IEnumerable<Sushi> GetItemList(string typeSushi)
        {
            var fileName = File.ReadAllText(Observer.FileNameProduct);
            var sushis = JsonConvert.DeserializeObject<ListProducts>(fileName);

            var currentSushiType = sushis.SushiMenu.FindAll(_ => _.Type == typeSushi);

            return currentSushiType;
        }

        public void Update(Sushi item)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = objectJson.SushiMenu;

                int index = model.SushiMenu.IndexOf(model.SushiMenu.FirstOrDefault(_ => _.Id == item.Id));

                model.SushiMenu[index] = item;
                model.DrinksMenu = objectJson.DrinksMenu;
                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                Logger.Debug($"Product updated to Json {item.ShowDataForAdmin()}");
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
            }
        }
    }
}
