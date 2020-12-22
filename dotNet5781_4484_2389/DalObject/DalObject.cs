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
            throw new BusNotFoundEx(_LicenseNumber, $"Bus number {_LicenseNumber} was not found");
        }
        public List<Bus> GetExistBuses()
        {
            List<Bus> ListBS = DataSource.AllBuses.FindAll(x => x.IsExist).Clone();
            foreach (Bus b in ListBS)
                b.Clone(); //return to??
            return ListBS;
        }
        public List<Bus> GetAllBuses() 
        {
            return DataSource.AllBuses.Clone();
        }
        public int AddBus(int _LicenseNumber, DateTime _LicensingDate, double _Kilometerage, double _Fuel, StatusEnum _Status, string _Driver)
        {  
            if (DataSource.AllBuses.Exists(x => x.LicenseNumber == _LicenseNumber))
                throw new ExistLicenseNumberEx(_LicenseNumber, $"Bus number {_LicenseNumber} is already exist");
            if ((_LicenseNumber > 9999999 && _LicensingDate.Year < 2018) || (_LicenseNumber < 10000000 && _LicensingDate.Year >= 2018)) //license number and date don't match
                throw new InappropriateDateAndLicenseNumEx(_LicenseNumber, $"Bus number {_LicenseNumber} does not match the licensing date");
            Bus b = new Bus();
            b.LicenseNumber = _LicenseNumber;
          
            b.IsExist = true;
            b.LicensingDate = _LicensingDate; //checking if exist?
            b.Kilometerage = _Kilometerage;
            b.Fuel = _Fuel;
            b.Status = _Status;
            b.Driver = _Driver;
            DataSource.AllBuses.Add(b);
            return b.LicenseNumber;
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
            throw new BusStationNotFoundEx(_StationCode, $"Bus station number {_StationCode} was not found");
        }
        public List<BusStation> GetExistBusStations() 
        {
            return DataSource.AllBusStations.FindAll(x => x.IsExist).Clone();
        }
        public List<BusStation> GetAllBusStations() 
        {
            return DataSource.AllBusStations.Clone();
        }
        public int AddBusStation(double _Latitude, double _Longitude, string _Name, string _Address, bool _Accessibility) 
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
            return bs.StationCode;
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
            Line l = DataSource.AllLines.Find(x => x.Code == _Code);
            if(l!=null)
                return l.Clone();
            throw new LineNotFoundEx(_Code, $"Line number {_Code} was not found");
        }
        public List<Line> GetStationLines(int _StationCode) // all the lines which cross in this station
        {
            List<LineStation> lsLst = DataSource.AllLineStations.FindAll(x => x.StationCode == _StationCode);
            List<Line> bsLst = new List<Line>();
            List <Line> exLines= GetExistLines();
            foreach (Line ln in exLines)
                bsLst.Add(exLines.Find(x => x.Code == ln.Code));
            return bsLst.Clone(); 
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