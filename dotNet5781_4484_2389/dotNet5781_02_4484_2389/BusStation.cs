using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; //exeptions

namespace dotNet5781_02_4484_2389
{
    class BusStation
    {
        private static int busStationKey=100000;
        private double latitude;
        private double longitude;
        private string address;

        public static int BusStationKey { get => busStationKey; set => busStationKey = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }

        public string ToString ()
        {
            return ("Bus Station Code: "+ BusStationKey+", "+ Latitude+"°N "+ Longitude+"°E" );
        }

        public BusStation (string add)
        {

        }



    }
}
