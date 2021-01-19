using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DLAPI;
using System.Xml.Linq;

namespace DL
{
    public class DalXml :IDAL
    {
        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor
        DalXml() { } //  private
        public static DalXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string BusesPath = @"BusesXml.xml"; //XElement
        string LineTripsPath = @"LineTripsXml.xml"; //XElement

        string busStationsPath = @"BusStationsXml.xml"; //XMLSerializer
        string lecturersPath = @"LecturersXml.xml"; //XMLSerializer
        string lectInCoursesPath = @"LecturerInCourseXml.xml"; //XMLSerializer
        string studInCoursesPath = @"StudentInCoureseXml.xml"; //XMLSerializer
        #endregion

        #region Bus
        public Bus GetBus(int _LicenseNumber)
        {
            XElement BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);

            Bus b = (from bus in BusesRootElem.Elements()
                    where int.Parse(bus.Element("LicenseNumber").Value) == _LicenseNumber
                    select new Bus()
                    {
                        LicenseNumber = Int32.Parse(bus.Element("LicenseNumber").Value),
                        LicensingDate = DateTime.Parse(bus.Element("LicensingDate").Value),
                        Kilometerage = double.Parse(bus.Element("Kilometerage").Value),
                        KmFromLastRefuel = double.Parse(bus.Element("KmFromLastRefuel").Value),
                        KmFromLastCare = double.Parse(bus.Element("KmFromLastCare").Value),
                        DateOfLastCare = DateTime.Parse(bus.Element("DateOfLastCare").Value),
                        Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), bus.Element("Status").Value),
                        Driver = bus.Element("Driver").Value,
                        IsExist =bool.Parse(bus.Element("IsExist").Value)
                    }
                    ).FirstOrDefault();
          
            if (b != null && b.IsExist)
                return b;
            throw new DOException($"Bus number {_LicenseNumber} was not found");
        }

        public void UpdateBus(Bus b)  
        {
            XElement BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);

            XElement bs = (from bus in BusesRootElem.Elements()
                     where int.Parse(bus.Element("LicenseNumber").Value) == b.LicenseNumber
                     select bus).FirstOrDefault();

                if (bs != null && bool.Parse(bs.Element("IsExist").Value))
                {
                bs.Element("LicenseNumber").Value = b.LicenseNumber.ToString();
                bs.Element("LicensingDate").Value = b.LicensingDate.ToString();
                bs.Element("Kilometerage").Value = b.Kilometerage.ToString();
                bs.Element("KmFromLastRefuel").Value = b.KmFromLastRefuel.ToString();
                bs.Element("KmFromLastCare").Value = b.KmFromLastCare.ToString();
                bs.Element("DateOfLastCare").Value = b.DateOfLastCare.ToString();
                bs.Element("Status").Value = b.Status.ToString();
                bs.Element("Driver").Value = b.Driver.ToString();
                bs.Element("IsExist").Value = b.IsExist.ToString();
              
                XMLTools.SaveListToXMLElement(BusesRootElem, BusesPath);
                }
                else
                    throw new DOException($"Bus number {b.LicenseNumber} was not found");
        }

        public IEnumerable<Bus> GetSpecificBuses(Predicate<Bus> p)
        {
            XElement BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);

            var ListBS = from bus in BusesRootElem.Elements()
                         let b= new Bus()
                         {
                             LicenseNumber = Int32.Parse(bus.Element("LicenseNumber").Value),
                             LicensingDate = DateTime.Parse(bus.Element("LicensingDate").Value),
                             Kilometerage = double.Parse(bus.Element("Kilometerage").Value),
                             KmFromLastRefuel = double.Parse(bus.Element("KmFromLastRefuel").Value),
                             KmFromLastCare = double.Parse(bus.Element("KmFromLastCare").Value),
                             DateOfLastCare = DateTime.Parse(bus.Element("DateOfLastCare").Value),
                             Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), bus.Element("Status").Value),
                             Driver = bus.Element("Driver").Value,
                             IsExist = bool.Parse(bus.Element("IsExist").Value)
                         }
                         where p(b)
                          select b;
            if (ListBS != null)
                return ListBS;
            throw new DOException("No exist buses were found");
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            XElement BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);

            var ListBS= from bus in BusesRootElem.Elements()
                        where bool.Parse(bus.Element("IsExist").Value)
                         select new Bus()
                          {
                              LicenseNumber = Int32.Parse(bus.Element("LicenseNumber").Value),
                              LicensingDate = DateTime.Parse(bus.Element("LicensingDate").Value),
                              Kilometerage = double.Parse(bus.Element("Kilometerage").Value),
                              KmFromLastRefuel = double.Parse(bus.Element("KmFromLastRefuel").Value),
                              KmFromLastCare = double.Parse(bus.Element("KmFromLastCare").Value),
                              DateOfLastCare = DateTime.Parse(bus.Element("DateOfLastCare").Value),
                              Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), bus.Element("Status").Value),
                              Driver = bus.Element("Driver").Value,
                              IsExist = bool.Parse(bus.Element("IsExist").Value)
                          };

            if (ListBS != null)
                return ListBS;
            throw new DOException("No buses were found");
        }

        public void AddBus(Bus b)
        {
            XElement BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);

            XElement bus1 = (from x in BusesRootElem.Elements()
                           where int.Parse(x.Element("LicenseNumber").Value) == b.LicenseNumber
                           select x).FirstOrDefault();

            if (bus1 != null)
                throw new DOException($"Bus number {b.LicenseNumber} is already exist");

                XElement bus = new XElement("Bus",
                                   new XElement("LicenseNumber", b.LicenseNumber.ToString()),
                                   new XElement("LicensingDate", b.LicensingDate.ToString()),
                                   new XElement("Kilometerage", b.Kilometerage.ToString()),
                                   new XElement("KmFromLastRefuel", b.KmFromLastRefuel.ToString()),
                                   new XElement("KmFromLastCare", b.KmFromLastCare.ToString()),
                                   new XElement("DateOfLastCare", b.DateOfLastCare.ToString()),
                                   new XElement("Status", b.Status.ToString()),
                                   new XElement("Driver", b.Driver),
                                   new XElement("IsExist", b.IsExist.ToString()));

            BusesRootElem.Add(bus);

            XMLTools.SaveListToXMLElement(BusesRootElem, BusesPath);
        }

        public void DeleteBus(int _LicenseNumber)
        {
            XElement BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);

            XElement bs = (from bus in BusesRootElem.Elements()
                           where int.Parse(bus.Element("LicenseNumber").Value) == _LicenseNumber
                           select bus).FirstOrDefault();

            if (bs != null && bool.Parse(bs.Element("IsExist").Value))
            {
                bs.Element("IsExist").Value = false.ToString();
                XMLTools.SaveListToXMLElement(BusesRootElem, BusesPath);
            }
            else
                throw new DOException($"Bus number {_LicenseNumber} was not found");
        }
        #endregion

        #region LineTrip
        public LineTrip GetLineTrip(int _LineCode, TimeSpan _Start)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);

            LineTrip lt = (from x in LineTripsRootElem.Elements()
                     where int.Parse(x.Element("LineCode").Value) == _LineCode && TimeSpan.Parse(x.Element("Start").Value)== _Start
                           select new LineTrip()
                     {
                         LineCode = Int32.Parse(x.Element("LineCode").Value),
                         Start = TimeSpan.Parse(x.Element("Start").Value),
                         IsExist = bool.Parse(x.Element("IsExist").Value)
                     }
                    ).FirstOrDefault();

            if (lt != null && lt.IsExist)
                return lt;
            throw new DOException($"Line {_LineCode} has no trip till {_Start}");
        }

        public void AddLineTrip(LineTrip lt)
        {
            //can add identical linetrip, rush hours..
            DataSource.AllLinesTrip.Add(lt.Clone());
        }

        public IEnumerable<LineTrip> GetAllLineTrips(int _LineCode)
        {
            return from LineTrip x in DataSource.AllLinesTrip
                   where x.LineCode == _LineCode && x.IsExist
                   select x.Clone();
        }

        public IEnumerable<LineTrip> GetAllStationLineTrips(int _StationCode, TimeSpan _Start)
        {
            List<LineStation> ls = DataSource.AllLineStations.FindAll(x => x.IsExist && x.StationCode == _StationCode);
            if (ls == null)
                throw new DOException($"Bus station {_StationCode} has no lines");

            IEnumerable<LineTrip> lt = from LineStation t in ls
                                       from LineTrip x in DataSource.AllLinesTrip
                                       where x.LineCode == t.LineCode && x.Start <= _Start && x.IsExist
                                       select x.Clone();
            return lt;
        }

        public void DeleteLineTrip(int _LineCode, TimeSpan _Start)
        {
            int n = DataSource.AllLinesTrip.FindIndex(x => x.IsExist && x.LineCode == _LineCode && x.Start == _Start);
            if (n > -1)
                DataSource.AllLinesTrip[n].IsExist = false;
            else
                throw new DOException($"Line {_LineCode} has no trip at {_Start}");
        }
        public void UpdateLineTrip(LineTrip lt, TimeSpan NewStart) //lt==original!
        {
            int n = DataSource.AllLinesTrip.FindIndex(x => x.IsExist && x.LineCode == lt.LineCode && x.Start == lt.Start);
            if (n > -1)
            {
                lt.Start = NewStart;
                DataSource.AllLinesTrip[n] = lt;
            }
            else
                throw new DOException($"Line {lt.LineCode} has no trip at {lt.Start}");
        }
        #endregion

        #region Bus Station
        public BusStation GetBusStation(int _StationCode)
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(busStationsPath);

            BusStation bs = lst.Find(x => x.StationCode == _StationCode && x.IsExist);
            if (bs != null)
                return bs;
            else
                throw new DOException( $"Bus station number {_StationCode} was not found");
        }


        public void UpdateStation(BusStation bs)
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(busStationsPath);

            BusStation station = lst.Find(x => x.StationCode == bs.StationCode&& x.IsExist);
            if (bs != null)
            {
                lst.Remove(station);
                lst.Add(bs); 
            }
            else
                throw new DOException($"Bus station number {bs.StationCode} was not found");

            XMLTools.SaveListToXMLSerializer(lst, busStationsPath);
        }
        public IEnumerable<BusStation> GetSpecificBusStations(Predicate<BusStation> p)
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(busStationsPath);

            var ListBS = from BusStation bs in lst
                          where p(bs)&& bs.IsExist
                          select bs;

            if (ListBS != null)
                return ListBS;
            throw new DOException("No exist bus stations were found");
        }
        public IEnumerable<BusStation> GetAllBusStations()
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(busStationsPath);

            var ListBS = from BusStation bs in lst
                          where bs.IsExist
                          select bs;
            if (ListBS != null)
                return ListBS;
            throw new DOException("No bus stations were found");
        }

        public int AddBusStation(BusStation bs)
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(busStationsPath);

            bs.StationCode = ConfigurationClass.StationCode;
            lst.Add(bs);
         
            XMLTools.SaveListToXMLSerializer(lst, busStationsPath);
            return bs.StationCode;
        }
        public void DeleteBusStation(int _StationCode) //delete line-stations
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(busStationsPath);

            int n = lst.FindIndex(x => x.IsExist && x.StationCode == _StationCode);
            if (n > -1)
            {
                lst[n].IsExist = false;
               XMLTools.SaveListToXMLSerializer(lst, busStationsPath);
            }
            else
                throw new DOException($"Station number {_StationCode} was not found");
        }
        #endregion
    }
}
