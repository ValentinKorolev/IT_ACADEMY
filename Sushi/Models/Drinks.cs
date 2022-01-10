using SushiMarcet.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    [DrinksValidate]
    internal sealed class Drinks : IShowDataProduct
    {
        private decimal _price;
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price
        {
            get { return _price * Servings; }
            set { _price = value; }
        }/*=> _price * Servings;*/
        public int Servings { get; set; }

        public Drinks(int id, string name, decimal price,string description)
        {
            Id = id;
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

            return $"{Name}|\n\nDescription: {Description}|\n\nServings: {Servings}|\n\nPrice: {Price:c}";
        }
    }
}
