using System;
using System.Collections.Generic;
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
                /*set { kilometerage = value; }*/
            }
            public Bus (int liceNum, DateTime begin) //can get more?
            {
                licenseNum = liceNum;
                beginning = begin;
                lastCare = begin;
                kmOfLastCare = 0;
                kmOfLastRefuel = 0;
                kilometerage = 0;
            }
        }
        enum Choice { addBus = 1, busToDrive, fuelCare, showKmLastCare, exit }
        static void Main(string[] args)
        {
            DateTime dt = new DateTime();
            int tmp1, tmp2, tmp3;
            bool bl1, bl2;
            Random rndKm = new Random(DateTime.Now.Millisecond); //random Km
            int myChoice;
            List<Bus> buses= new List<Bus>();
            //List<Bus> //איטרטור
            do
            {
                bl1 = bl2 = false;
                Console.WriteLine("Enter your choice");
                myChoice = int.Parse(Console.ReadLine());
                switch ((Choice)myChoice)
                {
                    case Choice.addBus:
                        Console.WriteLine("Enter licence number and date of beginning in format [day/month/year]");
                        tmp1= int.Parse(Console.ReadLine()); //cin licNum. להוסיף חריגות 7-8 ספרות לפי שנה
                        dt = DateTime.Parse(Console.ReadLine());
                        buses.Add(new Bus(tmp1, dt));
                        break;
                    case Choice.busToDrive:
                        Console.WriteLine("Enter licence number");
                        tmp1 = int.Parse(Console.ReadLine());
                        rndKm.Next(1201); //זה המרחק המקס?

                        //Bus wantedBus= //ref?iterator
                        foreach (Bus b in buses) //להוציא כפונק' חיפוש שמחזירה את האוטובוס המבוקש
                        {
                            if (tmp1 == b.licenseNum)
                                bl1 = true; break;
                        }
                        //return Bus;
                        if (wantedBus==null)
                            Console.WriteLine("Bus is not exist"); //break;

                        dt = DateTime.Now;
                        if((dt-DateTime.Parse(1))<wantedBus.
                            //להוציא כפונק' תקינות אטובוס
                        
                        if(!bl2)
                                Console.WriteLine("");
                            else
                        {

                        }
                            
                        break;
                    case Choice.fuelCare:
                        break;
                    case Choice.showKmLastCare:
                        break;
                    case Choice.exit:
                        break;
                }
            } while (myChoice != 5);
        }
    }
}
