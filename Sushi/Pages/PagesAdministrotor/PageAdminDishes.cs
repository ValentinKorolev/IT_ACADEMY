using System.ComponentModel.DataAnnotations;

namespace SushiMarcet.Pages
{
    internal sealed class PageAdminDishes : PageFather
    {
        Logger Logger = new Logger();

        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        SqlDishesRepository sqlDishes = new SqlDishesRepository();
        JsonDishesRepository jsonDishes = new JsonDishesRepository();

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

        private void DeleteDishes()
        {
            int _dishesId;

            do
            {
                Clear();
                WriteLine("Delete Dishes\n");
                Write("Enter the Id of the dishes you want to delete: ");

                _ = int.TryParse(ReadLine(), out _dishesId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                bool isDish = CheckProduct(_dishesId);

                if (isDish && keyPressed != ConsoleKey.Escape)
                {
                    //Delete Dishes Db 
                    sqlDishes.Delete(_dishesId);
                    sqlDishes.Dispose();

                    //Delete Dishes Json
                    jsonDishes.Delete(_dishesId);

                    Logger.Debug($"Dish with Id - {_dishesId} DELETE");

                    Clear();
                    WriteLine($"Dish with Id - {_dishesId} DELETE");
                    Thread.Sleep(3000);

                    PageAdminDishesRun();
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Logger.Debug($"Incorrect input or dish with Id - ({ _dishesId}) NOT FOUND");

                    Clear();
                    WriteLine($"Incorrect input or dish with Id - ({_dishesId}) NOT FOUND");
                    Thread.Sleep(4000);

                    PageAdminDishesRun();
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDishesRun();
        }

        private void UpdateDishes()
        {
            int _dishesId;

            do
            {
                Clear();
                WriteLine("Update SASD\n");
                Write("Enter the Id of the sushi you want to update: ");

                _ = int.TryParse(ReadLine(), out _dishesId);

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                bool isDish = CheckProduct(_dishesId);

                if (isDish && keyPressed != ConsoleKey.Escape)
                {
                    Clear();

                    WriteLine("Enter Name Dishes: ");
                    _name = ReadLine();

                    WriteLine("Enter Price Dishes: ");
                    _ = decimal.TryParse(ReadLine(), out _price);

                    WriteLine("Enter Descripion Dishes (500 symbol): ");
                    _description = ReadLine();

                    if(_price >= 50)
                    {
                        _price /= 100; 
                    }

                    SauceAndDishes updateDish = new(_dishesId, _name, _price, _description);

                    ValidateDishAndUpdate(updateDish);

                    Logger.Debug($"Dish with Id - {_dishesId} UPDATE");

                    Clear();
                    WriteLine($"Dish with Id - {_dishesId} UPDATE");
                    Thread.Sleep(3000);

                    PageAdminDishesRun();
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
                    Logger.Debug($"Incorrect input or dish with Id - ({ _dishesId}) NOT FOUND");

                    Clear();
                    WriteLine($"Incorrect input or dish with Id - ({_dishesId}) NOT FOUND");
                    Thread.Sleep(4000);

                    PageAdminDishesRun();
                }

            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDishesRun();
        }

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
                    _ = decimal.TryParse(ReadLine(), out _price);

                    WriteLine("Enter Descripion Dishes (500 symbol): ");
                    _description = ReadLine();

                    SauceAndDishes dishes = new(_infoId, _name, _price, _description);

                    ValidateDishAndCreate(dishes);

                    Logger.Debug($"Dishes with id ({dishes.Id}) - ADDED");

                    Clear();
                    WriteLine($"Dishes: {dishes.ShowDataForAdmin()} - ADDED");
                    Thread.Sleep(3000);
                }

            } while (keyPressed != ConsoleKey.Escape);

        }

        private void ViewListDishes()
        {
            List<SauceAndDishes> listDishes = new List<SauceAndDishes>();

            listDishes = (List<SauceAndDishes>)sqlDishes.GetItemList();
            //listDishes = (List<SauceAndDishes>)jsonDishes.GetItemList();

            do
            {
                Clear();
                WriteLine("List Sauces and side dishes (Press ESC to go back)");
                WriteLine();

                foreach (var dish in listDishes)
                {
                    WriteLine(dish.ShowDataForAdmin());
                    WriteLine();
                }
                
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;                
               
            } while (keyPressed != ConsoleKey.Escape);

            PageAdminDishesRun();
        }

        private void ValidateDishAndCreate(SauceAndDishes dishes)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(dishes);

            if (!Validator.TryValidateObject(dishes, context, result, true))
            {
                Clear();

                foreach (var error in result)
                {

                    WriteLine($"{error} - incorrect input or not all fields are required! The drink is not made.");
                    Thread.Sleep(4000);

                    PageAdminDishesRun();
                }
            }
            else
            {
                //Create in Db dishes
                sqlDishes.Create(dishes);
                sqlDishes.Dispose();

                //Create in Json dishes
                jsonDishes.Create(dishes);
            }
        }

        private void ValidateDishAndUpdate(SauceAndDishes dish)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(dish);

            if (!Validator.TryValidateObject(dish, context, result, true))
            {
                Clear();

                foreach (var error in result)
                {

                    WriteLine($"{error} - incorrect input or not all fields are required! The drink is not update.");
                    Thread.Sleep(4000);

                    PageAdminDishesRun();
                }
            }
            else
            {
                //Update in Db dishes
                sqlDishes.Update(dish);
                sqlDishes.Dispose();

                //Update in Json dishes
                jsonDishes.Update(dish);
            }
        }

        private bool CheckProduct(int id)
        {
            return jsonDishes.GetItem(id) is not null && sqlDishes.GetItem(id) is not null;
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
