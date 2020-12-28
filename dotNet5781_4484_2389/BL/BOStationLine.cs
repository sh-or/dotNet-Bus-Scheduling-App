using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOStationLine //line(that cross in spesific station) details
    {
        public int Code { get; set; }
        public int BusLine { get; set; }
        public int LastStation { get; set; }
        public int IndexOfThisStation { get; set; }

    }
}
