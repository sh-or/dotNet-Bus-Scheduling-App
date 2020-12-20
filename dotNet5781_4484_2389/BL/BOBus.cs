using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {
        public int LicenseNumber { get; set; }
        public DateTime LicensingDate { get; set; }
        public double Kilometerage { get; set; }
        public double Fuel { get; set; }
        public StatusEnum Status { get; set; }
        public string Driver { get; set; }
        public bool IsExist { get; set; } //???
    }
}
