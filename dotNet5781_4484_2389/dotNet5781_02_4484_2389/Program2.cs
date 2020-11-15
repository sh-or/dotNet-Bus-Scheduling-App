using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4484_2389
{
    class Program2
    {
        //אולי להכניס מאפיין (של הרשימה הכללית) למערכת ולוודא בכל קו שמוסיפים למערכת שהרשימה הכללית של התחנות תואמת לשלו
        enum Choice { Add = 1, Delete, SearchLine, SearchRoute, Print, exit }
        static void Main(string[] args)
        {
            List<BusStation> allBusStations = new List<BusStation>();  //the main stations list
            BusLineSystem allBusLines = new BusLineSystem();  //all the lines

            restart(allBusStations, allBusLines);  //input 10 lines & 40 stations

            List<int> lineKeysList;
            List<BusLine> linesList;
            int ch, st1, st2;
            Console.WriteLine("1: add new line bus or station in line bus\n" +
                "2: delete line bus or station from line bus\n" +
                "3: search lines in station\n" +
                "4: search route\n" +
                "5: print all line's bus or lines in stations\n");

            do
            {
                Console.WriteLine("Enter your choice\n"); //+menu
                ch = int.Parse(Console.ReadLine());
                switch ((Choice)ch)
                {
                    case Choice.Add:
                        {
                            Console.WriteLine("enter 1 to add new line bus, enter 2 to add new station in line bus");
                            int tmp1 = int.Parse(Console.ReadLine());   //cin choice
                            if (tmp1 == 1)
                            {
                                Console.WriteLine("enter area for bus line:");//המשתמש מכניס? לעהציג לו תפריט?
                                Area area1 = int.Parse(Console.ReadLine());   /// 
                                allBusLines.addLine(new BusLine(area1, allBusStations));
                            }
                            if (tmp1 == 2)
                            {
                                Console.WriteLine("enter name of station");
                                string addres = Console.ReadLine();
                                allBusStations.Add(new BusStation(addres));  //add station to the nain station list
                                int stkey = allBusStations.Find(BusStation.address == addres).busStationKey;  //למצוא את הקוד של התחנה שהוספנו
                                Console.WriteLine("enter number line");
                                int line1= int.Parse(Console.ReadLine());
                                //BusLine line = new BusLine[line1];
                                //שימוש באינדקסר. למי הוא מחזיר מופע??
                              //  if (allBusLines[line1]) //למצוא את הקו המבוקש בתוך רשימת הקווים הכללית
                              //  {
                              //  }
                                Console.WriteLine("enter index to add station in line bus");
                                int index = int.Parse(Console.ReadLine());
                                //busline.addStation(stkey, index)   //להוסיף לקו שנמצא תחנה
                            }



                            break;
                        }

                    case Choice.Delete:
                        {
                            Console.WriteLine("enter 1 to delete line bus, enter 2 to delete station from line bus");
                            int tmp1 = int.Parse(Console.ReadLine());   //cin choice

                            if (tmp1 == 1)
                            {
                                Console.WriteLine("enter number line" );
                                int line1 = int.Parse(Console.ReadLine());
                                BusLine busline1 =   //חיפןש קו לפי מספר קו ברשימה הכללית של הקווים
                               if (allBusLines.deletedLine(busline1))
                                    Console.WriteLine("The line bus deleted"); 
                            else
                                    Console.WriteLine("ERROR");
                                    
                            }
                            if (tmp1 == 2)
                            {
                                Console.WriteLine("enter name of station");
                                string addres = Console.ReadLine();
                                Console.WriteLine("enter number line");
                                int line1 = int.Parse(Console.ReadLine());

                                int stkey = allBusStations.Find(BusStation.address == addres).busStationKey;  //למצוא את הקוד של התחנה
                               // BusLine busline1=allBusLines.line
                               // deleteStation()

                            }
                            break;
                        }

                    case Choice.SearchLine:
                        {
                            Console.WriteLine("Enter station number\n");
                            st1 = int.Parse(Console.ReadLine());
                            lineKeysList = allBusLines.findLinesOfStation(st1);  //st1?
                            foreach (int n in lineKeysList)
                                Console.Write(n + " ");
                            Console.WriteLine();
                            //ex if lst empty(with bool flag?) + try...
                            break;
                        }

                    case Choice.SearchRoute:
                        {
                            Console.WriteLine("Enter first and last stations numbers\n");
                            st1 = int.Parse(Console.ReadLine());
                            st2 = int.Parse(Console.ReadLine());
                            linesList = new List<BusLine>();
                            //linesList.sortedlines.print(); //צריך להפוך את הרשימה לטיפוס מערכת קווים...
                            break;
                        }
                    case Choice.Print:
                        {
                            Console.WriteLine("enter 1 to print all bus line's , enter 2 to print all bus stations and bus line's in thr ststions");
                            int tmp1 = int.Parse(Console.ReadLine());   //cin choice
                            if (tmp1 == 1)  //printAll מדפיס הכל
                            {

                            }
                            if (tmp1==2)
                            {

                            }
                                break;
                        }
                    case Choice.exit:
                        break;

                    default:
                        Console.WriteLine("wrong input\n");
                        break;
                }

            } while (ch != 6);  //6 exit 

        }

        private static void restart(List<BusStation> allBusStations, BusLineSystem allBusLines)
        {
            //יהודית:) יש 10 קוים ו40 תחנות. עכשיו צריך להוסיף מסלול לכל קו, לפי ההוראות שהם נתנו
            Console.WriteLine("There are 40 stations and 10 lines\n");

            allBusLines.addLine(new BusLine(Area.Center, allBusStations));   //צריך להוסיף את האתחול בבנאי של ה2 תחנות. הוא ירוק
            allBusLines.addLine(new BusLine(Area.Center, allBusStations));
            allBusLines.addLine(new BusLine(Area.Center, allBusStations));
            allBusLines.addLine(new BusLine(Area.Jerusalem, allBusStations));
            allBusLines.addLine(new BusLine(Area.Jerusalem, allBusStations));
            allBusLines.addLine(new BusLine(Area.South, allBusStations));
            allBusLines.addLine(new BusLine(Area.North, allBusStations));
            allBusLines.addLine(new BusLine(Area.North, allBusStations));
            allBusLines.addLine(new BusLine(Area.General, allBusStations));
            allBusLines.addLine(new BusLine(Area.General, allBusStations));

            allBusStations.Add(new BusStation("Halevy"));
            allBusStations.Add(new BusStation("Trumpeldor"));
            allBusStations.Add(new BusStation("Kats"));
            allBusStations.Add(new BusStation("Rotshild"));
            allBusStations.Add(new BusStation("Hazait"));
            allBusStations.Add(new BusStation("Hapardes"));
            allBusStations.Add(new BusStation("Perah"));
            allBusStations.Add(new BusStation("Hagana"));
            allBusStations.Add(new BusStation("Dvir"));
            allBusStations.Add(new BusStation("Ariel"));
            allBusStations.Add(new BusStation("Hagibor")); /////////////
            allBusStations.Add(new BusStation("Truman"));
            allBusStations.Add(new BusStation("Karny"));
            allBusStations.Add(new BusStation("Roman"));
            allBusStations.Add(new BusStation("Hagefen"));
            allBusStations.Add(new BusStation("Pilon"));
            allBusStations.Add(new BusStation("Parpar"));
            allBusStations.Add(new BusStation("HaEtzel"));
            allBusStations.Add(new BusStation("Doron"));
            allBusStations.Add(new BusStation("Armon"));
            allBusStations.Add(new BusStation("Hamigdal")); /////////////////
            allBusStations.Add(new BusStation("Turki"));
            allBusStations.Add(new BusStation("kanion"));
            allBusStations.Add(new BusStation("Rubi"));
            allBusStations.Add(new BusStation("HaMelech"));
            allBusStations.Add(new BusStation("Hafira"));
            allBusStations.Add(new BusStation("Laila"));
            allBusStations.Add(new BusStation("Ratzon"));
            allBusStations.Add(new BusStation("Donald"));
            allBusStations.Add(new BusStation("Moshe"));
            allBusStations.Add(new BusStation("Hacohen")); /////////////////
            allBusStations.Add(new BusStation("Avraham"));
            allBusStations.Add(new BusStation("Yzhak"));
            allBusStations.Add(new BusStation("Jonathan"));
            allBusStations.Add(new BusStation("Jakobs"));
            allBusStations.Add(new BusStation("Rachel"));
            allBusStations.Add(new BusStation("Miriam"));
            allBusStations.Add(new BusStation("Dvora"));
            allBusStations.Add(new BusStation("Mapal"));
            allBusStations.Add(new BusStation("Kineret"));
           
        }


        /*
        addNewBus
        addSt
        delLine
        delStation

          
        
         */
    }


}
