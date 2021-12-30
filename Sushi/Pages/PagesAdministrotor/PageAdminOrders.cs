using Newtonsoft.Json;
using SushiMarcet.Pages.PagesAdministrotor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal class PageAdminOrders : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        public PageAdminOrders()
        {
            _bannerPage = "Orders";

            _options = new string[] {$"0.Orders not reviewed",
                                     "1.Completed orders",
                                     "2.Rejected orders",
                                     "3.Orders in progress",
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
                case "0.Orders not reviewed":
                    PageAdminOrdersNotReviewed pageAdminOrdersNotConsidered = new();
                    _ = pageAdminOrdersNotConsidered.Run();
                    break;
                case "1.Completed orders":
                    GetOrdersCompleted();
                    PageAdminOrdersRun();
                    break;
                case "2.Rejected orders":
                    GetRejectedOrders();
                    PageAdminOrdersRun();
                    break;
                case "3.Orders in progress":
                    GetOrdersInProgress();
                    PageAdminOrdersRun();
                    break;
            }
        }

        private void GetOrdersInProgress()
        { 
            do
            {
                Clear();
                WriteLine("List orders in progress (Press ESC to go back)");
                WriteLine();

                //GetOrdersInProgressDb();
                GetOrdersInProgressJson();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

            } while (keyPressed != ConsoleKey.Escape) ;
        }

        private void GetOrdersInProgressJson()
        {
            var fileName = File.ReadAllText(Observer.FileNameOrders);
            var objectJson = JsonConvert.DeserializeObject<ListOrders>(fileName);

            var orders = objectJson.Orders.FindAll(_ => _.Status == StatusOrder.InProgress);

            if (orders.Count == 0)
            {
                Clear();
                WriteLine("No rejected orders found");
                Thread.Sleep(3000);

                PageAdminOrdersRun();
            }
            foreach (var order in orders)
            {
                WriteLine(order.ToString());
                WriteLine();
            }
        }

        private void GetOrdersInProgressDb()
        {
            try
            {

                List<Order> orders = new List<Order>();

                using (ApplicationContext db = new ApplicationContext())
                {
                    orders = db.Order.Where(_ => _.Status == StatusOrder.InProgress).ToList();

                    if (orders.Count == 0)
                    {
                        Clear();
                        WriteLine("No completed orders found");
                        Thread.Sleep(3000);

                        PageAdminOrdersRun();
                    }
                    else
                    {
                        foreach (var order in orders)
                        {
                            WriteLine(order.ToString());
                            WriteLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GetRejectedOrders()
        {
            do
            {
                Clear();
                WriteLine("List of rejected orders (Press ESC to go back)");
                WriteLine();

                //GetRejectedOrdersJson();
                GetRejectedOrdersDb();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

            } while (keyPressed != ConsoleKey.Escape);
        }

        private void GetRejectedOrdersJson()
        {
            var fileName = File.ReadAllText(Observer.FileNameOrders);
            var objectJson = JsonConvert.DeserializeObject<ListOrders>(fileName);

            var orders = objectJson.Orders.FindAll(_ => _.Status == StatusOrder.Rejected);

            if (orders.Count == 0)
            {
                Clear();
                WriteLine("No rejected orders found");
                Thread.Sleep(3000);

                PageAdminOrdersRun();
            }
            foreach (var order in orders)
            {
                WriteLine(order.ToString());
                WriteLine();
            }
        }

        private void GetRejectedOrdersDb()
        {
            try
            {

                List<Order> orders = new List<Order>();

                using (ApplicationContext db = new ApplicationContext())
                {
                    orders = db.Order.Where(_ => _.Status == StatusOrder.Rejected).ToList();

                    if (orders.Count == 0)
                    {
                        Clear();
                        WriteLine("No completed orders found");
                        Thread.Sleep(3000);

                        PageAdminOrdersRun();
                    }
                    else
                    {
                        foreach (var order in orders)
                        {
                            WriteLine(order.ToString());
                            WriteLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GetOrdersCompleted()
        {                   
            do
            {
                Clear();
                WriteLine("List of completed orders (Press ESC to go back)");
                WriteLine();

                //GetOrdersCompletedDb();
                GetOrdersCompletedJson();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

            } while (keyPressed != ConsoleKey.Escape);

        }

        private void GetOrdersCompletedJson()
        {

            if (File.Exists(Observer.FileNameOrders))
            {

                var fileName = File.ReadAllText(Observer.FileNameOrders);
                var objectJson = JsonConvert.DeserializeObject<ListOrders>(fileName);

                var orders = objectJson.Orders.FindAll(_ => _.Status == StatusOrder.Completed);
                
                if(orders.Count == 0)
                {
                    Clear();
                    WriteLine("No completed orders found");
                    Thread.Sleep(3000);

                    PageAdminOrdersRun();
                }
                foreach (var order in orders)
                {
                    WriteLine(order.ToString());
                    WriteLine();
                }                
            }
            else
            {
                Clear();
                WriteLine("File Orders.json not found");
                Thread.Sleep(3000);

            }
        }

        private void GetOrdersCompletedDb()
        {
            try
            {

                List<Order> orders = new List<Order>();

                using (ApplicationContext db = new ApplicationContext())
                {
                    orders = db.Order.Where(_ => _.Status == StatusOrder.Completed).ToList();
                    
                    if(orders.Count == 0)
                    {
                        Clear();
                        WriteLine("No completed orders found");
                        Thread.Sleep(3000);

                        PageAdminOrdersRun();
                    }
                    else
                    {
                        foreach (var order in orders)
                        {
                            WriteLine(order.ToString());
                            WriteLine();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        private void BackToPageAdmin()
        {
            PageAdmin pageAdmin = new(NameAdmin, PassAdmin);
            _ = pageAdmin.Run();
        }

        private void PageAdminOrdersRun()
        {
            PageAdminOrders pageAdminOrders = new();
            _ = pageAdminOrders.Run();
        }
    }
}
