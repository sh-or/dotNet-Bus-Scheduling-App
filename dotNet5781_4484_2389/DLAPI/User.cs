using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        public string Name { get; set; }        //id
        public string Password { get; set; }  
        public bool IsManager { get; set; }
        public bool IsExist { get; set; } //for deleting

    }
}
