using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BlAPI
{
   public interface IBL
    {
        BOBus GetBus(int _LicenseNumber);
        void UpdateBus(BOBus b);
        List<BOBus> GetSpecificBuses();
        List<BOBus> GetAllBuses();
        void AddBus(BOBus b);
        void DeleteBus(int _LicenseNumber);
        BOBusStation GetBusStation(int _StationCode);
        void UpdateStation(BusStation bs);
        List<BOBusStation> GetSpecificBusStations();
        List<BOBusStation> GetAllBusStations();
        int AddBusStation(BOBusStation bs);
        void DeleteBusStation(int _StationCode);
        BOLine GetLine(int _Code);
        void UpdateLine(BOLine l);
        List<BOLine> GetStationLines(int _StationCode);
        List<BOLine> GetAllLines();
        List<BOLine> GetSpecificLines();
        List<BOBusStation> GetStationsOfLine(int _LineCode);
        int AddLine(BOLine l);
        void DeleteLine(int _Code);
        void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);
        BOLineStation GetLineStation(int _LineCode, int _StationCode);
        void DeleteLineStation(int _LineCode, int _StationCode);
        void AddConsecutiveStations(int _StationCode1, int _StationCode2, double _Distance, DateTime _DriveTime, bool _Regional);
        //BOConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);
        //void UpdateConsecutiveStations(BOConsecutiveStations cs);

        //   Weather GetWeather(int day);
    }
}
