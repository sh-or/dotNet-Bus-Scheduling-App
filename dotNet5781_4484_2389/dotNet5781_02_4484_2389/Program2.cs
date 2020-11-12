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
        enum Choice{ Add = 1, Delete, SearchLine, SearchRoute, Print}
        static void Main(string[] args)
        {
            List<BusStation> allBusStations=new List<BusStation>();  //the main stations list
            BusLineSystem allBusLines;  //all the lines
            //input 10 lines
            //40 stations
            restart(allBusStations);  

        }

        private static void restart(List<BusStation>  allBusStations)
        {
            Console.WriteLine("enter");

            BusStation b=new BusStation("halevy");
            BusStation c = new BusStation("trumpeldor");
            BusLine aa = new BusLine(Center, allBusStations);
        }


        /*
        addNewBus
        addSt
        delLine
        delStation

          
        
         */
    }


}
