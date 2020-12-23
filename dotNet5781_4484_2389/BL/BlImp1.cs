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
        public List<BOBus> GetSpecificBuses()//conditionnnn
        {
            List<Bus> bs;
            try
            {
                bs = dal.GetSpecificBuses();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            List<BOBus> bobs = (from Bus b in bs
                                select (BOBus)b).ToList();
            return bobs;
        }
        public List<BOBus> GetAllBuses()
        {
            List<Bus> b;
            try
            {
                b = dal.GetAllBuses();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            List<BOBus> bobs = (from Bus bb in b
                                select (BOBus)bb).ToList();
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

        public BOBusStation GetBusStation(int _StationCode)
        {
            //checking
            //add linestation
            BOBusStation bs;
            List<Line> ls;
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
            //bs.Lines = (from BOStationLine x in ls
            //            select x).ToList(); //work???
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
                //lines?
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public List<BOBusStation> GetSpecificBusStations()
        {
            List<BusStation> bs;
            try
            {
                bs = dal.GetSpecificBusStations();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            List<BOBusStation> bobs = (from BusStation b in bs
                                select (BOBusStation)b).ToList();
            return bobs;
        }
        public List<BOBusStation> GetAllBusStations()
        {
            List<BusStation> bs;
            try
            {
                bs = dal.GetAllBusStations();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
            List<BOBusStation> bobs = (from BusStation b in bs
                                       select (BOBusStation)b).ToList();
            return bobs;
        }
        public int AddBusStation(BOBusStation bs)
        {
            if(/*checking*/)
                throw new BLException($"Bus number {b.LicenseNumber} does not match the licensing date");
            try
            {
                dal.AddBusStation((BusStation)bs);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public void DeleteBusStation(int _StationCode)
        {
            //try
            //{
            //    dal.DeleteBusStation(_StationCode);
            //}
            //catch (DOException dex)
            //{
            //    throw new BLException(dex.Message);
            //}
        }

        public BOLine GetLine(int _Code)
        {
            BOLine l;
            List<BusStation> st;
            //List<LineStation> ls;
            ConsecutiveStations cs;
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
                        cs=dal.GetConsecutiveStations(st[i - 1].StationCode, s.StationCode);
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
        public void UpdateLine(BOLine l);
        public List<BOLine> GetStationLines(int _StationCode);
        public List<BOLine> GetAllLines();
        public List<BOLine> GetSpecificLines();
        public List<BOBusStation> GetStationsOfLine(int _LineCode);
        public int AddLine(BOLine l);
        public void DeleteLine(int _Code);
        public void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);
        public BOLineStation GetLineStation(int _LineCode, int _StationCode);
        public void DeleteLineStation(int _LineCode, int _StationCode);
        public void AddConsecutiveStations(int _StationCode1, int _StationCode2, double _Distance, DateTime _DriveTime, bool _Regional);
        //public ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);
        //public void UpdateConsecutiveStations(ConsecutiveStations cs);
    }
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