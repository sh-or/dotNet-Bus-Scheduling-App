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

        private static Bus findBus(int lcNum, List<Bus> buses) //find bus in buses' list by license number
        {
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
            Random rndKm = new Random(DateTime.Now.Millisecond); //random Km for drive
            int myChoice;
            List<Bus> buses = new List<Bus>(); //list of buses
            Console.WriteLine("1: add bus\n2: bus to drive\n3: refuel or Care\n4: show Km from last care\n5: exit\n");   //Print menu selection

            do
            {
                Console.WriteLine("Enter your choice");
                myChoice = int.Parse(Console.ReadLine());
                switch ((Choice)myChoice)
                {
                    case Choice.addBus:
                        Console.WriteLine("Enter licence number and date of beginning in format [day/month/year]");
                        lcNum = int.Parse(Console.ReadLine()); //cin license number
                        dt = DateTime.Parse(Console.ReadLine());  //cin date beggining
                        buses.Add(new Bus(lcNum, dt));   //add bus to list
                        break;

                    case Choice.busToDrive:
                        Console.WriteLine("Enter licence number");
                        lcNum = int.Parse(Console.ReadLine());  //cin license number
                        numOfKm = rndKm.Next(1201);  // random Km for this drive. (1200 is the longest possible rout)
                        try
                        {
                            Bus wantedBus = findBus(lcNum, buses);
                            if (wantedBus != null)  //the bus exists in list
                            {
                                if (wantedBus.isReady(numOfKm))
                                {
                                    wantedBus.Kilometerage += numOfKm;
                                    Console.WriteLine("Bus number {0} went on a ride of {1} km\n", wantedBus.licenseNum, numOfKm);  //המספר צריך להיות מודפס בפורמט??
                                }
                            }
                        }
                        catch (SomeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case Choice.fuelCare:
                        {
                            try
                            {
                                Console.WriteLine("Enter licence number");
                                lcNum = int.Parse(Console.ReadLine());  //cin license number
                                Bus wantedBus = findBus(lcNum, buses);
                                Console.WriteLine("Enter 1 for refuel or 2 for care");
                                tmp1 = int.Parse(Console.ReadLine());   //cin choice for refuel/care.

                                //if (tmp1!=1||tmp1!=2)     //להחליט מה עושים אם הקלט שונה מ1 או 2
                                //   throw "Your coice does not exist"

                                if (tmp1 == 1) //refuel
                                {
                                    wantedBus.kmOfLastRefuel = wantedBus.Kilometerage;
                                    Console.WriteLine("The treatment was performed successfully\n");
                                }
                                if (tmp1 == 2)  //care
                                {
                                    wantedBus.kmOfLastCare = wantedBus.Kilometerage;
                                    wantedBus.lastCare = DateTime.Now;
                                    Console.WriteLine("Refueling was successful\n");
                                }
                            }
                            catch (SomeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        break;

                    case Choice.showKmLastCare:
                        {
                            foreach (Bus b in buses)
                            {
                                string v = b.licenseNum.ToString();
                                if (v.Length == 8)
                                {
                                    int A = b.licenseNum / 100000;
                                    int B = b.licenseNum / 1000 % 100;
                                    int C = b.licenseNum % 1000;
                                    Console.WriteLine("Bus number: {0}-{1}-{2} , kilometerage from last care: {3}\n", A, B, C, b.kmOfLastCare);
                                }

                                else
                                {
                                    int A = b.licenseNum / 100000;
                                    int B = b.licenseNum / 100 % 1000;
                                    int C = b.licenseNum % 100;
                                    Console.WriteLine("Bus number: {0}-{1}-{2} , kilometerage from last care: {3}\n", A, B, C, b.kmOfLastCare);
                                }
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
