using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Servises
{
    public interface IRepositorySushi<Sushi> 
    {
        IEnumerable<Sushi> GetSushi(string typeSushi);
    }
}
