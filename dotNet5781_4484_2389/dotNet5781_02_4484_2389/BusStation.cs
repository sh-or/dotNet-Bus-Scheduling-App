using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; //exeptions

namespace dotNet5781_02_4484_2389
{
    public class BusStation
    {
        private static int count=100000;
        public int busStationKey;
        private double latitude;
        private double longitude;
        public string address;
        public static Random r = new Random(DateTime.Now.Millisecond);


        public static int Count { get => count; set => count = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }

        public override string ToString  ()
        {
            return ("Station Code: "+busStationKey+ ", Address: " + address+",   "+ Latitude+"°N "+ Longitude+"°E" );
        }

        public BusStation (string add)
        {
            busStationKey= Count++;
            latitude = r.NextDouble()*(33.31-31.00)+31.0;
            longitude = r.NextDouble()*(35.51-34.30)+34.3;
            address = add;
        }

    }
}
