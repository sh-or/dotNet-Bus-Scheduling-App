﻿using System;
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
        List<Bus> GetExistBuses(); 
        List<Bus> GetAllBuses();

        void AddBus(DateTime _LicensingDate, double _Kilometerage, double _Fuel, StatusEnum _Status, string _Driver);
        void DeleteBus(int _LicenseNumber);

        BusStation GetBusStation(int _StationCode);
        List<BusStation> GetExistBusStations();
        List<BusStation> GetAllBusStations();
        void AddBusStation(double _Latitude, double _Longitude, string _Name, string _Address, bool _Accessibility);
        void DeleteBusStation(int _StationCode);


        Line GetLine(int _Code);
        List<Line> GetStationLines(int _StationCode);
        List<Line> GetAllLines();
        List<Line> GetExistLines();
        List<BusStation> GetStationsOfLine(int _LineCode);
        void AddLine(int _BusLine, AreaEnum _Area, int _FirstStation, int _LastStation);
        void DeleteLine(int _Code);
        void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);
        LineStation GetLineStation(int _LineCode, int _StationCode);
        void DeleteLineStation(int _LineCode, int _StationCode);
        void AddConsecutiveStations(int _StationCode1, int _StationCode2 , double _Distance, DateTime _DriveTime, bool _Regional);
        //? void DeleteConsecutiveStations();

       // double GetDistance();

        //double GetTemparture(int day);
        //WindDirection GetWindDirection(int day);

    }
}
