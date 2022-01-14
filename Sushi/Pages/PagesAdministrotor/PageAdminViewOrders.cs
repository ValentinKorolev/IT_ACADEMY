using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;

namespace SushiMarcet.Pages.PagesAdministrotor
{
    internal sealed class PageAdminViewOrders : PageFather
    {
        Logger Logger = new Logger();

        private Order _currentOrder;  

        public PageAdminViewOrders(Order order)
        {
            _currentOrder = order;

            _bannerPage = order.ShowData();

            _options = new string[] { "Accept the order", "Reject an order","Go back" };
        }

        protected override void TransferPage(string[] options, int selectedIndex)
        {
            switch (options[selectedIndex])
            {
                case "Accept the order":
                    AcceptTheOrder(_currentOrder);
                    BackToPageAdminOrders();
                    break;
                case "Reject an order":
                    RejectAnOrder(_currentOrder);
                    break;
                case "Go back":
                    BackToPageAdminOrders();
                    break;
            }
        }

        private void AcceptTheOrder(Order order)
        {
            Messenger messenger = new(order);

            Thread thread = new Thread(new ThreadStart(messenger.SendMessageAccepted));
            thread.Start();

            Logger.Debug($"An order accepted {order}");

            Clear();
            WriteLine("An order acceptance letter has been sent");
            Thread.Sleep(4000);
        }

        private void RejectAnOrder(Order order)
        {
            do
            {
                Clear();
                WriteLine("Write the reason for the refusal\n");

                string message = ReadLine();

                WriteLine();
                WriteLine("Do you want to continue?(Press ESC to go back)");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                Messenger messenger = new(order,message);

                Thread thread = new Thread(new ThreadStart(messenger.SendMessageRejected));
                thread.Start();

                Clear();
                WriteLine($"Order with id: {order.Id} - REJECTED");
                Thread.Sleep(4000);

                BackToPageAdminOrders();

            } while (keyPressed != ConsoleKey.Escape);

            BackToPageAdminOrders();
        }

        private void BackToPageAdminOrders()
        {
            PageAdminOrders pageAdminOrders = new();
            _ = pageAdminOrders.Run();
        }
    }
}
