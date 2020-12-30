using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO; //?

namespace BO
{
    public class BOBus
    {
        public int LicenseNumber { get; set; }
        public DateTime LicensingDate { get; set; }
        public double Kilometerage { get; set; }
        public double Fuel { get; set; }
        public double KmFromLastRefuel { get; set; }
        public double KmFromLastCare { get; set; }
        public DateTime DateOfLastCare { get; set; }
        public StatusEnum Status { get; set; }
        public string Driver { get; set; }
        public bool IsExist { get; set; } //flag for deleting
    }
}
