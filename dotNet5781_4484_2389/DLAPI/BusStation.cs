using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class BusStation
    {
        int StationCode { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        string Name { get; set; }
        string Address { get; set; }
        bool Accessibility { get; set; } //Accessibility for disabled
        bool IsExist { get; set; } //flag for deleting
    }
}
