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
        protected Sushi()
        {

        }

        public Sushi(int id, string name, decimal price,SushiAdditionalInfo addInfo)
        {
            Id = id;
            Name = name;
            Weight = addInfo.Weight;
            Calories = addInfo.Calories;
            Price = price;    
        }

        internal class SushiAdditionalInfo
        {
            public float Weight { get; set; }
            public float Calories { get; set; }

            public SushiAdditionalInfo(float weight, float calories)
            {
                Weight = weight;
                Calories = calories;
            }
        }
    }
}
