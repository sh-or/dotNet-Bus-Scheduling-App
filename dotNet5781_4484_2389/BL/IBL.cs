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
        IEnumerable<BOBus> GetSpecificBuses(Predicate<BOBus> p);
        IEnumerable<BOBus> GetAllBuses();
        void AddBus(BOBus b);
        void DeleteBus(int _LicenseNumber);

        BOBusStation GetBusStation(int _StationCode);
        void UpdateStation(BOBusStation bs);
        IEnumerable<BOBusStation> GetSpecificBusStations(Predicate<BOBusStation> p);
        IEnumerable<BOBusStation> GetAllBusStations();
        int AddBusStation(BOBusStation bs);
        void DeleteBusStation(int _StationCode);

        BOLine GetLine(int _Code);
        void UpdateLine(BOLine l);
        void DeleteStationInLine(BOLine l, int _StationCode);
        void AddStationInLine(BOLine l, int _StationCode, int index);
      //  IEnumerable<BOLine> GetStationLines(int _StationCode);
        IEnumerable<BOLine> GetAllLines();
        IEnumerable<BOLine> GetSpecificLines(Predicate<BOLine> p);
        
        IEnumerable<BOBusStation> GetStationsOfLine(int _LineCode);
        int AddLine(BOLine l);
        void DeleteLine(int _Code);
     //   void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);
        
         void UpdateLineStation(BOLineStation ls);
        //void DeleteLineStation(int _LineCode, int _StationCode);
        void AddConsecutiveStations(int _StationCode1, int _StationCode2, double _Distance, DateTime _DriveTime, bool _Regional);
        //BOConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);
        //void UpdateConsecutiveStations(BOConsecutiveStations cs);

        //   Weather GetWeather(int day);
    }
}
