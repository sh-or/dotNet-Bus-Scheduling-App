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
            try
            {
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
            //more checking?
            if ((b.LicenseNumber > 9999999 && b.LicensingDate.Year < 2018) || (b.LicenseNumber < 10000000 && b.LicensingDate.Year >= 2018)) //license number and date don't match
                throw new BLException($"Bus number {b.LicenseNumber} does not match the licensing date");
            if (b.LicensingDate < b.DateOfLastCare)
                throw new BLException($"Invalid dates");
            if (b.Kilometerage < b.KmFromLastCare || b.Kilometerage < b.KmFromLastRefuel)
                throw new BLException($"Kilometerage cannot be less than KmFromLastCare or KmFromLastRefuel");
            //b.Kilometerage=Math.Max(b.Kilometerage,Math.Max(b.KmFromLastCare,b.KmFromLastRefuel));
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
            //checking
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
                    //b = bobs[i];
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
                //foreach (BOBusStation b in bobs)
                //{
                //    b.Lines = from l in dal.GetStationLines(b.StationCode)
                //              select new BOStationLine
                //              {
                //                  BusLine = l.BusLine,
                //                  Code = l.Code,
                //                  LastStation = l.LastStation,
                //                  IndexOfThisStation = dal.IsStationInLine(l.Code, b.StationCode)
                //              };
                //}
                return bobs;
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        public int AddBusStation(BOBusStation bs) //it was just build.adding with no lines. empty list created in UI.
        {
            //if (/*checking*/) //address and name not empty?
            //    throw new BLException($"Bus number {b.LicenseNumber} does not match the licensing date");
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
                //var lst = (from ls in dal.GetSpecificLineStations(x => x.StationCode == _StationCode)
                //          select ls.LineCode).ToList();
                //if (lst != null)
                //    foreach (var x in lst)
                //        DeleteStationInLine(GetLine(x), _StationCode);
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
                dal.DeleteBusStation(_StationCode);
                //if (ls != null)
                //    //foreach (LineStation x in ls)
                //    //    dal.DeleteLineStation(x.LineCode, x.StationCode);
                //    dal.DeleteStationLineStations(_StationCode);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        #endregion

        #region Line

        public BOLine GetLine(int _Code)  // use GetConsecutiveStations
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
                //BOLineStation s = new BOLineStation();
                //foreach (BOLineStation s in l.Stations)
                //{
                //    //s = l.Stations.ElementAt(i);
                //    if (i>0) //not first station
                //    {
                //        cs1 = dal.GetConsecutiveStations(st.ElementAt(i - 1).StationCode, s.StationCode);
                //        //l.Stations.ElementAt(i).Distance = cs1.Distance;
                //        //l.Stations.ElementAt(i).DriveTime = cs1.DriveTime;
                //        s.Distance = cs1.Distance;
                //        s.DriveTime = cs1.DriveTime;
                //        l.Stations[i] = s;
                //        //UpdateLineStation(s);
                //    }
                //    i++;
                //}
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

            //foreach (BusStation s in st)
            //{
            //    tmp.StationCode = s.StationCode;
            //    tmp.Name = s.Name;
            //    if(i==0) //first station
            //    {
            //        tmp.Distance = 0;
            //        tmp.DriveTime = TimeSpan.Zero;
            //    }
            //    else
            //    {
            //        try
            //        {
            //            cs=dal.GetConsecutiveStations(st.ElementAt(i-1).StationCode, s.StationCode);
            //            tmp.Distance = cs.Distance;
            //            tmp.DriveTime = cs.DriveTime;
            //        }
            //        catch (DOException dex)
            //        {
            //            throw new BLException(dex.Message, dex);
            //        }
            //    }
            //    l.Stations.ToList().Add(tmp);
            //    i++;
            //}
            return l;
        }
        public void UpdateLine(BOLine l) //not for updating station list
        {
            //if first/last station??? ->throw "delete/add station in its button"
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
                    {//if not exist ConsecutiveStations->creat ConsecutiveStations!
                        if (dal.isExistConsecutiveStations(l.Stations.ElementAt(location - 1).StationCode, l.Stations.ElementAt(location + 1).StationCode))
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
            try
            {
                BOLineStation ls = new BOLineStation();
                ls.StationCode = _StationCode;
                ls.Name = dal.GetBusStation(_StationCode).Name;
                List<BOLineStation> bols = l.Stations.ToList(); ///////index-1!!!!!!!!!!!!!!!!!!!!!!!!!!1111
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
                    if (dal.isExistConsecutiveStations(l.Stations.ElementAt(index - 1).StationCode, _StationCode))
                        AddConsecutiveStations(l.Stations.ElementAt(index - 1).StationCode, _StationCode);
                    ConsecutiveStations cs = dal.GetConsecutiveStations(l.Stations.ElementAt(index - 1).StationCode, _StationCode); //maybe get from new UI window
                    l.Stations.ElementAt(index).Distance = cs.Distance;
                    l.Stations.ElementAt(index).DriveTime = cs.DriveTime;
                    dal.AddLineStation(l.Code, _StationCode, index);
                    //UpdateLineStation(l.Stations.ElementAt(index));//change to update dal.UpdateLineStation!!creat do.linestation..
                }
                if (index != l.Stations.Count() - 1) //not last station
                {
                    if (dal.isExistConsecutiveStations(_StationCode, l.Stations.ElementAt(index + 1).StationCode))
                        AddConsecutiveStations(_StationCode, l.Stations.ElementAt(index + 1).StationCode);
                    ConsecutiveStations cs = dal.GetConsecutiveStations(_StationCode, l.Stations.ElementAt(index + 1).StationCode); //maybe get from new UI window
                    l.Stations.ElementAt(index + 1).Distance = cs.Distance;
                    l.Stations.ElementAt(index + 1).DriveTime = cs.DriveTime;
                    //UpdateLineStation(l.Stations.ElementAt(index + 1)); //change to update dal.UpdateLineStation!!creat do.linestation..
                    foreach (LineStation x in dal.GetAllLineStations(l.Code)) //change the index of later stations in l
                    {
                        if (x.StationNumberInLine > index)
                            dal.UpdateLineStation(l.Code, x.StationCode, 1);
                    }
                }

            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        //public IEnumerable<BOLine> GetStationLines(int _StationCode){}
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
        //                               select (BOBusStation)Transform.trans(b,tmp.GetType()); //חוקי? זה בלי הרשימת קווים
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
                    //יצירת קונסקיוטיב ואז שליחה להוספה בדאל
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
                    dal.DeleteLineStation(_Code, x.StationCode);
                //foreach (LineStation x in ls)
                //    dal.DeleteLineStation(x.LineCode, x.StationCode);
                dal.DeleteLine(_Code);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        //public void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);  // AddStationInLine

        #endregion

        #region LineStation

        //public void UpdateLineStation(BOLineStation ls) //add stationinline
        //{

        //}
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
        //public void UpdateConsecutiveStations(ConsecutiveStations cs);
        #endregion

        #region Drive
        public void AddDrive(int _StartStation, int _DestinationStation)
        {
            BODrive drive = new BODrive();
            try
            {
                drive.StartStation = _StartStation;
                drive.DestinationStation = _DestinationStation;
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message, dex);
            }
        }
        #endregion
    }
}