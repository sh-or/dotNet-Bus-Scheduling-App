using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4484_2389
{
    class BusLine
    {
        private class Station : BusStation
        {
            public double distance;
            //timeLast;
            //public Station(){};
        }
        private static int busLine = 0;
        List<Station> stations; //bus lines stations list
        Station st;
        BusStation bs;
        int a=bs.BusStationKey;
        int FirstStation() {return stations.BusStationKey;}
        int LastStation() { return stations[stations.Capacity].Bus; }
        string Area;

        public static int BusLine { get => busLine; set => busLine = value; }
    }
}
