using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlAPI
{
    public static class BlFactory
    {
        internal static readonly IBL instance = new BL.BlImp1();

        public static IBL GetBl()
        {
            return instance;
        }
    }

}
