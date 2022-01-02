using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.DataBase
{
    internal class SqlOrdersRepository : IRepository<Order>, IDisposable
    {

        private ApplicationContext db;

        public SqlOrdersRepository()
        {
            db = new ApplicationContext();
        }

        public void Create(Order item)
        {
            try
            {
                db.Add(item);
                db.SaveChanges();

            }catch (Exception ex)
            {
                Logger<PageOrder>.Error("Read InnerException", ex.InnerException);
                WriteLine("Order not accepted! No internet connection!");
                Thread.Sleep(10000);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Order GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetItemList()
        {
            return db.Order.ToList();
        }

        public IEnumerable<Order> GetItemList(StatusOrder status)
        {
            return db.Order.Where(_ =>_.Status == status).ToList();
        }

        public void Update(Order item)
        {
            Order updateOrder = db.Order.FirstOrDefault(_ => _.Id == item.Id);
            updateOrder.Status = item.Status;
            db.SaveChanges();
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
