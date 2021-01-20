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
        static DalXml() { } // static c-tor
        DalXml() { } // private
        public static DalXml Instance { get => instance; }// for public use
        #endregion

        #region DS XML Files
        string BusesPath = @"BusesXml.xml"; //XElement
        string LineTripsPath = @"LineTripsXml.xml"; //XElement

        string BusStationsPath = @"BusStationsXml.xml"; //XMLSerializer
        string LineStationsPath = @"LineStationsXml.xml"; //XMLSerializer
        string LinesPath = @"LinesXml.xml"; //XMLSerializer
        string UsersPath = @"UserXml.xml"; //XMLSerializer
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
        {//can add identical linetrip, rush hours..

            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);

            XElement lt1 = (from x in LineTripsRootElem.Elements()
                           where int.Parse(x.Element("LineCode").Value) == lt.LineCode && TimeSpan.Parse(x.Element("Start").Value) == lt.Start && bool.Parse(x.Element("IsExist").Value)
                            select x).FirstOrDefault();
            if (lt1 != null)
                throw new DOException($"Line {lt.LineCode} already has trip at {lt.Start}");

            XElement bus = new XElement("LineTrip",
                               new XElement("LineCode", lt.LineCode.ToString()),
                               new XElement("Start", lt.Start.ToString()),
                               new XElement("IsExist", lt.IsExist.ToString()));

            LineTripsRootElem.Add(bus);
            XMLTools.SaveListToXMLElement(LineTripsRootElem, LineTripsPath);
        }

        public IEnumerable<LineTrip> GetAllLineTrips(int _LineCode)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);

            var lst = from x in LineTripsRootElem.Elements()
                      let lt1 = new LineTrip()
                      {
                          LineCode = Int32.Parse(x.Element("LineCode").Value),
                          Start = TimeSpan.Parse(x.Element("Start").Value),
                          IsExist = bool.Parse(x.Element("IsExist").Value)
                      }
                      where lt1.LineCode == _LineCode && lt1.IsExist
                      select lt1;
            if (lst != null)
                return lst;
            throw new DOException("No exist Line trips were found");
        }

        public IEnumerable<LineTrip> GetAllStationLineTrips(int _StationCode, TimeSpan _Start)  ///////
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);
            XElement LineStationsRootElem = XMLTools.LoadListFromXMLElement(LineStationsPath);

            var lst = from x in LineTripsRootElem.Elements()
                     from l in LineStationsRootElem.Elements()
                     where bool.Parse(x.Element("IsExist").Value) && bool.Parse(l.Element("IsExist").Value)
                        && Int32.Parse(x.Element("LineCode").Value) == _StationCode
                        && Int32.Parse(x.Element("LineCode").Value) == Int32.Parse(l.Element("LineCode").Value)
                        && TimeSpan.Parse(x.Element("Start").Value) <= _Start 
                     select x;
            //if (lst != null) want to return even null..
                return (IEnumerable<LineTrip>)lst;
            //throw new DOException($"No exist Line trips were found in station {_StationCode}");
        }

        public void DeleteLineTrip(int _LineCode, TimeSpan _Start)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);

            XElement lt1 = (from x in LineTripsRootElem.Elements()
                            where int.Parse(x.Element("LineCode").Value) == _LineCode && TimeSpan.Parse(x.Element("Start").Value) == _Start && bool.Parse(x.Element("IsExist").Value)
                            select x).FirstOrDefault();

            if (lt1 != null)
            {
                lt1.Remove();
                XMLTools.SaveListToXMLElement(LineTripsRootElem, LineTripsPath);
            }
            else
             throw new DOException($"Line {_LineCode} has no trip at {_Start}");
        }

        public void UpdateLineTrip(LineTrip lt, TimeSpan NewStart) //lt==original!
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);

            XElement lt1 = (from x in LineTripsRootElem.Elements()
                            where int.Parse(x.Element("LineCode").Value) == lt.LineCode && TimeSpan.Parse(x.Element("Start").Value) == _Start && bool.Parse(x.Element("IsExist").Value)
                            select x).FirstOrDefault();

            if (lt1 != null)
            {
                lt1.Element("LineCode").Value = lt.LineCode.ToString();
                lt1.Element("Start").Value = NewStart.ToString();
                lt1.Element("IsExist").Value = lt.IsExist.ToString();
                XMLTools.SaveListToXMLElement(LineTripsRootElem, LineTripsPath);
            }
            else
                throw new DOException($"Line {lt.LineCode} has no trip at {lt.Start}");
        }
        #endregion
        #region Bus Station
        public BusStation GetBusStation(int _StationCode)
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(BusStationsPath);

            BusStation bs = lst.Find(x => x.StationCode == _StationCode && x.IsExist);
            if (bs != null)
                return bs;
            else
                throw new DOException( $"Bus station number {_StationCode} was not found");
        }
        public void UpdateStation(BusStation bs)
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(BusStationsPath);

            BusStation station = lst.Find(x => x.StationCode == bs.StationCode && x.IsExist);
            if (bs != null)
            {
                lst.Remove(station);
                lst.Add(bs); 
            }
            else
                throw new DOException($"Bus station number {bs.StationCode} was not found");

            XMLTools.SaveListToXMLSerializer(lst, BusStationsPath);
        }
        public IEnumerable<BusStation> GetSpecificBusStations(Predicate<BusStation> p)
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(BusStationsPath);

            var ListBS = from BusStation bs in lst
                          where p(bs)&& bs.IsExist
                          select bs;

            if (ListBS != null)
                return ListBS;
            throw new DOException("No exist bus stations were found");
        }
        public IEnumerable<BusStation> GetAllBusStations()
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(BusStationsPath);

            var ListBS = from BusStation bs in lst
                          where bs.IsExist
                          select bs;
            if (ListBS != null)
                return ListBS;
            throw new DOException("No bus stations were found");
        }

        public int AddBusStation(BusStation bs)
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(BusStationsPath);

            XElement XMLRunNumber = XElement.Load(@"XMLConfiguration.xml");
            int RunNum = int.Parse(XMLRunNumber.Element("StationCode").Value);
            bs.StationCode = RunNum++;
            XMLRunNumber.Element("StationCode").Value = RunNum.ToString();
            XMLRunNumber.Save(@"XMLConfiguration.xml");
            lst.Add(bs);
         
            XMLTools.SaveListToXMLSerializer(lst, BusStationsPath);
            return bs.StationCode;
        }
        public void DeleteBusStation(int _StationCode) //delete line-stations
        {
            List<BusStation> lst = XMLTools.LoadListFromXMLSerializer<BusStation>(BusStationsPath);

            int n = lst.FindIndex(x => x.IsExist && x.StationCode == _StationCode);
            if (n > -1)
            {
                lst[n].IsExist = false;
               XMLTools.SaveListToXMLSerializer(lst, BusStationsPath);
            }
            else
                throw new DOException($"Station number {_StationCode} was not found");
        }
        public IEnumerable<BusStation> GetStationsOfLine(int _LineCode)
        {
            List<LineStation> lst1 = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);
            List<BusStation> lst2 = XMLTools.LoadListFromXMLSerializer<BusStation>(BusStationsPath);
            IEnumerable<LineStation> lsLst = lst1.FindAll(x => x.IsExist && x.LineCode == _LineCode);
            IEnumerable<BusStation> bsLst = from ls in lsLst
                                            orderby ls.StationNumberInLine
                                            select GetBusStation(ls.StationCode);
            if (bsLst != null)
                return bsLst;
            throw new DOException("No line stations were found");
        }
        #endregion
        #region Line
        public Line GetLine(int _Code)
        {
            List<Line> lst = XMLTools.LoadListFromXMLSerializer<Line>(LinesPath);  
            Line l = lst.Find(x => x.IsExist && x.Code == _Code);
            if (l != null)
                return l;
            throw new DOException($"Line number {_Code} was not found");
        }
        public void UpdateLine(Line l)
        {
            List<Line> lst = XMLTools.LoadListFromXMLSerializer<Line>(LinesPath);
            int n = lst.FindIndex(x => x.Code == l.Code);
            lst[n] = l;
            XMLTools.SaveListToXMLSerializer(lst, LinesPath);
        }
        public int AddLine(Line l) 
        {
            List<Line> lst = XMLTools.LoadListFromXMLSerializer<Line>(LinesPath);

            XElement XMLRunNumber = XElement.Load(@"XMLConfiguration.xml");
            int RunNum= int.Parse(XMLRunNumber.Element("LineCode").Value);
            l.Code = RunNum++;
            XMLRunNumber.Element("LineCode").Value = RunNum.ToString();
            XMLRunNumber.Save(@"XMLConfiguration.xml");
            lst.Add(l);
            XMLTools.SaveListToXMLSerializer(lst, LinesPath);
            return l.Code;
        }
        public void DeleteLine(int _Code) 
        {
            List<Line> lst = XMLTools.LoadListFromXMLSerializer<Line>(LinesPath);
            int n = lst.FindIndex(x => x.IsExist && x.Code == _Code);
            if (n > -1)
            {
                lst[n].IsExist = false;
                XMLTools.SaveListToXMLSerializer(lst, LinesPath);
            }
            else
                throw new DOException($"Line number {_Code} was not found");
        }
        public IEnumerable<Line> GetAllLines()
        {
            List<Line> lst = XMLTools.LoadListFromXMLSerializer<Line>(LinesPath);
            var Listl = from Line l in lst
                         where l.IsExist
                         select l;
            if (Listl != null)
                return Listl;
            throw new DOException("No lines were found");
        }
        public IEnumerable<Line> GetSpecificLines(Predicate<Line> p)
        {
            List<Line> lst = XMLTools.LoadListFromXMLSerializer<Line>(LinesPath);
            var Listl = from Line l in lst
                         where p(l)
                         select l;
                return Listl;
            // even if null..
        }
        public IEnumerable<Line> GetStationLines(int _StationCode) // all the lines which cross in this station
        {
            List<LineStation> lst = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);
            return from ls in lst.FindAll(x => x.IsExist && x.StationCode == _StationCode)
                   where GetLine(ls.LineCode).IsExist
                   select GetLine(ls.LineCode);
        }
        #endregion

        #region Line Station
        public void AddLineStation(int _LineCode, int _StationCode, int _StationNumberInLine) 
        {
            List<LineStation> lst = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);

            LineStation ls = new LineStation();
            ls.IsExist = true;
            ls.StationCode = _StationCode;
            ls.LineCode = _LineCode;
            ls.StationNumberInLine = _StationNumberInLine;

            lst.Add(ls);
            XMLTools.SaveListToXMLSerializer(lst, LineStationsPath);
        }
        public LineStation GetLineStation(int _LineCode, int _StationCode)
        {
            List<LineStation> lst = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);

            LineStation l = lst.Find(x => x.IsExist && (x.LineCode == _LineCode && x.StationCode == _StationCode));
            if (l != null)
                return l;
            throw new DOException($"Line number {_LineCode} does not cross in station {_StationCode}");
        }
        public IEnumerable<LineStation> GetAllLineStations(int _LineCode)
        {
            List<LineStation> lst = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);

            var Listls = from LineStation x in lst
                        where x.IsExist && x.LineCode == _LineCode
                         select x;

            if (Listls != null)
                return Listls;
            throw new DOException("No lines were found");
        }
        public void UpdateLineStation(int _LineCode, int _StationCode, int n)//change index in +/-1
        {
            List<LineStation> lst = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);

            LineStation ls = GetLineStation(_LineCode, _StationCode);
            int ind = lst.FindIndex(x => x.LineCode == _LineCode && x.StationCode == _StationCode);
            lst[ind].StationNumberInLine += n;
            XMLTools.SaveListToXMLSerializer(lst, LineStationsPath);
        }

        public int IsStationInLine(int _LineCode, int _StationCode) //check if exist specific line station and return the station location in the line or -1
        {
            List<LineStation> lst = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);

            if (!lst.Exists(x => x.IsExist && x.LineCode == _LineCode && x.StationCode == _StationCode))
                return -1;
            LineStation ls = GetLineStation(_LineCode, _StationCode);
            return ls.StationNumberInLine;
        }
        
        public IEnumerable<LineStation> GetSpecificLineStations(Predicate<LineStation> p)
        {
            List<LineStation> lst = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);

            var Listl = from LineStation l in lst
                                         where l.IsExist && p(l)
                                          select l;
            return Listl;
            //return spesific collection OR NULL
        }

        public void DeleteLineStation(int _LineCode, int _StationCode)
        {
            List<LineStation> lst = XMLTools.LoadListFromXMLSerializer<LineStation>(LineStationsPath);

            int n = lst.FindIndex(x => x.StationCode == _StationCode && x.LineCode == _LineCode);
            if (n > -1)
            {
                lst[n].IsExist = false;
                XMLTools.SaveListToXMLSerializer(lst, LineStationsPath);
            }
            else
                throw new DOException($"Line number {_LineCode} does not cross in station {_StationCode}");
        }
        #endregion

        #region User
        public User GetUser(string name, string password)
        {
            List<User> lst = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);
            User u = lst.FirstOrDefault(x => x.IsExist && x.Name == name && x.Password == password);
            if (u != null)
                return u;
            throw new DOException($"User name or password are wrong");
        }

        public void UpdateUser(User u)
        {
            List<User> lst = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);
            int index = lst.FindIndex(x => x.IsExist && x.Name == u.name);
            if (index > -1)
            {
                lst[index] = u;
                XMLTools.SaveListToXMLSerializer(lst, UsersPath);
            }
            else
                throw new DOException($"User named {u.Name} was not found");
        }

        public IEnumerable<User> GetSpecificUsers(Predicate<User> p)
        {
            List<User> lst = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);

            return from User u in lst
                   where p(u)
                   select u;
        }

        public IEnumerable<User> GetAllUsers()
        {
            List<User> lst = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);
            var List = from User x in lst
                        where x.IsExist
                        select x;
            if (List != null)
                return List;
            throw new DOException("No Users were found");
        }

        public void AddUser(User u)
        {
            List<User> lst = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);

            if (!lst.Exists(x => x.IsExist && x.Name == u.Name))
            {
                lst.Add(u);
                XMLTools.SaveListToXMLSerializer(lst, UsersPath);
            }
            else
                throw new DOException($"User named {u.Name} is already exist");
        }

        public void DeleteUser(string name)
        {
            List<User> lst = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);
            int index = lst.FindIndex(x => x.IsExist && x.Name == name);
            if (index > -1)
            {
                lst[index].IsExist = false;
                XMLTools.SaveListToXMLSerializer(lst, UsersPath);
            }
            else
                throw new DOException($"User named {name} was not found");
        }
        #endregion

    }
}
