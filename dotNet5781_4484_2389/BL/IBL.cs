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
        void reset();

        #region Bus
        BOBus GetBus(int _LicenseNumber);
        void UpdateBus(BOBus b);
        IEnumerable<BOBus> GetSpecificBuses(Predicate<BOBus> p);
        IEnumerable<BOBus> GetAllBuses();
        void AddBus(BOBus b);
        void DeleteBus(int _LicenseNumber);
        #endregion

        #region Bus Station
        BOBusStation GetBusStation(int _StationCode);
        void UpdateStation(BOBusStation bs);
        IEnumerable<BOBusStation> GetSpecificBusStations(Predicate<BOBusStation> p);
        IEnumerable<BOBusStation> GetAllBusStations();
        int AddBusStation(BOBusStation bs);
        void DeleteBusStation(int _StationCode);
        #endregion

        #region Line
        BOLine GetLine(int _Code);
        void UpdateLine(BOLine l);
        void DeleteStationInLine(BOLine l, int _StationCode);
        void AddStationInLine(BOLine l, int _StationCode, int index);
      //  IEnumerable<BOLine> GetStationLines(int _StationCode);
        IEnumerable<BOLine> GetAllLines();
        IEnumerable<BOLine> GetSpecificLines(Predicate<BOLine> p);
        //IEnumerable<BOBusStation> GetStationsOfLine(int _LineCode);
        int AddLine(BOLine l);
        void DeleteLine(int _Code);
        #endregion

        #region Consecutive Stations
        void AddConsecutiveStations(int _StationCode1, int _StationCode2);
        BOConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2);
        void UpdateConsecutiveStations(BOConsecutiveStations cs);
        #endregion

        #region Drive
        //we didnt use Drive, but it can use for program expanding

        //void AddDrive(int _StartStation, int _DestinationStation);
        //void UpdateDrive(BODrive d);
        //BODrive GetDrive(BODrive d);
        //void DeleteDrive(int _StartStation, int _DestinationStation);
        #endregion

        #region User
        BOUser GetUser(string name, string password);
        void UpdateUser(BOUser u);
        //IEnumerable<BOUser> GetSpecificUsers(Predicate<BOUser> p);
        IEnumerable<BOUser> GetAllUsers();
        void AddUser(BOUser u);
        void DeleteUser(string name);

        #endregion

        #region LineTrip
        void AddLineTrip(BOLineTrip lt);
        BOLineTrip GetLineTrip(int _LineCode, TimeSpan _Start);
        IEnumerable<BOLineTrip> GetAllLineTrips(int _LineCode);
        IEnumerable<BOLineTrip> GetAllStationLineTrips(int _StationCode, TimeSpan _Start);
        void DeleteLineTrip(int _LineCode, TimeSpan _Start);
        void UpdateLineTrip(BOLineTrip lt, TimeSpan NewStart); //lt=original

        #endregion
        TimeSpan DriveTimeToStation(int _LineCode, int _StationCode);  //return the time that take to the line to reach the asked station
        IEnumerable<BOLine> SearchRoute(int _stationCode1, int StationCode2);  //return all the lines that cross this stations(in order)
    }
}
