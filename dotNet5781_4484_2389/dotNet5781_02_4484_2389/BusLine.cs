﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4484_2389
{
    class BusLine
    {
        private class Station //: BusStation
        {
            public int stKey;
            public double distance;
            //public timeLast;
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
        //tostring
        bool addStation(int stKey, int index)
        {
            foreach(BusStation bs in allSt)
                if(bs.busStationKey==stKey)
                {
                    if(index>stations.Capacity)
                        stations.Add(new Station(stKey));
                    stations.Insert(index-1, new Station(stKey));
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

        public Station findSt(int stNum) //find and return a station
        {

        }

        public double stDistance(int st1, int st2) //return distance between 2 stations
        {
            if (st1 == st2)
                return 0;
            double sum = 0;
            bool flag = false; 
            reverse foreach
            foreach (Station st in stations)
            {
                if (st.stKey == st1)
                    flag = true;
                if (st.stKey == st2)
                    break;
                if (flag)
                    sum += st.distance;
            }
            return sum;

        }

        public TimeSpan stTime(int st1, int st2)//return time between 2 stations
            {
            if (st1 == st2)
                return TimeSpan.Parse(0:00:00);

        }

    }
}
