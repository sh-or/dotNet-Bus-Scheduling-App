using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlAPI;
using DLAPI;
using BO;
using DO;
using System.Device.Location;

namespace BL
{
    public class BlImp1 : IBL
    {
        readonly IDAL dal = DalFactory.GetDal();
        public static Random r = new Random(DateTime.Now.Millisecond);
        public static string ManagePassword = "dotNet5781";
        public void reset()
        {
            //try
            //{
            //    dal.reset();
            //}
            //catch (DOException dex)
            //{
            //    throw new BLException(dex.Message, dex);
            //}
        }

        #region Bus
        public BOBus GetBus(int _LicenseNumber)
        {
            BOBus b = new BOBus();
            try
            {
                b = (BOBus)Transform.trans(dal.GetBus(_LicenseNumber), b.GetType());
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            return b;
        }
        public void UpdateBus(BOBus b)
        {
            Bus tmp = new Bus();
            if (b.LicensingDate > b.DateOfLastCare)
                throw new BLException($"Invalid dates");
            if (b.Kilometerage < b.KmFromLastCare || b.Kilometerage < b.KmFromLastRefuel)
                throw new BLException($"Kilometerage cannot be less than KmFromLastCare or KmFromLastRefuel");
            if (b.KmFromLastCare > 20000)
                throw new BLException($"Bus cannot drive {b.KmFromLastCare} Km from last care");
            if (b.KmFromLastRefuel > 1200)
                throw new BLException($"Bus cannot drive {b.KmFromLastRefuel} Km from last refuel");
            try
            {
                tmp = dal.GetBus(b.LicenseNumber);
                b.Fuel = 1 - b.KmFromLastRefuel / 1200;
                if((int)b.Status<5) //not in refuel/care
                {
                    if (!((DateTime.Today.AddYears(-1)) < b.DateOfLastCare) || (b.KmFromLastCare) > 18500) //checking time/km from last care
                    {
                        b.Status = (BO.StatusEnum)2; //need care 
                    }
                    else if (b.KmFromLastRefuel > 1000) //checking fuel
                    {
                        b.Status = (BO.StatusEnum)3; //need refuel 
                    }
                    else
                        b.Status = (BO.StatusEnum)1;
                }
                dal.UpdateBus((Bus)Transform.trans(b, tmp.GetType())); ;
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        public IEnumerable<BOBus> GetSpecificBuses(Predicate<BOBus> p)//conditionnnn
        {
            BOBus tmp = new BOBus();
            IEnumerable<Bus> bs;
            try
            {
                bs = dal.GetSpecificBuses((Predicate<Bus>)p);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            IEnumerable<BOBus> bobs = from Bus b in bs
                                      select (BOBus)Transform.trans(b, tmp.GetType());
            return bobs;
        }
        public IEnumerable<BOBus> GetAllBuses()
        {
            BOBus tmp = new BOBus();
            IEnumerable<Bus> b;
            try
            {
                b = dal.GetAllBuses();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            IEnumerable<BOBus> bobs = from Bus bb in b
                                      select (BOBus)Transform.trans(bb, tmp.GetType());
            return bobs;
        }
        public void AddBus(BOBus b)
        {
            Bus tmp = new Bus();
            if ((b.LicenseNumber > 9999999 && b.LicensingDate.Year < 2018) || (b.LicenseNumber < 10000000 && b.LicensingDate.Year >= 2018)) //license number and date don't match
                throw new BLException($"Bus number {b.LicenseNumber} does not match the licensing date");
            if (b.LicensingDate > b.DateOfLastCare)
                throw new BLException($"Invalid dates");
            if (b.Kilometerage < b.KmFromLastCare || b.Kilometerage < b.KmFromLastRefuel)
                throw new BLException($"Kilometerage cannot be less than KmFromLastCare or KmFromLastRefuel");
            if (b.KmFromLastCare > 20000)
                throw new BLException($"Bus cannot drive {b.KmFromLastCare} Km from last care");
            if (b.KmFromLastRefuel > 1200)
                throw new BLException($"Bus cannot drive {b.KmFromLastRefuel} Km from last refuel");
            //b.Kilometerage=Math.Max(b.Kilometerage,Math.Max(b.KmFromLastCare,b.KmFromLastRefuel));
            b.Fuel = 1 - b.KmFromLastRefuel / 1200;
            b.Status = (BO.StatusEnum)1;
            if (!((DateTime.Today.AddYears(-1)) < b.DateOfLastCare) || (b.KmFromLastCare) > 18500) //checking time/km from last care
            {
                b.Status = (BO.StatusEnum)2; //need care 
            }
            else if (b.KmFromLastRefuel > 1000) //checking fuel
            {
                b.Status = (BO.StatusEnum)3; //need refuel 
            }
            try
            {
                dal.AddBus((Bus)Transform.trans(b, tmp.GetType()));
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        public void DeleteBus(int _LicenseNumber)
        {
            try
            {
                dal.DeleteBus(_LicenseNumber);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        #endregion

        #region BusStation

        public BOBusStation GetBusStation(int _StationCode)
        {
            BOBusStation bs = new BOBusStation();
            try
            {
                bs = (BOBusStation)Transform.trans(dal.GetBusStation(_StationCode), bs.GetType());
                bs.Lines = from l in dal.GetStationLines(_StationCode)
                           where dal.IsStationInLine(l.Code, _StationCode) > -1
                           select new BOStationLine
                           {
                               BusLine = l.BusLine,
                               Code = l.Code,
                               LastStation = l.LastStation,
                               IndexOfThisStation = dal.IsStationInLine(l.Code, _StationCode)
                           };

            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            return bs;
        }
        public void UpdateStation(BOBusStation bs)
        {
            if (bs.Latitude < 31 || bs.Latitude > 33.3)
                throw new BLException($"Latitude {bs.Latitude} exceeds the borders of Israel");
            if (bs.Longitude < 34.3 || bs.Longitude > 35.5)
                throw new BLException($"Longitude {bs.Longitude} exceeds the borders of Israel");

            BusStation tmp = new BusStation();
            try
            {
                dal.UpdateStation((BusStation)Transform.trans(bs, tmp.GetType()));
                List<ConsecutiveStations> lst = dal.GetSomeConsecutiveStations(bs.StationCode).ToList();
                BOBusStation b1, b2;
                if (lst != null)
                    foreach (ConsecutiveStations cs in lst)
                    {
                        if (cs.StationCode1 == bs.StationCode)
                        {
                            b1 = bs;
                            b2 = GetBusStation(cs.StationCode2);
                        }
                        else
                        {
                            b1 = GetBusStation(cs.StationCode1);
                            b2 = bs;
                        }
                        GeoCoordinate loc1 = new GeoCoordinate(b1.Latitude, b1.Longitude);
                        GeoCoordinate loc2 = new GeoCoordinate(b2.Latitude, b2.Longitude);
                        cs.Distance = loc1.GetDistanceTo(loc2) * (1 + r.NextDouble() / 2); //air-distance(in meters)*(1 to 1.5)
                        cs.DriveTime = TimeSpan.FromSeconds(cs.Distance / (r.Next(50, 70) * 1 / 3.6)); //the bus cross 50-70 KmH
                        dal.UpdateConsecutiveStations(cs);
                    }

                //lines will be updated by lines
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        public IEnumerable<BOBusStation> GetSpecificBusStations(Predicate<BOBusStation> p)
        {
            IEnumerable<BusStation> bs;
            IEnumerable<BOBusStation> bobs;
            BOStationLine tmp = new BOStationLine();
            try
            {
                bs = dal.GetSpecificBusStations((Predicate<BusStation>)p);
                bobs = from BusStation b in bs
                       select (BOBusStation)Transform.trans(b, tmp.GetType());
                foreach (BOBusStation b in bobs)
                {
                    b.Lines = from l in dal.GetStationLines(b.StationCode)
                              where dal.IsStationInLine(l.Code, b.StationCode) > -1
                              select new BOStationLine
                              {
                                  BusLine = l.BusLine,
                                  Code = l.Code,
                                  LastStation = l.LastStation,
                                  IndexOfThisStation = dal.IsStationInLine(l.Code, b.StationCode)
                              };
                }
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            return bobs;
        }
        public IEnumerable<BOBusStation> GetAllBusStations()
        {
            IEnumerable<BusStation> bs;
            List<BOBusStation> bobs;
            BOBusStation tmp = new BOBusStation();
            try
            {
                bs = dal.GetAllBusStations();
                bobs = (from BusStation x in bs
                        select (BOBusStation)Transform.trans(x, tmp.GetType())).ToList();
                List<BOStationLine> b;
                for (int i = 0; i < bobs.Count(); i++)
                {
                    b = (from l in dal.GetStationLines(bobs[i].StationCode)
                         where dal.IsStationInLine(l.Code, bobs[i].StationCode) > -1
                         select new BOStationLine
                         {
                             BusLine = l.BusLine,
                             Code = l.Code,
                             LastStation = l.LastStation,
                             IndexOfThisStation = dal.IsStationInLine(l.Code, bobs[i].StationCode)
                         }).ToList();
                    bobs[i].Lines = b;
                }
                
                return bobs;
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        public int AddBusStation(BOBusStation bs) //it was just build.adding with no lines. empty list created in UI.
        {
            if(bs.Latitude<31|| bs.Latitude > 33.3)
                throw new BLException($"Latitude {bs.Latitude} exceeds the orders of Israel");
            if (bs.Longitude < 34.3 || bs.Longitude > 35.5)
                throw new BLException($"Longitude {bs.Longitude} exceeds the orders of Israel");

            BusStation tmp = new BusStation();
            try
            {
                int runNumber = dal.AddBusStation((BusStation)Transform.trans(bs, tmp.GetType()));
                bs.StationCode = runNumber;
                return runNumber;
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        public void DeleteBusStation(int _StationCode)
        {
            try
            {
                string str = "";
                IEnumerable<LineStation> ls = dal.GetSpecificLineStations(x => x.StationCode == _StationCode).ToList();
                if (ls != null)
                {
                    foreach (LineStation x in ls)
                    {
                        if (dal.GetStationsOfLine(x.LineCode).Count() < 3) //creats string of problematic lines
                            str += x.LineCode + " ";
                    }
                    if (str == "")
                        foreach (LineStation x in ls)
                            DeleteStationInLine(GetLine(x.LineCode), _StationCode);
                    else
                        throw new BLException($"Cannot delete station {_StationCode}!\n" +
                            "Line(s) " + str + "cannot stay with less than 2 stations");
                }
                IEnumerable<ConsecutiveStations> cs = dal.GetSomeConsecutiveStations( _StationCode).ToList();
                if (cs != null)
                    foreach (ConsecutiveStations c in cs)
                        dal.DeleteConsecutiveStations(c.StationCode1, c.StationCode2);
                }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        #endregion

        #region Line

        public BOLine GetLine(int _Code) 
        {
            BOLine l = new BOLine();
            IEnumerable<BusStation> st;
            ConsecutiveStations cs1;
            int i = 0;
            BOLineStation tmp = new BOLineStation();
            try
            {
                l = (BOLine)Transform.trans(dal.GetLine(_Code), l.GetType());
                st = dal.GetStationsOfLine(_Code);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }

            l.Stations = from BusStation s in st
                         select new BOLineStation
                         {
                             StationCode = s.StationCode,
                             Name = s.Name,
                             Distance = 0,
                             DriveTime = TimeSpan.Zero
                         };
            try
            {
                List<BOLineStation> lst = l.Stations.ToList();
                BOLineStation s = new BOLineStation();
                for (i = 1; i < l.Stations.Count(); i++)
                {
                    s = lst[i];
                    cs1 = dal.GetConsecutiveStations(st.ElementAt(i - 1).StationCode, s.StationCode);
                    lst[i].Distance = cs1.Distance;
                    lst[i].DriveTime = cs1.DriveTime;
                }
                l.Stations = lst;

            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            return l;
        }
        public void UpdateLine(BOLine l) //not for updating station list
        {
            Line tmp = dal.GetLine(l.Code); //check if exist
            try
            {
                dal.UpdateLine((Line)Transform.trans(l, tmp.GetType()));
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        public void DeleteStationInLine(BOLine l, int _StationCode)
        {
            BOLineStation ls = l.Stations.FirstOrDefault(x => x.StationCode == _StationCode);  //UI catch ex
            if (ls == null)
                throw new BLException($"Station number {_StationCode} was not found");
            if (l.Stations.Count() < 3)
                throw new BLException($"Line {l.Code} cannot be with less than 2 stations");
            Line tmp = new Line();
            int location;
            List<BOLineStation> lst = l.Stations.ToList();
            try
            {
                location = dal.GetLineStation(l.Code, _StationCode).StationNumberInLine;
                if (location < l.Stations.Count() - 1)   //not last station
                { //update time&distance
                    if (location == 0)
                    {
                        lst[1].Distance = 0;
                        lst[1].DriveTime = TimeSpan.Zero;
                        l.FirstStation = l.Stations.ElementAt(1).StationCode;
                        dal.UpdateLine((Line)Transform.trans(l, tmp.GetType()));
                    }
                    else
                    {//if not exist ConsecutiveStations->create ConsecutiveStations!
                        if (!dal.isExistConsecutiveStations(l.Stations.ElementAt(location - 1).StationCode, l.Stations.ElementAt(location + 1).StationCode))
                            AddConsecutiveStations(l.Stations.ElementAt(location - 1).StationCode, l.Stations.ElementAt(location + 1).StationCode);
                        ConsecutiveStations cs = dal.GetConsecutiveStations(l.Stations.ElementAt(location - 1).StationCode, l.Stations.ElementAt(location + 1).StationCode);
                        lst[location + 1].Distance = cs.Distance;
                        lst[location + 1].DriveTime = cs.DriveTime;
                    }
                    foreach (LineStation x in dal.GetAllLineStations(l.Code)) //change the index of later stations in l
                    {
                        if (x.StationNumberInLine >= location)
                            dal.UpdateLineStation(l.Code, x.StationCode, -1);
                    }
                }
                else
                {
                    l.LastStation = l.Stations.ElementAt(l.Stations.Count() - 2).StationCode;
                    dal.UpdateLine((Line)Transform.trans(l, tmp.GetType()));
                }
                lst.RemoveAt(location);
                l.Stations = lst;
                dal.DeleteLineStation(l.Code, _StationCode);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        public void AddStationInLine(BOLine l, int _StationCode, int index)
        {
            if (l.Stations.ToList().Exists(x => x.StationCode == _StationCode))
                throw new BLException($"Station number {_StationCode} is already exist in this line");
            if (index > l.Stations.Count())
                throw new BLException($"Index {index} exceeds from list length");
            try
            {
                BOLineStation ls = new BOLineStation();
                ls.StationCode = _StationCode;
                
                ls.Name = dal.GetBusStation(_StationCode).Name;
                List<BOLineStation> bols = l.Stations.ToList();
                bols.Insert(index, ls);
                l.Stations = bols;
                if (index == 0)
                {
                    l.FirstStation = _StationCode;
                    UpdateLine(l);
                    l.Stations.ElementAt(index).Distance = 0;
                    l.Stations.ElementAt(index).DriveTime = TimeSpan.Zero;
                    
                    dal.AddLineStation(l.Code, _StationCode, 0);
                    // UpdateLineStation(l.Stations.ElementAt(index));//change to update dal.UpdateLineStation!!create do.linestation..
                }
                else  //not first station
                {
                    if (index == l.Stations.Count() - 1) //the last station
                    {
                        l.LastStation = _StationCode;
                        UpdateLine(l);
                    }
                    if (!dal.isExistConsecutiveStations(l.Stations.ElementAt(index - 1).StationCode, _StationCode))
                        AddConsecutiveStations(l.Stations.ElementAt(index - 1).StationCode, _StationCode);
                    ConsecutiveStations cs = dal.GetConsecutiveStations(l.Stations.ElementAt(index - 1).StationCode, _StationCode); //maybe get from new UI window
                    l.Stations.ElementAt(index).Distance = cs.Distance;
                    l.Stations.ElementAt(index).DriveTime = cs.DriveTime;
                    dal.AddLineStation(l.Code, _StationCode, index);
                    //UpdateLineStation(l.Stations.ElementAt(index));//change to update dal.UpdateLineStation!!create do.linestation..
                }
                if (index != l.Stations.Count() - 1) //not last station
                {
                    if (!dal.isExistConsecutiveStations(_StationCode, l.Stations.ElementAt(index + 1).StationCode))
                        AddConsecutiveStations(_StationCode, l.Stations.ElementAt(index + 1).StationCode);
                    ConsecutiveStations cs = dal.GetConsecutiveStations(_StationCode, l.Stations.ElementAt(index + 1).StationCode); //maybe get from new UI window
                    l.Stations.ElementAt(index + 1).Distance = cs.Distance;
                    l.Stations.ElementAt(index + 1).DriveTime = cs.DriveTime;
                    //UpdateLineStation(l.Stations.ElementAt(index + 1)); //change to update dal.UpdateLineStation!!create do.linestation..
                    foreach (LineStation x in dal.GetAllLineStations(l.Code)) //change the index of later stations in l
                    {
                        if (x.StationNumberInLine >= index && x.StationCode!=_StationCode)
                            dal.UpdateLineStation(l.Code, x.StationCode, 1);
                    }
                }

            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }

        public IEnumerable<BOLine> GetAllLines()
        {
            IEnumerable<Line> l;
            try
            {
                l = dal.GetAllLines();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            IEnumerable<BOLine> bol = (from Line ll in l
                                       select GetLine(ll.Code));
            return bol;
        }
        public IEnumerable<BOLine> GetSpecificLines(Predicate<BOLine> p)
        {
            IEnumerable<Line> l;
            try
            {
                l = dal.GetSpecificLines((Predicate<Line>)p);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            IEnumerable<BOLine> bol = (from Line ll in l
                                       select GetLine(ll.Code));
            return bol;
        }
        //we didnt use Drive, but it can use for program expanding
        //public IEnumerable<BOBusStation> GetStationsOfLine(int _LineCode) 
        //{
        //    BOBusStation tmp = new BOBusStation();
        //    IEnumerable<BusStation> bs;
        //    try
        //    {
        //         bs = dal.GetStationsOfLine(_LineCode);
        //    }
        //    catch (DOException dex)
        //    {
        //        throw new BLException(dex.Message, dex);
        //    }
        //    IEnumerable<BOBusStation> bobs = from BusStation b in bs
        //                               select (BOBusStation)Transform.trans(b,tmp.GetType()); 
        //    return bobs;
        //}
        public int AddLine(BOLine l)
        {
            if (l.FirstStation == 0 || l.LastStation == 0)
                throw new BLException("Cannot add new line without first&last stations");
            if (l.FirstStation == l.LastStation)
                throw new BLException("Cannot add new line with identical first&last stations");

            try
            {
                l.Stations = new List<BOLineStation>();
                BOLineStation first = new BOLineStation() { StationCode = l.FirstStation, Distance = 0, DriveTime = TimeSpan.Zero };
                first.Name = dal.GetBusStation(l.FirstStation).Name;
                List<BOLineStation> lll = l.Stations.ToList();
                lll.Add(first);
                BOLineStation last = new BOLineStation() { StationCode = l.LastStation };
                last.Name = dal.GetBusStation(l.LastStation).Name;
                ConsecutiveStations cs;
                if (!dal.isExistConsecutiveStations(l.FirstStation, l.LastStation))
                {
                    AddConsecutiveStations(l.FirstStation, l.LastStation);
                    cs = dal.GetConsecutiveStations(l.FirstStation, l.LastStation);
                    last.Distance = cs.Distance;
                    last.DriveTime = cs.DriveTime;
                }
                else
                {
                    cs = dal.GetConsecutiveStations(l.FirstStation, l.LastStation);
                    last.Distance = cs.Distance;
                    last.DriveTime = cs.DriveTime;
                }
                Line tmp = new Line();
                lll.Add(last);
                l.Stations = lll;
                l.Code = dal.AddLine((Line)Transform.trans(l, tmp.GetType()));
                dal.AddLineStation(l.Code, first.StationCode, 0);
                dal.AddLineStation(l.Code, last.StationCode, 1);
                return l.Code;
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }


        public void DeleteLine(int _Code)
        {
            try
            {
                IEnumerable<LineStation> ls = dal.GetSpecificLineStations(x => x.LineCode == _Code);
                foreach (LineStation x in ls)
                {
                    dal.DeleteLineStation(_Code, x.StationCode);
                    List<LineTrip> lst = dal.GetAllLineTrips(_Code).ToList();
                    for (int i = lst.Count() - 1; i >= 0; i--)
                    {
                        dal.DeleteLineTrip(_Code, lst[i].Start);
                    }
                }
                dal.DeleteLine(_Code);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        #endregion

        #region ConsecutiveStations

        public void AddConsecutiveStations(int _StationCode1, int _StationCode2)
        {
            BusStation tmp = new BusStation();
            ConsecutiveStations cs = new ConsecutiveStations();
            cs.StationCode1 = _StationCode1;
            cs.StationCode2 = _StationCode2;
            BusStation b1 = (BusStation)Transform.trans(GetBusStation(_StationCode1), tmp.GetType()); //also check if exist..
            BusStation b2 = (BusStation)Transform.trans(GetBusStation(_StationCode2), tmp.GetType());//
            GeoCoordinate loc1 = new GeoCoordinate(b1.Latitude, b1.Longitude);
            GeoCoordinate loc2 = new GeoCoordinate(b2.Latitude, b2.Longitude);
            cs.Distance = loc1.GetDistanceTo(loc2) * (1 + r.NextDouble() / 2); //air-distance(in meters)*(1 to 1.5)
            cs.DriveTime = TimeSpan.FromSeconds(cs.Distance / (r.Next(50, 70) * 1 / 3.6)); //the bus cross 50-70 KmH
            try
            {
                dal.AddConsecutiveStations(cs);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        public BOConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2)
        {
            BOConsecutiveStations bocs = new BOConsecutiveStations() { StationCode1 = _StationCode1, StationCode2 = _StationCode2 };
            ConsecutiveStations cs = new ConsecutiveStations();
            try
            {
                cs = dal.GetConsecutiveStations(_StationCode1, _StationCode2);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            bocs.Distance = cs.Distance;
            bocs.DriveTime = cs.DriveTime;
            return bocs;
        }
        //public IEnumerable<BOConsecutiveStations> GetSpecificConsecutiveStations(predicate<BOConsecutiveStations> p/*or code1*/) //all?of 1 station?
        public void UpdateConsecutiveStations(BOConsecutiveStations cs)
        {
            ConsecutiveStations tmp = new ConsecutiveStations();
            try
            {
                dal.UpdateConsecutiveStations((ConsecutiveStations)Transform.trans(cs, tmp.GetType()));
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        #endregion

        #region Drive
        //we didnt use Drive, but it can use for program expanding

        //public void AddDrive(int _StartStation, int _DestinationStation)
        //{
        //    BODrive drive = new BODrive();
        //    try
        //    {
        //        drive.StartStation = _StartStation;
        //        drive.DestinationStation = _DestinationStation;
        //    }
        //    catch (DOException dex)
        //    {
        //        throw new BLException(dex.Message, dex);
        //    }
        //}
        #endregion

        #region User
        
        public BOUser GetUser(string name, string password)
        {
            BOUser u=new BOUser();
            try
            {
                return (BOUser)Transform.trans(dal.GetUser(name, password), u.GetType());
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        public void UpdateUser(BOUser u) //only for changing password or managing 
        {
            User du = new User();
            try
            {
                if (u.IsManager && u.Password != ManagePassword)
                    throw new BLException("Wrong manage password");
                dal.UpdateUser((User)Transform.trans(u, du.GetType()));
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        //public IEnumerable<BOUser> GetSpecificUsers(Predicate<BOUser> p)
        //{
        //    BOUser u = new BOUser();
        //    try
        //    {
        //        return (BOUser)Transform.trans(dal.GetUser(name), u.GetType());
        //    }
        //    catch (DOException ex)
        //    {
        //        throw new BLException(ex.Message, ex);
        //    }
        //}
        public IEnumerable<BOUser> GetAllUsers()
        {
            BOUser tmp = new BOUser();
            IEnumerable<User> u;
            try
            {
                u = dal.GetAllUsers();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
            IEnumerable<BOUser> bou = from User x in u
                                      select (BOUser)Transform.trans(x, tmp.GetType());
            return bou;
        }
        public void AddUser(BOUser u)
        {
            User du = new User();
            try
            {
                if (u.IsManager && u.Password != ManagePassword)
                    throw new BLException("Wrong manage password");
                dal.AddUser((User)Transform.trans(u, du.GetType()));
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        public void DeleteUser(string name)
        {
            try
            {
                dal.DeleteUser(name);
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        #endregion

        #region LineTrip
        public void AddLineTrip(BOLineTrip lt)
        {
            LineTrip tmp = new LineTrip();
            try
            {
                dal.AddLineTrip((LineTrip)Transform.trans(lt, tmp.GetType()));
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        public BOLineTrip GetLineTrip(int _LineCode, TimeSpan _Start)
        {
            try
            {
                LineTrip l = dal.GetLineTrip(_LineCode, _Start);
                Line bl = dal.GetLine(l.LineCode);
                return new BOLineTrip()
                {
                    LineCode = l.LineCode,
                    BusLine = bl.BusLine,
                    Start = l.Start,
                    Destination = dal.GetBusStation(bl.LastStation).Name,
                    IsExist = true
                };
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        public IEnumerable<BOLineTrip> GetAllLineTrips(int _LineCode)
        {
            BOLineTrip tmp = new BOLineTrip();
            try
            {
                //An option to use grouping, but it wasnt necessary in this case..
                //var lst = from LineTrip l in dal.GetAllLineTrips(_LineCode)
                //          let bl = dal.GetLine(l.LineCode)
                //          orderby l.Start
                //          group new BOLineTrip()
                //          {
                //              LineCode = l.LineCode,
                //              BusLine = bl.BusLine,
                //              Start = l.Start,
                //              Destination = dal.GetBusStation(bl.LastStation).Name,
                //              IsExist = true
                //          } by bl.Area;
                //return (IEnumerable<BOLineTrip>)lst;
                return from LineTrip l in dal.GetAllLineTrips(_LineCode)
                       let bl = dal.GetLine(l.LineCode)
                       orderby l.Start
                       select new BOLineTrip()
                       {
                           LineCode = l.LineCode,
                           BusLine = bl.BusLine,
                           Start = l.Start,
                           Destination = dal.GetBusStation(bl.LastStation).Name,
                           IsExist = true
                           //       };
                       };
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        public IEnumerable<BOLineTrip> GetAllStationLineTrips(int _StationCode, TimeSpan _Start)
        {
            BOLineTrip tmp = new BOLineTrip();
            try
            {
                return from l in dal.GetAllStationLineTrips(_StationCode, _Start)
                       let x= dal.GetLine(l.LineCode)
                       let dt= DriveTimeToStation(l.LineCode, _StationCode)
                       where (l.Start + dt) >= _Start
                       orderby l.Start
                       select new BOLineTrip()
                       {
                           LineCode=l.LineCode,
                           BusLine=x.BusLine,
                           StationCode=_StationCode,
                           Start=l.Start,
                           Arrive=l.Start + dt- _Start,
                           Destination= dal.GetBusStation(x.LastStation).Name,
                           IsExist =true
                       };
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        public void DeleteLineTrip(int _LineCode, TimeSpan _Start)
        {
            LineTrip tmp = new LineTrip();
            try
            {
                dal.DeleteLineTrip(_LineCode,_Start);
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        public void UpdateLineTrip(BOLineTrip lt, TimeSpan NewStart) //lt=original
        {
            LineTrip tmp = new LineTrip();
            try
            {
                dal.UpdateLineTrip((LineTrip)Transform.trans(lt, tmp.GetType()),NewStart);
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }
        #endregion

        public TimeSpan DriveTimeToStation(int _LineCode, int _StationCode)
        {
            BOLine l = GetLine(_LineCode);
            List<BOLineStation> lst = l.Stations.ToList();
            TimeSpan t = TimeSpan.Zero;
            if(l.Stations.FirstOrDefault(x=>x.StationCode==_StationCode)!=null)
            {
                int i = 1;
                while(lst[i-1].StationCode!=_StationCode)
                {
                    t += lst[i].DriveTime;
                    i++;
                }
                return t;
            }
            else
                throw new BLException($"Line {_LineCode} does not cross at station {_StationCode}");
        } //return the time that take to the line to reach the asked station
        public IEnumerable<BOLine> SearchRoute(int _StationCode1, int _StationCode2) //return all the lines that cross this stations(in order)
        {
            try
            {
                IEnumerable<BOLine> lst = from x in dal.GetSpecificLineStations(x => x.StationCode == _StationCode1)
                                          from t in dal.GetSpecificLineStations(t => t.StationCode == _StationCode2 && t.LineCode==x.LineCode)
                                          where x.StationNumberInLine < t.StationNumberInLine
                                          select GetLine(x.LineCode);
                    return lst; //return even null
            }
            catch (DOException ex)
            {
                throw new BLException(ex.Message, ex);
            }
        }  
    }
}