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
        public Guid Id { get; init; }
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
            return  $"<<Id: {Id}|\nOrder time: {OrderTime}| {OrderAmount()}| Status: {Status}>>";
        }

        public string ShowData()
        {
            return $"Id: {Id}| Order time: {OrderTime}|\nName: {NameClient}| Email: {EmailClient}| Phone: {PhoneNumberClient}|\nAdress: {AdressDeliveryClient}|\nCheque: {Cheque}";
        }

        public string OrderAmount()
        {   
            return Cheque[Cheque.IndexOf("\nOrder amount:")..];
        }
    }
}
