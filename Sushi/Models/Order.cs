using SushiMarcet.Attributes;


namespace SushiMarcet.Models
{
    enum StatusOrder
    {
        NotReviewed = 1,
        InProgress = 2,
        Rejected = 3,
        Completed = 4,
        Delivered = 5,
        IsPaid = 6,
    }

    [OrderValidate]
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
            return Status == StatusOrder.NotReviewed
               ? $"--Id: { Id}| Order time: {OrderTime}|"
               : $"--Id: { Id}| Order time: {OrderTime}| Status: {Status}";
        }
        public string ShowData()
        {
            return $"Id: {Id}| Order time: {OrderTime}|\n Name: {NameClient}| Email: {EmailClient}| Phone: {PhoneNumberClient}|\n Adress: {AdressDeliveryClient}|\n Cheque: {Cheque}";
        }
    }
}
