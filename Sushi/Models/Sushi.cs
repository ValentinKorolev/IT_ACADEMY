using SushiMarcet.Attributes;


namespace SushiMarcet
{
    [SushiValidate]
    internal class Sushi : IShowDataProduct
    {
        private decimal _price;
        public int Id { get; protected set; }
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
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            return $"{Name}| Number of servings: {Servings}| Price: {Price:c}";
        }

        public string ShowDataForAdmin()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            return $"Id: {Id}| {Name}| Description: {Description}| Servings: {Servings}| Price: {Price:c}";
        }

        public string ShowData(int numServings = 1)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            return Servings == numServings || Servings == 0
                ? $"{Name}| Description: {Description}| Price: {Price:c}"
                : $"{Name}| Description: {Description}| Servings: {Servings}| Price: {Price:c}"; 
        }
    }  
}
