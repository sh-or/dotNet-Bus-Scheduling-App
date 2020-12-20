using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusStation
    {
        public int StationCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //area??
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Accessibility { get; set; } //for disabled
        public bool IsExist { get; set; } //???
        public List<StationLine> Lines { get; set; }
    }
}
