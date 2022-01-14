
namespace SushiMarcet.DataBase
{
    internal class SqlDrinksRepository : IRepository<Drinks>, IDisposable
    {
        Logger Logger  = new Logger();

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

                Logger.Debug($"Product added to Db {item.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                new Logger().Error("Adding a product to the database ended with an error", ex);

                Clear();
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

                Logger.Debug($"Product deleted to Db {deleteDrinks.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                new Logger().Error("Deleting a product to the database ended with an error", ex);

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
                var updateDrinks = GetItem(item.Id);
                db.Entry(updateDrinks).CurrentValues.SetValues(item);
                db.SaveChanges();

                Logger.Debug($"Product updated to Db {updateDrinks.ShowDataForAdmin()}");
            }
            catch (Exception ex)
            {
                new Logger().Error("Updating a product to the database ended with an error", ex);

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
