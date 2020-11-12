using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_4484_2389
{
    class BusLineSystem /*: IEnumerable*/
    {

        public List<BusLine> busLinesList;

        public BusLineSystem()
        {
            busLinesList = new List<BusLine>();
        }

        //public IEnumerator GetEnumerator()
        //{
        //    //foreach (var item in busLinesList)
        //    //{
        //    //    yield return item;
        //    //}
        //    return busLinesList.GetEnumerator();
        //}

        public interface IEnumerator
        {
            object Current { get; }
            bool MoveNext();
            void Reset();
        }

        public interface IEnumerable
        {
            IEnumerator GetEnumerator();
        }
    }

}


}


}

