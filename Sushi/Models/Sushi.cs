using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet
{
    internal class Sushi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public float Calories { get; set; }
        public decimal Price { get; set; }

        public Sushi(int id, string name, decimal price, float weight, float calories)
        {
            Id = id;
            Name = name;
            Weight = weight;
            Calories = calories;
            Price = price;    
        }
    }
}
