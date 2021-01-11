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
        enum CoiceBus { add = 1, delete, update, viewDetails, viewAll }
        enum CoiceBusStation { add = 1, delete, update, viewDetails, viewAll }
        enum CoiceLine { add = 1, delete, update, viewDetails, viewAll, addStation, deleteStation, viewStations }

        static void Main(string[] args)
        {
            bl = BlFactory.GetBl();
            Console.WriteLine("1:bus\n2:bus station\n3:line\n");
            int myChoice, myChoice2;

            do
            {
                Console.WriteLine("Enter your choice");
                myChoice = int.Parse(Console.ReadLine());
                try
                {
                    switch ((Choice)myChoice)
                    {
                        case Choice.Bus:
                            {
                                Console.WriteLine("1:add bus\n 2:delete bus\n 3:update bus\n 4:view bus details\n 5:view all buses");
                                Console.WriteLine("Enter your choice");
                                myChoice2 = int.Parse(Console.ReadLine());
                                switch ((CoiceBus)myChoice2)
                                {
                                    case CoiceBus.add:
                                        {
                                            try
                                            {
                                                Console.WriteLine("Enter details:{LicenseNumber, LicensingDate, Kilometerage, KmFromLastRefuel, Fuel, KmFromLastCare, DateOfLastCare, Status, Driver name");
                                                //int _lcNum = int.Parse(Console.ReadLine()); //cin license number
                                                //DateTime _dt = DateTime.Parse(Console.ReadLine());  //cin date beggining
                                                //string v = _lcNum.ToString();
                                                //double _km = double.Parse(Console.ReadLine());
                                                //double _KmOfLastRefuel = double.Parse(Console.ReadLine());
                                                //double _KmOfLastCare = double.Parse(Console.ReadLine());
                                                //double _kilometer;
                                                //DateTime _dtc = DateTime.Parse(Console.ReadLine());
                                                ////string timerAct;
                                                //StatusEnum _status = (StatusEnum)int.Parse(Console.ReadLine());
                                                ////
                                                BOBus b = new BOBus
                                                {
                                                    LicenseNumber = 12345677,
                                                    LicensingDate = DateTime.Now,
                                                    Kilometerage = 200,
                                                    KmFromLastRefuel = 150,
                                                    Fuel = (1200 - 150) / 1200,
                                                    KmFromLastCare = 200,
                                                    DateOfLastCare = DateTime.Now,
                                                    Status = (StatusEnum)1,
                                                    Driver = "mosh",
                                                    IsExist = true
                                                    //timerAct="",
                                                };
                                                //while ((v.Length != 7 && _dt.Year < 2018) || (v.Length != 8 && _dt.Year >= 2018))  //check license number
                                                //{
                                                //    Console.WriteLine("illegal number. enter again");
                                                //    _lcNum = int.Parse(Console.ReadLine()); //cin license number
                                                //    v = _lcNum.ToString();
                                                //}
                                                //////
                                                //if (_km < _KmOfLastCare || _km < _KmOfLastRefuel)
                                                //    _kilometer = Math.Max(_KmOfLastCare, _KmOfLastRefuel);
                                                //else
                                                //    _kilometer = _km;
                                                //////
                                                //if (!((DateTime.Today.AddYears(-1)) < _dtc) || (_KmOfLastCare) > 18500) //checking time/km from last care
                                                //{
                                                //    _status = (StatusEnum)2; //need care 
                                                //}
                                                //else if (_KmOfLastRefuel > 1000) //checking fuel
                                                //{
                                                //    _status = (StatusEnum)3; //need refuel 
                                                //}
                                                //else
                                                //    _status = (StatusEnum)1; //ready

                                                //BOBus b = new BOBus
                                                //{
                                                //    LicenseNumber = _lcNum,
                                                //    LicensingDate = _dt,
                                                //    Kilometerage = _kilometer,
                                                //    Fuel = (1200 - _KmOfLastRefuel) / 1200,
                                                //    KmFromLastRefuel = _KmOfLastRefuel,
                                                //    KmFromLastCare = _KmOfLastCare,
                                                //    DateOfLastCare = _dtc,
                                                //    Status = _status,
                                                //    Driver = Console.ReadLine(),
                                                //    IsExist = true
                                                //    //timerAct="",
                                                //};
                                                bl.AddBus(b);
                                                Console.WriteLine("bus added!");
                                            }

                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;

                                    case CoiceBus.delete:
                                        {
                                            try
                                            {
                                                Console.WriteLine("enter license number:");
                                                int _LicenseNumber = int.Parse(Console.ReadLine());
                                                bl.DeleteBus(_LicenseNumber);
                                                Console.WriteLine("bus deleted");
                                            }

                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceBus.update:
                                        {
                                            try
                                            {
                                                Console.WriteLine("Enter details:{LicenseNumber, LicensingDate, Kilometerage, KmFromLastRefuel, Fuel, KmFromLastCare, DateOfLastCare, Status, Driver name");
                                                //int _lcNum = int.Parse(Console.ReadLine()); //cin license number
                                                //DateTime _dt = DateTime.Parse(Console.ReadLine());  //cin date beggining
                                                //                                                    //string v = _lcNum.ToString();
                                                //double _km = double.Parse(Console.ReadLine());
                                                //double _KmOfLastRefuel = double.Parse(Console.ReadLine());
                                                //double _KmOfLastCare = double.Parse(Console.ReadLine());
                                                //double _kilometer = _km;
                                                //DateTime _dtc = DateTime.Parse(Console.ReadLine());
                                                //StatusEnum _Status = (StatusEnum)int.Parse(Console.ReadLine());
                                                //BOBus b = new BOBus
                                                //{
                                                //    LicenseNumber = _lcNum,
                                                //    LicensingDate = _dt,
                                                //    Kilometerage = _kilometer,
                                                //    Fuel = (1200 - _KmOfLastRefuel) / 1200,
                                                //    KmFromLastRefuel = _KmOfLastRefuel,
                                                //    KmFromLastCare = _KmOfLastCare,
                                                //    DateOfLastCare = _dtc,
                                                //    Status = _Status,
                                                //    Driver = Console.ReadLine(),
                                                //    IsExist = true
                                                //    //timerAct="",
                                                //};
                                                BOBus b = new BOBus
                                                {
                                                    LicenseNumber = 23456789,
                                                    LicensingDate = DateTime.Now,
                                                    Kilometerage = 200,
                                                    KmFromLastRefuel = 150,
                                                    Fuel = (1200 - 150) / 1200,
                                                    KmFromLastCare = 200,
                                                    DateOfLastCare = DateTime.Now,
                                                    Status = (StatusEnum)1,
                                                    Driver = "mosh",
                                                    IsExist = true
                                                    //timerAct="",
                                                };
                                                bl.UpdateBus(b);
                                                Console.WriteLine("bus updated");
                                            }

                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceBus.viewDetails:
                                        {
                                            try
                                            {
                                                Console.WriteLine("enter license number:");
                                                int _LicenseNumber = int.Parse(Console.ReadLine());
                                                BOBus b = bl.GetBus(_LicenseNumber);
                                                Console.WriteLine($"LicenseNumber: {b.LicenseNumber}");
                                            }
                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceBus.viewAll:
                                        {
                                            try
                                            {
                                                IEnumerable<BOBus> ls = bl.GetAllBuses();
                                                foreach (BOBus b in ls)
                                                    Console.WriteLine(b.LicenseNumber);
                                                IEnumerable<BOBus> lsp = bl.GetSpecificBuses(x=>x.IsExist);
                                            }

                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
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
                                myChoice2 = int.Parse(Console.ReadLine());
                                switch ((CoiceBusStation)myChoice2)
                                {
                                    case CoiceBusStation.add:
                                        {
                                            try
                                            {
                                                BOBusStation bs = new BOBusStation { Name = "aaa", Longitude = 32.3, Latitude = 33.2, Address = "aaa 1", IsExist = true, Accessibility = true };
                                                bl.AddBusStation(bs);
                                                Console.WriteLine("bus station added");
                                            }
                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;

                                    case CoiceBusStation.delete:
                                        {
                                            try
                                            {
                                                //bl.DeleteBusStation(60);
                                                Console.WriteLine("enter Station Code number:");
                                                int _StationCode = int.Parse(Console.ReadLine());
                                                bl.DeleteBusStation(_StationCode);
                                                Console.WriteLine("bus Station deleted");
                                            }
                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceBusStation.update:
                                        {
                                            try
                                            {
                                                BOBusStation bs1 = new BOBusStation { StationCode=5, Name = "bbb", Longitude = 32.3, Latitude = 33.2, Address = "bbb 1", IsExist = true, Accessibility = true };
                                                bl.UpdateStation(bs1);
                                            }

                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceBusStation.viewDetails:
                                        {
                                            try
                                            {
                                                Console.WriteLine("enter station number:");
                                                int _stationNumber = int.Parse(Console.ReadLine());
                                                BOBusStation bs = bl.GetBusStation(_stationNumber);
                                                Console.WriteLine($"staion number:{bs.StationCode}, {bs.Name},\n {bs.Lines}");
                                            }

                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceBusStation.viewAll:
                                        {
                                            try
                                            {
                                                IEnumerable<BOBusStation> ls = bl.GetAllBusStations();
                                                Console.WriteLine();
                                                foreach (BOBusStation b in ls)
                                                    Console.WriteLine(b.Name);
                                            }

                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
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
                                myChoice2 = int.Parse(Console.ReadLine());
                                switch ((CoiceLine)myChoice2)
                                {
                                    case CoiceLine.add:
                                        {
                                            try
                                            {
                                                BOLine l = new BOLine { BusLine = 40, Code = 40, FirstStation = 1, LastStation = 2, Area = (AreaEnum)3, IsExist = true };
                                                bl.AddLine(l);
                                                Console.WriteLine("Line added");
                                            }
                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;

                                    case CoiceLine.delete:
                                        {
                                            try
                                            {
                                                Console.WriteLine("enter Line Code:");
                                                int _code = int.Parse(Console.ReadLine());
                                                bl.DeleteLine(_code);
                                                Console.WriteLine("Line deleted");
                                                bl.DeleteLine(180);
                                            }
                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceLine.update:
                                        {
                                            try
                                            {
                                                BOLine l = new BOLine { BusLine = 345, Code = 20, FirstStation = 19, LastStation = 5, Area = (AreaEnum)3, IsExist = true };
                                                bl.UpdateLine(l);
                                                Console.WriteLine("Line update");
                                            }
                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceLine.viewDetails:  //לבדוק הדפסה של התחנות
                                        {
                                            try
                                            {
                                                Console.WriteLine("enter Line Code:");
                                                int _code = int.Parse(Console.ReadLine());
                                                BOLine l = bl.GetLine(_code);
                                                Console.WriteLine($"Line code {l.Code}, {l.BusLine}, {l.FirstStation}, {l.LastStation},\n {l.Stations}");
                                            }
                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceLine.viewAll:
                                        {
                                            try
                                            {
                                                IEnumerable<BOLine> ls = bl.GetAllLines();
                                                foreach (BOLine l in ls)
                                                    Console.WriteLine($"{l.Code}, {l.BusLine}, {l.Area}");
                                            }

                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceLine.addStation:
                                        {
                                            Console.WriteLine("enter line code, station code, index");
                                            int _code = int.Parse(Console.ReadLine());
                                            int _StationCode = int.Parse(Console.ReadLine());
                                            int index = int.Parse(Console.ReadLine());
                                            BOLine l = bl.GetLine(_code);
                                            bl.AddStationInLine(l, _StationCode, index);
                                        }
                                        break;
                                    case CoiceLine.deleteStation:
                                        {//לבדוק שמוחק גם את התחנת קו
                                            try
                                            {
                                                Console.WriteLine("enter line code, station code");
                                                int _code = int.Parse(Console.ReadLine());
                                                int _StationCode = int.Parse(Console.ReadLine());
                                                BOLine l = bl.GetLine(_code);
                                                bl.DeleteStationInLine(l, _StationCode);
                                            }
                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
                                        }
                                        break;
                                    case CoiceLine.viewStations:
                                        {
                                            try
                                            {//?
                                                Console.WriteLine("enter line code");
                                                int _code = int.Parse(Console.ReadLine());
                                                BOLine l = bl.GetLine(_code);
                                                //List<BOBusStation> lll=bl.GetStationsOfLine(8).ToList();
                                                Console.WriteLine(l.Stations);

                                            }
                                            catch (BLException dex)
                                            {
                                                Console.WriteLine(dex.Message);
                                            }
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
                }
                catch (BLException dex)
                {
                    Console.WriteLine(dex.Message);
                }
            } while (myChoice != 4);
            Console.WriteLine("end\n");
        }
    }
}
