using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class BOUser
    {
        public string Name { get; set; } //the ID
        public string Password { get; set; }
        public bool IsManager { get; set; }
        public bool IsExist { get; set; } //for deleting
        public IEnumerable<BOUserDrive> Drives { get; set; }
    }
}
