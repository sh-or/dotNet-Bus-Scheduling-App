using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4484_2389
{
    public class BusLineSystem : IEnumerable
    {

       public List<BusLine> busLinesList;

        public BusLineSystem()
        {
            busLinesList = new List<BusLine>();
        }

        public IEnumerator GetEnumerator()
        {
            return busLinesList.GetEnumerator();
        }

        public BusLine this[int busKey]
        {
            get
            {
                if (busKey < 0)
                    throw new ArgumentOutOfRangeException("ERROR: no negative line numbers");
                return busLinesList.Find(x => x.line == busKey);
                //ex ArgumentNullExeption if not found
            }
        }

        public void addLine(BusLine bus)  //add line to the list
        {
            //*the stations are only from the main stations' list
            if (busLinesList.Count != 0 && bus.allSt != busLinesList[0].allSt)
                    throw new ArgumentException("ERROR: not suitable stations list"); //ex if not the same main stations list
            busLinesList.Add(bus);
        }

        public bool deletedLine(int line1) //remove line from the list. return false if not found.
        {
            return busLinesList.Remove(busLinesList.Find(x=>x.line==line1));
            //ex ArgumentNullExeption if not found
        }

        public List<int> findLinesOfStation(int stKey) //return lines' list that cross at the asked station
        {
            List<int> lst = new List<int>();
            foreach (BusLine bs in busLinesList)
                if (bs.searchStation(stKey))
                    lst.Add(bs.line);
            return lst; //if there are no stations-return empty list
        }
        public void sortedLines() // sort the lines' list by their time
        {
            busLinesList.Sort();
        }

       public void printAll()
        {
            foreach(BusLine bs in busLinesList)
            {
                Console.WriteLine(bs);
            }
        }

    }

}




