using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet
{
    internal interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        public T GetById(Guid id);
        public T Create(T item);
        public void Update(T item);
        public void Delete(Guid id);
    }
}
