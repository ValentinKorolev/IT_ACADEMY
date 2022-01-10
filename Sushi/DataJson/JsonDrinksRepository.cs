using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.DataJson
{
    internal sealed class JsonDrinksRepository : IRepository<Drinks>
    {
        Logger Logger = new Logger();

        public void Create(Drinks item)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = objectJson.SushiMenu;
                model.DrinksMenu = objectJson.DrinksMenu;
                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;
                model.DrinksMenu.Add(item);

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }
            else
            {
                model.DrinksMenu.Add(item);

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

                model.DrinksMenu = objectJson.DrinksMenu;

                Drinks deleteDrinks = model.DrinksMenu.FirstOrDefault(_ => _.Id == id);

                model.SushiMenu = objectJson.SushiMenu;
                model.DrinksMenu.Remove(deleteDrinks);
                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                Logger.Debug($"Product deleted to Json {deleteDrinks.ShowDataForAdmin()}");
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
            }
        }

        public Drinks GetItem(int id)
        {
            if (File.Exists(Observer.FileNameProduct))
            {
                ListProducts model = new ListProducts();

                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.DrinksMenu = objectJson.DrinksMenu;

                return model.DrinksMenu.FirstOrDefault(_ => _.Id == id);
            }
            else
            {
                Clear();
                WriteLine("File Products.json not found");
                Thread.Sleep(4000);

                return null;
            }
        }

        public IEnumerable<Drinks> GetItemList()
        {
            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var jsonObject = JsonConvert.DeserializeObject<ListProducts>(fileName);

                return jsonObject.DrinksMenu.ToList();
            }
            else
            {
                Clear();
                WriteLine("File Products.json not found");
                Thread.Sleep(4000);

                return null;
            }
        }

        public void Update(Drinks item)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.DrinksMenu = objectJson.DrinksMenu;

                int index = model.DrinksMenu.IndexOf(model.DrinksMenu.FirstOrDefault(_ => _.Id == item.Id));

                model.DrinksMenu[index] = item;
                model.SushiMenu = objectJson.SushiMenu;
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
