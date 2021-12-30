using Newtonsoft.Json;


namespace SushiMarcet.DataJson
{
    internal class JsonDishesRepository : IRepository<SauceAndDishes>
    {
        public void Create(SauceAndDishes item)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = objectJson.SushiMenu;
                model.DrinksMenu = objectJson.DrinksMenu;
                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;
                model.SauceAndDishesMenu.Add(item);

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }
            else
            {
                model.SauceAndDishesMenu.Add(item);

                string _jsonObject = JsonConvert.SerializeObject(model);

                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }
        }

        public void Delete(int id)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                SauceAndDishes deleteDish = model.SauceAndDishesMenu.FirstOrDefault(_ => _.Id == id);

                model.SushiMenu = objectJson.SushiMenu;
                model.SauceAndDishesMenu.Remove(deleteDish);
                model.DrinksMenu = objectJson.DrinksMenu;

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);

            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
            }
        }

        public SauceAndDishes GetItem(int id)
        {
            if (File.Exists(Observer.FileNameProduct))
            {
                ListProducts model = new ListProducts();

                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                return model.SauceAndDishesMenu.FirstOrDefault(_ => _.Id == id);
            }
            else
            {
                Clear();
                WriteLine("File Products.json not found");
                Thread.Sleep(4000);

                return null;
            }
        }

        public IEnumerable<SauceAndDishes> GetItemList()
        {
            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var jsonObject = JsonConvert.DeserializeObject<ListProducts>(fileName);

                return jsonObject.SauceAndDishesMenu.ToList();
            }
            else
            {
                Clear();
                WriteLine("File Products.json not found");
                Thread.Sleep(4000);

                return null;
            }
        }

        public void Update(SauceAndDishes item)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                int index = model.SauceAndDishesMenu.IndexOf(model.SauceAndDishesMenu.FirstOrDefault(_ => _.Id == item.Id));

                model.SauceAndDishesMenu[index] = item;
                model.SushiMenu = objectJson.SushiMenu;
                model.DrinksMenu = objectJson.DrinksMenu;

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);

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
