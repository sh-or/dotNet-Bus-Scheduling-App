using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4484_2389
{
    class Program2
    {
        enum Choice { Add = 1, Delete, SearchLine, SearchRoute, Print, exit }
        static void Main(string[] args)
        {
            List<BusStation> allBusStations = new List<BusStation>();  //the main stations list
            BusLineSystem allBusLines = new BusLineSystem();  //all the lines

            restart(allBusStations, allBusLines);  //input 10 lines & 40 stations

            List<int> lineKeysList;
            int ch, line1, stkey1, stkey2;
            string address1, address2;

            Console.WriteLine("1: add new line bus or station in line bus\n" +
                "2: delete line bus or station from line bus\n" +
                "3: search lines in station\n" +
                "4: search route\n" +
                "5: print all line's bus or lines in stations\n");

            do
            {
                Console.WriteLine("Enter your choice"); 
                ch = int.Parse(Console.ReadLine());
                try
                {
                    switch ((Choice)ch)
                    {
                        case Choice.Add:
                            {
                                Console.WriteLine("Enter 1 to add new line bus, enter 2 to add new station in line bus");
                                int tmp1 = int.Parse(Console.ReadLine());   //cin choice
                                if (tmp1 == 1)
                                {
                                    Console.WriteLine("Enter area number for bus line:\n" +
                                    "1: General, 2: North, 3: South, 4: Center, 5: Jerusalem");
                                    Area area1 = (Area)int.Parse(Console.ReadLine());
                                    allBusLines.addLine(new BusLine(area1, allBusStations));
                                    Console.WriteLine("Enter name of first and last stations");
                                    address1 = Console.ReadLine();
                                    stkey1 = allBusStations.Find(x => x.address == address1).busStationKey; //ex if not found
                                    allBusLines[allBusLines.busLinesList.Count - 1].addStation(stkey1, 0);
                                    address2 = Console.ReadLine();
                                    stkey2 = allBusStations.Find(x => x.address == address2).busStationKey; //ex if not found
                                    allBusLines[allBusLines.busLinesList.Count - 1].addStation(stkey2, 1);
                                    Console.WriteLine("line " + allBusLines[allBusLines.busLinesList.Count - 1].line + " added");
                                }
                                if (tmp1 == 2)
                                {
                                    Console.WriteLine("Enter name of station");
                                    address1 = Console.ReadLine();
                                    stkey1 = allBusStations.Find(x => x.address == address1).busStationKey; //ex if not found
                                    Console.WriteLine("Enter number line");
                                    line1 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter index to add station in line bus");
                                    int index1 = int.Parse(Console.ReadLine());
                                    if (allBusLines[line1].addStation(stkey1, index1))
                                        Console.WriteLine("The station was added successfully");
                                    //else -ex if not found in the main stations list
                                }



                                break;
                            }

                        case Choice.Delete:
                            {
                                Console.WriteLine("Enter 1 to delete line bus, enter 2 to delete station from line bus");
                                int tmp1 = int.Parse(Console.ReadLine());   //cin choice

                                if (tmp1 == 1)
                                {
                                    Console.WriteLine("Enter number line");
                                    line1 = int.Parse(Console.ReadLine());
                                    if (allBusLines.deletedLine(line1))
                                        Console.WriteLine("The line bus deleted");
                                    // else -ex if not found in the main list
                                }
                                if (tmp1 == 2)
                                {
                                    Console.WriteLine("Enter name of station");
                                    address1 = Console.ReadLine();
                                    Console.WriteLine("Enter number line");
                                    line1 = int.Parse(Console.ReadLine());

                                    if (allBusLines[line1].stations.Count > 2)
                                    {
                                        stkey1 = allBusStations.Find(x => x.address == address1).busStationKey;  //למצוא את הקוד של התחנה
                                        if (allBusLines[line1].deleteStation(stkey1))
                                            Console.WriteLine("The station was deleted");
                                        // else ex if not found in the line
                                    }
                                    else
                                        Console.WriteLine("Error: line can't stay with-less than 2 stations");
                                }
                                break;
                            }

                        case Choice.SearchLine:
                            {
                                Console.WriteLine("Enter name of station");
                                address1 = Console.ReadLine();
                                stkey1 = allBusStations.Find(x => x.address == address1).busStationKey; //ex if not found
                                lineKeysList = allBusLines.findLinesOfStation(stkey1);
                                foreach (int n in lineKeysList)  //if there are no lines, print empty line
                                    Console.Write(n + " ");
                                Console.WriteLine();
                                break;
                            }

                        case Choice.SearchRoute:
                            {
                                Console.WriteLine("Enter name of first and last stations");
                                address1 = Console.ReadLine();
                                address2 = Console.ReadLine();
                                stkey1 = allBusStations.Find(x => x.address == address1).busStationKey; //ex if not found
                                stkey2 = allBusStations.Find(x => x.address == address2).busStationKey; //ex if not found
                                BusLineSystem sorting = new BusLineSystem();

                                foreach (BusLine bs in allBusLines)
                                {
                                    if (bs.searchStation(stkey1) && bs.searchStation(stkey2) && bs.timeGap(stkey1, stkey2) > TimeSpan.Zero)
                                    {
                                        sorting.busLinesList.Add(bs.subRout(stkey1, stkey2));
                                    }
                                }
                                sorting.sortedLines();
                                Console.Write("Lines list, sorted by time: ");
                                foreach(BusLine bs in sorting.busLinesList)
                                    Console.Write(bs.originalLine+ " ");
                                Console.WriteLine();
                                break;
                            }
                        case Choice.Print:
                            {
                                Console.WriteLine("Enter 1 to print all bus line's , enter 2 to print all bus stations and bus line's in the stations");
                                int tmp1 = int.Parse(Console.ReadLine());   //cin choice
                                if (tmp1 == 1)  //printAll 
                                {
                                    allBusLines.printAll();
                                }
                                if (tmp1 == 2)
                                {
                                    foreach (BusStation st in allBusStations)
                                    {
                                        Console.WriteLine(st);
                                        lineKeysList = allBusLines.findLinesOfStation(st.busStationKey);
                                        foreach (int n in lineKeysList)
                                            Console.Write("lines: "+n + " ");
                                        Console.WriteLine();
                                    }
                                }
                                break;
                            }
                        case Choice.exit:
                            break;

                        default:
                            Console.WriteLine("Wrong input");
                            break;
                    }
                }
                catch(NullReferenceException)  //catch all the faild "find" errors
                {
                    throw new KeyNotFoundException("ERROR: key not found");
                }

                catch(Exception ex) //catch all-kinds exeptions and print an appropriate message(then return to the switch)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (ch != 6);  

        }

        private static void restart(List<BusStation> allBusStations, BusLineSystem allBusLines)
        { //restart 10 lines and 40 stations

            allBusLines.addLine(new BusLine(Area.Center, allBusStations));  
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

            allBusLines[1].addStation(100000, 0);
            allBusLines[1].addStation(100001, 0);
            allBusLines[2].addStation(100002, 0);
            allBusLines[2].addStation(100003, 0);
            allBusLines[3].addStation(100004, 0);
            allBusLines[3].addStation(100005, 0);
            allBusLines[4].addStation(100006, 0);
            allBusLines[4].addStation(100007, 0);
            allBusLines[5].addStation(100008, 0);
            allBusLines[5].addStation(100009, 0); ///
            allBusLines[6].addStation(100010, 0);
            allBusLines[6].addStation(100011, 0);
            allBusLines[7].addStation(100012, 0);
            allBusLines[7].addStation(100013, 0);
            allBusLines[8].addStation(100014, 0);
            allBusLines[8].addStation(100015, 0); 
            allBusLines[9].addStation(100016, 0);
            allBusLines[9].addStation(100017, 0);
            allBusLines[10].addStation(100018, 0);
            allBusLines[10].addStation(100019, 0);
            allBusLines[1].addStation(100020, 0);
            allBusLines[1].addStation(100021, 0);
            allBusLines[2].addStation(100022, 0);
            allBusLines[2].addStation(100023, 0);
            allBusLines[3].addStation(100024, 0);
            allBusLines[3].addStation(100025, 0);
            allBusLines[4].addStation(100027, 0);
            allBusLines[4].addStation(100028, 0);
            allBusLines[5].addStation(100029, 0);
            allBusLines[5].addStation(100030, 0); 
            allBusLines[6].addStation(100031, 0);
            allBusLines[6].addStation(100032, 0);
            allBusLines[7].addStation(100033, 0);
            allBusLines[7].addStation(100034, 0);
            allBusLines[8].addStation(100035, 0);
            allBusLines[8].addStation(100036, 0);
            allBusLines[9].addStation(100037, 0);
            allBusLines[9].addStation(100038, 0);
            allBusLines[10].addStation(100039, 0);
            allBusLines[10].addStation(100026, 0);

            allBusLines[2].addStation(100000, 0);

            allBusLines[2].addStation(100033, 0); //between 100001 to 100000

            allBusLines[2].addStation(100001, 0);
            allBusLines[3].addStation(100002, 0);
            allBusLines[3].addStation(100003, 0);
            allBusLines[4].addStation(100004, 0);
            allBusLines[4].addStation(100005, 0);
            allBusLines[5].addStation(100006, 0);
            allBusLines[5].addStation(100007, 0);
            allBusLines[6].addStation(100008, 0);
            allBusLines[6].addStation(100009, 0);


            Console.WriteLine("There are 40 stations and 10 lines\n");

        }

    }


}
