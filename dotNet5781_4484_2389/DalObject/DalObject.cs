
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;

namespace DL
{
    public class DalObject  //: IDAL
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }
        DalObject() { }
        public static DalObject Instance => instance;
        #endregion

        //public IEnumerator GetEnumerator()
        //{
        //    return ExistBuses.GetEnumerator();
        //}

        Bus GetBus(int _LicenseNumber)
        {
            // Bus this[_LicenseNumber];
             return DataSource.ExistBuses.Find(x => x.LicenseNumber == _LicenseNumber); //עוד לא הגדרנו מספר רישוי לאוטובוס
        }

        List<Bus> GetExistBuses
        {
            get { return DataSource.ExistBuses; }
        }

        //IEnumerable<Bus> GetExistBuses() 
        //{
        //    IEnumerable<Bus> ListOfExistBuses;
        //    return ListOfExistBuses;
        //}

        IEnumerable<Bus> GetAllBuses() { }

        void AddBus(DateTime LicensingDate, double Kilometerage, double Fuel, StatusEnum Status, string Driver) { }
        void DeleteBus(int _LicenseNumber) { }

        BusStation GetBusStation(int _StationCode) { }
        IEnumerable<BusStation> GetExistBusStations() { }
        IEnumerable<BusStation> GetAllBusStations() { }
        void AddBusStotion(double Latitude, double Longitude, string Name, string Address, bool Accessibility) { }
        void DeleteBusStotion(int _StationCode) { }

        Line GetLine(/*int _Code,*/ int _BusLine) { }
        IEnumerable<Line> GetStationLines(int _StationCode) { }
        IEnumerable<Line> GetAllLines() { }
        IEnumerable<BusStation> GetStationsOfLine(int _BisLine) { }
        int GetBusCode(int _BusLine);//from busLine to busCode -how?
        void AddLine(int _BusLine, AreaEnum _Area, int _FirstStation, int _LastStation) { }
        void DeleteLine(/*int _Code,*/ int _BusLine) { }
        void AddLineStation(int _LineCode, int _StationCode) { }
        void DeleteLineStation(int _LineCode, int _StationCode, int _StationNumberInLine) { }
        void AddConsecutiveStations(int _StationCode1, int _StationCode2, double _Distance, DateTime _DriveTime, bool _Regional) { }
      


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