﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.DataBase
{
    internal class SqlDishesRepository : IRepository<SauceAndDishes>, IDisposable
    {
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
                Logger<PageAdmin>.Debug($"Admin added dish: {item.ShowDataForAdmin()}");
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
                SauceAndDishes deleteDish = db.SauceAndDishes.FirstOrDefault(_ => _.Id == id);
                db.SauceAndDishes.Remove(deleteDish);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
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
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
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
