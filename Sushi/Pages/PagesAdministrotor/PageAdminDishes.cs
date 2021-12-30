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

                    Clear();
                    WriteLine($"Dish with Id - {_dishesId} DELETE");
                    Thread.Sleep(3000);

                    PageAdminDishesRun();
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
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
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Dishes (500 symbol): ");
                    _description = ReadLine();

                    SauceAndDishes updateDish = new(_dishesId, _name, _price, _description);

                    //Update Dishes Db
                    sqlDishes.Update(updateDish);
                    sqlDishes.Dispose();

                    //Update Dishe Json
                    jsonDishes.Update(updateDish);

                    Clear();
                    WriteLine($"Dish with Id - {_dishesId} UPDATE");
                    Thread.Sleep(3000);

                    PageAdminDishesRun();
                }
                else if (keyPressed != ConsoleKey.Escape)
                {
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
                    _price = Convert.ToDecimal(ReadLine());

                    WriteLine("Enter Descripion Dishes (500 symbol): ");
                    _description = ReadLine();

                    SauceAndDishes dishes = new(_infoId, _name, _price, _description);

                    //Create Dish Db
                    sqlDishes.Create(dishes);
                    sqlDishes.Dispose();

                    //Create Dish Json
                    jsonDishes.Create(dishes);

                    WriteLine($"Dishes: {dishes.ShowDataForAdmin()} - ADDED");
                }

            } while (keyPressed != ConsoleKey.Escape);

        }


        //View Dishes
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
