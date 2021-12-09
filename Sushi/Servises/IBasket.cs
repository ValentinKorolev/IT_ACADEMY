using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Servises
{
    public interface IBasket<T> where T : class
    {
        public void Add(T item);
        public void Delete(T item);
    }
}
