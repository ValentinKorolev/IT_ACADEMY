using System.Net;
using System.Net.Mail;

namespace SushiMarcet.Models
{

    internal class Messenger
    {
        readonly Logger Logger = new Logger();

        public delegate void MessageHandler(Order order);
        public event MessageHandler OrderAcceptedMessageEvent;
        public event MessageHandler OrderRejectedMessageEvent;

        SqlOrdersRepository sqlOrdersRepository;
        JsonOrderRepository jsonOrderRepository;

        private TimeSpan _timeMessage = new (0,1,0);
        
        private string _nameAdmin = "Administrator Sushi Marcet";
        private string _emailMarcet = "sush1marcet@gmail.com";
        private string _pass = "VhGfTgOy6D";
        private string _textAcceptedMessage;
        private string _textRejectedMessage;

        private MailAddress _from;
        private MailAddress _to;
        private MailMessage _sender;

        private SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

        private Order _order;

        public Messenger(Order order, string message = null)
        {
            sqlOrdersRepository = new SqlOrdersRepository();
            jsonOrderRepository = new JsonOrderRepository();

            _order = order;
            _textRejectedMessage = message;
        }

        public void SendMessageAccepted()
        {
            OrderAcceptedMessageEvent += AcceptedOrderMessage;
            OrderAcceptedMessageEvent += CompletedOrderMessage;
            OrderAcceptedMessageEvent += OrderDeliveredByCourierMessage;
            OrderAcceptedMessageEvent += OrderIsPaidMessage;
            OrderAcceptedMessageEvent(_order);
        }

        public void SendMessageRejected()
        {
            OrderRejectedMessageEvent += RejectedOrderMessage;
            OrderRejectedMessageEvent(_order);
        }

        private void AcceptedOrderMessage(Order order)
        {
            _textAcceptedMessage = $"{order.Cheque}" + $"\nYour order number: {order.Id}";

            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(order.EmailClient);

            _sender = new(_from,_to);

            _sender.Subject = "Your order is accepted";
            _sender.Body = _textAcceptedMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);

            order.Status = StatusOrder.InProgress;
            SaveData(order);

            Logger.Debug("Sended message (accepted order)");

            Thread.Sleep(_timeMessage);
        }

        private void RejectedOrderMessage(Order _order)
        {
            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(_order.EmailClient);

            _sender = new(_from, _to);

            _sender.Subject = "Your order is rejected";
            _sender.Body = _textRejectedMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);

            _order.Status = StatusOrder.Rejected;
            SaveData(_order);

            smtp.Dispose();

            Logger.Debug("Sended message (rejected order)");
        }

        private void CompletedOrderMessage(Order order)
        {
            _textAcceptedMessage = $"Your order ({order.Id}) is completed and will be delivered soon";

            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(order.EmailClient);

            _sender = new(_from, _to);

            _sender.Subject = "Your order is completed";
            _sender.Body = _textAcceptedMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);

            order.Status = StatusOrder.Completed;
            SaveData(order);

            Logger.Debug("Sended message (complited order)");

            Thread.Sleep(_timeMessage);
        }

        private void OrderDeliveredByCourierMessage(Order order)
        {
            _textAcceptedMessage = $"\nYour order ({order.Id}) has been delivered";

            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(order.EmailClient);

            _sender = new(_from, _to);

            _sender.Subject = "The order has been delivered";
            _sender.Body = _textAcceptedMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);

            order.Status = StatusOrder.Delivered;
            SaveData(order);

            Logger.Debug("Sended message (delivered order)");

            Thread.Sleep(_timeMessage);
        }

        private void OrderIsPaidMessage(Order order)
        {
            _textAcceptedMessage = "Thank you for shopping at Sushi Marcet";

            _from = new MailAddress(_emailMarcet, _nameAdmin);
            _to = new MailAddress(order.EmailClient);

            _sender = new(_from, _to);

            _sender.Subject = "Your order is paid";
            _sender.Body = _textAcceptedMessage;

            smtp.Credentials = new NetworkCredential(_emailMarcet, _pass);
            smtp.EnableSsl = true;
            smtp.Send(_sender);
            smtp.Dispose();

            order.Status = StatusOrder.IsPaid;
            SaveData(order);

            Logger.Debug("Sended message (paided order)");
        }

        private void SaveData(Order order)
        {
            sqlOrdersRepository.Update(order);
            jsonOrderRepository.Update(order);
        }
    }
}
