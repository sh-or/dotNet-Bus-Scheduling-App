using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlAPI;
using DLAPI;
using BO;
using DO;
//specific- conditions?!

namespace BL
{
    public class BlImp1 : IBL
    {
        readonly IDAL dal = DalFactory.GetDal();

        #region Bus
        public BOBus GetBus(int _LicenseNumber)
        {
            BOBus b;
            try
            {
                b = (BOBus)dal.GetBus(_LicenseNumber);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            return b;
        }
        public void UpdateBus(BOBus b)
        {
            try
            {
                dal.UpdateBus(b);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public IEnumerable<BOBus> GetSpecificBuses(Predicate<BOBus> p)//conditionnnn
        {
            IEnumerable<Bus> bs;
            try
            {
                bs = dal.GetSpecificBuses((Predicate <Bus>) p);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            IEnumerable<BOBus> bobs = (from Bus b in bs
                                select (BOBus)b);
            return bobs;
        }
        public IEnumerable<BOBus> GetAllBuses()
        {
            IEnumerable<Bus> b;
            try
            {
                b = dal.GetAllBuses();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            IEnumerable<BOBus> bobs = (from Bus bb in b
                                select (BOBus)bb);
            return bobs;
        }
        public void AddBus(BOBus b)
        {
            //more checking?
            if ((b.LicenseNumber > 9999999 && b.LicensingDate.Year < 2018) || (b.LicenseNumber < 10000000 && b.LicensingDate.Year >= 2018)) //license number and date don't match
                throw new BLException($"Bus number {b.LicenseNumber} does not match the licensing date");
            try
            {
                dal.AddBus((Bus)b);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
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
                throw new BLException(dex.Message);
            }
        }
        #endregion

        #region BusStation

        public BOBusStation GetBusStation(int _StationCode)
        {
            //checking
            BOBusStation bs;
            IEnumerable<Line> ls;
            BOStationLine tmp=new BOStationLine();
            try
            {
                bs = (BOBusStation)dal.GetBusStation(_StationCode);
                ls = dal.GetStationLines(_StationCode);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }

            foreach (Line l in ls)
            {
                tmp.BusLine = l.BusLine;
                tmp.Code = l.Code;
                tmp.LastStation = l.LastStation;
                bs.Lines.Add(tmp);
            }
            return bs;
        }
        public void UpdateStation(BOBusStation bs)
        {
            try
            {
                dal.UpdateStation(bs);
                //lines will be updated by lines
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
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
                bobs = (from BusStation b in bs
                                           select (BOBusStation)b);
                foreach (BOBusStation b in bs)
                {
                    var sl = dal.GetStationLines(b.StationCode);
                    foreach (Line l in sl)
                    {
                        tmp.BusLine = l.BusLine;
                        tmp.Code = l.Code;
                        tmp.LastStation = l.LastStation;
                        b.Lines.Add(tmp);
                    }
                }
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            return bobs;
        }
        public IEnumerable<BOBusStation> GetAllBusStations()   
        {
            IEnumerable<BusStation> bs;
            IEnumerable<BOBusStation> bobs;
            BOStationLine tmp = new BOStationLine();
            try
            {
                bs = dal.GetAllBusStations();
                bobs = (from BusStation b in bs
                        select (BOBusStation)b);
                foreach (BOBusStation b in bs)
                {
                    var sl = dal.GetStationLines(b.StationCode);
                    foreach (Line l in sl)
                    {
                        tmp.BusLine = l.BusLine;
                        tmp.Code = l.Code;
                        tmp.LastStation = l.LastStation;
                        b.Lines.Add(tmp);
                    }
                }
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            return bobs;
        }
        public int AddBusStation(BOBusStation bs) //it was just build.adding with no lines. empty list created in UI.
        {
            //if (/*checking*/) //address and name not empty?
            //    throw new BLException($"Bus number {b.LicenseNumber} does not match the licensing date");
            try
            {
                int runNumber=dal.AddBusStation((BusStation)bs);
                bs.StationCode = runNumber;
                return runNumber;
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public void DeleteBusStation(int _StationCode)
        {
            try
            {
                IEnumerable<LineStation> ls= dal.GetSpecificLineStations(x=>x.StationCode==_StationCode);
                if (ls != null)
                    foreach (LineStation x in ls)
                        dal.DeleteLineStation(x.LineCode, x.StationCode);
                dal.DeleteBusStation(_StationCode);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        #endregion

        #region Line

        public BOLine GetLine(int _Code)  // use GetConsecutiveStations
        {
            BOLine l;
            IEnumerable<BusStation> st;
            BOConsecutiveStations cs;
            int i = 0;
            BOLineStation tmp = new BOLineStation();
            try
            {
                l = (BOLine)dal.GetLine(_Code);
                st = dal.GetStationsOfLine(_Code);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            foreach (BusStation s in st)
            {
                tmp.StationCode = s.StationCode;
                tmp.Name = s.Name;
                if(i==0) //first station
                {
                    tmp.Distance = 0;
                    tmp.DriveTime = TimeSpan.Zero;
                }
                else
                {
                    try
                    {
                        cs=dal.GetConsecutiveStations(st[i-1].StationCode, s.StationCode);
                        tmp.Distance = cs.Distance;
                        tmp.DriveTime = cs.DriveTime;
                    }
                    catch (DOException dex)
                    {
                        throw new BLException(dex.Message);
                    }
                }
                l.Stations.Add(tmp);
                i++;
            }
            return l;
        }
        public void UpdateLine(BOLine l) //not for updating station list
        {
            try
            {
                dal.UpdateLine(l);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public void DeleteStationInLine(BOLine l, int _StationCode)
        {
            BOLineStation ls = l.Stations.Find(x => x.StationCode == _StationCode);  //UI catch ex
            if (l.Stations.Count<3)
                throw new BLException("Line cannot be with less than 2 stations");

            if (!l.Stations.Remove(ls))
                throw new BLException($"Station number {_StationCode} was not found");
            int location;
            try
            {
                location = dal.GetLineStation(l.Code, _StationCode).StationNumberInLine;
                if (location<l.Stations.Count/*-1*/) //not last station
                { //update time&distance
                    if(location==0)
                    {
                        l.Stations[0].Distance = 0;
                        l.Stations[0].DriveTime = TimeSpan.Zero;
                    }
                    else
                    {//if not exist ConsecutiveStations->new window for insert data and creat ConsecutiveStations!
                        BOConsecutiveStations cs = dal.GetConsecutiveStations(l.Stations[location - 1].StationCode, l.Stations[location].StationCode);
                        l.Stations[location].Distance = cs.Distance;
                        l.Stations[location].DriveTime = cs.DriveTime;
                    }
                }
                dal.DeleteLineStation(l.Code, _StationCode);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public void AddStationInLine(BOLine l, int _StationCode, int index)
        {
            try
            {
                BOLineStation ls = new BOLineStation();
                ls.StationCode = _StationCode;
                ls.Name = dal.GetBusStation(_StationCode).Name;
                l.Stations.Insert(index, ls); 

                if (index== 0)
                {
                    l.FirstStation = _StationCode;
                    UpdateLine(l);
                    l.Stations[index].Distance = 0;
                    l.Stations[index].DriveTime = TimeSpan.Zero;
                    UpdateLineStation(l.Stations[index]);//change to update dal.UpdateLineStation!!creat do.linestation..
                }
                else  //not first station
                { 
                    if(index==l.Stations.Count-1) //the last station
                    {
                        l.LastStation = _StationCode;
                        UpdateLine(l);
                    }
                    BOConsecutiveStations cs = dal.GetConsecutiveStations(l.Stations[index - 1].StationCode, _StationCode); //maybe get from new UI window
                    l.Stations[index].Distance = cs.Distance;
                    l.Stations[index].DriveTime = cs.DriveTime;
                    UpdateLineStation(l.Stations[index]);//change to update dal.UpdateLineStation!!creat do.linestation..
                }
                if (index != l.Stations.Count - 1) //not last station
                {
                    BOConsecutiveStations cs = dal.GetConsecutiveStations( _StationCode, l.Stations[index +1].StationCode); //maybe get from new UI window
                    l.Stations[index+ 1].Distance = cs.Distance;
                    l.Stations[index + 1].DriveTime = cs.DriveTime;
                    UpdateLineStation(l.Stations[index + 1]); //change to update dal.UpdateLineStation!!creat do.linestation..
                }

            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
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
                throw new BLException(dex.Message);
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
                throw new BLException(dex.Message);
            }
            IEnumerable<BOLine> bol = (from Line ll in l
                                select GetLine(ll.Code));
            return bol;
        }
        public IEnumerable<BOBusStation> GetStationsOfLine(int _LineCode) 
        {
            IEnumerable<BusStation> bs;
            try
            {
                 bs = dal.GetStationsOfLine(_LineCode);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            IEnumerable<BOBusStation> bobs = from BusStation b in bs
                                       select (BOBusStation)b; //חוקי? זה בלי הרשימת קווים
            return bobs;
        }
        public int AddLine(BOLine l)
        {
            if (l.FirstStation == 0 || l.LastStation == 0)
                throw new BLException("Cannot add new line without first&last station");
            try
            {
                l.Stations = new List<BOLineStation>();
                BOLineStation first = new BOLineStation() { StationCode = l.FirstStation, Distance=0, DriveTime=TimeSpan.Zero};
                first.Name = dal.GetBusStation(l.FirstStation).Name;
                l.Stations.Add(first);
                BOLineStation last = new BOLineStation() { StationCode = l.LastStation };
                last.Name = dal.GetBusStation(l.LastStation).Name;
                ConsecutiveStations cs;
                if (!dal.isExistConsecutiveStations(l.FirstStation, l.LastStation))
                {
                    //יצירת קונסקיוטיב ואז שליחה להוספה בדאל
                    dal.AddConsecutiveStations(l.FirstStation, l.LastStation);
                    //cs = dal.
                    last.Distance = cs.Distance;
                    last.DriveTime = cs.DriveTime;
                }
                else
                {
                    cs = dal.GetConsecutiveStations(l.FirstStation, l.LastStation);
                    last.Distance = cs.Distance;
                    last.DriveTime = cs.DriveTime;
                }
                l.Stations.Add(last);
                l.Code = dal.AddLine((Line)l);
                return l.Code;
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public void DeleteLine(int _Code) 
        {
            try
            {
                IEnumerable<LineStation> ls = dal.GetSpecificLineStations(x => x.LineCode == _Code); 
                if (ls != null)
                    foreach (LineStation x in ls)
                        dal.DeleteLineStation(x.LineCode, x.StationCode);
                dal.DeleteLine(_Code);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        //public void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);  // AddStationInLine

        #endregion

        #region LineStation
       
        public void UpdateLineStation(BOLineStation ls) //add stationinline
        {

        }
        #endregion

        #region ConsecutiveStations

        public void AddConsecutiveStations(int _StationCode1, int _StationCode2, double _Distance, DateTime _DriveTime /*bool _Regional*/);
        
        //public ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);
        //public void UpdateConsecutiveStations(ConsecutiveStations cs);
    }
    #endregion
}



//        static Random rnd = new Random(DateTime.Now.Millisecond);

//        readonly IDAL dal = DalFactory.GetDal();

//        public Weather GetWeather(int day)
//        {
//            Weather w = new Weather();
//            double feeling;
//            WindDirections dir;


//            feeling = dal.GetTemparture(day);
//            dir = dal.GetWindDirection(day).direction;

//            switch (dir)
//            {
//                case WindDirections.S:
//                    feeling += 2;
//                    break;
//                case WindDirections.SSE:
//                    feeling += 1.5;
//                    break;
//                case WindDirections.SE:
//                    feeling += 1;
//                    break;
//                case WindDirections.SEE:
//                    feeling += 0.5;
//                    break;
//                case WindDirections.E:
//                    feeling -= 0.5;
//                    break;
//                case WindDirections.NEE:
//                    feeling -= 1;
//                    break;
//                case WindDirections.NE:
//                    feeling -= 1.5;
//                    break;
//                case WindDirections.NNE:
//                    feeling -= 2;
//                    break;
//                case WindDirections.N:
//                    feeling -= 3;
//                    break;
//                case WindDirections.NNW:
//                    feeling -= 2.5;
//                    break;
//                case WindDirections.NW:
//                    feeling -= 2;
//                    break;
//                case WindDirections.NWW:
//                    feeling -= 1.5;
//                    break;
//                case WindDirections.W:
//                    feeling -= 1;
//                    break;
//                case WindDirections.SWW:
//                    feeling -= 0;
//                    break;
//                case WindDirections.SW:
//                    break;
//                case WindDirections.SSW:
//                    feeling += 1;
//                    break;
//            }
//            w.Feeling = (int)feeling;
//            return w;
//        }


//    }
//}