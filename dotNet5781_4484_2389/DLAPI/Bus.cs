using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {
        public int LicenseNumber { get; set; }
        public DateTime LicensingDate { get; set; }
        public double Kilometerage { get; set; }
        public double Fuel { get; set; }
        public double KmFromLastRefuel { get; set; }
        public double KmFromLastCare { get; set; }
        public DateTime DateOfLastCare { get; set; }
        public StatusEnum Status { get; set; }
        public string Driver { get; set; }
        public bool IsExist { get; set; } //flag for deleting
    }


    //public enum WindDirections { S, N, W, E, SE, SW, NE, NW, SSE, SEE, SSW, SWW, NNE, NEE, NNW, NWW }
    //public class WindDirection //: IClonable
    //{
    //    public WindDirections direction { get; set; }
    //}


}
