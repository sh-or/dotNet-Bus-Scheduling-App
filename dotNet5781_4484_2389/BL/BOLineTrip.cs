using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOLineTrip
    {
        public int LineCode { get; set; }
        public int BusLine { get; set; } //for presentation
        public int StationCode { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan Arrive { get; set; }
        public bool IsExist { get; set; }
    }
}
