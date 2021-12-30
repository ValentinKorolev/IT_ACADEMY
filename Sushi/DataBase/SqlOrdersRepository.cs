using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.DataBase
{
    internal class SqlOrdersRepository : IRepository<Order>
    {

        private ApplicationContext db;

        public SqlOrdersRepository()
        {
            db = new ApplicationContext();
        }

        public void Create(Order item)
        {
            throw new NotImplementedException();
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

        public void Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
