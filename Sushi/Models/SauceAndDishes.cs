﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal class SauceAndDishes
    {
        private decimal _price;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price => _price * Servings;

        [NotMapped]
        public int Servings { get; set; }

        public SauceAndDishes(int id,string name, string description, decimal price)
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

            return $"{Name}; Number of servings: {Servings}; Price: {Price:c}";
        }
    }
}
