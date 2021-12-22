using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string NameClient { get; set; }
        public string EmailClient { get; set; }
        public string PhoneNumderClient { get; set; }
        public string AdressDeliveryClient { get; set; }

        public Order(string name, string email, string phoneNumber, string address)
        {
            Id = Guid.NewGuid();
            NameClient = name;
            EmailClient = email;
            PhoneNumderClient = phoneNumber;
            AdressDeliveryClient = address;
            OrderTime = DateTime.Now;
        }
    }
}
