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

        /*1.	מתודה שמקבלת מספר מזהה (קוד) של תחנת אוטובוס ומחזירה את רשימת הקווים העוברים בתחנה זו.
         * במידה ואין קווים שעוברים בתחנה תיזרק חריגה.

2.	מתודה שמחזירה רשימת כל הקוים הממוינת לפי משך הנסיעה הכולל,
        מהקצר לארוך (ראה גם סעיף 7 ברשימת המתודות של המחלקה קו אוטובוס)
*/
        public List<int> findLinesOfStation(int stKey) //return lines' list that cross at the asked station
        {
            List<int> lst = new List<int>();
            foreach (BusLine bs in busLinesList)
                if (bs.searchStation(stKey))
                    lst.Add(bs.line);
            //ex if lst empty(with bool flag?)
            return lst;
        }
        public List<int> sortedLines() //return sorrted lines' list by their time
        {
            List<int> lst = new List<int>();
            busLinesList.Sort(); //ok to sort the original?!
            foreach (BusLine bs in busLinesList)
                    lst.Add(bs.line); //create lines' numbers
            return lst;
        }

    }

}




