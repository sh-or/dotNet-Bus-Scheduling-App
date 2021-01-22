using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation //connect station to line that cross in it
    {
        public int LineCode { get; set; }
        public int StationCode { get; set; }
        public int StationNumberInLine { get; set; }
        public bool IsExist { get; set; }
    }
}
