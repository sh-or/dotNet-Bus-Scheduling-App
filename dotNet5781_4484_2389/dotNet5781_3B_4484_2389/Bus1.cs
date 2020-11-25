using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_3B_4484_2389
{
    public class Bus1
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
                while (value < 0)     //Negative distance cannot be added
                {
                    Console.WriteLine("ERROR: negative distance");
                    value = int.Parse(Console.ReadLine());
                }
                kilometerage = value;
            }
        }

        public Bus1(int liceNum, DateTime begin, DateTime lastC, int kmLastCare=0, int kmLastRefuel=0, int km=0)  //c-tor 
        {
            licenseNum = liceNum;
            beginning = begin;
            lastCare = lastC;
            kmOfLastCare = kmLastCare;
            kmOfLastRefuel = kmLastRefuel;
            if (km == 0)
                kilometerage = Math.Max(kmLastCare, kmLastRefuel);
            else
                kilometerage = km;
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
