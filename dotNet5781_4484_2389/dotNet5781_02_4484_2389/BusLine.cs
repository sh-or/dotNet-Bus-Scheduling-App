using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace dotNet5781_02_4484_2389
{
    public enum Area { General=1, North, South, Center, Jerusalem}
    public class BusLine : IComparable
    {
        public int CompareTo(object obj) //compare 2 lines by their whole rout time
        {
            BusLine tmp = (BusLine)obj;
            return timeGap(FirstStation(), LastStation()).CompareTo(timeGap(tmp.FirstStation(), tmp.LastStation()));
        }
        public class Station
        {
            public int stKey;
            public double distance;
            public TimeSpan timeLast;
            public Station(int key) { stKey = key; }

            public override string ToString()
            {
                string str = ("Station Code: ");
                //foreach (BusStation st1 in allSt)
                //{
                //    if (st1.busStationKey == stKey)
                //        str += (" " + st1.busStationKey + ", " + st1.Latitude + "°N " + st1.Longitude + "°E " + /*st1. +*/ "\n");
                //}
                return str;
                //return allSt.Find(stKey).ToString();
            }
        }
        
        private List<BusStation> allStations;
        public List<BusStation> allSt //the all-stations list with the details
        { 
            get { return allStations; }
            private set { allStations = value; }
        } 
        private static int counter = 0; //running line number
        public int line;
        public List<Station> stations; //bus line stations list
        public List<Station> Stations //the all-stations list with the details
        {
            get { return stations; }
            set { stations = value; }
        }
        Area area;  //enum
        public int FirstStation() { return stations[0].stKey; } 
        public int LastStation() { return stations[stations.Count-1].stKey; } 
        public static int Counter { get => counter; private set => counter = value; }
        public int originalLine; //for sublines

        public BusLine(Area area1, List<BusStation> allSt1) 
        {
            line = ++counter;
            area = area1;  //enum
            allSt = allSt1; //connect to the main stations list
            stations = new List<Station>();
            originalLine = line;
        }

        ~BusLine()
        {
            stations.Clear();
        }
        //private const int x = 1;
        public bool addStation(int stKey, int index) //add station to the line by station number and index 
        {
            if (index < 0 || index > stations.Count)
                throw new IndexOutOfRangeException("ERROR: " + index + " out of range");
            if (searchStation(stKey)) //ex if station already in the line
                throw new ArgumentException("ERROR: station " + stKey + " allready exist");
            BusStation lastSt, nextSt;
            GeoCoordinate local1, local2;
            foreach (BusStation bs in allSt)
                if (bs.busStationKey==stKey)
                {
                    if (index == stations.Count) //adding last station
                        stations.Add(new Station(stKey));
                    else
                        stations.Insert(index, new Station(stKey)); //adding first or middle station

                    if (index == 0) //updates after adding first bus to the list
                    {
                        stations[index].distance = 0;
                        stations[index].timeLast = TimeSpan.Zero;
                    }
                    else //update time&distance from last station
                    {
                        lastSt = allSt.Find(x => x.busStationKey == stations[index - 1].stKey);
                        local1 = new GeoCoordinate(bs.Latitude, bs.Longitude);
                        local2 = new GeoCoordinate(lastSt.Latitude, lastSt.Longitude);
                        stations[index].distance = local1.GetDistanceTo(local2); //distance calculating(in meters)
                        stations[index].timeLast = TimeSpan.FromSeconds(stations[index].distance); //the bus cross meter for second
                    }
                    if(index < stations.Count-1) //update the next station time&distance
                    {
                        nextSt = allSt.Find(x => x.busStationKey == stations[index+1].stKey);
                        local1 = new GeoCoordinate(bs.Latitude, bs.Longitude);
                        local2 = new GeoCoordinate(nextSt.Latitude, nextSt.Longitude);
                        stations[index + 1].distance = local1.GetDistanceTo(local2); //distance calculating
                        stations[index + 1].timeLast = TimeSpan.FromSeconds(stations[index+1].distance); //the bus cross meter for second
                    }
                    return true;
                }
            throw new KeyNotFoundException("ERROR: station " + stKey + " not found in the main list");
           // return false;
        }

        public override string ToString()
        {
            string str = ("Bus Line: " + line + " area: " + area + "   stations list:");
            foreach (Station st in stations)
                str += (" " + st.stKey);
            return str;
        }

        public Station findSt(int stNum) //find and return a station in the line
        {
            foreach (Station bs in stations)
                if (bs.stKey == stNum)
                {
                    return bs;
                }
            throw new KeyNotFoundException("ERROR: station " + stNum + " not found");   //exeption if not found
        }

        public double distanceGap(int st1, int st2) //return distance gap between 2 stations(according to the route)
        {
            if (st1 == st2)
                return 0;
            if(!(searchStation(st1) && searchStation(st2)))
                throw new KeyNotFoundException("ERROR: station not found");   //exeption if not found
            double sum = 0;
            bool flag = false;
            stations.Reverse(); //reverse the list
            foreach (Station st in stations)
            {
                if (st.stKey == st2)
                        flag = true;
                if (st.stKey == st1) 
                    break;
                if (flag)
                    sum += st.distance;
            }
            stations.Reverse(); //reverse the list back
            if(sum == 0)
                throw new ArgumentException("ERROR: stations are not in order");   //exeption if in  opposite order
            return sum;
        }

        public TimeSpan timeGap(int st1, int st2)//return time gap between 2 stations
            {
            if (st1 == st2)
                return TimeSpan.Zero;
            if (!(searchStation(st1) && searchStation(st2)))
                throw new KeyNotFoundException("ERROR: station not found");   //exeption if not found
            TimeSpan sum = TimeSpan.Zero;
            bool flag = false;
            stations.Reverse(); //reverse the list
            foreach (Station st in stations)
            {
                if (st.stKey == st2)
                    flag = true;
                if (st.stKey == st1)
                    break;
                if (flag)
                    sum += st.timeLast;
            }
            stations.Reverse();  //reverse the list back
            if (sum == TimeSpan.Zero)
                throw new ArgumentException("ERROR: stations are not in order");   //exeption if in  opposite order
            return sum;
        }


        public bool deleteStation(int stationKey)
        {
            foreach (Station bs in stations)
                if (bs.stKey == stationKey)
                {
                    stations.Remove(bs);
                    return true;
                }
            throw new KeyNotFoundException("ERROR: station " + stationKey + " not found in the line");
            // return false;
        }

        public bool searchStation(int stationKey) //check if a station exist in the line
        {
            foreach (Station bs in stations)
                if (bs.stKey == stationKey)
                {
                    return true;
                }
            return false;
        }

        public BusLine subRout(int st1, int st2)//return sub line between 2 stations
        {
            BusLine subLine= new BusLine(area, allSt); //the new sub line to return
            bool flag = false;
            if (!(searchStation(st1) && searchStation(st2)))
                throw new KeyNotFoundException("ERROR: station not found");   //exeption if not found
            foreach (Station it in stations)
                {
                    if (it.stKey == st1) //reached to the first station
                        flag = true;
                    if(flag)
                        subLine.addStation(it.stKey, subLine.stations.Count);
                    if (it.stKey == st2) //reached to the second station
                        break;
                }
            if (subLine.stations.Count == 0)
                throw new ArgumentException("ERROR: stations are not in order");   //exeption if in  opposite order
            subLine.originalLine = this.line;
            return subLine;
        }

    }
}
