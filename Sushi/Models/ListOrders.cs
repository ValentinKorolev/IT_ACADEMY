using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Models
{
    internal sealed class ListOrders
    {
        public List<Order> Orders { get; set; } = new();
    }
}
