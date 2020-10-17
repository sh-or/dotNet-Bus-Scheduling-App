using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_4484_2389
{
   partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2389();
            Welcome4484();
            Console.ReadKey();
        }

       static partial void Welcome4484();
        private static void Welcome2389()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine(name + ", welcome to my first console application");
        }
    }
}
