using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserDrive
    {
        public int DriveCode { get; set; }
        public string UserName { get; set; }
        public int LineCode { get; set; }
        public int FirstStation { get; set; }
        public TimeSpan GetOnTime { get; set; }
        public int LastStation { get; set; }
        public TimeSpan GetOffTime { get; set; }
    }
}
