using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI
{

    public interface IDAL
    {
        //DateTime SetLicensingDate(int _LicenseNumber, DateTime _LicensingDate);
        //double SetKilometerage(int _LicenseNumber, double _Kilometerage);
        //double Fuel(int _LicenseNumber);
        //StatusEnum Status(int _LicenseNumber);
        //string Driver(int _LicenseNumber);
        //bool IsExist(int _LicenseNumber);
        Bus GetBus(int _LicenseNumber);
        void UpdateBus(Bus b);
        IEnumerable<Bus> GetSpecificBuses(Predicate<Bus> p); 
        IEnumerable<Bus> GetAllBuses();
        void AddBus(Bus b /*int _LicenseNumber, DateTime _LicensingDate, double _Kilometerage, double _Fuel, StatusEnum _Status, string _Driver*/);
        void DeleteBus(int _LicenseNumber);
        BusStation GetBusStation(int _StationCode);
        void UpdateStation(BusStation bs);
        IEnumerable<BusStation> GetSpecificBusStations(Predicate<BusStation> p);
        IEnumerable<BusStation> GetAllBusStations();
        int AddBusStation(BusStation bs /*double _Latitude, double _Longitude, string _Name, string _Address, bool _Accessibility*/);
        void DeleteBusStation(int _StationCode);
        Line GetLine(int _Code);
        void UpdateLine(Line l);
        IEnumerable<Line> GetStationLines(int _StationCode);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetSpecificLines(Predicate<Line> p);
        IEnumerable<BusStation> GetStationsOfLine(int _LineCode);
        int AddLine(Line l/*int _BusLine, AreaEnum _Area, int _FirstStation, int _LastStation*/);
        void DeleteLine(int _Code);
        void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);
        LineStation GetLineStation(int _LineCode, int _StationCode);
        IEnumerable<LineStation> GetAllLineStations(int _LineCode);
        void UpdateLineStation(int _LineCode, int _StationCode, int n); //change index in +/-1
        int IsStationInLine(int _LineCode, int _StationCode); //check if exist specific linestation and return the station location in the line or -1
        IEnumerable<LineStation> GetSpecificLineStations(Predicate<LineStation> p /*condition*/);
        void DeleteLineStation(int _LineCode, int _StationCode);
        void AddConsecutiveStations(ConsecutiveStations cs);
        ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);
        void UpdateConsecutiveStations(ConsecutiveStations cs);
        bool isExistConsecutiveStations(int _FirstStation, int _LastStation);
        //? void DeleteConsecutiveStations();
        // double GetDistance();

        //double GetTemparture(int day);
        //WindDirection GetWindDirection(int day);

    }
}
