using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    public static class Observer
    {
        public static string color; 

        public static string nameUser;
        public static string currentTypeSushi;

        public const string FileNameProduct = "Products.json";
        public const string FileNameOrders = "Orders.json";

        public const string Uramaki ="Uramaki";
        public const string Futomaki = "Futomaki";
        public const string Nigiri = "Nigiri";
        public const string BakedSushi = "Baked sushi";
        public const string SaucesAndSideDishes = "Sauces and side dishes";
    }
}
