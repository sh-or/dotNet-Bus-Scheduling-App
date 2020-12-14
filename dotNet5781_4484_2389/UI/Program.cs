//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UI
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//        }
//    }
//}

using System;
using BlAPI;
using BO;


namespace PlConsole
{
    class Program
    {
        static IBL bl;

        static void Main(string[] args)
        {
            bl = BlFactory.GetBl();

            Console.Write("Please enter how many days back: ");
            int days = int.Parse(Console.ReadLine());
            for (int d = days; d >= 0; --d)
            {
                Weather w = bl.GetWeather(d);
                Console.WriteLine($"{d} days before - Feeling was: {w.Feeling} Celsius degrees");
            }

        }
    }
}