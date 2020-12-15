using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum StatusEnum { ready=1, InDrive, InTreat, InRefuel }

    public class Bus
    {
        int LicenseNumber { get; set; }
        DateTime LicensingDate { get; set; }
        double Kilometerage { get; set; }
        double Fuel { get; set; }
        StatusEnum Status { get; set; }
        string Driver { get; set; }
        bool IsExist { get; set; } //flag for deleting
    }


    //public enum WindDirections { S, N, W, E, SE, SW, NE, NW, SSE, SEE, SSW, SWW, NNE, NEE, NNW, NWW }
    //public class WindDirection //: IClonable
    //{
    //    public WindDirections direction { get; set; }
    //}


}
