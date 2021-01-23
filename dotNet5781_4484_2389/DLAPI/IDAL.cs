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
        void reset();
        #region Bus
        Bus GetBus(int _LicenseNumber);
        void UpdateBus(Bus b);
        IEnumerable<Bus> GetSpecificBuses(Predicate<Bus> p); 
        IEnumerable<Bus> GetAllBuses();
        void AddBus(Bus b);
        void DeleteBus(int _LicenseNumber);
        #endregion

        #region BusStation
        BusStation GetBusStation(int _StationCode);
        void UpdateStation(BusStation bs);
        IEnumerable<BusStation> GetSpecificBusStations(Predicate<BusStation> p);
        IEnumerable<BusStation> GetAllBusStations();
        int AddBusStation(BusStation bs);
        void DeleteBusStation(int _StationCode);
        #endregion

        #region Line
        Line GetLine(int _Code);
        void UpdateLine(Line l);
        IEnumerable<Line> GetStationLines(int _StationCode);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetSpecificLines(Predicate<Line> p);
        IEnumerable<BusStation> GetStationsOfLine(int _LineCode);
        int AddLine(Line l);
        void DeleteLine(int _Code);
        #endregion

        #region LineStation
        void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine);
        LineStation GetLineStation(int _LineCode, int _StationCode);
        IEnumerable<LineStation> GetAllLineStations(int _LineCode);
        void UpdateLineStation(int _LineCode, int _StationCode, int n); //change index in +/-1
        int IsStationInLine(int _LineCode, int _StationCode); //check if exist specific linestation and return the station location in the line or -1
        IEnumerable<LineStation> GetSpecificLineStations(Predicate<LineStation> p);
        void DeleteLineStation(int _LineCode, int _StationCode);

        #endregion

        #region ConsecutiveStations
        void AddConsecutiveStations(ConsecutiveStations cs);
        ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);
        void UpdateConsecutiveStations(ConsecutiveStations cs);
        bool isExistConsecutiveStations(int _FirstStation, int _LastStation);
        IEnumerable<ConsecutiveStations> GetSomeConsecutiveStations(int _StationCode);
        #endregion

        #region User
        User GetUser(string name, string password);
        void UpdateUser(User u);
        IEnumerable<User> GetSpecificUsers(Predicate<User> p);
        IEnumerable<User> GetAllUsers();
        void AddUser(User u);
        void DeleteUser(string name);
        #endregion

        #region LineTrip
        LineTrip GetLineTrip(int _LineCode, TimeSpan _Start);
        void AddLineTrip(LineTrip lt);
        IEnumerable<LineTrip> GetAllLineTrips(int _LineCode);
        IEnumerable<LineTrip> GetAllStationLineTrips(int _StationCode, TimeSpan _Start);
        void DeleteLineTrip(int _LineCode, TimeSpan _Start);
        void UpdateLineTrip(LineTrip lt, TimeSpan NewStart); //lt=original
        #endregion
    
    }
}
