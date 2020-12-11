using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace dotNet5781_3B_4484_2389
{
    public enum Status { Ready = 1,NeedCare, NeedRefeul, InDrive, InCare, InRefuel }

    public class Bus1 : INotifyPropertyChanged
    {
        public Random r = new Random(DateTime.Now.Millisecond);
        public event PropertyChangedEventHandler PropertyChanged;
        public bool isAvailable { get; set; } //is in drive/care/refuel?
        public string timerAct { get; set; } //timer for coming back from the act
        public int licenseNum { get; set; }   // save license number 
        public string showLicenseNum
        {
            get
            {
                string str = "";
                string v = licenseNum.ToString();
                if (v.Length == 8)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        str += v[i];
                        if (i == 2 || i == 4)
                            str += "-";
                    }
                }
                   // return (v[0] + v[1] + v[2] + "-" + v[3] + v[4] + "-" + v[5] + v[6] + v[7]);
                else
                    for (int i = 0; i < 7; i++)
                    {
                        str += v[i];
                        if (i == 1 || i == 4)
                            str += "-";
                    }
                //return (v[0] + v[1] + "-" + v[2] + v[3] + v[4] + "-" + v[5] + v[6]);
                return str;
            }
        }
        public DateTime beginning { get; set; }  //Save the date the bus was added
        private DateTime LastCare; //save the date of last care
        public DateTime lastCare { get { return LastCare; } set { LastCare = value; } } //save the date of last care
        private int kmOfLastCare;  //save the kilometers from last care
        public int KmOfLastCare
        {
            get { return kmOfLastCare; }
            set
            {
                kmOfLastCare = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("KmOfLastCare"));
                }
            }
        }
        private int kmOfLastRefuel; //save the kilometers from last refuel
        public int KmOfLastRefuel
        {
            get { return kmOfLastRefuel; }
            set
            {
                kmOfLastRefuel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("KmOfLastRefuel"));
                }
            }
        }
        public double Fuel { get; set; } //save the fuel state according to KmOfLastRefuel
        private int kilometerage;  //save the general kilometerage
        private Status status;
        public Status Status
        {
            get { return status; }
            set
            {
                status = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }  //enum
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
        public string showBeginning //show in date format for the details window
        {
            get { return beginning.ToString(@"dd\/MM\/yyyy"); }
            set { }
        }

        public string showLastCare //show in date format for the details window
        {
            get { return lastCare.ToString(@"dd\/MM\/yyyy"); }
            set { }
        }

        public Bus1(int liceNum, DateTime begin, DateTime lastC, int kmLastCare=0, int kmLastRefuel=0, int km=0)  //c-tor 
        {
            licenseNum = liceNum;
            beginning = begin;
            lastCare = lastC;
            KmOfLastCare = kmLastCare;
            KmOfLastRefuel = kmLastRefuel;
            Fuel = 1 - KmOfLastRefuel / 1200.0;
            if (km < KmOfLastCare|| km < KmOfLastRefuel)
                kilometerage = Math.Max(kmLastCare, kmLastRefuel);
            else
                kilometerage = km;
            if (!((DateTime.Today.AddYears(-1)) < this.lastCare) || (KmOfLastCare) > 18500) //checking time/km from last care
            {
                Status = (Status)2; //need care 
            }
            else if (KmOfLastRefuel > 1000) //checking fuel
            {
                Status = (Status)3; //need refuel 
            }
            else
                Status = (Status)1; //ready
            isAvailable = true/*(Status== (Status)1)*/;

            timerAct = "";
        }

        public bool isReady(int numOfKm)   //check the fuel and care of asked bus and ride
        {
            DateTime dt = DateTime.Now;
            if (!((dt.AddYears(-1)) < this.lastCare)) //the time from last care
            {
                Status = (Status)2;  //need care
                 //close window1, and message box "need care"
                return false;
            }

            if ((numOfKm + this.KmOfLastCare) > 20000) //the km from last care
            {
                if(KmOfLastCare>18500)
                    Status = (Status)2; //need care
                return false;
            }
            else if ((numOfKm + this.KmOfLastRefuel) > 1200) //the km from last refuel
            {
                if (KmOfLastRefuel>1000)
                    Status = (Status)3;  //need refuel
                return false;
            }
            return true;

        }
    }
}
