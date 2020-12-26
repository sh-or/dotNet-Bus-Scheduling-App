using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOLineStation
    {
        public int StationCode { get; set; } //linestation
        public string Name { get; set; } //busstation
        public double Distance { get; set; } //from consec'
        public TimeSpan DriveTime { get; set; } //from consec'
        

    }
}
