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
        List<Bus> GetExistBuses();  //מותר ?? בדוגמה בדטה סורס הם עשו ליסט
       // IEnumerable<Bus> GetExistBuses();
        IEnumerable<Bus> GetAllBuses();

        void AddBus(DateTime LicensingDate, double Kilometerage, double Fuel, StatusEnum Status, string Driver);
        void DeleteBus(int _LicenseNumber);

        BusStation GetBusStation(int _StationCode);
        IEnumerable<BusStation> GetExistBusStations();
        IEnumerable<BusStation> GetAllBusStations();
        void AddBusStotion(double Latitude, double Longitude, string Name, string Address, bool Accessibility);
        void DeleteBusStotion(int _StationCode);


        Line GetLine(/*int _Code,*/ int _BusLine);
        IEnumerable<Line> GetStationLines(int _StationCode);
        IEnumerable<Line> GetAllLines();
        IEnumerable<BusStation> GetStationsOfLine(int _BisLine);
        int GetBusCode(int _BusLine);//from busLine to busCode -how?
        void AddLine(int _BusLine, AreaEnum _Area, int _FirstStation, int _LastStation);
        void DeleteLine(/*int _Code,*/ int _BusLine);
        void AddLineStation(int _LineCode, int _StationCode); 
        void DeleteLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);
        void AddConsecutiveStations(int _StationCode1, int _StationCode2 , double _Distance, DateTime _DriveTime, bool _Regional);
        //? void DeleteConsecutiveStations();

       // double GetDistance();

        //double GetTemparture(int day);
        //WindDirection GetWindDirection(int day);

    }
}
