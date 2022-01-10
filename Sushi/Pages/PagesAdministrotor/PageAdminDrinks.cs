using System.ComponentModel.DataAnnotations;


namespace SushiMarcet.Pages
{
    internal sealed class PageAdminDrinks : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        SqlDrinksRepository sqlDrinks = new SqlDrinksRepository();
        JsonDrinksRepository jsonDrinks = new JsonDrinksRepository();

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
            List<Drinks> listDrinks = new List<Drinks>();

            //listDrinks = (List<Drinks>)sqlDrinks.GetItemList();
            listDrinks = (List<Drinks>)jsonDrinks.GetItemList();

            sqlDrinks.Dispose();

            do
            {
                Clear();
                WriteLine("List Drinks (Press ESC to go back)");
                WriteLine();

      
                foreach(var drink in listDrinks)
                {
                    WriteLine(drink.ShowDataForAdmin());
                    WriteLine();
                }
               
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;                    
                
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDrinksRun();
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
                    _ = decimal.TryParse(ReadLine(), out _price);

                    WriteLine("Enter Descripion Drink (200 symbol): ");
                    _description = ReadLine();

                    if (_price >= 100)
                    {
                        _price /= 100;
                    }

                    Drinks drink = new(_infoId, _name, _price, _description);

                    ValidateDrinkAndCreate(drink);

                    Clear();
                    WriteLine($"Drink: {drink.ShowDataForAdmin()} - ADDED");
                    Thread.Sleep(3000);
                }

            } while (keyPressed != ConsoleKey.Escape);
        }

        private void UpdateDrinks()
        {
            int drinkId;

            do
            {
                Clear();
                WriteLine("Update Drinks\n");
                Write("Enter the Id of the drink you want to update: ");

                _ = int.TryParse(ReadLine(), out drinkId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                bool isDrink = CheckProduct(drinkId);

                if (isDrink && keyPressed != ConsoleKey.Escape)
                {
                    WriteLine("Enter Name Drink: ");
                    _name = ReadLine();

                    WriteLine("Enter Price Drink: ");
                    _ = decimal.TryParse(ReadLine(), out _price);

                    WriteLine("Enter Descripion Drink (200 symbol): ");
                    _description = ReadLine();

                    if (_price >= 100)
                    {
                        _price /= 100;
                    }

                    Drinks updateDrinks = new(drinkId, _name, _price, _description);

                    ValidateDrinkAndUpdate(updateDrinks);

                    Clear();
                    WriteLine($"Drink with Id - {drinkId} UPDATE");
                    Thread.Sleep(3000);

                    PageAdminDrinksRun();
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine($"Incorrect input or drink with Id - ({drinkId}) NOT FOUND");
                    Thread.Sleep(2000);

                    PageAdminDrinksRun();
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDrinksRun();
        }

        private void DeleteDrinks()
        {
            int drinkId;

            do
            {
                Clear();
                WriteLine("Delete Drinks\n");
                Write("Enter the Id of the drink you want to delete: ");

                _ = int.TryParse(ReadLine(), out drinkId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                bool isDrink = CheckProduct(drinkId);

                if (isDrink && keyPressed != ConsoleKey.Escape)
                {
                    //Delete Drinks Db 
                    sqlDrinks.Delete(drinkId);
                    sqlDrinks.Dispose();

                    //Delete Drinks Json
                    jsonDrinks.Delete(drinkId);

                    Clear();
                    WriteLine($"Drink with Id - {drinkId} DELETE");
                    Thread.Sleep(3000);

                    PageAdminDrinksRun();
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Clear();
                    WriteLine($"Incorrect input or drink with Id - ({drinkId}) NOT FOUND");
                    Thread.Sleep(2000);

                    PageAdminDrinksRun();
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDrinksRun();
        }
        private void ValidateDrinkAndCreate(Drinks drink)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(drink);

            if (!Validator.TryValidateObject(drink, context, result, true))
            {
                Clear();

                foreach (var error in result)
                {

                    WriteLine($"{error} - incorrect input or not all fields are required! The drink is not made.");
                    Thread.Sleep(4000);

                    PageAdminDrinksRun();
                }
            }
            else
            {
                //Create in Db drink
                sqlDrinks.Create(drink);
                sqlDrinks.Dispose();

                //Create in Json drink
                jsonDrinks.Create(drink);
            }
        }

        private void ValidateDrinkAndUpdate(Drinks drink)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(drink);

            if (!Validator.TryValidateObject(drink, context, result, true))
            {
                Clear();

                foreach (var error in result)
                {

                    WriteLine($"{error} - incorrect input or not all fields are required! The drink is not update.");
                    Thread.Sleep(4000);

                    PageAdminDrinksRun();
                }
            }
            else
            {
                //Update in Db drink
                sqlDrinks.Update(drink);
                sqlDrinks.Dispose();

                //Update in Json drink
                jsonDrinks.Update(drink);
            }
        }
        private bool CheckProduct(int id)
        {
            return jsonDrinks.GetItem(id) is not null && sqlDrinks.GetItem(id) is not null;
        }

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


