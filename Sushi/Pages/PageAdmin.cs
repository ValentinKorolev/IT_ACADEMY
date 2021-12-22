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
        private const string Name = "Admin123";
        private const string Pass = "122345";

        private int _infoId;
        private string _type;
        private string _nameSushi;
        private decimal _price;
        private string _description;

        public PageAdmin(string name, string pass)
        {
            if (Name != name || Pass != pass)
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
                    BackToPageAdmin();
                    break;
                case "2.Update sushi":
                    UpdateSushi();
                    break;
                case "3.Delete sushi":
                    DeleteSushi();
                    break;
                
                

            }
        }

        private void PageAdminRun(string currentOption)
        {
            PageAdmin pageAdmin = new(currentOption);
            _ = pageAdmin.Run();
        }

        private void ViewListSushi()
        {
            ViewSushiFromJson();            
        }

        private void ViewSushiFromJson()
        {
            do
            {
                Clear();
                WriteLine("List Sushi (Press ESC to go back)");
                WriteLine();

                if (File.Exists(Observer.FileNameSushi))
                {
                    var fileName = File.ReadAllText(Observer.FileNameSushi);
                    var sushis = JsonConvert.DeserializeObject<ListProducts>(fileName);

                    foreach (var product in sushis.SushiMenu)
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
                }
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("2.Sushi");
        }

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
                    _nameSushi = ReadLine();

                    WriteLine("Enter Price Sushi: ");
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Sushi (200 symbol): ");
                    _description = ReadLine();

                    Sushi sushi = new(_infoId, _type, _nameSushi, _price, _description);

                    // AddSushiToDB should be the first
                    AddSushiToDB(sushi);

                    AddSushiToJson(sushi);

                    WriteLine($"Sushi: {sushi.ShowDataForAdmin} - ADDED");
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminRun("2.Sushi");
        }

        private void AddSushiToJson(Sushi sushi)
        {
            ListProducts model = new ListProducts();

            if (File.Exists(Observer.FileNameSushi))
            {
                var fileName = File.ReadAllText(Observer.FileNameSushi);
                var sushiJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = sushiJson.SushiMenu;
                model.SushiMenu.Add(sushi);

                File.Delete(Observer.FileNameSushi);

                string _jsonObject = JsonConvert.SerializeObject(model);
                File.AppendAllText(Observer.FileNameSushi, _jsonObject);
            }
            else
            {
                model.SushiMenu.Add(sushi);

                string _jsonObject = JsonConvert.SerializeObject(model);

                File.AppendAllText(Observer.FileNameSushi, _jsonObject);
            }            
        }

        private void AddSushiToDB(Sushi sushi)
        {
            using ApplicationContext db = new ApplicationContext();

            try
            {
                db.Sushi.Add(sushi);
                db.SaveChanges();
                Logger<PageAdmin>.Debug($"Admin added sushi: {sushi}");
            }
            catch (Exception ex)
            {
                Logger<PageAdmin>.Error("Read InnerException", ex.InnerException);
                WriteLine("Error, please look logs!");
                Thread.Sleep(10000);
            }
        }

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

            if (File.Exists(Observer.FileNameSushi))
            {
                var fileName = File.ReadAllText(Observer.FileNameSushi);
                var sushiJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = sushiJson.SushiMenu;

                Sushi updateSushi = model.SushiMenu.FirstOrDefault(_ => _.Id == sushiId);

                if (updateSushi != null)
                {
                    Clear();
                    WriteLine("Enter Type Sushi: ");
                    _type = ReadLine();

                    WriteLine("Enter Name Sushi: ");
                    _nameSushi = ReadLine();

                    WriteLine("Enter Price Sushi: ");
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Sushi (200 symbol): ");
                    _description = ReadLine();

                    Sushi newSushi = new(sushiId, _type, _nameSushi, _price, _description);

                    int index = model.SushiMenu.IndexOf(model.SushiMenu.FirstOrDefault( _ => _.Id == sushiId));
                    model.SushiMenu[index] = newSushi;


                    File.Delete(Observer.FileNameSushi);

                    string _jsonObject = JsonConvert.SerializeObject(model);
                    File.AppendAllText(Observer.FileNameSushi, _jsonObject);

                    Clear();
                    WriteLine($"Sushi with Id - {sushiId} UPDATE");
                    Thread.Sleep(3000);
                    PageAdminRun("2.Sushi");
                }
                else
                {
                    Clear();
                    WriteLine($"There is no sushi with this Id - {sushiId}");
                    Thread.Sleep(3000);
                    PageAdminRun("2.Sushi");
                }

            }
        }

        private void UpdateSushiDb(int sushiId)
        {

        }

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

            if (File.Exists(Observer.FileNameSushi))
            {
                var fileName = File.ReadAllText(Observer.FileNameSushi);
                var sushiJson = JsonConvert.DeserializeObject<ListProducts>(fileName);

                model.SushiMenu = sushiJson.SushiMenu;

                Sushi deleteSushi = model.SushiMenu.FirstOrDefault(_ => _.Id == sushiId);

                if(deleteSushi != null)
                {
                    model.SushiMenu.Remove(deleteSushi);

                    File.Delete(Observer.FileNameSushi);

                    string _jsonObject = JsonConvert.SerializeObject(model);
                    File.AppendAllText(Observer.FileNameSushi, _jsonObject);

                    Clear();
                    WriteLine($"Sushi with Id - {sushiId} DELETE");
                    Thread.Sleep(3000);
                    PageAdminRun("2.Sushi");
                }
                else
                {
                    Clear();
                    WriteLine($"There is no sushi with this Id - {sushiId}");
                    Thread.Sleep(3000);
                    PageAdminRun("2.Sushi");
                }
                
            }
        }


        private void BackToPageAdmin()
        {
            PageAdmin pageAdmin = new(Name, Pass);
            _ = pageAdmin.Run();
        }
    }
}
