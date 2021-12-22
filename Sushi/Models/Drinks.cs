using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal class Drinks
    {
        private decimal _price;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price => _price * Servings;

        [NotMapped]
        public int Servings { get; set; }

        public Drinks(int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            _price = price;
            Servings = 1;
        }
    }
}
