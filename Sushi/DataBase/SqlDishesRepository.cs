using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.DataBase
{
    internal class SqlDishesRepository : IRepository<SauceAndDishes>, IDisposable
    {
        Logger Logger = new Logger();

        private ApplicationContext db;

        public SqlDishesRepository()
        {
            db = new ApplicationContext();
        }

        public void Create(SauceAndDishes item)
        {
            try
            {
                db.SauceAndDishes.Add(item);
                db.SaveChanges();

                Logger.Debug($"Product added to Db {item.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                new Logger().Error("Deleting a product to the database ended with an error", ex);

                WriteLine("Error, please look logs!");
                Thread.Sleep(10000);
            }
        }

        public void Delete(int id)
        {
            try
            {
                SauceAndDishes deleteDish = db.SauceAndDishes.FirstOrDefault(_ => _.Id == id);
                db.SauceAndDishes.Remove(deleteDish);
                db.SaveChanges();

                Logger.Debug($"Product deleted to Db {deleteDish.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                new Logger().Error("Deleting a product to the database ended with an error", ex);

                Clear();
                WriteLine($"Dish with Id - ({id}) NOT FOUND in DataBase");
                Thread.Sleep(3000);
            }
        }

        public SauceAndDishes GetItem(int id)
        {
            return db.SauceAndDishes.FirstOrDefault(_ => _.Id == id);
        }

        public IEnumerable<SauceAndDishes> GetItemList()
        {
            return db.SauceAndDishes.ToList();
        }

        public void Update(SauceAndDishes item)
        {
            try
            {
                var updateDish = GetItem(item.Id);
                db.Entry(updateDish).CurrentValues.SetValues(item);
                db.SaveChanges();

                Logger.Debug($"Product updated to Db {updateDish.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                new Logger().Error("Updating a product to the database ended with an error", ex);

                Clear();
                WriteLine($"Dish with Id - ({item.Id}) NOT FOUND in DataBase");
                Thread.Sleep(3000);
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
