using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.DataBase
{
    internal class SqlDrinksRepository : IRepository<Drinks>, IDisposable
    {
        private ApplicationContext db;

        public SqlDrinksRepository()
        {
            db = new ApplicationContext();
        }

        public void Create(Drinks item)
        {
            try
            {
                db.Drinks.Add(item);
                db.SaveChanges();
                Logger<PageAdmin>.Debug($"Admin added drink: {item.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                Logger<PageAdmin>.Error("Read InnerException", ex.InnerException);
                WriteLine("Error, please look logs!");
                Thread.Sleep(10000);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Drinks deleteDrinks = db.Drinks.FirstOrDefault(_ => _.Id == id);
                db.Drinks.Remove(deleteDrinks);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Clear();
                WriteLine($"Drink with Id - ({id}) NOT FOUND in DataBase");
                Thread.Sleep(3000);
            }
        }

        public Drinks GetItem(int id)
        {
            return db.Drinks.FirstOrDefault(_ => _.Id == id);
        }

        public IEnumerable<Drinks> GetItemList()
        {
            return db.Drinks.ToList();
        }

        public void Update(Drinks item)
        {
            try
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Clear();
                WriteLine($"Drink with Id - ({item.Id}) NOT FOUND in DataBase");
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
