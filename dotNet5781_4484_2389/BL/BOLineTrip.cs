using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOLineTrip
    {
        int LineCode { get; set; }
        int BusLine { get; set; } //for presentation
        int StationCode { get; set; }
        TimeSpan Start { get; set; }
        TimeSpan Arrive { get; set; }
    }
}
