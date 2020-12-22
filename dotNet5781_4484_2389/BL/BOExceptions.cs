using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    //BO Exceptions:
    public class BLException : Exception
    {
        public BLException(string message) : base(message) { }
        public BLException(string message, Exception inner) : base(message, inner) { }
        public override string ToString() => base.ToString() + Message;
    }
}
