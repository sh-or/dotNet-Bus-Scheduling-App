using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Device.dll;

namespace dotNet5781_02_4484_2389
{
    class BusLine
    {
        private class Station //: BusStation
        {
            public int stKey;
            public double distance;
            public TimeSpan timeLast;
            public Station(int key) { stKey = key; }
        }
        /*static*/ List<BusStation> allSt; //the all-stations list with the details
        private static int counter = 0; //running line number
        public int line;
        List<Station> stations; //bus line stations list
        string area;
        int FirstStation() {return stations[0].stKey;}
        int LastStation() { return stations[stations.Capacity].stKey; }
        public static int Counter { get => counter; set => counter = value; }

        BusLine(string area1,/**/ List<BusStation> allSt1) 
        {
            line = counter++;
            area = area1; //enum?
            allSt = allSt1; //connect to the main stations list
            stations = new List<Station>();
        }

     

        ~BusLine()
        {
            stations.Clear();
        }
        private const int x = 1;
        bool addStation(int stKey, int index) //לתת לו להכניס מס' תחנה שאחריה יוכנס איבר חדש?
        {
            TimeSpan sec=TimeSpan.Zero;
            foreach (BusStation bs in allSt)
                if (bs.busStationKey==stKey)
                {
                    if(index==stations.Capacity+1)
                        stations.Add(new Station(stKey));
                    else
                        stations.Insert(index, new Station(stKey));
                    //stations[index].distance =; //distance calculating
                    //stations[index].timeLast= TimeSpan.Parse(stations[index].distance) * sec; //the bus cross meter for second
                    return true;
                }
            return false;
        }

        public override string ToString()
        {
            string str = ("Bus Line: " + line + " area: " + area + "stations list:");
            foreach (Station st in stations)
                str += (" " + st.stKey);
            return str;
        }

        Station findSt(int stNum) //find and return a station in the line
        {
            foreach (Station bs in stations)
                if (bs.stKey == stNum)
                {
                    return bs;
                }
            //exeption if not found...
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


        bool deleteStation(int stationKey)
        {
            foreach (Station bs in stations)
                if (bs.stKey == stationKey)
                {
                    stations.Remove(bs);
                    return true;
                }
            return false;
        }

        bool searchStation(int stationKey) //check if a station exist in the line
        {
            foreach (Station bs in stations)
                if (bs.stKey == stationKey)
                {
                    return true;
                }
            return false;
        }


        BusLine subRout(int st1, int st2)//return sub line between 2 stations
        {

            BusLine subLine= new BusLine(area, allSt);
            int mone = 0;
            bool flag = false;
            //foreach (Station bs in stations)
            //    if (bs.stKey == bs1.busStationKey)
            //    {
            //        iterator it1 = bs;   //בניית איטרטור
            //    }

            //foreach (Station bs in stations)
            //    if (bs.stKey == bs2.busStationKey)
            //    {
            //        iterator it2 = bs;
            //    }
            if (searchStation(st1) && searchStation(st2)) //אולי לעשות פונק' שתכניס תחנה ותחזיר את אינדקס המיקום שלה ואז לולאה רק בין האינדקסים
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
