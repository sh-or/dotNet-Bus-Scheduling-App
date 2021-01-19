using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    //DO Exception:
    public class DOException : Exception 
    {
        //public int Num { get; set; }
        public DOException(string message) : base(message) { }
        public DOException(string message, Exception inner) : base(message, inner) { }
       // public DOException(int num, string message) : base(message) => Num = num;
       // public DOException(int num, string message, Exception inner) : base(message, inner) => Num = num;
        public override string ToString() => base.ToString() + Message;
         
    }

    public class XMLFileLoadCreateException : Exception
    {
        public XMLFileLoadCreateException() : base() { }
        public XMLFileLoadCreateException( string message) : base(message){ }
        public XMLFileLoadCreateException(string message, Exception innerException) :base(message, innerException)  { }
        public override string ToString() => base.ToString();
    }
}

