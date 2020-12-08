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
        public Random r = new Random(DateTime.Now.Millisecond);

        public bool isAvailable { get; set; } //is in drive/care/refuel?
        public string timerAct { get; set; } //timer for coming back from the act
        public int licenseNum { get; set; }   // save license number 
        public string showLicenseNum
        {
            get
            {
                string v = licenseNum.ToString();
                if (v.Length == 8)
                    return (v[0]+v[1]+v[2] + "-" + v[3] + v[4] + "-" + v[5] + v[6] + v[7]);
                else
                    return (v[0] + v[1] + "-" + v[2] + v[3] + v[4] + "-" + v[5] + v[6]);
            }
        }
        public DateTime beginning { get; set; }  //Save the date the bus was added
        private DateTime LastCare; //save the date of last care
        public DateTime lastCare { get { return LastCare; } set { LastCare = value; } } //save the date of last care
        public int kmOfLastCare { get; set; }  //save the kilometers from last care
        public int kmOfLastRefuel { get; set; } //save the kilometers from last refuel
        public double Fuel { get; set; } //save the fuel state according to kmOfLastRefuel
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
        public string Begin
        {
            get { return beginning.ToString(@"dd\/MM\/yyyy"); }
            set { }
        }

        public string LCare
        {
            get { return lastCare.ToString(@"dd\/MM\/yyyy"); }
            set { }
        }

        public Bus1(int liceNum, DateTime begin, DateTime lastC, int kmLastCare=0, int kmLastRefuel=0, int km=0)  //c-tor 
        {
            licenseNum = liceNum;
            beginning = begin;
            lastCare = lastC;
            kmOfLastCare = kmLastCare;
            kmOfLastRefuel = kmLastRefuel;
            Fuel = 1 - kmOfLastRefuel / 1200.0;
            if (km < kmOfLastCare|| km < kmOfLastRefuel)
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
            isAvailable = true;
            timerAct = "";
        }

        public bool isReady(int numOfKm)   //check the fuel and care of asked bus and ride
        {
            DateTime dt = DateTime.Now;
            if (!((dt.AddYears(-1)) < this.lastCare)) //the time from last care
            {
                status = (Status)2;  //need care
                 //close window1, and message box "need care"
                return false;
            }

            if ((numOfKm + this.kmOfLastCare) > 20000) //the km from last care
            {
                if(kmOfLastCare>18500)
                    status = (Status)2; //need care
                return false;
            }
            else if ((numOfKm + this.kmOfLastRefuel) > 1200) //the km from last refuel
            {
                if (kmOfLastRefuel>1000)
                    status = (Status)3;  //need refuel
                return false;
            }
            return true;

        }
    }
}
