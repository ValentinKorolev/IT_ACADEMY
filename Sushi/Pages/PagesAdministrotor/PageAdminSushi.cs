using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageAdminSushi : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        private int _infoId;
        private string _type;
        private string _name;
        private decimal _price;
        private string _description;

        public PageAdminSushi()
        {
            _bannerPage = "Administrator Page(Sushi)";

            _options = new string[] {"0.View list sushi",
                                         "1.Add sushi",
                                         "2.Update sushi",
                                         "3.Delete sushi",
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
                case "0.View list sushi":
                    ViewListSushi();
                    break;
                case "1.Add sushi":
                    AddSushi();
                    PageAdminSushiRun();
                    break;
                case "2.Update sushi":
                    UpdateSushi();
                    break;
                case "3.Delete sushi":
                    DeleteSushi();
                    break;               
            }
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

            PageAdminSushiRun();
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

            PageAdminSushiRun();
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

                    int index = model.SushiMenu.IndexOf(model.SushiMenu.FirstOrDefault(_ => _.Id == sushiId));
                    model.SushiMenu[index] = newSushi;
                    model.DrinksMenu = sushiJson.DrinksMenu;
                    model.SauceAndDishesMenu = sushiJson.SauceAndDishesMenu;

                    File.Delete(Observer.FileNameProduct);

                    string _jsonObject = JsonConvert.SerializeObject(model);
                    File.AppendAllText(Observer.FileNameProduct, _jsonObject);

                    Clear();
                    WriteLine($"Sushi with Id - {sushiId} UPDATE");
                    Thread.Sleep(3000);
                    PageAdminSushiRun();
                }
                else
                {
                    Clear();
                    WriteLine($"There is NO sushi with this Id - {sushiId}");
                    Thread.Sleep(3000);
                    PageAdminSushiRun();
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminSushiRun();
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
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine("Incorrect input");
                    Thread.Sleep(2000);
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminSushiRun();

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

                if (deleteSushi != null)
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
                    PageAdminSushiRun();
                }
                else
                {
                    Clear();
                    WriteLine($"There is NO sushi with this Id - {sushiId}");
                    Thread.Sleep(3000);
                    PageAdminSushiRun();
                }
            }
            else
            {
                Clear();
                WriteLine("File Products.json NOT FOUND");
                Thread.Sleep(3000);
                PageAdminSushiRun();
            }

        }

        private void PageAdminSushiRun()
        {
            PageAdminSushi pageAdminSushi = new();
            _ = pageAdminSushi.Run();
        }

        private void BackToPageAdmin()
        {
            PageAdmin pageAdmin = new(NameAdmin, PassAdmin);
            _ = pageAdmin.Run();
        }
    }
}
