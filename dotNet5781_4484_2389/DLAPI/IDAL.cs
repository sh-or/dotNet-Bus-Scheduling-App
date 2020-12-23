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
        List<Bus> GetSpecificBuses(); 
        List<Bus> GetAllBuses();
        void AddBus(Bus b /*int _LicenseNumber, DateTime _LicensingDate, double _Kilometerage, double _Fuel, StatusEnum _Status, string _Driver*/);
        void DeleteBus(int _LicenseNumber);
        BusStation GetBusStation(int _StationCode);
        void UpdateStation(BusStation bs);
        List<BusStation> GetSpecificBusStations();
        List<BusStation> GetAllBusStations();
        int AddBusStation(BusStation bs /*double _Latitude, double _Longitude, string _Name, string _Address, bool _Accessibility*/);
        void DeleteBusStation(int _StationCode);
        Line GetLine(int _Code);
        void UpdateLine(Line l);
        List<Line> GetStationLines(int _StationCode);
        List<Line> GetAllLines();
        List<Line> GetSpecificLines();
        List<BusStation> GetStationsOfLine(int _LineCode);
        int AddLine(int _BusLine, AreaEnum _Area, int _FirstStation, int _LastStation);
        void DeleteLine(int _Code);
        void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);
        LineStation GetLineStation(int _LineCode, int _StationCode);
        void DeleteLineStation(int _LineCode, int _StationCode);
        void AddConsecutiveStations(int _StationCode1, int _StationCode2 , double _Distance, DateTime _DriveTime, bool _Regional);
        ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);
        void UpdateConsecutiveStations(ConsecutiveStations cs);
        //? void DeleteConsecutiveStations();
        // double GetDistance();

        //double GetTemparture(int day);
        //WindDirection GetWindDirection(int day);

    }
}
