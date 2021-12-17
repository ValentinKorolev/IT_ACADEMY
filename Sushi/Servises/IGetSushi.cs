using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Servises
{
    public interface IGetSushi<T> where T : class
    {
        IEnumerable<T> GetAllUramaki();
        IEnumerable<T> GetAllFutomaki();
        IEnumerable<T> GetAllNigiri();
        IEnumerable<T> GetAllBakedSushi();
    }
}
