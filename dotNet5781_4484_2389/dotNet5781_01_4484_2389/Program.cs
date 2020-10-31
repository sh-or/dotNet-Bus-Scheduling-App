using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_4484_2389
{
    class Program
    {
        class Bus
        {
            public int licenseNum;
            public DateTime beginning;
            public DateTime lastCare; //save the date of last care
            public int kmOfLastCare; //save the kilometerage of last care
            public int kmOfLastRefuel; //save the kilometerage of last refuel
            private int kilometerage;
            public int Kilometerage
            {
                get { return kilometerage; }
                set
                {
                    if (value < 0)
                        throw new SomeException("Kilometerage cannot be reduced\n");
                    kilometerage = value; 
                }
            }
            public Bus(int liceNum, DateTime begin) //can get more?
            {
                licenseNum = liceNum;
                beginning = begin;
                lastCare = begin;
                kmOfLastCare = 0;
                kmOfLastRefuel = 0;
                kilometerage = 0;
            }
            public bool isReady(int numOfKm)
            {//check the fuel and care of the asked bus and ride
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
        private static Bus findBus(int lcNum, List<Bus> buses)
        {//find bus in buses' list by lcNum
            foreach (Bus b in buses)
            {
                if (lcNum == b.licenseNum)
                {
                    return b;
                }
            }
            throw new SomeException("The bus is not exist\n");
        }
        enum Choice { addBus = 1, busToDrive, fuelCare, showKmLastCare, exit, /*Default=exit*/ }
        static void Main(string[] args)
        {
            DateTime dt;
            int lcNum, tmp1, numOfKm;
            Random rndKm = new Random(DateTime.Now.Millisecond); //random Km
            int myChoice;
            List<Bus> buses = new List<Bus>(); //list of buses
            do
            {
                Console.WriteLine("Enter your choice");
                myChoice = int.Parse(Console.ReadLine());
                switch ((Choice)myChoice)
                {
                    case Choice.addBus:
                        Console.WriteLine("Enter licence number and date of beginning in format [day/month/year]");
                        lcNum = int.Parse(Console.ReadLine()); //cin lcNum. להוסיף חריגות 7-8 ספרות לפי שנה
                        dt = DateTime.Parse(Console.ReadLine());
                        buses.Add(new Bus(lcNum, dt));
                        break;
                    case Choice.busToDrive:
                        Console.WriteLine("Enter licence number\n");
                        lcNum = int.Parse(Console.ReadLine());
                        numOfKm = rndKm.Next(1201); //the longest possible rout
                        try
                        {
                            Bus wantedBus = findBus(lcNum, buses);
                            //return Bus;
                            if (wantedBus != null)
                            {
                                if (wantedBus.isReady(numOfKm))
                                {
                                    wantedBus.Kilometerage += numOfKm;
                                    Console.WriteLine("Bus number {0} went on a ride of {1} km\n",wantedBus.licenseNum, numOfKm);
                                }
                            }
                        }
                        catch (SomeException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        break;
                    case Choice.fuelCare:
                        {
                            try
                            {
                                Console.WriteLine("Enter licence number\n");
                                lcNum = int.Parse(Console.ReadLine());
                                Bus wantedBus = findBus(lcNum, buses);
                                Console.WriteLine("Enter 1 for refuel and 2 for care\n");
                                tmp1 = int.Parse(Console.ReadLine());
                                if (tmp1 == 1)
                                {
                                    wantedBus.kmOfLastRefuel = wantedBus.Kilometerage;
                                }
                                if(tmp1==2)
                                {
                                    wantedBus.kmOfLastCare = wantedBus.Kilometerage;
                                    wantedBus.lastCare = DateTime.Now;
                                }
                                //להחליט מה עושים אם הקלט שונה מ1 או 2
                            }
                            catch (SomeException ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        break;
                    case Choice.showKmLastCare:
                        {
                            foreach (Bus b in buses)
                            {
                                Console.WriteLine("Bus number {0} with kilometerage {1}", b.licenseNum, b.Kilometerage);
                            }
                            //הודעה לרשימה ריקה?
                        }
                        break;
                    case Choice.exit:
                        break;
                    default:
                        Console.WriteLine("wrong input\n");
                        break;
                    //default: ->auotomatic throw?
                }
            } while (myChoice != 5);
            Console.WriteLine("end\n");
        }
    }
}
