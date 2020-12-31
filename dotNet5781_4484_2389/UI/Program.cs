using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlAPI;
using BO;


namespace UI  //PlConsole
{
   // public enum Status { Ready = 1, NeedCare, NeedRefeul, InDrive, InCare, InRefuel }
    class Program
    {
        static IBL bl;
        enum Choice { Bus = 1, BusStation, Line, exit }
        enum CoiceBus {add=1, delete, update, viewDetails, viewAll }
        enum CoiceBusStation { add = 1, delete, update, viewDetails, viewAll }
        enum CoiceLine { add = 1, delete, update, viewDetails, viewAll, addStation, deleteStation, viewStations }

        static void Main(string[] args)
        {
            bl = BlFactory.GetBl();


            Console.WriteLine("1:bus\n2:bus station\n3:line\n");
            int myChoice;

            do
            {
                Console.WriteLine("Enter your choice");
                myChoice = int.Parse(Console.ReadLine());
                switch ((Choice)myChoice)
                {
                    case Choice.Bus:
                        {
                            Console.WriteLine("1: add bus\n 2: delete bus\n 3:update bus\n 4:view bus details\n 5: view all buses");
                            Console.WriteLine("Enter your choice");
                            myChoice = int.Parse(Console.ReadLine());
                            switch ((CoiceBus)myChoice)
                            {
                                case CoiceBus.add:
                                    {
                                        Console.WriteLine("Enter details:{LicenseNumber, LicensingDate, Kilometerage, KmFromLastRefuel, Fuel, KmFromLastCare, DateOfLastCare, Status, Driver name");
                                        int _lcNum = int.Parse(Console.ReadLine()); //cin license number
                                        DateTime _dt = DateTime.Parse(Console.ReadLine());  //cin date beggining
                                        string v = _lcNum.ToString();
                                        double _km = double.Parse(Console.ReadLine());
                                        double _KmOfLastRefuel = double.Parse(Console.ReadLine());
                                        double _KmOfLastCare = double.Parse(Console.ReadLine());
                                        double _kilometer;
                                        DateTime _dtc = DateTime.Parse(Console.ReadLine());
                                        //string timerAct;
                                        StatusEnum _status = (StatusEnum)int.Parse(Console.ReadLine());
                                        ////
                                        while ((v.Length != 7 && _dt.Year < 2018) || (v.Length != 8 && _dt.Year >= 2018))  //check license number
                                        {
                                            Console.WriteLine("illegal number. enter again");
                                            _lcNum = int.Parse(Console.ReadLine()); //cin license number
                                            v = _lcNum.ToString();
                                        }
                                        ////
                                        if (_km < _KmOfLastCare || _km < _KmOfLastRefuel)
                                             _kilometer = Math.Max(_KmOfLastCare, _KmOfLastRefuel);
                                        else
                                            _kilometer = _km;
                                        ////
                                        if (!((DateTime.Today.AddYears(-1)) < _dtc) || (_KmOfLastCare) > 18500) //checking time/km from last care
                                        {
                                            _status = (StatusEnum)2; //need care 
                                        }
                                        else if (_KmOfLastRefuel > 1000) //checking fuel
                                        {
                                            _status = (StatusEnum)3; //need refuel 
                                        }
                                        else
                                            _status = (StatusEnum)1; //ready

                                        BOBus b = new BOBus
                                        {
                                            LicenseNumber = _lcNum,
                                            LicensingDate = _dt,
                                            Kilometerage = _kilometer,
                                            Fuel = (1200 - _KmOfLastRefuel) / 1200,
                                            KmFromLastRefuel = _KmOfLastRefuel,
                                            KmFromLastCare = _KmOfLastCare,
                                            DateOfLastCare = _dtc,
                                            Status= _status,
                                            Driver = Console.ReadLine(),
                                            IsExist = true
                                             //timerAct="",
                                        };
                                        bl.AddBus(b);
                                        Console.WriteLine("bus added!");
                                    }
                                    break;

                                case CoiceBus.delete:
                                    {
                                        Console.WriteLine("enter license number:");
                                        int _LicenseNumber = int.Parse(Console.ReadLine());
                                        bl.DeleteBus(_LicenseNumber);
                                        Console.WriteLine("bus deleted");
                                    }
                                    break;
                                case CoiceBus.update:
                                    {
                                        Console.WriteLine("Enter details:{LicenseNumber, LicensingDate, Kilometerage, KmFromLastRefuel, Fuel, KmFromLastCare, DateOfLastCare, Status, Driver name");
                                        int _lcNum = int.Parse(Console.ReadLine()); //cin license number
                                        DateTime _dt = DateTime.Parse(Console.ReadLine());  //cin date beggining
                                        //string v = _lcNum.ToString();
                                        double _km = double.Parse(Console.ReadLine());
                                        double _KmOfLastRefuel = double.Parse(Console.ReadLine());
                                        double _KmOfLastCare = double.Parse(Console.ReadLine());
                                        double _kilometer = _km;
                                        DateTime _dtc = DateTime.Parse(Console.ReadLine());
                                        StatusEnum _Status = (StatusEnum)int.Parse(Console.ReadLine());
                                        BOBus b = new BOBus
                                        {
                                            LicenseNumber = _lcNum,
                                            LicensingDate = _dt,
                                            Kilometerage = _kilometer,
                                            Fuel = (1200 - _KmOfLastRefuel) / 1200,
                                            KmFromLastRefuel = _KmOfLastRefuel,
                                            KmFromLastCare = _KmOfLastCare,
                                            DateOfLastCare = _dtc,
                                            Status = _Status,
                                            Driver = Console.ReadLine(),
                                            IsExist = true
                                            //timerAct="",
                                        };
                                        bl.UpdateBus(b);
                                        Console.WriteLine("bus update");
                                    }
                                    break;
                                case CoiceBus.viewDetails:
                                    {
                                        Console.WriteLine("enter license number:");
                                        int _LicenseNumber = int.Parse(Console.ReadLine());
                                        BOBus b = bl.GetBus(_LicenseNumber);
                                        Console.WriteLine($"LicenseNumber: {b.LicenseNumber}");
                                    }
                                    break;
                                case CoiceBus.viewAll:
                                    {
                                        IEnumerable<BOBus> ls=bl.GetAllBuses();
                                        foreach (BOBus b in ls)
                                            Console.WriteLine(b.LicenseNumber);
                                    }
                                    break;
                                default:
                                    Console.WriteLine("wrong input\n");
                                    break;
                            }
                        }
                        break;

                    case Choice.BusStation:
                        {
                            Console.WriteLine("1: add bus station\n 2: delete bus station\n 3:update bus station\n 4:view bus station details\n 5: view all bus stations");
                            Console.WriteLine("Enter your choice");
                            myChoice = int.Parse(Console.ReadLine());
                            switch ((CoiceBusStation)myChoice)
                            {
                                case CoiceBusStation.add:
                                    {

                                    }
                                    break;

                                case CoiceBusStation.delete:
                                    {

                                    }
                                    break;
                                case CoiceBusStation.update:
                                    {

                                    }
                                    break;
                                case CoiceBusStation.viewDetails:
                                    {

                                    }
                                    break;
                                case CoiceBusStation.viewAll:
                                    {

                                    }
                                    break;
                                default:
                                    Console.WriteLine("wrong input\n");
                                    break;
                            }
                        }
                        break;

                    case Choice.Line:
                        {
                            Console.WriteLine("1: add line\n 2: delete line\n 3:update line\n 4:view line details\n 5: view all lines" +
    "6: add station to line\n 7: delete station from line\n 8: view stations in line");
                            Console.WriteLine("Enter your choice");
                            myChoice = int.Parse(Console.ReadLine());
                            switch ((CoiceLine)myChoice)
                            {
                                case CoiceLine.add:
                                    {

                                    }
                                    break;

                                case CoiceLine.delete:
                                    {

                                    }
                                    break;
                                case CoiceLine.update:
                                    {

                                    }
                                    break;
                                case CoiceLine.viewDetails:
                                    {

                                    }
                                    break;
                                case CoiceLine.viewAll:
                                    {

                                    }
                                    break;
                                case CoiceLine.addStation:
                                    {

                                    }
                                    break;
                                case CoiceLine.deleteStation:
                                    {

                                    }
                                    break;
                                case CoiceLine.viewStations:
                                    {

                                    }
                                    break;
                                default:
                                    Console.WriteLine("wrong input\n");
                                    break;
                            }
                        }
                        break;

                    case Choice.exit:
                        break;

                    default:
                        Console.WriteLine("wrong input\n");
                        break;
                }
            } while (myChoice != 4);
            Console.WriteLine("end\n");
        }
    }
}
            
            
            
            
            //    static IBL bl;

                        //    static void Main(string[] args)
                        //    {
                        //        bl = BlFactory.GetBl();

                        //        Console.Write("Please enter how many days back: ");
                        //        int days = int.Parse(Console.ReadLine());
                        //        for (int d = days; d >= 0; --d)
                        //        {
                        //            Weather w = bl.GetWeather(d);
                        //            Console.WriteLine($"{d} days before - Feeling was: {w.Feeling} Celsius degrees");
                        //        }

                        //    }