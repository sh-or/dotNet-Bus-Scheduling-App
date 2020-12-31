using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;

//clone!!!

namespace DL
{
    public class DalObject  : IDAL
    {
        #region singelton
        class Nested
        {
            static Nested() { }
            internal static readonly IDAL instance = new DalObject();
        }
        static DalObject() { }
        DalObject() { }
        public static IDAL Instance { get { return Nested.instance; } }
        #endregion
       public Bus GetBus(int _LicenseNumber)
        {
            Bus b = DataSource.AllBuses.Find(x => x.LicenseNumber == _LicenseNumber);
            if(b!=null)
                return b.Clone();
            throw new DOException(_LicenseNumber, $"Bus number {_LicenseNumber} was not found");
        }
        public void UpdateBus(Bus b)  //fix?
        {
           // DataSource.AllBuses.Remove(GetBus(b.LicenseNumber));
            int n = DataSource.AllBuses.FindIndex(x => x.LicenseNumber == b.LicenseNumber);
            DataSource.AllBuses[n] = b.Clone();
            //DataSource.AllBuses.Add(b.Clone());
        }
        public IEnumerable<Bus> GetSpecificBuses(Predicate<Bus> p) 
        {
            var ListBS = (from Bus b in DataSource.AllBuses
                               where p(b)
                               select b.Clone());
            if(ListBS!=null)
                return ListBS;
            throw new DOException("No exist buses were found");
        }
        public IEnumerable<Bus> GetAllBuses() 
        {
            var ListBS = (from Bus b in DataSource.AllBuses
                         select b.Clone());
            if (ListBS != null)
                return ListBS;
            throw new DOException("No buses were found");
        }
        public void AddBus(Bus b /*int _LicenseNumber, DateTime _LicensingDate, double _Kilometerage, double _Fuel, StatusEnum _Status, string _Driver*/)
        {  
            if (DataSource.AllBuses.Exists(x => x.LicenseNumber == b.LicenseNumber))
                throw new DOException(b.LicenseNumber, $"Bus number {b.LicenseNumber} is already exist");
            DataSource.AllBuses.Add(b.Clone());
            //Bus b = new Bus();
            //b.LicenseNumber = _LicenseNumber;          
            //b.IsExist = true;
            //b.LicensingDate = _LicensingDate;
            //b.Kilometerage = _Kilometerage;
            //b.Fuel = _Fuel;
            //b.Status = _Status;
            //b.Driver = _Driver;
            //DataSource.AllBuses.Add(b);
            //return b.LicenseNumber;
        }
        public void DeleteBus(int _LicenseNumber)
        {
            Bus b = GetBus(_LicenseNumber);
            int n = DataSource.AllBuses.FindIndex(x => x.LicenseNumber == b.LicenseNumber);
            DataSource.AllBuses[n].IsExist = false;
            //b.IsExist = false;
            //AddBus(b.Clone());
        }
        public BusStation GetBusStation(int _StationCode) 
        {
            BusStation bs = DataSource.AllBusStations.Find(x => x.StationCode == _StationCode);
            if (bs!=null)
                return bs.Clone();
            throw new DOException(_StationCode, $"Bus station number {_StationCode} was not found");
        }
        public void UpdateStation(BusStation bs)
        {
            DataSource.AllBusStations.Remove(GetBusStation(bs.StationCode));
            DataSource.AllBusStations.Add(bs.Clone());
        }
        public IEnumerable<BusStation> GetSpecificBusStations(Predicate<BusStation> p) 
        {
            var ListBS = (from BusStation bs in DataSource.AllBusStations
                         where p(bs)
                         select bs.Clone());
            if (ListBS != null)
                return ListBS;
            throw new DOException("No exist bus stations were found");
        }
        public IEnumerable<BusStation> GetAllBusStations()
        {
            var ListBS = (from BusStation bs in DataSource.AllBusStations
                         select bs.Clone());
            if (ListBS != null)
                return ListBS;
            throw new DOException("No bus stations were found");
        }
        public int AddBusStation(BusStation bs ) 
        {
            DataSource.AllBusStations.Add(bs.Clone());
            bs.StationCode = ConfigurationClass.StationCode;
            return bs.StationCode;
            //BusStation bs = new BusStation();
            //bs.StationCode = ConfigurationClass.StationCode;
            //bs.IsExist = true;
            //bs.Latitude = _Latitude;
            //bs.Longitude = _Longitude;
            //bs.Name = _Name;
            //bs.Address = _Address;
            //bs.Accessibility = _Accessibility;
            //DataSource.AllBusStations.Add(bs);
            //return bs.StationCode;
        }
        public void DeleteBusStation(int _StationCode) //delete line-stations
        {
            BusStation bs = GetBusStation(_StationCode);
            //DataSource.AllBusStations.Remove(bs);
            bs.IsExist = false;
            //AddBusStation(bs.Latitude, bs.Longitude, bs.Name, bs.Address, bs.Accessibility);
        }
        public Line GetLine(int _Code)
        {
            Line l = DataSource.AllLines.Find(x => x.Code == _Code);
            if(l!=null)
                return l.Clone();
            throw new DOException(_Code, $"Line number {_Code} was not found");
        }
        public void UpdateLine(Line l) 
        {
            DataSource.AllLines.Remove(GetLine(l.Code));
            DataSource.AllLines.Add(l.Clone());
        }
        public IEnumerable<Line> GetStationLines(int _StationCode) // all the lines which cross in this station
        {
           return from ls in DataSource.AllLineStations.FindAll(x => x.StationCode == _StationCode)
                  where GetLine(ls.LineCode).IsExist
                  select GetLine(ls.LineCode);
        }

