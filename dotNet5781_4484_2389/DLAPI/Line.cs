using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum AreaEnum { Center=1, South, North, Lowland, Jerusalem}

    public class Line
    {
        int Code { get; set; }
        int BusLine { get; set; }
        AreaEnum Area { get; set; }
        int FirstStation { get; set; }
        int LastStation { get; set; }
       // bool IsExist { get; set; } //flag for deleting

    }
}
