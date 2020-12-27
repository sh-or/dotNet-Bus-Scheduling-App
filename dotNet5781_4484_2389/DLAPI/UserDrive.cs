using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class UserDrive
    {
        int DriveCode { get; set; }
        string UserName { get; set; }
        int LineCode { get; set; }
        int FirstStation { get; set; }
        TimeSpan GetOnTime { get; set; }
        int LastStation { get; set; }
        TimeSpan GetOffTime { get; set; }
    }
}
