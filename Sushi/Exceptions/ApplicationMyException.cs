using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarcet.Exceptions
{
    [Serializable]
    public class ApplicationMyException : Exception
    {
        public ApplicationMyException() { }

        public ApplicationMyException(string message) : base(message) { }

        public ApplicationMyException(string message, Exception inner) : base(message, inner) { }

    }
}
