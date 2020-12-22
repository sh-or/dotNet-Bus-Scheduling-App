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
        int AddBus(BOBus b);
        void DeleteBus(int _LicenseNumber);
        BusStation GetBusStation(int _StationCode);
        void UpdateStation(BusStation bs);
        List<BusStation> GetSpecificBusStations();
        List<BusStation> GetAllBusStations();
        int AddBusStation(BusStation bs);
        void DeleteBusStation(int _StationCode);
        Line GetLine(int _Code);
        void UpdateLine(Line l);
        List<Line> GetStationLines(int _StationCode);
        List<Line> GetAllLines();
        List<Line> GetSpecificLines();
        List<BusStation> GetStationsOfLine(int _LineCode);
        int AddLine(Line l);
        void DeleteLine(int _Code);
        DO.ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);

        //   Weather GetWeather(int day);
    }
}
