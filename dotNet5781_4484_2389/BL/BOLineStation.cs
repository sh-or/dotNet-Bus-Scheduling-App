using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOLineStation //details about station in line
    {
        public int StationCode { get; set; } 
        public string Name { get; set; } 
        public double Distance { get; set; } 
        public TimeSpan DriveTime { get; set; }      
    }
}
