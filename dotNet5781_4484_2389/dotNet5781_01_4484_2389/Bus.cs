using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_4484_2389
{
    class Bus
    {
        public int licenseNum;   // save license number 
        public DateTime beginning;  //Save the date the bus was added
        public DateTime lastCare; //save the date of last care
        public int kmOfLastCare;  //save the kilometerage from last care
        public int kmOfLastRefuel; //save the kilometerage from last refuel
        private int kilometerage;  //save the general kilometerage

        public int Kilometerage  //"set/get" of general kilometerage
        {
            get { return kilometerage; }
            set
            {
                if (value < 0)     //Negative distance cannot be added
                    throw new SomeException("Kilometerage cannot be reduced\n");
                kilometerage = value;
            }
        }

        public Bus(int liceNum, DateTime begin)  //constructor bus
        {
            licenseNum = liceNum;
            beginning = begin;
            lastCare = begin;
            kmOfLastCare = 0;
            kmOfLastRefuel = 0;
            kilometerage = 0;
        }

        public bool isReady(int numOfKm)   //check the fuel and care of asked bus and ride
        {
            DateTime dt = DateTime.Now;
            if (!((dt.AddYears(-1)) < this.lastCare)) //the time from last care
            {
                Console.WriteLine("The last care was more than a year ago\n");
                return false;
            }
            bool flag = true;
            if ((this.Kilometerage + numOfKm - this.kmOfLastCare) > 20000) //the km from last care
            {
                Console.WriteLine("This ride requires a care\n");
                flag = false;
            }
            if ((this.Kilometerage + numOfKm - this.kmOfLastRefuel) > 1200) //the km from last refuel
            {
                Console.WriteLine("This ride requires refuel\n");
                flag = false;
            }
            return flag;
        }
    }
}