        public IEnumerable<Line> GetAllLines() 
        {
            var Listl = (from Line l in DataSource.AllLines
                        select l.Clone());
            if (Listl != null)
                return Listl;
            throw new DOException("No lines were found");
        }
        public IEnumerable<Line> GetSpecificLines(Predicate<Line> p)
        {
            var Listl = (from Line l in DataSource.AllLines
                        where p(l)
                        select l.Clone());
            if (Listl != null)
                return Listl;
            throw new DOException("No exist lines were found");
        }
        public IEnumerable<BusStation> GetStationsOfLine(int _LineCode)
        {
            IEnumerable<LineStation> lsLst = DataSource.AllLineStations.FindAll(x => x.LineCode == _LineCode);
            IEnumerable<BusStation> bsLst = (from ls in lsLst
                                             orderby ls.StationNumberInLine
                                             select GetBusStation(ls.StationCode).Clone());
            if (bsLst != null)
                return bsLst;
            throw new DOException("No line stations were found");

        }
        public int AddLine( Line l ) 
        {
            l.Code = ConfigurationClass.LineCode;
            //Line bl = new Line();
            //bl.Code = ConfigurationClass.LineCode;
            //bl.IsExist = true;
            //bl.BusLine = _BusLine;
            //bl.Area = _Area;
            //bl.FirstStation = _FirstStation;
            //bl.LastStation = _LastStation;
            DataSource.AllLines.Add(l.Clone());
            return l.Code;
        }

