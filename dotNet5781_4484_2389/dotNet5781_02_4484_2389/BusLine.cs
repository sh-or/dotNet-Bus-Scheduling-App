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
    class BusLine : IComparable
    {
        public int CompareTo(object obj) //compare 2 lines by their whole rout time
        {
            BusLine tmp = (BusLine)obj;
            return timeGap(FirstStation(), LastStation()).CompareTo(timeGap(tmp.FirstStation(), tmp.LastStation()));
        }
        public class Station //: BusStation
        {
            public int stKey;
            public double distance;
            public TimeSpan timeLast;
            public Station(int key) { stKey = key; }
        }
        /*static*/
        private List<BusStation> allStations;
        public List<BusStation> allSt //the all-stations list with the details
        { 
            get { return allStations; }
            private set { allStations = value; }
        } 
        private static int counter = 0; //running line number
        public int line;
        public List<Station> stations; //bus line stations list
        Area area;  //enum
        public int FirstStation() { return stations[0].stKey; } //ex if empty. return statio?busStation?
        public int LastStation() { return stations[stations.Capacity].stKey; } //ex if empty. return statio?busStation?
        public static int Counter { get => counter; private set => counter = value; }

        public BusLine(Area area1, List<BusStation> allSt1)  /*, int st1, int st2*/ 
        {
            line = ++counter;
            area = area1;  //enum
            allSt = allSt1; //connect to the main stations list
            stations = new List<Station>();
            //stations.Add(new Station(st1));
            //stations.Add(new Station(st2));
        }

        ~BusLine()
        {
            stations.Clear();
        }
        private const int x = 1;
        public bool addStation(int stKey, int index) //add station to the line by station number and index *add to allSt?!
        {
            if (searchStation(stKey)) //ex if station already in the line
                throw new Exception("ERROR: key allready exist");
            BusStation lastSt, nextSt;
            //TimeSpan sec=TimeSpan.Zero;
            GeoCoordinate local1, local2;
            foreach (BusStation bs in allSt)
                if (bs.busStationKey==stKey)
                {
                    if (index == stations.Capacity) //adding last station
                        stations.Add(new Station(stKey));
                    else
                        stations.Insert(index, new Station(stKey)); //adding first/middle station

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
                        stations[index].timeLast = TimeSpan.FromSeconds(stations[index].distance)/* *sec */; //the bus cross meter for second
                    }
                    if(index < stations.Capacity-1) //update the next station time&distance
                    {
                        nextSt = allSt.Find(x => x.busStationKey == stations[index+1].stKey);
                        local1 = new GeoCoordinate(bs.Latitude, bs.Longitude);
                        local2 = new GeoCoordinate(nextSt.Latitude, nextSt.Longitude);
                        stations[index + 1].distance = local1.GetDistanceTo(local2); //distance calculating
                        stations[index + 1].timeLast = TimeSpan.FromSeconds(stations[index+1].distance); //the bus cross meter for second
                    }
                    return true;
                }
            return false;
        }

        public override string ToString()
        {
            string str = ("Bus Line: " + line + " area: " + area + " stations list:");
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
            throw new KeyNotFoundException("ERROR: key not found");   //exeption if not found...
        }

        public double distanceGap(int st1, int st2) //return distance gap between 2 stations(according to the route)
        {
            if (st1 == st2)
                return 0;
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
            return sum;
        }

        public TimeSpan timeGap(int st1, int st2)//return time gap between 2 stations
            {
            if (st1 == st2)
                return TimeSpan.Zero;
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
            return false;
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
            int mone = 0;
            bool flag = false;
            if (searchStation(st1) && searchStation(st2))
            {
                foreach (Station it in stations)
                {
                    if (it.stKey == st1) //reached to the first station
                        flag = true;
                    if(flag)
                        subLine.addStation(it.stKey, mone++);
                    if (it.stKey == st2) //reached to the second station
                        break;
                }
            }
            return subLine;
        }

    }
}
