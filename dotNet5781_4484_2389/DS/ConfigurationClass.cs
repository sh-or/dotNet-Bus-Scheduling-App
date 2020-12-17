using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public static class ConfigurationClass //contains static counters
    {
        static int licenseNum = 10000000;
        public static int LicenseNum => licenseNum++;
        static int stationCode = 1;
        public static int StationCode => stationCode++;
        static int lineCode = 1;
        public static int LineCode => lineCode++;

    }
}
