using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.DataBase
{
    public class ApplicationContext : DbContext
    {
        internal DbSet<Sushi> Sushi { get; set; }
        internal DbSet<SauceAndDishes> SauceAndDishes { get; set; }
        internal DbSet<Order> Order { get; set; }
        internal DbSet<Drinks> Drinks { get; set; }
        

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = DESKTOP-22958VE\SQLEXPRESS;Initial Catalog=SushiMarcet;Integrated Security=True;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sushi>();
            modelBuilder.Entity<SauceAndDishes>();
            modelBuilder.Entity<Drinks>();
            modelBuilder.Entity<Order>();
        }
    }
}
