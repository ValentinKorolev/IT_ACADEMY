using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageAdminDrinks : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        private int _infoId;
        private string _name;
        private decimal _price;
        private string _description;

        public PageAdminDrinks()
        {

            _bannerPage = "Administrator Page(Drinks)";

            _options = new string[] {"0.View list drinks",
                                         "1.Add drinks",
                                         "2.Update drinks",
                                         "3.Delete drinks",
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
                case "0.View list drinks":
                    ViewListDrinks();
                    break;
                case "1.Add drinks":
                    AddDrinks();
                    PageAdminDrinksRun();
                    break;
                case "2.Update drinks":
                    UpdateDrinks();
                    break;
                case "3.Delete drinks":
                    DeleteDrinks();
                    break;
            }
        }

        //CRUD with Drinks

        //View Drinks
        private void ViewListDrinks()
        {
            do
            {
                Clear();
                WriteLine("List Drinks (Press ESC to go back)");
                WriteLine();

                //ViewDrinksFromDb();
                ViewDrinksFromJson();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;                    
                
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDrinksRun();
        }

        private void ViewDrinksFromJson()
        {
                if (File.Exists(Observer.FileNameProduct))
                {
                    var fileName = File.ReadAllText(Observer.FileNameProduct);
                    var jsonObject = JsonConvert.DeserializeObject<ListProducts>(fileName);

                    foreach (var product in jsonObject.DrinksMenu)
                    {
                        WriteLine(product.ShowDataForAdmin());
                        WriteLine();
                    }
                }
                else
                {
                    WriteLine();
                    WriteLine("Drinks not found");
                    Thread.Sleep(2000);
                    PageAdminDrinksRun();
                }    
        }

        private void ViewDrinksFromDb()
        {
            List<Drinks> drinks = new List<Drinks>();

            using (ApplicationContext db = new ApplicationContext())
            {
                drinks = db.Drinks.ToList();

                foreach (var drink in drinks)
                {
                    WriteLine(drink.ShowDataForAdmin());
                    WriteLine();
                }
            }
        }

        //Add Drinks
        private void AddDrinks()
        {
            do
            {
                Clear();
                WriteLine("Add Drink\nDo you want to continue?(Press ESC to go back)");


                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();

                    WriteLine("Enter Id Drink: ");
                    _infoId = Convert.ToInt32(ReadLine());

                    WriteLine("Enter Name Drink: ");
                    _name = ReadLine();

                    WriteLine("Enter Price Drink: ");
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Drink (200 symbol): ");
                    _description = ReadLine();

                    Drinks drink = new(_infoId, _name, _price, _description);

                    // AddDrinkToDb should be the first
                    AddDrinkToDb(drink);
                    AddDrinkToJson(drink);

                    WriteLine($"Drink: {drink.ShowDataForAdmin()} - ADDED");
                }

            } while (keyPressed != ConsoleKey.Escape);

        }

        private void AddDrinkToJson(Drinks drinks)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var drinkJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = drinkJson.SushiMenu;
                model.DrinksMenu = drinkJson.DrinksMenu;
                model.SauceAndDishesMenu = drinkJson.SauceAndDishesMenu;

                model.DrinksMenu.Add(drinks);

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }
            else
            {
                model.DrinksMenu.Add(drinks);

                string _jsonObject = JsonConvert.SerializeObject(model);

                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }
        }

        private void AddDrinkToDb(Drinks drinks)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    db.Drinks.Add(drinks);
                    db.SaveChanges();
                    Logger<PageAdmin>.Debug($"Admin added drink: {drinks.ShowDataForAdmin}");
                }
                catch (Exception ex)
                {
                    Logger<PageAdmin>.Error("Read InnerException", ex.InnerException);
                    WriteLine("Error, please look logs!");
                    Thread.Sleep(10000);
                }
            }
        }

        //Update Drinks
        private void UpdateDrinks()
        {
            int drinkId;

            do
            {
                Clear();
                WriteLine("Update Drinks\n");
                Write("Enter the Id of the drink you want to update: ");

                bool isId = int.TryParse(ReadLine(), out drinkId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (isId == true && keyPressed != ConsoleKey.Escape)
                {
                    WriteLine("Enter Name Drink: ");
                    _name = ReadLine();

                    WriteLine("Enter Price Drink: ");
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Drink (200 symbol): ");
                    _description = ReadLine();

                    UpdateDrinksDb(drinkId);
                    UpdateDrinksJson(drinkId);
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDrinksRun();
        }

        private void UpdateDrinksJson(int drinkId)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.DrinksMenu = objectJson.DrinksMenu;

                Drinks updateDrink = model.DrinksMenu.FirstOrDefault(_ => _.Id == drinkId);

                if (updateDrink != null)
                {

                    Drinks newDrink = new(drinkId, _name, _price, _description);

                    int index = model.DrinksMenu.IndexOf(model.DrinksMenu.FirstOrDefault(_ => _.Id == drinkId));
                    model.DrinksMenu[index] = newDrink;

                    model.SushiMenu = objectJson.SushiMenu;
                    model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                    File.Delete(Observer.FileNameProduct);

                    string _jsonObject = JsonConvert.SerializeObject(model);
                    File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                    Clear();
                    WriteLine($"Drink with Id - {drinkId} UPDATE");
                    Thread.Sleep(3000);
                    PageAdminDrinksRun();
                }
                else
                {
                    Clear();
                    WriteLine($"Drink with Id - ({drinkId}) NOT FOUND in file json");
                    Thread.Sleep(3000);
                    PageAdminDrinksRun();
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminDrinksRun();
            }
        }

        private void UpdateDrinksDb(int drinkId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Drinks updateDrink = db.Drinks.FirstOrDefault(_ => _.Id == drinkId);
                    updateDrink.Name = _name;
                    updateDrink.Price = _price;
                    updateDrink.Description = _description;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Clear();
                WriteLine($"Drink with Id - ({drinkId}) NOT FOUND in DataBase");
                Thread.Sleep(3000);
            }
        }

        //Delete Drinks
        private void DeleteDrinks()
        {
            int drinkId;

            do
            {
                Clear();
                WriteLine("Delete Drinks\n");
                Write("Enter the Id of the drink you want to delete: ");

                bool isId = int.TryParse(ReadLine(), out drinkId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (isId == true && keyPressed != ConsoleKey.Escape)
                {
                    DeleteDrinkDb(drinkId);
                    DeleteDrinkJson(drinkId);
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDrinksRun();
        }

        private void DeleteDrinkJson(int drinkId)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.DrinksMenu = objectJson.DrinksMenu;

                Drinks deleteDrink = model.DrinksMenu.FirstOrDefault(_ => _.Id == drinkId);

                if (deleteDrink != null)
                {
                    model.DrinksMenu.Remove(deleteDrink);
                    model.SushiMenu = objectJson.SushiMenu;
                    model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                    File.Delete(Observer.FileNameProduct);

                    string _jsonObject = JsonConvert.SerializeObject(model);
                    File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                    Clear();
                    WriteLine($"Drink with Id - {drinkId} DELETE");
                    Thread.Sleep(3000);
                    PageAdminDrinksRun();
                }
                else
                {
                    Clear();
                    WriteLine($"Drink with Id - ({drinkId}) NOT FOUND in file json");
                    Thread.Sleep(3000);
                    PageAdminDrinksRun();
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminDrinksRun();
            }
        }

        private void DeleteDrinkDb(int drinkId)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Drinks deleteDrink = db.Drinks.FirstOrDefault(_ => _.Id == drinkId);

                    db.Drinks.Remove(deleteDrink);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Clear();
                WriteLine($"Drink with Id - ({drinkId}) NOT FOUND in DataBase");
                Thread.Sleep(3000);
            }            
        }

        //Other methods

        private void BackToPageAdmin()
        {
            PageAdmin pageAdmin = new(NameAdmin, PassAdmin);
            _ = pageAdmin.Run();
        }

        private void PageAdminDrinksRun()
        {
            PageAdminDrinks pageAdminDrinks = new PageAdminDrinks();
            _ = pageAdminDrinks.Run();
        }

    }
}   


