
namespace SushiMarcet.Models
{
    internal class SqlSushiRepository : IRepository<Sushi>, IDisposable
    {
        Logger Logger = new Logger();

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

                Logger.Debug($"Product added to Db {item.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                new Logger().Error("Creating a product to the database ended with an error", ex);

                Clear();
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

                Logger.Debug($"Product deleted to Db {deleteSushi.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                new Logger().Error("Deleting a product to the database ended with an error", ex);

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

        public IEnumerable<Sushi> GetListItem(string typeSushi)
        {
            return db.Sushi.Where(_ => _.Type == typeSushi).ToList();
        }

        public void Update(Sushi item)
        {
            try
            {
                var updateSushi = GetItem(item.Id);

                db.Entry(updateSushi).CurrentValues.SetValues(item);
                db.SaveChanges();

                Logger.Debug($"Product updated to Db {updateSushi.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                new Logger().Error("Updating a product to the database ended with an error", ex);

                Clear();
                WriteLine($"Error !!! Please, read logs");
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
