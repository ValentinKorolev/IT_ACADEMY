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
        public delegate void MessageHandler(Order order);
        public event MessageHandler? OrderAcceptedMessageEvent;

        SqlOrdersRepository sqlOrdersRepository = new SqlOrdersRepository();
        JsonOrderRepository jsonOrderRepository = new JsonOrderRepository();

        private Order _currentOrder;  
        Messenger _messenger;

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
            _messenger = new Messenger();
            OrderAcceptedMessageEvent += _messenger.AcceptedOrderMessage;
            OrderAcceptedMessageEvent += _messenger.CompletedOrderMessage;
            OrderAcceptedMessageEvent += _messenger.OrderDeliveredByCourierMessage;
            OrderAcceptedMessageEvent += _messenger.OrderIsPaidMessage;            
            OrderAcceptedMessageEvent(order);

            //SendMessage(order);
            
            order.Status = StatusOrder.InProgress;
            UpdateStatusOrder(order);

            Clear();
            WriteLine("An order acceptance letter has been sent");
            Thread.Sleep(4000);
        }



        //private void SendMessageAccepted(Order order)
        //{
        //    string textMessage = "Your order is accepted\n" + $"{order.Cheque}" + $"\nYour order number: {order.Id}";

        //    // отправитель - устанавливаем адрес и отображаемое в письме имя
        //    MailAddress from = new MailAddress("sush1marcet@gmail.com", "Administrator Sushi Marcet");
        //    // кому отправляем
        //    MailAddress to = new MailAddress(order.EmailClient);
        //    // создаем объект сообщения
        //    MailMessage m = new MailMessage(from, to);
        //    // тема письма
        //    m.Subject = "Order Notification";
        //    // текст письма
        //    m.Body = textMessage;

        //    // адрес smtp-сервера и порт, с которого будем отправлять письмо
        //    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        //    // логин и пароль
        //    smtp.Credentials = new NetworkCredential("sush1marcet@gmail.com", "VhGfTgOy6D");
        //    smtp.EnableSsl = true;
        //    smtp.Send(m);
        //    smtp.Dispose();
        //}

        private void UpdateStatusOrder(Order order)
        {
            sqlOrdersRepository.Update(order);
            jsonOrderRepository.Update(order);
            //UpdateDateOrderDb(order);
            //UpdateDateOrderJson(order);
            
        }

        //private void UpdateDateOrderJson(Order order)
        //{
        //    ListOrders model = new ListOrders();

        //    if (File.Exists(Observer.FileNameOrders))
        //    {
        //        var fileName = File.ReadAllText(Observer.FileNameOrders);
        //        var ordersJson = JsonConvert.DeserializeObject<ListOrders>(fileName);

        //        model.Orders = ordersJson.Orders;

        //        int index = model.Orders.IndexOf(model.Orders.FirstOrDefault(_ => _.Id == order.Id));
        //        model.Orders[index] = order;
        //    }
        //    else
        //    {
        //        Clear();
        //        WriteLine("File Orders.json not found");
        //        Thread.Sleep(4000);

        //        BackToPageAdminOrders();
        //    }
        //}

        //private void UpdateDateOrderDb(Order order)
        //{
        //    using(ApplicationContext db = new ApplicationContext())
        //    {
        //        Order updateOrder = db.Order.FirstOrDefault(_ => _.Id == order.Id);
        //        updateOrder.Status = order.Status;
        //        db.SaveChanges();
        //    }
        //}

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

                //SendMessage(order, message);

                order.Status = StatusOrder.Rejected;

                UpdateStatusOrder(order);

                Clear();
                WriteLine($"Order with id: {order.Id} - REJECTED");
                Thread.Sleep(4000);

                BackToPageAdminOrders();

            } while (keyPressed != ConsoleKey.Escape);

            BackToPageAdminOrders();
        }

        //private void SendMessage(Order order, string message = null)
        //{
        //    string textMessage;

        //    if (message is not null)
        //    {
        //         textMessage = message;
        //    }
        //    else
        //    {
        //        textMessage = "Your order is accepted\n" + $"{order.Cheque}" + $"\nYour order number: {order.Id}";
        //    }
            

        //    MailAddress from = new MailAddress("sush1marcet@gmail.com", "Administrator Sushi Marcet");

        //    MailAddress to = new MailAddress(order.EmailClient);

        //    MailMessage m = new MailMessage(from, to);


        //    if (message is not null)
        //    {
        //        m.Subject = "Your order has been rejected";
        //    }
        //    else
        //    {
        //        m.Subject = "Order Notification";
        //    }            
            
        //    m.Body = textMessage;
            
        //    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            
        //    smtp.Credentials = new NetworkCredential("sush1marcet@gmail.com", "VhGfTgOy6D");
        //    smtp.EnableSsl = true;
        //    smtp.Send(m);
        //    smtp.Dispose();
        //}

        private void BackToPageAdminOrders()
        {
            PageAdminOrders pageAdminOrders = new();
            _ = pageAdminOrders.Run();
        }
    }
}
