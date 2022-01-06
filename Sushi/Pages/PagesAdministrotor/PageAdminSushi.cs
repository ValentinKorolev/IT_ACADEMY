
using System.ComponentModel.DataAnnotations;

namespace SushiMarcet.Pages
{
    internal sealed class PageAdminSushi : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        SqlSushiRepository sqlSushi = new SqlSushiRepository();
        JsonSushiRepository jsonSushi = new JsonSushiRepository();

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
            List<Sushi> listSushi = new List<Sushi>();

            listSushi = (List<Sushi>)sqlSushi.GetItemList();
            //listSushi = (List<Sushi>)jsonSushi.GetItemList();

            sqlSushi.Dispose();

            do
            {
                Clear();
                WriteLine("List Sushi (Press ESC to go back)");
                WriteLine();

                foreach(var sushi in listSushi)
                {
                    WriteLine(sushi.ShowDataForAdmin());
                    WriteLine();
                }
                

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
              
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminSushiRun();
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
                    _name = ReadLine();

                    WriteLine("Enter Price Sushi: ");
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Sushi (200 symbol): ");
                    _description = ReadLine();

                    Sushi sushi = new(_infoId, _type, _name, _price, _description);

                    ValidateSushiAndCreate(sushi);

                    Clear();
                    WriteLine($"Sushi: {sushi.ShowDataForAdmin()} - ADDED");
                    Thread.Sleep(3000);
                }

            } while (keyPressed != ConsoleKey.Escape);
        }

        private void UpdateSushi()
        {
            int sushiId;

            do
            {
                Clear();
                WriteLine("Update Sushi\n");
                Write("Enter the Id of the sushi you want to update: ");

                _ = int.TryParse(ReadLine(), out sushiId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                bool isSushi = CheckProduct(sushiId);

                if (isSushi && keyPressed != ConsoleKey.Escape)
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

                    Sushi updateSushi = new(sushiId, _type, _name, _price, _description);

                    ValidateSushiAndUpdate(updateSushi);

                    Clear();
                    WriteLine($"Sushi with Id - {sushiId} UPDATE");
                    Thread.Sleep(3000);

                    PageAdminSushiRun();
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine($"Incorrect input or sushi with Id - ({sushiId}) NOT FOUND");
                    Thread.Sleep(4000);

                    PageAdminSushiRun();
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminSushiRun();
        }

        private void DeleteSushi()
        {
            int sushiId;

            do
            {
                Clear();
                WriteLine("Delete Sushi\n");
                Write("Enter the Id of the sushi you want to delete: ");

                _ = int.TryParse(ReadLine(), out sushiId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                bool isSushi = CheckProduct(sushiId);

                if (isSushi && keyPressed != ConsoleKey.Escape)
                {
                    //Delete Sushi Db 
                    sqlSushi.Delete(sushiId);
                    sqlSushi.Dispose();

                    //Delete Sushi Json
                    jsonSushi.Delete(sushiId);

                    Clear();
                    WriteLine($"Sushi with Id - {sushiId} DELETE");
                    Thread.Sleep(3000);

                    PageAdminSushiRun();
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine($"Incorrect input or sushi with Id - ({sushiId}) NOT FOUND");
                    Thread.Sleep(4000);

                    PageAdminSushiRun();
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminSushiRun();

        }

        private void ValidateSushiAndCreate(Sushi sushi)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(sushi);

            if (!Validator.TryValidateObject(sushi, context, result, true))
            {
                Clear();

                foreach (var error in result)
                {

                    WriteLine($"{error} - incorrect input or not all fields are required! The sushi is not made.");
                    Thread.Sleep(4000);

                    PageAdminSushiRun();
                }
            }
            else
            {
                //Create in Db sushi
                sqlSushi.Create(sushi);
                sqlSushi.Dispose();

                //Create in Json sushi
                jsonSushi.Create(sushi);
            }
        }

        private void ValidateSushiAndUpdate(Sushi sushi)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(sushi);

            if (!Validator.TryValidateObject(sushi, context, result, true))
            {
                Clear();

                foreach (var error in result)
                {

                    WriteLine($"{error} - incorrect input or not all fields are required! The sushi is not update.");
                    Thread.Sleep(4000);

                    PageAdminSushiRun();
                }
            }
            else
            {
                //Update in Db sushi
                sqlSushi.Update(sushi);
                sqlSushi.Dispose();

                //Update in Json sushi
               jsonSushi.Update(sushi);
            }
        }

        private bool CheckProduct(int id)
        {
            return  jsonSushi.GetItem(id) is not null && sqlSushi.GetItem(id) is not null;
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
