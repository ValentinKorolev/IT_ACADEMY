using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SushiMarcet
{
    internal interface ITransferPage
    {
        public void TransferPage(ConsoleKey keyPressed, string[] _options, int _selectedIndex) { }
    }
}
