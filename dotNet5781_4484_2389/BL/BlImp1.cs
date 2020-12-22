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
            BOBus b = new BOBus();
            try
            {
                b.bus = dal.GetBus(_LicenseNumber);
            }
            catch(DOException dex)
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
        public List<Bus> GetSpecificBuses()//conditionnnn
        {
            try
            {
                return dal.GetSpecificBuses();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public List<Bus> GetAllBuses()
        {
            try
            {
                return dal.GetAllBuses();
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public int AddBus(BOBus b)
        {

            if ((b.LicenseNumber > 9999999 && b.LicensingDate.Year < 2018) || (b.LicenseNumber < 10000000 && b.LicensingDate.Year >= 2018)) //license number and date don't match
                throw new BLException($"Bus number {b.LicenseNumber} does not match the licensing date");
            try
            {
                dal.AddBus(b);
            }
            catch (DOException dex)
            {
                throw new BLException(dex.Message);
            }
        }
        public void DeleteBus(int _LicenseNumber);
        public BusStation GetBusStation(int _StationCode);
        public void UpdateStation(BusStation bs);
        public List<BusStation> GetSpecificBusStations();
        public List<BusStation> GetAllBusStations();
        public int AddBusStation(BusStation bs);
        public void DeleteBusStation(int _StationCode);
        public Line GetLine(int _Code);
        public void UpdateLine(Line l);
        public List<Line> GetStationLines(int _StationCode);
        public List<Line> GetAllLines();
        public List<Line> GetSpecificLines();
        public List<BusStation> GetStationsOfLine(int _LineCode);
        public int AddLine(Line l);
        public void DeleteLine(int _Code);
        public DO.ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);
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