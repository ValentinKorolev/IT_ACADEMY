using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Interfaces
{
    public interface IShowDataProduct
    {
        string ShowData(int numServings = 1);

        string ShowDataForAdmin();
    }
}
