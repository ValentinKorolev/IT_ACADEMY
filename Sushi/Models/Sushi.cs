using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet
{
    internal class Sushi
    {
        public int Id { get; protected set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Sushi(int id,string type, string name, decimal price, string description)
        {
            Id = id;
            Type = type;
            Name = name;
            Description = description;
            Price = price;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Type: {Type}, Name: {Name}; Description: {Description}; Price: {Price}$";
        }
    }
}
