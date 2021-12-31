using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal class Messenger
    {
        private string _message;
        private Order _order;

        public Messenger()
        {           

        }

        public void SendMessage(Order order, string message = null)
        {
            string textMessage;

            if (message is not null)
            {
                textMessage = message;
            }
            else
            {
                textMessage = "Your order is accepted\n" + $"{order.Cheque}" + $"\nYour order number: {order.Id}";
            }


            MailAddress from = new MailAddress("sush1marcet@gmail.com", "Administrator Sushi Marcet");

            MailAddress to = new MailAddress(order.EmailClient);

            MailMessage m = new MailMessage(from, to);


            if (message is not null)
            {
                m.Subject = "Your order has been rejected";
            }
            else
            {
                m.Subject = "Order Notification";
            }

            m.Body = textMessage;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("sush1marcet@gmail.com", "VhGfTgOy6D");
            smtp.EnableSsl = true;
            smtp.Send(m);
            smtp.Dispose();
        }
    }
}
