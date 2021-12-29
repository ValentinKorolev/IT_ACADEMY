using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageAdminDishes : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        private int _infoId;
        private string _name;
        private decimal _price;
        private string _description;

        public PageAdminDishes()
        {
            _bannerPage = "Administrator Page(Sauces and side dishes - SASD)";

            _options = new string[] {"0.View list SASD",
                                         "1.Add SASD",
                                         "2.Update SASD",
                                         "3.Delete SASD",
                                         "Go back",
                                        };
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "Go back":
                    BackToPageAdmin();
                    break;
                case "0.View list SASD":
                    ViewListDishes();
                    break;
                case "1.Add SASD":
                    AddDishes();
                    PageAdminDishesRun();
                    break;
                case "2.Update SASD":
                    UpdateDishes();
                    break;
                case "3.Delete SASD":
                    DeleteDishes();
                    break;
            }
        }

        //CRUD Sauce and side dishes

        //Delete Dishes
        private void DeleteDishes()
        {
            int _dishesId;

            do
            {
                Clear();
                WriteLine("Delete Dishes\n");
                Write("Enter the Id of the dishes you want to delete: ");

                bool isId = int.TryParse(ReadLine(), out _dishesId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (isId == true && keyPressed != ConsoleKey.Escape)
                {
                    DeleteDishesDb(_dishesId);
                    DeleteDishesJson(_dishesId);
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDishesRun();
        }

        private void DeleteDishesJson(int dishesId)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                SauceAndDishes deleteDishes = model.SauceAndDishesMenu.FirstOrDefault(_ => _.Id == dishesId);

                if (deleteDishes != null)
                {
                    model.SauceAndDishesMenu.Remove(deleteDishes);
                    model.DrinksMenu = objectJson.DrinksMenu;
                    model.SushiMenu = objectJson.SushiMenu;

                    File.Delete(Observer.FileNameProduct);

                    string _jsonObject = JsonConvert.SerializeObject(model);
                    File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                    Clear();
                    WriteLine($"Dishes with Id - {dishesId} DELETE");
                    Thread.Sleep(3000);
                    PageAdminDishesRun();
                }
                else
                {
                    Clear();
                    WriteLine($"Dish with Id - ({dishesId}) NOT FOUND in file json");
                    Thread.Sleep(3000);
                    PageAdminDishesRun();
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminDishesRun();
            }
        }

        private void DeleteDishesDb(int dishesId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    SauceAndDishes deleteDish = db.SauceAndDishes.FirstOrDefault(_ => _.Id == dishesId);

                    db.SauceAndDishes.Remove(deleteDish);
                    db.SaveChanges();
                }

            }catch (Exception ex)
            {
                Clear();
                WriteLine($"Dish with Id - ({dishesId}) NOT FOUND in DataBase");
                Thread.Sleep(3000);
            }            
        }

        //Update Dishes
        private void UpdateDishes()
        {
            int _dishesId;

            do
            {
                Clear();
                WriteLine("Update SASD\n");
                Write("Enter the Id of the sushi you want to update: ");

                bool isId = int.TryParse(ReadLine(), out _dishesId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (isId == true && keyPressed != ConsoleKey.Escape)
                {
                    Clear();

                    WriteLine("Enter Name Dishes: ");
                    _name = ReadLine();

                    WriteLine("Enter Price Dishes: ");
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Dishes (500 symbol): ");
                    _description = ReadLine();

                    UpdateDishesDb(_dishesId);
                    UpdateDishesJson(_dishesId);
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDishesRun();
        }

        private void UpdateDishesJson(int dishesId)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var dishesJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SauceAndDishesMenu = dishesJson.SauceAndDishesMenu;

                SauceAndDishes updateDishes = model.SauceAndDishesMenu.FirstOrDefault(_ => _.Id == dishesId);

                if (updateDishes != null)
                {

                    SauceAndDishes newDishes = new(dishesId, _name, _price, _description);

                    int index = model.SauceAndDishesMenu.IndexOf(model.SauceAndDishesMenu.FirstOrDefault(_ => _.Id == dishesId));
                    model.SauceAndDishesMenu[index] = newDishes;
                    model.DrinksMenu = dishesJson.DrinksMenu;
                    model.SushiMenu = dishesJson.SushiMenu;

                    File.Delete(Observer.FileNameProduct);

                    string _jsonObject = JsonConvert.SerializeObject(model);
                    File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                    Clear();
                    WriteLine($"Dishes with Id - {dishesId} UPDATE");
                    Thread.Sleep(3000);
                    PageAdminDishesRun();
                }
                else
                {
                    Clear();
                    WriteLine($"Dish with Id - ({dishesId}) NOT FOUND in file json");
                    Thread.Sleep(3000);
                    PageAdminDishesRun();
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminDishesRun();
            }
        }

        private void UpdateDishesDb(int dishesId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    SauceAndDishes updateDishes = db.SauceAndDishes.FirstOrDefault(_ => _.Id == dishesId);
                    updateDishes.Name = _name;
                    updateDishes.Price = _price;
                    updateDishes.Description = _description;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Clear();
                WriteLine($"Dish with Id - ({dishesId}) NOT FOUND in DataBase");
                Thread.Sleep(3000);
            }

        }

        //Add Dishes
        private void AddDishes()
        {
            do
            {
                Clear();
                WriteLine("Add Dishes\nDo you want to continue?(Press ESC to go back)");


                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();

                    WriteLine("Enter Id Dishes: ");
                    _infoId = Convert.ToInt32(ReadLine());

                    WriteLine("Enter Name Dishes: ");
                    _name = ReadLine();

                    WriteLine("Enter Price Dishes: ");
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Dishes (500 symbol): ");
                    _description = ReadLine();

                    SauceAndDishes dishes = new(_infoId, _name, _price, _description);

                    // AddDishesDb should be the first
                    AddDishesDb(dishes);
                    AddDishesJson(dishes);

                    WriteLine($"Dishes: {dishes.ShowDataForAdmin()} - ADDED");
                }

            } while (keyPressed != ConsoleKey.Escape);

        }

        private void AddDishesJson(SauceAndDishes dishes)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var dishesJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = dishesJson.SushiMenu;
                model.DrinksMenu = dishesJson.DrinksMenu;
                model.SauceAndDishesMenu = dishesJson.SauceAndDishesMenu;
                model.SauceAndDishesMenu.Add(dishes);

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }
            else
            {
                model.SauceAndDishesMenu.Add(dishes);

                string _jsonObject = JsonConvert.SerializeObject(model);

                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }
        }

        private void AddDishesDb(SauceAndDishes dishes)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    db.SauceAndDishes.Add(dishes);
                    db.SaveChanges();
                    Logger<PageAdmin>.Debug($"Admin added dishes: {dishes.ShowDataForAdmin()}");
                }
                catch (Exception ex)
                {
                    Logger<PageAdmin>.Error("Read InnerException", ex.InnerException);
                    WriteLine("Error, please look logs!");
                    Thread.Sleep(10000);
                }
            }
        }

        //View Dishes
        private void ViewListDishes()
        {
            do
            {
                Clear();
                WriteLine("List Sauces and side dishes (Press ESC to go back)");
                WriteLine();

                ViewListDishesFromDb();
                //ViewListDishesFromJson();
                
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;                
               
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDishesRun();
        }

        private void ViewListDishesFromJson()
        {


            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var jsonObject = JsonConvert.DeserializeObject<ListProducts>(fileName);

                foreach (var product in jsonObject.SauceAndDishesMenu)
                {
                    WriteLine(product.ShowDataForAdmin());
                    WriteLine();
                }
            }
            else
            {
                Clear();
                WriteLine("Dishes not found");
                Thread.Sleep(4000);

                PageAdminDishesRun();
            }
        }

        private void ViewListDishesFromDb()
        {
            List<SauceAndDishes> dishes = new List<SauceAndDishes>();

            using (ApplicationContext db = new ApplicationContext())
            {
                dishes = db.SauceAndDishes.ToList();

                foreach (var dish in dishes)
                {
                    WriteLine(dish.ShowDataForAdmin());
                    WriteLine();
                }
            }
        }

        private void BackToPageAdmin()
        {
            PageAdmin pageAdmin = new(NameAdmin, PassAdmin);
            _ = pageAdmin.Run();
        }

        private void PageAdminDishesRun()
        {
            PageAdminDishes pageAdminDishes = new();
            _ = pageAdminDishes.Run();
        }
    }
}
