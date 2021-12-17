using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    public static class Observer
    {
        public static string nameUser;
        public static string currentTypeSushi;

        public const string FileNameSushi = "Sushi.json";

        public const string Uramaki ="Uramaki";
        public const string Futomaki = "Futomaki";
        public const string Nigiri = "Nigiri";
        public const string BakedSushi = "Baked sushi";
        public const string Sets = "Sets";
        public const string SaucesAndSideDishes = "Sauces and side dishes";
    }
}
