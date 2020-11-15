using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4484_2389
{
    class BusLineSystem : IEnumerable
    {

       public List<BusLine> busLinesList;

        public BusLineSystem()
        {
            busLinesList = new List<BusLine>();
        }

        public IEnumerator GetEnumerator()
        {
            return busLinesList.GetEnumerator();

            //foreach (var item in busLinesList)
            //{
            //    yield return item;
            //}
        }

        public BusLine this[int busKey]
        {
            get { return busLinesList.Find(x => x.line == busKey); } 
            //ex if not found
        }

        public void addLine(BusLine bus)  //add line to the list
        {
            //check the stations?
            busLinesList.Add(bus);
        }

        public bool deletedLine(int line1) //remove line from the list. return false if not found.
        {
            return busLinesList.Remove(busLinesList.Find(x=>x.line==line1));
        }

        public List<int> findLinesOfStation(int stKey) //return lines' list that cross at the asked station
        {
            List<int> lst = new List<int>();
            foreach (BusLine bs in busLinesList)
                if (bs.searchStation(stKey))
                    lst.Add(bs.line);
            //ex if lst empty(with bool flag?)
            return lst;
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




