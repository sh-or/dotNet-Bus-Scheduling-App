using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_3B_4484_2389
{
    public enum Status { Ready = 1,NeedCare, NeedRefeul, InDrive, InCare, InRefuel }

    public class Bus1
    {
        public int licenseNum { get; set; }   // save license number 
        public DateTime beginning { get; set; }  //Save the date the bus was added
        public DateTime lastCare { get; set; } //save the date of last care
        public int kmOfLastCare { get; set; }  //save the kilometers from last care
        public int kmOfLastRefuel { get; set; } //save the kilometers from last refuel
        private int kilometerage;  //save the general kilometerage
        public Status status { get; set; }  //enum

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
            if (!((DateTime.Today.AddYears(-1)) < this.lastCare) || (kmOfLastCare) > 18500) //checking time/km from last care
            {
                status = (Status)2; //need care 
            }
            else if (kmOfLastRefuel > 1000) //checking fuel
            {
                status = (Status)3; //need refuel 
            }
            else
                status = (Status)1; //ready
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
            if ((numOfKm + this.kmOfLastCare) > 20000) //the km from last care
            {
                Console.WriteLine("This ride requires a care\n");
                flag = false;
            }
            if ((numOfKm + this.kmOfLastRefuel) > 1200) //the km from last refuel
            {
                Console.WriteLine("This ride requires refuel\n");
                flag = false;
            }
            return flag;
        }
}
}
