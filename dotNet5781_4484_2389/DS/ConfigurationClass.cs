using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public static class ConfigurationClass //contains static counters
    {
        static int stationCode = 52; //because of the reset
        public static int StationCode => stationCode++;

        static int lineCode = 26;//because of the reset
        public static int LineCode => lineCode++;

        static int driveCode = 1; //for bus in drive
        public static int DriveCode => driveCode++;

        //static int userDriveCode = 1;
        //public static int UserDriveCode => driveCode++;
    }
}
