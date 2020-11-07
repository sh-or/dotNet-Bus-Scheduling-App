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
        private static int count=100000;
        public int busStationKey;
        private double latitude;
        private double longitude;
        private string address;

        public static int Count { get => count; set => count = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }

        public override string ToString  ()
        {
            return ("Bus Station Code: "+ busStationKey+", "+ Latitude+"°N "+ Longitude+"°E" );
        }

        public BusStation (string add)
        {
            busStationKey= Count++;
            Random r = new Random(DateTime.Now.Millisecond);
            latitude = r.Next(3100, 3331) / 100.0;
            longitude = r.Next(3430, 3551) / 100.0;
            address = add;
        }

    }
}
