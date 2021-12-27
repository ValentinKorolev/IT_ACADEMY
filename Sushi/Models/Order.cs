using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    enum StatusOrder
    {
        NotReviewed = 1,
        Accepted = 2,
        Rejected = 3,
        InProgress = 4,
    }

    internal class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string NameClient { get; set; }
        public string EmailClient { get; set; }
        public string PhoneNumberClient { get; set; }
        public string AdressDeliveryClient { get; set; }
        public string Cheque { get; set; }
        public StatusOrder Status { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
            OrderTime = DateTime.Now;     
            Status = StatusOrder.NotReviewed;
        }

        public override string ToString()
        {
            return $"--Id: { Id}| Order time: {OrderTime}|";
        }
        public string ShowData()
        {
            return $"Id: {Id}| Order time: {OrderTime}|\n Name: {NameClient}| Email: {EmailClient}| Phone: {PhoneNumberClient}|\n Adress: {AdressDeliveryClient}|\n Cheque: {Cheque}";
        }
    }
}
