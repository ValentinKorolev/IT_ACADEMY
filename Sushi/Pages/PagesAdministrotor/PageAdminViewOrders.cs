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
                    break;
                case "Go back":
                    BackToPageAdminOrders();
                    break;
            }
        }

        private void AcceptTheOrder(Order order)
        {
            SendMessageAccepted(order);
            order.Status = StatusOrder.Accepted;

            Clear();
            WriteLine("An order acceptance letter has been sent");
            Thread.Sleep(4000);
        }

        private void SendMessageAccepted(Order order)
        {
            string textMessage = "Your order is accepted\n" + $"{order.Cheque}" + $"\nYour order number: {order.Id}";

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("sush1marcet@gmail.com", "Administrator Sushi Marcet");
            // кому отправляем
            MailAddress to = new MailAddress(order.EmailClient);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Order Notification";
            // текст письма
            m.Body = textMessage;
            
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("sush1marcet@gmail.com", "VhGfTgOy6D");
            smtp.EnableSsl = true;
            smtp.Send(m);
            smtp.Dispose();
        }

        private void UpdateDateOrder(Order order)
        {

        }

        private void UpdateDateOrderJson(Order order)
        {
            List<Order> orders = new List<Order>();

            if (File.Exists(Observer.FileNameOrders))
            {
                var fileName = File.ReadAllText(Observer.FileNameOrders);
                var ordersJson = JsonConvert.DeserializeObject<ListProducts>(fileName);
            }
        }

        private void UpdateDateOrderDb(Order order)
        {

        }

        private void RejectAnOrder(Order order)
        {
            do
            {
                Clear();
                WriteLine("Write the reason for the refusal\n");

                string message = ReadLine();
                

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;


            } while (keyPressed != ConsoleKey.Escape);
        }

        private void SendMessageReject(Order order)
        {
            string textMessage = "Your order has been rejected";

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("sush1marcet@gmail.com", "Administrator Sushi Marcet");
            // кому отправляем
            MailAddress to = new MailAddress(order.EmailClient);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Your order has been rejected";
            // текст письма
            m.Body = textMessage;

            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("sush1marcet@gmail.com", "VhGfTgOy6D");
            smtp.EnableSsl = true;
            smtp.Send(m);
            smtp.Dispose();
        }

        private void BackToPageAdminOrders()
        {
            PageAdminOrders pageAdminOrders = new();
            _ = pageAdminOrders.Run();
        }
    }
}
