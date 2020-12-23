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
        public void UpdateBus(Bus b) ///////////////
        {
            DataSource.AllBuses.Remove(GetBus(b.LicenseNumber));
            DataSource.AllBuses.Add(b.Clone());
        }
        public List<Bus> GetSpecificBuses() 
        {
            var ListBS = (from Bus b in DataSource.AllBuses
                               where b.IsExist
                               select b.Clone()).ToList();
            if(ListBS!=null)
                return ListBS;
            throw new DOException("No exist buses were found");
        }
        public List<Bus> GetAllBuses() 
        {
            var ListBS = (from Bus b in DataSource.AllBuses
                         select b.Clone()).ToList();
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
        public void DeleteBus(int _LicenseNumber) //how to update the DS??
        {
            Bus b = GetBus(_LicenseNumber);
            //DataSource.AllBuses.Remove(b);
            b.IsExist = false;
            //AddBus(b.LicensingDate, b.Kilometerage, b.Fuel, b.Status, b.Driver);
        }
        public BusStation GetBusStation(int _StationCode) 
        {
            BusStation bs = DataSource.AllBusStations.Find(x => x.StationCode == _StationCode);
            if (bs!=null)
                return bs.Clone();
            throw new DOException(_StationCode, $"Bus station number {_StationCode} was not found");
        }
        public void UpdateStation(BusStation bs) //////////
        {
            DataSource.AllBusStations.Remove(GetBusStation(bs.StationCode));
            DataSource.AllBusStations.Add(bs.Clone());
        }
        public List<BusStation> GetSpecificBusStations() 
        {
            var ListBS = (from BusStation bs in DataSource.AllBusStations
                         where bs.IsExist
                         select bs.Clone()).ToList();
            if (ListBS != null)
                return ListBS;
            throw new DOException("No exist bus stations were found");
        }
        public List<BusStation> GetAllBusStations()
        {
            var ListBS = (from BusStation bs in DataSource.AllBusStations
                         select bs.Clone()).ToList();
            if (ListBS != null)
                return ListBS;
            throw new DOException("No bus stations were found");
        }
        public int AddBusStation(BusStation bs /*double _Latitude, double _Longitude, string _Name, string _Address, bool _Accessibility*/) 
        {
            DataSource.AllBusStations.Add(bs.Clone());
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
        public void UpdateLine(Line l) ////////////
        {
            DataSource.AllLines.Remove(GetLine(l.Code));
            DataSource.AllLines.Add(l.Clone());
        }
        public List<Line> GetStationLines(int _StationCode) // all the lines which cross in this station
        {
            List<LineStation> lsLst = DataSource.AllLineStations.FindAll(x => x.StationCode == _StationCode);
            List<Line> bsLst = new List<Line>();
            List <Line> exLines= GetSpecificLines();
            foreach (Line ln in exLines)
                bsLst.Add(exLines.Find(x => x.Code == ln.Code));
            return bsLst.Clone(); 
        }
        public List<Line> GetAllLines() 
        {
            var Listl = (from Line l in DataSource.AllLines
                        select l.Clone()).ToList();
            if (Listl != null)
                return Listl;
            throw new DOException("No lines were found");
        }
        public List<Line> GetSpecificLines()
        {
            var Listl = (from Line l in DataSource.AllLines
                        where l.IsExist
                        select l.Clone()).ToList();
            if (Listl != null)
                return Listl;
            throw new DOException("No exist lines were found");
        }
        public List<BusStation> GetStationsOfLine(int _LineCode)
        {
            List <LineStation> lsLst = DataSource.AllLineStations.FindAll(x => x.LineCode == _LineCode);
            List<BusStation> bsLst = new List<BusStation>();
            foreach (LineStation ls in lsLst)
            {
                //if(ls.flag)
                bsLst.Add(GetBusStation(ls.StationCode)); //*need to check if deleted?
                //else throw NotFoundEx
            }
            //bsLst.sortByStationNumberInLine(); //sort according to the line's rout
            return bsLst.Clone();
        }
        //private void sortByStationNumberInLine() //or lemammesh Icompareable on BusStation
        //{
        //will be in LINQ!:)
        //}
        public int AddLine(int _BusLine, AreaEnum _Area, int _FirstStation, int _LastStation) 
        {
            Line bl = new Line();
            bl.Code = ConfigurationClass.LineCode;
            bl.IsExist = true;
            bl.BusLine = _BusLine;
            bl.Area = _Area;
            bl.FirstStation = _FirstStation;
            bl.LastStation = _LastStation;
            DataSource.AllLines.Add(bl);
            return bl.Code;
        }

     //לבדוק שזה טוב
        public void DeleteLine(int _Code) //delete line-stations
        {
            List<LineStation> lls = DataSource.AllLineStations.FindAll(x => x.LineCode == _Code);
            foreach (LineStation ls in lls)
                DeleteLineStation(ls.LineCode, ls.StationCode);
            Line bl = GetLine(_Code);
            //DataSource.AllBusStations.Remove(bs);
            bl.IsExist = false;
            //AddBusStation(bs.Latitude, bs.Longitude, bs.Name, bs.Address, bs.Accessibility);

        }
        public void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine) 
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
        public void DeleteLineStation(int _LineCode, int _StationCode)
        {
            LineStation ls = GetLineStation(_LineCode, _StationCode);
            DataSource.AllLineStations.Remove(ls);
        }
        public void AddConsecutiveStations(int _StationCode1, int _StationCode2, double _Distance, TimeSpan _DriveTime, bool _Regional) 
        {
            ConsecutiveStations cs = new ConsecutiveStations();
            cs.StationCode1 = _StationCode1;
            cs.StationCode2 = _StationCode2;
            cs.Distance = _Distance;
            cs.DriveTime = _DriveTime;
            cs.Regional = _Regional;
            DataSource.AllConsecutiveStations.Add(cs);
        }
        public ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2)
        {
            ConsecutiveStations cs = DataSource.AllConsecutiveStations.Find(x => x.StationCode1 == _StationCode1 && x.StationCode2 == _StationCode1);
            if (cs != null)
                return cs.Clone();
            throw new DOException($"Station {_StationCode1} and station {_StationCode2} are not consecutive stations");
        }
        public void UpdateConsecutiveStations(ConsecutiveStations cs)
        {
            DataSource.AllConsecutiveStations.Remove(GetConsecutiveStations(cs.StationCode1, cs.StationCode2));
            DataSource.AllConsecutiveStations.Add(cs.Clone());
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