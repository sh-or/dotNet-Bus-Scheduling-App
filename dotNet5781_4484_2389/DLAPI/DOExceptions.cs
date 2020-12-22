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
        public int Num { get; set; }
        public DOException(string message) : base(message) { }
        public DOException(string message, Exception inner) : base(message, inner) { }
        public DOException(int num, string message) : base(message) => Num = num;
        public DOException(int num, string message, Exception inner) : base(message, inner) => Num = num;
        public override string ToString() => base.ToString() + Message;
    }
    //public class BusStationNotFoundEx : Exception
    //{
    //    public int Num { get; set; }
    //    public BusStationNotFoundEx(int num, string message) : base(message) => Num = num;
    //    public BusStationNotFoundEx(int num, string message, Exception inner) : base(message, inner) => Num = num;
    //    public override string ToString() => base.ToString() + $"Bus station number {Num} was not found";
    //}
    //public class BusNotFoundEx : Exception /////////
    //{
    //    public int Num { get; set; }
    //    public BusNotFoundEx(int num, string message) : base(message) => Num = num;
    //    public BusNotFoundEx(int num, string message, Exception inner) : base(message, inner) => Num = num;
    //    public override string ToString() => base.ToString() + $" Bus number {Num} was not found";
    //}
    //public class LineNotFoundEx : Exception /////////
    //{
    //    public int Num { get; set; }
    //    public LineNotFoundEx(int num, string message) : base(message) => Num = num;
    //    public LineNotFoundEx(int num, string message, Exception inner) : base(message, inner) => Num = num;
    //    public override string ToString() => base.ToString() + $" Line number {Num} was not found";
    //}
    //public class ExistBusesNotFoundEx : Exception /////////
    //{
    //    public ExistBusesNotFoundEx(string message) : base(message) { }
    //    public ExistBusesNotFoundEx(string message, Exception inner) : base(message, inner) { }
    //    public override string ToString() => base.ToString() + " Exist Buses were not found";
    //}
}

