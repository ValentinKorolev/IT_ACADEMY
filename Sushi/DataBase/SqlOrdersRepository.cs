
namespace SushiMarcet.DataBase
{
    internal class SqlOrdersRepository : IRepository<Order>, IDisposable
    {
        Logger Logger = new Logger();

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

                Logger.Debug($"Order added to Db {item.ShowData()}");

            }
            catch (Exception ex)
            {
                Logger.Error($"Adding the order to Db ends with error ", ex);

                WriteLine("The operation ended in failure");
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

        public Order GetItem(Guid id)
        {
            return db.Order.FirstOrDefault(_ => _.Id == id);
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
            try
            {
                Order updateOrder = db.Order.FirstOrDefault(_ => _.Id == item.Id);
                updateOrder.Status = item.Status;
                db.SaveChanges();

                Logger.Debug($"Order updated to Db {updateOrder}");
            }
            catch (Exception ex)
            {
                Logger.Error("Updating the order to Db ends with error", ex);

                Clear();
                WriteLine("The operation ended in failure");
                Thread.Sleep(10000);
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
