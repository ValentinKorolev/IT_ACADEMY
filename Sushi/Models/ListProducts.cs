
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal sealed class ListProducts
    {
        public List<Sushi> SushiMenu { get; set; } = new();
        public List<SauceAndDishes> SauceAndDishesMenu { get; set; } = new();

    }
}
