﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlAPI
{
    public static class BlFactory
    {
        public static IBL GetBl()
        {
            return new BL.BlImp1();
        }
    }

}
