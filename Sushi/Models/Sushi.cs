using SushiMarcet.Attributes;


namespace SushiMarcet
{
    [SushiValidate]
    internal sealed class Sushi : IShowDataProduct
    {
        private decimal _price;
        public int Id { get; init; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price
        {
            get { return _price * Servings; }
            set { _price = value; }
        }
        public int Servings { get; set; }

        public Sushi(int id,string type, string name, decimal price, string description)
        {
            Id = id;
            Type = type;
            Name = name;
            Description = description;
            _price = price;
            Servings = 1;
        }

        public override string ToString()
        {
            return $"{Name}| Number of servings: {Servings}| Price: {Price:c}";
        }

        public string ShowDataForAdmin()
        {
            return $"Id: {Id}| {Name}| Description: {Description}| Servings: {Servings}| Price: {Price:c}";
        }

        public string ShowData(int numServings = 1)
        {
             return $"{Name}\n\nDescription: {Description}\n\nServings: {Servings}\n\nPrice: {Price:c}"; 
        }
    }  
}
