using Newtonsoft.Json;
using SushiMarcet.Pages.PagesAdministrotor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Pages
{
    internal sealed class PageAdminOrders : PageFather
    {
        private const string NameAdmin = "Admin123";
        private const string PassAdmin = "122345";

        List<Order> orders = new List<Order>();

        SqlOrdersRepository sqlOrdersRepository = new SqlOrdersRepository();
        JsonOrderRepository jsonOrderRepository = new JsonOrderRepository();

        public PageAdminOrders()
        {
            _bannerPage = "Orders";

            _options = new string[] {$"0.Orders not reviewed",
                                     "1.Completed orders",
                                     "2.Rejected orders",
                                     "3.Orders in progress",
                                     "4.Orders delivered",
                                     "5.Orders paided",
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
                case "4.Orders delivered":
                    GetOrdersDelivered();
                    PageAdminOrdersRun();
                    break;
                case "5.Orders paided":
                    GetOrdersPaided();
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

                orders = (List<Order>)sqlOrdersRepository.GetItemList(StatusOrder.InProgress);
                // orders = (List<Order>)jsonOrderRepository.GetItemList(StatusOrder.InProgress);
                sqlOrdersRepository.Dispose();

                ShowOrdes(orders);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

            } while (keyPressed != ConsoleKey.Escape) ;
        }

        private void GetOrdersDelivered()
        {

            do
            {
                Clear();
                WriteLine("List orders delivered (Press ESC to go back)");
                WriteLine();

                orders = (List<Order>)sqlOrdersRepository.GetItemList(StatusOrder.Delivered);
                // orders = (List<Order>)jsonOrderRepository.GetItemList(StatusOrder.InProgress);
                sqlOrdersRepository.Dispose();

                ShowOrdes(orders);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

            } while (keyPressed != ConsoleKey.Escape);
        }

        private void GetOrdersPaided()
        {

            do
            {
                Clear();
                WriteLine("List orders paided (Press ESC to go back)");
                WriteLine();

                orders = (List<Order>)sqlOrdersRepository.GetItemList(StatusOrder.IsPaid);
                // orders = (List<Order>)jsonOrderRepository.GetItemList(StatusOrder.InProgress);
                sqlOrdersRepository.Dispose();

                ShowOrdes(orders);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

            } while (keyPressed != ConsoleKey.Escape);
        }

        private void GetRejectedOrders()
        {
            do
            {
                Clear();
                WriteLine("List of rejected orders (Press ESC to go back)");
                WriteLine();

                orders = (List<Order>)sqlOrdersRepository.GetItemList(StatusOrder.Rejected);
                //orders = (List<Order>)jsonOrderRepository.GetItemList(StatusOrder.Rejected);
                sqlOrdersRepository.Dispose();

                ShowOrdes(orders);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

            } while (keyPressed != ConsoleKey.Escape);
        }

        private void GetOrdersCompleted()
        {                   
            do
            {
                Clear();
                WriteLine("List of completed orders (Press ESC to go back)");
                WriteLine();

                orders = (List<Order>)sqlOrdersRepository.GetItemList(StatusOrder.Completed);    
                //orders = (List<Order>)jsonOrderRepository.GetItemList(StatusOrder.Completed);
                sqlOrdersRepository.Dispose();

                ShowOrdes(orders);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

            } while (keyPressed != ConsoleKey.Escape);

        }

        private void ShowOrdes(List<Order> orders)
        {
            if (orders.Count == 0)
            {
                WriteLine();
                WriteLine("Not found");
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
