using System.Net;
using System.Net.Mail;

namespace SushiMarcet.Models
{

    internal class Messenger
    {
        private TimeSpan _timeMessage = new (0,1,0);
        
        private string _nameAdmin = "Administrator Sushi Marcet";
        private string _emailMarcet = "sush1marcet@gmail.com";
        private string _pass = "VhGfTgOy6D";
        private string _textMessage;

        private MailAddress _from;
        private MailAddress _to;
        private MailMessage _sender;

        private SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

        public void AcceptedOrderMessage(Order order)
        {
            _textMessage = $"{order.Cheque}" + $"\nYour order number: {order.Id}";

            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(order.EmailClient);

            _sender = new(_from,_to);

            _sender.Subject = "Your order is accepted";
            _sender.Body = _textMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);
            smtp.Dispose();

            Thread.Sleep(_timeMessage);
        }

        public void RejectedOrderMessage(Order order, string textMessage)
        {
            _textMessage = textMessage;

            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(order.EmailClient);

            _sender = new(_from, _to);

            _sender.Subject = "Your order is rejected";
            _sender.Body = _textMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);
            smtp.Dispose();
        }

        public void CompletedOrderMessage(Order order)
        {
            _textMessage = $"Your order ({order.Id}) is completed and will be delivered soon";

            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(order.EmailClient);

            _sender = new(_from, _to);

            _sender.Subject = "Your order is completed";
            _sender.Body = _textMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);
            smtp.Dispose();

            Thread.Sleep(_timeMessage);
        }

        public void OrderDeliveredByCourierMessage(Order order)
        {
            _textMessage = $"\nYour order ({order.Id}) has been delivered";

            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(order.EmailClient);

            _sender = new(_from, _to);

            _sender.Subject = "The order has been delivered";
            _sender.Body = _textMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);
            smtp.Dispose();

            Thread.Sleep(_timeMessage);
        }

        public void OrderIsPaidMessage(Order order)
        {
            _textMessage = "Thank you for shopping at Sushi Marcet";

            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(order.EmailClient);

            _sender = new(_from, _to);

            _sender.Subject = "Your order is paid";
            _sender.Body = _textMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);
            smtp.Dispose();

            Thread.Sleep(_timeMessage);
        }
    }
}
