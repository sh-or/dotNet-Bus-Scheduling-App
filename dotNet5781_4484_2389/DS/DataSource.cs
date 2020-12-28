
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DS
{
    public static class DataSource
    {
        public static List<Bus> AllBuses;
        public static List<Line> AllLines;
        public static List<BusStation> AllBusStations;
        public static List<ConsecutiveStations> AllConsecutiveStations;
        public static List<LineStation> AllLineStations;

        static DataSource() //c-tor of DS
        {
            AllBuses = new List<Bus>();
            AllLines = new List<Line>();
            AllBusStations = new List<BusStation>();
            AllConsecutiveStations = new List<ConsecutiveStations>();
            AllLineStations = new List<LineStation>();
        }
    

        //public static List<WindDirection> directions;
        //static DataSource()
        //{
        //    directions = new List<WindDirection>();
        //}
    }
}
