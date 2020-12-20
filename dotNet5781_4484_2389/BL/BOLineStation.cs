using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        public int StationCode { get; set; } //linestation
        public string Name { get; set; } //busstation
        public double Distance { get; set; } //from consec'
        public int DriveTime { get; set; } //from consec'

    }
}
