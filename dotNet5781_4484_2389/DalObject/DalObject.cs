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
            return DataSource.AllBuses.Find(x => x.LicenseNumber == _LicenseNumber).Clone();
        }
        public List<Bus> GetExistBuses()
        {
            return DataSource.AllBuses.FindAll(x => x.IsExist).Clone();
        }
        public List<Bus> GetAllBuses() 
        {
            return DataSource.AllBuses.Clone();
        }
        public void AddBus(DateTime _LicensingDate, double _Kilometerage, double _Fuel, StatusEnum _Status, string _Driver)
        {
            Bus b = new Bus();
            b.LicenseNumber = ConfigurationClass.LicenseNum;
            b.IsExist = true;
            b.LicensingDate = _LicensingDate; //checking if exist?
            b.Kilometerage = _Kilometerage;
            b.Fuel = _Fuel;
            b.Status = _Status;
            b.Driver = _Driver;
            DataSource.AllBuses.Add(b);
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
            return DataSource.AllBusStations.Find(x => x.StationCode == _StationCode).Clone();
        }
        public List<BusStation> GetExistBusStations() 
        {
            return DataSource.AllBusStations.FindAll(x => x.IsExist).Clone();
        }
        public List<BusStation> GetAllBusStations() 
        {
            return DataSource.AllBusStations.Clone();
        }
        public void AddBusStation(double _Latitude, double _Longitude, string _Name, string _Address, bool _Accessibility) 
        {
            BusStation bs = new BusStation();
            bs.StationCode = ConfigurationClass.StationCode;
            bs.IsExist = true;
            bs.Latitude = _Latitude;
            bs.Longitude = _Longitude;
            bs.Name = _Name;
            bs.Address = _Address;
            bs.Accessibility = _Accessibility;
            DataSource.AllBusStations.Add(bs);
        }
        public void DeleteBusStation(int _StationCode) //how to update the DS??
        {
            BusStation bs = GetBusStation(_StationCode);
            //DataSource.AllBusStations.Remove(bs);
            bs.IsExist = false;
            //AddBusStation(bs.Latitude, bs.Longitude, bs.Name, bs.Address, bs.Accessibility);
        }
        public Line GetLine(int _Code)
        {
            return DataSource.AllLines.Find(x => x.Code==_Code).Clone();
        }
        public List<Line> GetStationLines(int _StationCode) // all the lines which cross in this station
        {
            List<LineStation> lsLst = DataSource.AllLineStations.FindAll(x => x.StationCode == _StationCode);
            List<Line> bsLst = new List<Line>();
            List <Line> exLines= GetExistLines();
            foreach (Line ln in exLines)
                bsLst.Add(exLines.Find(x => x.Code == ln.Code));
            return bsLst.Clone(); //Clone??
        }
        public List<Line> GetAllLines() 
        {
            return DataSource.AllLines.Clone();
        }
        public List<Line> GetExistLines()
        {
            return DataSource.AllLines.FindAll(x => x.IsExist).Clone();
        }
        public List<BusStation> GetStationsOfLine(int _LineCode)
        {
            List <LineStation> lsLst = DataSource.AllLineStations.FindAll(x => x.LineCode == _LineCode);
            List<BusStation> bsLst = new List<BusStation>();
            foreach(LineStation ls in lsLst)
                bsLst.Add(GetBusStation(ls.StationCode)); //*need to check if deleted?
            //bsLst.sortByStationNumberInLine(); //sort according to the line's rout
            return bsLst.Clone(); //Clone??
        }
        //private void sortByStationNumberInLine() //or lemammesh Icompareable on BusStation
        //{

        //}
        public void AddLine(int _BusLine, AreaEnum _Area, int _FirstStation, int _LastStation) 
        {
            Line bl = new Line();
            bl.Code = ConfigurationClass.LineCode;
            bl.IsExist = true;
            bl.BusLine = _BusLine;
            bl.Area = _Area;
            bl.FirstStation = _FirstStation;
            bl.LastStation = _LastStation;
            DataSource.AllLines.Add(bl);
        }
        public void DeleteLine(int _Code) //how to update the DS??
        {
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
            return DataSource.AllLineStations.Find(x => (x.LineCode == _LineCode && x.StationCode == _StationCode));
        }
        public void DeleteLineStation(int _LineCode, int _StationCode)
        {
            LineStation ls = GetLineStation(_LineCode, _StationCode);
            DataSource.AllLineStations.Remove(ls);
        }
        public void AddConsecutiveStations(int _StationCode1, int _StationCode2, double _Distance, DateTime _DriveTime, bool _Regional) 
        {
            ConsecutiveStations cs = new ConsecutiveStations();
            cs.StationCode1 = _StationCode1;
            cs.StationCode2 = _StationCode2;
            cs.Distance = _Distance;
            cs.DriveTime = _DriveTime;
            cs.Regional = _Regional;
            DataSource.AllConsecutiveStations.Add(cs);
        }
      


        //#region singelton
        //static readonly DalObject instance = new DalObject();
        //static DalObject() { }
        //DalObject() { }
        //public static DalObject Instance => instance;
        //#endregion

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