        public void DeleteLine(int _Code) //include deleting line-stations 
        {
            IEnumerable<LineStation> lls = DataSource.AllLineStations.FindAll(x => x.LineCode == _Code);
            foreach (LineStation ls in lls)
                DeleteLineStation(ls.LineCode, ls.StationCode);
            Line bl = GetLine(_Code);
            //DataSource.AllBusStations.Remove(bs);
            bl.IsExist = false;
            //AddBusStation(bs.Latitude, bs.Longitude, bs.Name, bs.Address, bs.Accessibility);

        }
        public void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine) //gets linestation???
        {
            LineStation ls = new LineStation();
            ls.StationCode = _StationCode;
            ls.LineCode = _LineCode;
            ls.StationNumberInLine = _StationNumberInLine;
            DataSource.AllLineStations.Add(ls);
        }
        public LineStation GetLineStation(int _LineCode, int _StationCode) 
        {
            LineStation ls = DataSource.AllLineStations.Find(x => x.LineCode == _LineCode && x.StationCode == _StationCode);
            if (ls != null)
                return ls.Clone();
            throw new DOException($"Line number {_LineCode} does not cross in station {_StationCode}");
        }
        public int IsStationInLine(int _LineCode, int _StationCode) //check if exist specific line station and return the station location in the line or -1
        {
            if (! DataSource.AllLineStations.Exists(x => x.LineCode == _LineCode && x.StationCode == _StationCode))
                return -1;
            LineStation ls = GetLineStation(_LineCode, _StationCode);
            return ls.StationNumberInLine;
        }
        public IEnumerable<LineStation> GetSpecificLineStations(Predicate<LineStation> p)
        {
            IEnumerable<LineStation> Listl = (from LineStation l in DataSource.AllLineStations
                         where p(l)
                         select l.Clone());
            //if (Listl != null)
            return Listl;
            //throw new DOException("No exist lines were found");
            //return spesific collection OR NULL!!!!!!!!!!!!!!!!!!
        }
        public void DeleteLineStation(int _LineCode, int _StationCode)
        {
            LineStation ls = GetLineStation(_LineCode, _StationCode);
            DataSource.AllLineStations.Remove(ls);
        }
        //public void AddConsecutiveStations(int _StationCode1, int _StationCode2) 
        //{
        //    ConsecutiveStations cs = new ConsecutiveStations();
        //    cs.StationCode1 = _StationCode1;
        //    cs.StationCode2 = _StationCode2;
        //    BusStation b1 = GetBusStation(_StationCode1); //also check if exist..
        //    BusStation b2 = GetBusStation(_StationCode2);//
        //    GeoCoordinate loc1 = new GeoCoordinate(b1.Latitude, b1.Longitude);
        //    GeoCoordinate loc2 = new GeoCoordinate(b2.Latitude, b2.Longitude);
        //    cs.Distance = loc1.GetDistanceTo(loc2) * (1 + r.NextDouble() / 2); //air-distance(in meters)*(1 to 1.5)
        //    cs.DriveTime = TimeSpan.FromSeconds(cs.Distance / (r.Next(50, 70) * 1 / 3.6)); //the bus cross 50-70 KmH
        //    DataSource.AllConsecutiveStations.Add(cs);
        //}
        public void AddConsecutiveStations(ConsecutiveStations cs)
        {
            if(! isExistConsecutiveStations(cs.StationCode1,cs.StationCode2))
                DataSource.AllConsecutiveStations.ToList().Add(cs);
        }
        public ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2)
        {
            ConsecutiveStations cs = DataSource.AllConsecutiveStations.ToList().Find(x => x.StationCode1 == _StationCode1 && x.StationCode2 == _StationCode1);
            if (cs != null)
                return cs.Clone();
            throw new DOException($"Station {_StationCode1} and station {_StationCode2} are not consecutive stations");
        }
        public void UpdateConsecutiveStations(ConsecutiveStations cs) //for what??
        {
            DataSource.AllConsecutiveStations.ToList().Remove(GetConsecutiveStations(cs.StationCode1, cs.StationCode2));
            DataSource.AllConsecutiveStations.ToList().Add(cs.Clone());
        }
        public bool isExistConsecutiveStations(int _FirstStation, int _LastStation)
        {
            return DataSource.AllConsecutiveStations.ToList().Exists(x => x.StationCode1 == _FirstStation && x.StationCode2 == _LastStation);
        }

        //static Random rnd = new Random(DateTime.Now.Millisecond);
        //double temperature;

        //public double GetTemparture(int day)
        //{
        //    temperature = rnd.NextDouble() * 50 - 10;
        //    temperature += rnd.NextDouble() * 10 - 5;
        //    return temperature;
        //}

        //public WindDirection GetWindDirection(int day)
        //{
        //    WindDirection direction = DataSource.directions.Find(d => true);
        //    var directions = (WindDirections[])Enum.GetValues(typeof(WindDirections));
        //    direction.direction = directions[rnd.Next(0, directions.Length)];

        //    return direction.Clone();
        //}
    }
}