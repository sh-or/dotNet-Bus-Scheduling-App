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

        List<BusLine> busLinesList;

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

        public bool deletedLine(BusLine bus) //remove line from the list. return false if not found.
        {
            return busLinesList.Remove(bus);
        }
        
       



    }

}




