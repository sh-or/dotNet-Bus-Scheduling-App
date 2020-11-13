﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4484_2389
{
    class Program2
    {
        //אולי להכניס מאפיין (של הרשימה הכללית) למערכת ולוודא בכל קו שמוסיפים למערכת שהרשימה הכללית של התחנות תואמת לשלו
        enum Choice{ Add = 1, Delete, SearchLine, SearchRoute, Print}
        static void Main(string[] args)
        {
            List<BusStation> allBusStations=new List<BusStation>();  //the main stations list
            BusLineSystem allBusLines = new BusLineSystem();  //all the lines
            //input 10 lines
            //40 stations
            restart(allBusStations, allBusLines);

            List<int> lineKeysList;
            List<BusLine> linesList;
            int ch, st1, st2;
            Console.WriteLine("Enter you choice\n"); //+menu
            do
            {
                ch = int.Parse(Console.ReadLine());
                switch((Choice)ch)
                {
                    case Choice.Add:
                        {

                            break;
                        }
                    case Choice.Delete:
                        {

                            break;
                        }
                        /*•	קווים שעוברים בתחנה ע"פ מספר תחנה
•	הדפסת האפשרויות לנסיעה בין 2 תחנות, ללא החלפת אוטובוס
יש לקלוט תחנת מוצא ותחנת יעד ולהחזיר את התוצאות הממוינות לפי זמן הנסיעה.
זה העזרה לנוסע שהבטחנו. צריך לייצר רשימה חדשה של כל תתי- המסלול של הקווים */
                    case Choice.SearchLine:
                        {
                            Console.WriteLine("Enter station number\n");
                            st1 = int.Parse(Console.ReadLine());
                            lineKeysList = allBusLines.findLinesOfStation(st);
                            foreach(int n in lineKeysList)
                                Console.Write(n+" ");
                            Console.WriteLine();
                            //ex if lst empty(with bool flag?) + try...
                            break;
                        }
                    case Choice.SearchRoute:
                        {
                            Console.WriteLine("Enter first and last stations' numbers\n");
                            st1 = int.Parse(Console.ReadLine());
                            st2 = int.Parse(Console.ReadLine());
                            linesList = new List<BusLine>();
                            //linesList.sortedlines.print(); //צריך להפוך את הרשימה לטיפוס מערכת קווים...
                            break;
                        }
                    case Choice.Print:
                        {
                            //לקלוט 1-2 לאופציות
                            //printAll מדפיס הכל
                            break;
                        }
                }

            } while (ch != 5);

        }

        private static void restart(List<BusStation>  allBusStations, BusLineSystem allBusLines)
        {
            //יהודית:) יש 10 קוים ו40 תחנות. עכשיו צריך להוסיף מסלול לכל קו, לפי ההוראות שהם נתנו
            Console.WriteLine("There are 40 stations and 10 lines\n");

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
        }


        /*
        addNewBus
        addSt
        delLine
        delStation

          
        
         */
                }


            }
