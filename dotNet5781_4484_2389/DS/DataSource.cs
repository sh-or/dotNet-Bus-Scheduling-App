
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
        public static List<Bus> ExistBuses;
        public static List<Bus> AllBuses;

        public static List<Line> AllLines;


        static DataSource()
        {
            ExistBuses = new List<Bus>();
            AllBuses = new List <Bus>();
            AllLines = new List<Line>();
        }


        //public static List<WindDirection> directions;
        //static DataSource()
        //{
        //    directions = new List<WindDirection>();
        //}
    }
}
