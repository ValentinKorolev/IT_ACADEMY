using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal sealed class PageAdmin : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        private int _infoId;
        private string _type;
        private string _name;
        private decimal _price;
        private string _description;

        public PageAdmin(string name, string pass)
        {
            if (NameAdmin != name || PassAdmin != pass)
            {
                Clear();
                Bot.SayHello();
                Bot.AskNameUser();
                Bot.ShowMenu();
            }
            else
            {
                _bannerPage = "Administrator Page";

                
                _options = new string[] {"1.Orders",
                                         "2.Sushi",
                                         "3.Sauces and side dishes",
                                         "4.Drinks",
                                         "5.Support"};

            }

        }

        public PageAdmin(string currentOptions)
        {
            if (currentOptions == "2.Sushi")
            {
                _bannerPage = "Administrator Page(Sushi)";

                _options = new string[] {"0.View list sushi",
                                         "1.Add sushi",
                                         "2.Update sushi",
                                         "3.Delete sushi",
                                         "Go back",
                                        };
            }
            else if (currentOptions == "3.Sauces and side dishes")
            {
                _bannerPage = "Administrator Page(Sauces and side dishes - SASD)";

                _options = new string[] {"0.View list SASD",
                                         "1.Add SASD",
                                         "2.Update SASD",
                                         "3.Delete SASD",
                                         "Go back",
                                        };
            }
            else if (currentOptions == "4.Drinks")
            {
                _bannerPage = "Administrator Page(Drinks)";

                _options = new string[] {"0.View list drinks",
                                         "1.Add drinks",
                                         "2.Update drinks",
                                         "3.Delete drinks",
                                         "Go back",
                                        };
            }
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "1.Orders":
                    break;
                case "2.Sushi":
                    PageAdminRun("2.Sushi");
                    break;
                case "3.Sauces and side dishes":
                    PageAdminRun("3.Sauces and side dishes");
                    break;
                case "4.Drinks":
                    PageAdminRun("4.Drinks");
                    break;
                case "5.Support":
                    break;
                case "Go back":
                    BackToPageAdmin();
                    break;
                case "0.View list sushi":
                    ViewListSushi();
                    break;
                case "1.Add sushi":
                    AddSushi();
                    PageAdminRun("2.Sushi");
                    break;
                case "2.Update sushi":
                    UpdateSushi();
                    break;
                case "3.Delete sushi":
                    DeleteSushi();
                    break;
                case "0.View list drinks":
                    ViewListDrinks();
                    break;
                case "1.Add drinks":
                    AddDrinks();
                    PageAdminRun("4.Drinks");
                    break;
                case "2.Update drinks":
                    UpdateDrinks();
                    break;
                case "3.Delete drinks":
                    DeleteDrinks();
                    break;
                case "0.View list SASD":
                    ViewListDishes();
                    break;
                case "1.Add SASD":
                    AddDishes();
                    PageAdminRun("3.Sauces and side dishes");
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
                    //DeleteDishesDb(sushiId);
                    DeleteDishesJson(_dishesId);
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("3.Sauces and side dishes");
        }

        private void DeleteDishesJson(int dishesId)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SauceAndDishesMenu= objectJson.SauceAndDishesMenu;

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
                    PageAdminRun("3.Sauces and side dishes");
                }
                else
                {
                    Clear();
                    WriteLine($"There is NO dishes with this Id - {dishesId}");
                    Thread.Sleep(3000);
                    PageAdminRun("3.Sauces and side dishes");
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminRun("3.Sauces and side dishes");
            }
        }

        private void DeleteDishesDb(int dishesId)
        {

        }

        //Update Dishes
        private void UpdateDishes()
        {
            int _dishesId;

            do
            {
                Clear();
                WriteLine("Update Sushi\n");
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

                    //UpdateDishesDb(_dishesId);
                    UpdateDishesJson(_dishesId);
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("3.Sauces and side dishes");
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
                    PageAdminRun("3.Sauces and side dishes");
                }
                else
                {
                    Clear();
                    WriteLine($"There is NO dishe with this Id - {dishesId}");
                    Thread.Sleep(3000);
                    PageAdminRun("3.Sauces and side dishes");
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminRun("2.Sushi");
            }
        }

        private void UpdateDishesDb(int dishesId)
        {

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
            using ApplicationContext db = new ApplicationContext();

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

        //View Dishes
        private void ViewListDishes()
        {
            //ViewListDishesFromDb();
            ViewListDishesFromJson();
        }

        private void ViewListDishesFromJson()
        {
            do
            {
                Clear();
                WriteLine("List Sauces and side dishes (Press ESC to go back)");
                WriteLine();

                if (File.Exists(Observer.FileNameProduct))
                {
                    var fileName = File.ReadAllText(Observer.FileNameProduct);
                    var jsonObject = JsonConvert.DeserializeObject<ListProducts>(fileName);

                    foreach (var product in jsonObject.SauceAndDishesMenu)
                    {
                        WriteLine(product.ShowDataForAdmin());
                        WriteLine();
                    }

                    ConsoleKeyInfo keyInfo = ReadKey(true);
                    keyPressed = keyInfo.Key;
                }
                else
                {
                    WriteLine();
                    WriteLine("Dishes not found");
                    Thread.Sleep(2000);
                    keyPressed = ConsoleKey.Escape;
                }
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("3.Sauces and side dishes");
        }

        private void ViewListDishesFromDb()
        {

        }       


        //CRUD with Sushi

        //View Sushi
        private void ViewListSushi()
        {
            //ViewSushiFromDB();
            ViewSushiFromJson();            
        }

        private void ViewSushiFromJson()
        {
            do
            {
                Clear();
                WriteLine("List Sushi (Press ESC to go back)");
                WriteLine();

                if (File.Exists(Observer.FileNameProduct))
                {
                    var fileName = File.ReadAllText(Observer.FileNameProduct);
                    var jsonObject = JsonConvert.DeserializeObject<ListProducts>(fileName);

                    foreach (var product in jsonObject.SushiMenu)
                    {
                        WriteLine(product.ShowDataForAdmin());
                        WriteLine();
                    }

                    ConsoleKeyInfo keyInfo = ReadKey(true);
                    keyPressed = keyInfo.Key;
                }
                else
                {
                    WriteLine();
                    WriteLine("Sushi not found");
                    Thread.Sleep(2000);
                    keyPressed = ConsoleKey.Escape;
                }
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("2.Sushi");
        }

        private void ViewSushiFromDB()
        {

        }

        //Add Sushi
        private void AddSushi()
        {
            do
            {
                Clear();
                WriteLine("Add Sushi\nDo you want to continue?(Press ESC to go back)");
                

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();

                    WriteLine("Enter Id Sushi: ");
                    _infoId = Convert.ToInt32(ReadLine());

                    WriteLine("Enter Type Sushi: ");
                    _type = ReadLine();

                    WriteLine("Enter Name Sushi: ");
                    _name = ReadLine();

                    WriteLine("Enter Price Sushi: ");
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Sushi (200 symbol): ");
                    _description = ReadLine();

                    Sushi sushi = new(_infoId, _type, _name, _price, _description);

                    // AddSushiToDB should be the first
                    AddSushiToDB(sushi);

                    AddSushiToJson(sushi);

                    WriteLine($"Sushi: {sushi.ShowDataForAdmin()} - ADDED");
                }

            } while (keyPressed != ConsoleKey.Escape);            
        }

        private void AddSushiToJson(Sushi sushi)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var sushiJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = sushiJson.SushiMenu;
                model.DrinksMenu = sushiJson.DrinksMenu;
                model.SauceAndDishesMenu = sushiJson.SauceAndDishesMenu;
                model.SushiMenu.Add(sushi);

                File.Delete(Observer.FileNameProduct);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }
            else
            {
                model.SushiMenu.Add(sushi);

                string _jsonObject = JsonConvert.SerializeObject(model);

                File.AppendAllText(Observer.FileNameProduct, _jsonObject);
            }            
        }

        private void AddSushiToDB(Sushi sushi)
        {
            using ApplicationContext db = new ApplicationContext();

            try
            {
                db.Sushi.Add(sushi);
                db.SaveChanges();
                Logger<PageAdmin>.Debug($"Admin added sushi: {sushi.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                Logger<PageAdmin>.Error("Read InnerException", ex.InnerException);
                WriteLine("Error, please look logs!");
                Thread.Sleep(10000);
            }
        }

        //Update Sushi
        private void UpdateSushi()
        {
            int sushiId;

            do
            {
                Clear();
                WriteLine("Update Sushi\n");
                Write("Enter the Id of the sushi you want to update: ");

                bool isId = int.TryParse(ReadLine(), out sushiId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (isId == true && keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Enter Type Sushi: ");
                    _type = ReadLine();

                    WriteLine("Enter Name Sushi: ");
                    _name = ReadLine();

                    WriteLine("Enter Price Sushi: ");
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Sushi (200 symbol): ");
                    _description = ReadLine();

                    //UpdateSushiDb(int sushiId);
                    UpdateSushiJson(sushiId);
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("2.Sushi");
        }

        private void UpdateSushiJson(int sushiId)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var sushiJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = sushiJson.SushiMenu;

                Sushi updateSushi = model.SushiMenu.FirstOrDefault(_ => _.Id == sushiId);

                if (updateSushi != null)
                {
                    
                    Sushi newSushi = new(sushiId, _type, _name, _price, _description);

                    int index = model.SushiMenu.IndexOf(model.SushiMenu.FirstOrDefault( _ => _.Id == sushiId));
                    model.SushiMenu[index] = newSushi;
                    model.DrinksMenu = sushiJson.DrinksMenu;
                    model.SauceAndDishesMenu = sushiJson.SauceAndDishesMenu;

                    File.Delete(Observer.FileNameProduct);

                    string _jsonObject = JsonConvert.SerializeObject(model);
                    File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                    Clear();
                    WriteLine($"Sushi with Id - {sushiId} UPDATE");
                    Thread.Sleep(3000);
                    PageAdminRun("2.Sushi");
                }
                else
                {
                    Clear();
                    WriteLine($"There is NO sushi with this Id - {sushiId}");
                    Thread.Sleep(3000);
                    PageAdminRun("2.Sushi");
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminRun("2.Sushi");
            }
        }

        private void UpdateSushiDb(int sushiId)
        {

        }

        //Delete Sushi
        private void DeleteSushi()
        {
            int sushiId;

            do
            {
                Clear();
                WriteLine("Delete Sushi\n");
                Write("Enter the Id of the sushi you want to delete: ");

                bool isId = int.TryParse(ReadLine(), out sushiId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (isId == true && keyPressed != ConsoleKey.Escape)
                {
                    //DeleteSushiDb(sushiId);
                    DeleteSushiJson(sushiId);
                }
                else if(keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }
                
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("2.Sushi");

        }

        private void DeleteSushiDb(int sushiId)
        {

        }

        private void DeleteSushiJson(int sushiId)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameProduct))
            {
                var fileName = File.ReadAllText(Observer.FileNameProduct);
                var objectJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = objectJson.SushiMenu;

                Sushi deleteSushi = model.SushiMenu.FirstOrDefault(_ => _.Id == sushiId);

                if(deleteSushi != null)
                {
                    model.SushiMenu.Remove(deleteSushi);
                    model.DrinksMenu = objectJson.DrinksMenu;
                    model.SauceAndDishesMenu = objectJson.SauceAndDishesMenu;

                    File.Delete(Observer.FileNameProduct);

                    string _jsonObject = JsonConvert.SerializeObject(model);
                    File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                    Clear();
                    WriteLine($"Sushi with Id - {sushiId} DELETE");
                    Thread.Sleep(3000);
                    PageAdminRun("2.Sushi");
                }
                else
                {
                    Clear();
                    WriteLine($"There is NO sushi with this Id - {sushiId}");
                    Thread.Sleep(3000);
                    PageAdminRun("2.Sushi");
                }                
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminRun("2.Sushi");
            }

        }


        //CRUD with Drinks

        //View Drinks
        private void ViewListDrinks()
        {
            //ViewDrinksFromDb();
            ViewDrinksFromJson();
        }

        private void ViewDrinksFromJson()
        {
            do
            {
                Clear();
                WriteLine("List Drinks (Press ESC to go back)");
                WriteLine();

                if (File.Exists(Observer.FileNameProduct))
                {
                    var fileName = File.ReadAllText(Observer.FileNameProduct);
                    var jsonObject = JsonConvert.DeserializeObject<ListProducts>(fileName);

                    foreach (var product in jsonObject.DrinksMenu)
                    {
                        WriteLine(product.ShowDataForAdmin());
                        WriteLine();
                    }

                    ConsoleKeyInfo keyInfo = ReadKey(true);
                    keyPressed = keyInfo.Key;
                }
                else
                {
                    WriteLine();
                    WriteLine("Drinks not found");
                    Thread.Sleep(2000);
                    keyPressed = ConsoleKey.Escape;
                }
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("4.Drinks");
        }

        private void ViewDrinksFromDb()
        {

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

                    Drinks drink = new(_infoId, _name,  _price, _description);

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
            using ApplicationContext db = new ApplicationContext();

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

                    //UpdateDrinksDb(sushiId);
                    UpdateDrinksJson(drinkId);
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("4.Drinks");
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
                    PageAdminRun("4.Drinks");
                }
                else
                {
                    Clear();
                    WriteLine($"There is NO drink with this Id - {drinkId}");
                    Thread.Sleep(3000);
                    PageAdminRun("4.Drinks");
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminRun("4.Drinks");
            }
        }

        private void UpdateDrinksDb(int drinkId)
        {

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
                    //DeleteDrinkDb(drinkId);
                    DeleteDrinkJson(drinkId);
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("4.Drinks");
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
                    PageAdminRun("4.Drinks");
                }
                else
                {
                    Clear();
                    WriteLine($"There is NO drink with this Id - {drinkId}");
                    Thread.Sleep(3000);
                    PageAdminRun("4.Drinks");
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminRun("4.Drinks");
            }
        }

        private void DeleteDrinkDb()
        {

        }

        //Other methods

        private void BackToPageAdmin()
        {
            PageAdmin pageAdmin = new(NameAdmin, PassAdmin);
            _ = pageAdmin.Run();
        }

        private void PageAdminRun(string currentOption)
        {
            PageAdmin pageAdmin = new(currentOption);
            _ = pageAdmin.Run();
        }
    }
}
