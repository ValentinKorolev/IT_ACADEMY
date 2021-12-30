using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal class SqlSushiRepository : IRepository<Sushi>, IRepositorySushi<Sushi>, IDisposable
    {
        private ApplicationContext db;

        public SqlSushiRepository()
        {
            db = new ApplicationContext();
        }

        public void Create(Sushi item)
        {
                try
                {
                    db.Sushi.Add(item);
                    db.SaveChanges();
                    Logger<PageAdmin>.Debug($"Admin added sushi: {item.ShowDataForAdmin()}");
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
                    Sushi deleteSushi = db.Sushi.FirstOrDefault(_ => _.Id == id);
                    db.Sushi.Remove(deleteSushi);
                    db.SaveChanges();                
            }
            catch (Exception ex)
            {
                Clear();
                WriteLine($"Sushi with Id - ({id}) NOT FOUND in DataBase");
                Thread.Sleep(3000);
            }
        }

        public Sushi GetItem(int id)
        {
            return db.Sushi.FirstOrDefault(_ => _.Id == id);
        }

        public IEnumerable<Sushi> GetItemList()
        {
            return db.Sushi.ToList();
        }

        public void Update(Sushi item)
        {
            try
            {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
            }
            catch (Exception ex)
            {
                Clear();
                WriteLine($"Sushi with Id - ({item.Id}) NOT FOUND in DataBase");
                Thread.Sleep(3000);
            }
        }

        public IEnumerable<Sushi> GetSushi(string typeSushi)
        {
            return db.Sushi.Where(_ => _.Type == typeSushi).ToList();
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
