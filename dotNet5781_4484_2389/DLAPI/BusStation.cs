using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusStation
    {
        public int StationCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Accessibility { get; set; } //Accessibility for disabled
        public bool IsExist { get; set; } //flag for deleting
    }
}
