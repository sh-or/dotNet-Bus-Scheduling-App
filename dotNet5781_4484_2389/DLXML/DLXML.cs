using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DLAPI;
using System.Xml.Linq;
using System.Device.Location;

namespace DL
{
    public class DLXML : IDAL
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { } // static c-tor
        DLXML() { } // private
        public static DLXML Instance { get => instance; }// for public use
        public static Random r = new Random(DateTime.Now.Millisecond);

        #endregion

        #region DS XML Files
        string BusesPath = @"BusesXml.xml"; //XElement
        string LineTripsPath = @"LineTripsXml.xml"; //XElement

        string BusStationsPath = @"BusStationsXml.xml"; //XMLSerializer
        string LineStationsPath = @"LineStationsXml.xml"; //XMLSerializer
        string LinesPath = @"LinesXml.xml"; //XMLSerializer
        string UsersPath = @"UserXml.xml"; //XMLSerializer
        string ConsecutiveStationsPath = @"ConsecutiveStationsXml.xml"; //XElement
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
                         Fuel = double.Parse(bus.Element("Fuel").Value),
                         KmFromLastRefuel = double.Parse(bus.Element("KmFromLastRefuel").Value),
                         KmFromLastCare = double.Parse(bus.Element("KmFromLastCare").Value),
                         DateOfLastCare = DateTime.Parse(bus.Element("DateOfLastCare").Value),
                         Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), bus.Element("Status").Value),
                         Driver = bus.Element("Driver").Value,
                         IsExist = bool.Parse(bus.Element("IsExist").Value)
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
                bs.Element("Fuel").Value = b.Fuel.ToString();
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
                               new XElement("Fuel", b.Fuel.ToString()),
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
        public IEnumerable<Bus> GetSpecificBuses(Predicate<Bus> p)
        {
            XElement BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);

            var ListBS = from bus in BusesRootElem.Elements()
                         let b = new Bus()
                         {
                             LicenseNumber = Int32.Parse(bus.Element("LicenseNumber").Value),
                             LicensingDate = DateTime.Parse(bus.Element("LicensingDate").Value),
                             Kilometerage = double.Parse(bus.Element("Kilometerage").Value),
                             Fuel = double.Parse(bus.Element("Fuel").Value),
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

            var ListBS = from bus in BusesRootElem.Elements()
                         where bool.Parse(bus.Element("IsExist").Value)
                         select new Bus()
                         {
                             LicenseNumber = Int32.Parse(bus.Element("LicenseNumber").Value),
                             LicensingDate = DateTime.Parse(bus.Element("LicensingDate").Value),
                             Fuel = double.Parse(bus.Element("Fuel").Value),
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
        #endregion

        #region LineTrip
        public LineTrip GetLineTrip(int _LineCode, TimeSpan _Start)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);

            LineTrip lt = (from x in LineTripsRootElem.Elements()
                           where int.Parse(x.Element("LineCode").Value) == _LineCode && TimeSpan.Parse(x.Element("Start").Value) == _Start
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
                      let lt = new LineTrip()
                      {
                          LineCode = Int32.Parse(x.Element("LineCode").Value),
                          Start = TimeSpan.Parse(x.Element("Start").Value),
                          IsExist = bool.Parse(x.Element("IsExist").Value),
                      }
                      where lt.IsExist && bool.Parse(l.Element("IsExist").Value)
                         && Int32.Parse(l.Element("StationCode").Value) == _StationCode
                         && lt.LineCode == Int32.Parse(l.Element("LineCode").Value)
                         && lt.Start <= _Start
                      select lt;
            //if (lst != null) want to return even null..
            return /*(IEnumerable<LineTrip>)*/lst;
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
                            where int.Parse(x.Element("LineCode").Value) == lt.LineCode
                            && TimeSpan.Parse(x.Element("Start").Value) == lt.Start
                            && bool.Parse(x.Element("IsExist").Value)
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
                throw new DOException($"Bus station number {_StationCode} was not found");
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
                         where p(bs) && bs.IsExist
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

            XElement XMLRunNumber = XElement.Load(@"XMLruningNums.xml");
            int RunNum = int.Parse(XMLRunNumber.Element("StationCode").Value);
            bs.StationCode = RunNum++;
            XMLRunNumber.Element("StationCode").Value = RunNum.ToString();
            XMLRunNumber.Save(@"XMLruningNums.xml");
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

            XElement XMLRunNumber = XElement.Load(@"XMLruningNums.xml");
            int RunNum = int.Parse(XMLRunNumber.Element("LineCode").Value);
            l.Code = RunNum++;
            XMLRunNumber.Element("LineCode").Value = RunNum.ToString();
            XMLRunNumber.Save(@"XMLruningNums.xml");
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
                   let x= GetLine(ls.LineCode)
                   where x.IsExist
                   select x;
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
            int ind = lst.FindIndex(x => x.IsExist && x.LineCode == _LineCode && x.StationCode == _StationCode);
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

            int n = lst.FindIndex(x => x.IsExist && x.StationCode == _StationCode && x.LineCode == _LineCode);
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
            int index = lst.FindIndex(x => x.IsExist && x.Name == u.Name);
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

        #region Consecutive Stations
        public ConsecutiveStations GetConsecutiveStations(int _StationCode1, int _StationCode2)
        {
            XElement csRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            ConsecutiveStations cs = (from c in csRootElem.Elements()
                     where int.Parse(c.Element("StationCode1").Value) == _StationCode1 
                     && int.Parse(c.Element("StationCode2").Value) == _StationCode2 
                     select new ConsecutiveStations()
                     {
                         StationCode1=_StationCode1,
                         StationCode2=_StationCode2,
                         Distance= double.Parse(c.Element("Distance").Value),
                         DriveTime=TimeSpan.Parse(c.Element("DriveTime").Value)
                     }
                    ).FirstOrDefault();
            if (cs != null)
                return cs;
            throw new DOException($"Station {_StationCode1} and station {_StationCode2} are not consecutive stations");
        }
        public void AddConsecutiveStations(ConsecutiveStations cs)
        {
            XElement csRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement coSt = (from c in csRootElem.Elements()
                             where int.Parse(c.Element("StationCode1").Value) == cs.StationCode1
                                                  && int.Parse(c.Element("StationCode2").Value) == cs.StationCode2
                             select c).FirstOrDefault();

            if (coSt == null)
            {
                XElement c = new XElement("ConsecutiveStations",
                     new XElement("StationCode1", cs.StationCode1.ToString()),
                     new XElement("StationCode2", cs.StationCode2.ToString()),
                     new XElement("Distance", cs.Distance.ToString()),
                     new XElement("DriveTime", cs.DriveTime.ToString()));

                csRootElem.Add(c);

                XMLTools.SaveListToXMLElement(csRootElem, ConsecutiveStationsPath);
            }
        }
        public void UpdateConsecutiveStations(ConsecutiveStations cs)
        {
            XElement csRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement coSt = (from c in csRootElem.Elements()
                             where int.Parse(c.Element("StationCode1").Value) == cs.StationCode1
                                                  && int.Parse(c.Element("StationCode2").Value) == cs.StationCode2
                             select c).FirstOrDefault();

            if (coSt != null)
            {
                coSt.Element("Distance").Value = cs.Distance.ToString();
                coSt.Element("DriveTime").Value = cs.DriveTime.ToString();

                XMLTools.SaveListToXMLElement(csRootElem, ConsecutiveStationsPath);
            }
            else
                throw new DOException($"Station {cs.StationCode1} and station {cs.StationCode2} are not consecutive stations");
        }

        public bool isExistConsecutiveStations(int _FirstStation, int _LastStation)
        {
            XElement csRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement coSt = (from c in csRootElem.Elements()
                             where int.Parse(c.Element("StationCode1").Value) == _FirstStation
                                                  && int.Parse(c.Element("StationCode2").Value) == _LastStation
                             select c).FirstOrDefault();

            return (coSt != null);
        }

        public void DeleteConsecutiveStations(int _StationCode1, int _StationCode2)
        {
            XElement csRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            XElement cs = (from c in csRootElem.Elements()
                           where int.Parse(c.Element("StationCode1").Value) == _StationCode1
                           && int.Parse(c.Element("StationCode2").Value) == _StationCode2
                           select c).FirstOrDefault();

            if (cs != null)
            {
                cs.Remove();
                XMLTools.SaveListToXMLElement(csRootElem, ConsecutiveStationsPath);
            }
            else
                throw new DOException($"Station {_StationCode1} and station {_StationCode2} are not consecutive");
        }

        public IEnumerable<ConsecutiveStations> GetSomeConsecutiveStations(int _StationCode)
        {
            XElement csRootElem = XMLTools.LoadListFromXMLElement(ConsecutiveStationsPath);

            return from c in csRootElem.Elements()
                   where int.Parse(c.Element("StationCode1").Value) == _StationCode
                   select new ConsecutiveStations()
                   {
                       StationCode1 = _StationCode,
                       StationCode2 = int.Parse(c.Element("StationCode2").Value),
                       Distance = double.Parse(c.Element("Distance").Value),
                       DriveTime = TimeSpan.Parse(c.Element("DriveTime").Value)
                   };
        }
        #endregion

        #region reset
        public void reset()
        {
            //    var AllBuses = new List<Bus>()
            //    {
            //        new Bus{LicenseNumber=12345678, LicensingDate =new DateTime(2020, 12, 29), Kilometerage=2500, KmFromLastRefuel=600, Fuel=(1200-600)/1200.0, KmFromLastCare=2500, DateOfLastCare=new DateTime(2020, 12, 29),  Status=(StatusEnum)1, Driver="Yosef", IsExist=true},
            //        new Bus{LicenseNumber=23456789, LicensingDate =new DateTime(2019, 1, 9), Kilometerage=125000, KmFromLastRefuel=850, Fuel=(1200-850)/1200.0, KmFromLastCare=19800, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)2, Driver="Shlomi", IsExist=true},
            //        new Bus{LicenseNumber=34567890, LicensingDate =new DateTime(2018, 10, 6), Kilometerage=34500, KmFromLastRefuel=0, Fuel=(1200-0)/1200.0, KmFromLastCare=4500, DateOfLastCare=new DateTime(2018, 10, 6),  Status=(StatusEnum)2, Driver="Yakov", IsExist=true},
            //        new Bus{LicenseNumber=45678901, LicensingDate =new DateTime(2018, 12, 27), Kilometerage=600, KmFromLastRefuel=600, Fuel=(1200-600)/1200.0, KmFromLastCare=600, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)1, Driver="Dan", IsExist=true},
            //        new Bus{LicenseNumber=56789012, LicensingDate =new DateTime(2019, 12, 27), Kilometerage=18000, KmFromLastRefuel=1150, Fuel=(1200-1150)/1200.0, KmFromLastCare=9000, DateOfLastCare=new DateTime(2020, 7, 24),  Status=(StatusEnum)3, Driver="Noam", IsExist=true},
            //        new Bus{LicenseNumber=1234567, LicensingDate =new DateTime(2012, 4, 27), Kilometerage=25600, KmFromLastRefuel=700, Fuel=(1200-700)/1200.0, KmFromLastCare=10777, DateOfLastCare=new DateTime(2020, 8, 14),  Status=(StatusEnum)1, Driver="Yosi", IsExist=true},
            //        new Bus{LicenseNumber=2345678, LicensingDate =new DateTime(2015, 8, 10), Kilometerage=62500, KmFromLastRefuel=100, Fuel=(1200-100)/1200.0, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 8, 14),  Status=(StatusEnum)1, Driver="Dina", IsExist=true},
            //        new Bus{LicenseNumber=3456789, LicensingDate =new DateTime(2015, 6, 9), Kilometerage=25009, KmFromLastRefuel=120, Fuel=(1200-120)/1200.0, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)1, Driver="Yair", IsExist=true},
            //        new Bus{LicenseNumber=4567890, LicensingDate =new DateTime(2016, 10, 23), Kilometerage=2500, KmFromLastRefuel=1190, Fuel=(1200-1190)/1200.0, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 7, 14),  Status=(StatusEnum)3, Driver="Ori", IsExist=true},
            //        new Bus{LicenseNumber=5678901, LicensingDate =new DateTime(2016, 10, 27), Kilometerage=25001, KmFromLastRefuel=1110, Fuel=(1200-1110)/1200.0, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 7, 17),  Status=(StatusEnum)3, Driver="Itamar", IsExist=true},
            //        new Bus{LicenseNumber=6789012, LicensingDate =new DateTime(2017, 4, 27), Kilometerage=25001, KmFromLastRefuel=100, Fuel=(1200-100)/1200.0, KmFromLastCare=14302, DateOfLastCare=new DateTime(2020, 7, 24),  Status=(StatusEnum)1, Driver="Ron", IsExist=true},
            //        new Bus{LicenseNumber=7890123, LicensingDate =new DateTime(2017, 7, 21), Kilometerage=25001, KmFromLastRefuel=180, Fuel=(1200-180)/1200.0, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 7, 26),  Status=(StatusEnum)1, Driver="Yosef", IsExist=true},
            //        new Bus{LicenseNumber=8901234, LicensingDate =new DateTime(2017, 10, 22), Kilometerage=25001, KmFromLastRefuel=10, Fuel=(1200-10)/1200.0, KmFromLastCare=1589, DateOfLastCare=new DateTime(2020, 9, 25),  Status=(StatusEnum)1, Driver="Dan", IsExist=true},
            //        new Bus{LicenseNumber=90123456, LicensingDate =new DateTime(2018, 11, 23), Kilometerage=25001, KmFromLastRefuel=800, Fuel=(1200-800)/1200.0, KmFromLastCare=1600, DateOfLastCare=new DateTime(2020, 6, 13),  Status=(StatusEnum)1, Driver="Moshe", IsExist=true},
            //        new Bus{LicenseNumber=67890123, LicensingDate =new DateTime(2019, 9, 27), Kilometerage=25001, KmFromLastRefuel=800, Fuel=(1200-800)/1200.0, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)1, Driver="Dan", IsExist=true},
            //        new Bus{LicenseNumber=78901234, LicensingDate =new DateTime(2019, 8, 27), Kilometerage=25200, KmFromLastRefuel=650, Fuel=(1200-650)/1200.0, KmFromLastCare=12000, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)1, Driver="Aharon", IsExist=true},
            //        new Bus{LicenseNumber=89012345, LicensingDate =new DateTime(2019, 7, 27), Kilometerage=24001, KmFromLastRefuel=567, Fuel=(1200-567)/1200.0, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 6, 14),  Status=(StatusEnum)1, Driver="Dvir", IsExist=true},
            //        new Bus{LicenseNumber=90123456, LicensingDate =new DateTime(2019, 6, 25), Kilometerage=35001, KmFromLastRefuel=586, Fuel=(1200-586)/1200.0, KmFromLastCare=1500, DateOfLastCare=new DateTime(2020, 5, 7),  Status=(StatusEnum)1, Driver="Yosef", IsExist=true},
            //        new Bus{LicenseNumber=98765432, LicensingDate =new DateTime(2019, 5, 24), Kilometerage=27005, KmFromLastRefuel=543, Fuel=(1200-543)/1200.0, KmFromLastCare=19800, DateOfLastCare=new DateTime(2020, 5, 4),  Status=(StatusEnum)2, Driver="Yehuda", IsExist=true},
            //        new Bus{LicenseNumber=87654321, LicensingDate =new DateTime(2019, 4, 23), Kilometerage=25009, KmFromLastRefuel=234, Fuel=(1200-234)/1200.0, KmFromLastCare=19800, DateOfLastCare=new DateTime(2020, 4, 19),  Status=(StatusEnum)2, Driver="Yoni", IsExist=true},
            //        new Bus{LicenseNumber=76543210, LicensingDate =new DateTime(2019, 3, 8), Kilometerage=25099, KmFromLastRefuel=1180, Fuel=(1200-1180)/1200.0, KmFromLastCare=11800, DateOfLastCare=new DateTime(2020, 3, 22),  Status=(StatusEnum)3, Driver="Michael", IsExist=true},

            //    };

            //    var AllLines = new List<Line>()
            //                {
            //                    //10 lines with 10 stations
            //                    new Line{Code= 1 , BusLine=10, Area=(AreaEnum)1, FirstStation=1, LastStation=3, IsExist=true },
            //                    new Line{Code=  2, BusLine=23, Area=(AreaEnum)1, FirstStation=2, LastStation=3, IsExist=true },
            //                    new Line{Code=3  , BusLine=21, Area=(AreaEnum)1, FirstStation=4, LastStation=5, IsExist=true },
            //                    new Line{Code= 4 , BusLine=1, Area=(AreaEnum)1, FirstStation=6, LastStation=7, IsExist=true },
            //                    new Line{Code=  5, BusLine=2, Area=(AreaEnum)1, FirstStation=43, LastStation=5, IsExist=true },
            //                    new Line{Code=6  , BusLine=17, Area=(AreaEnum)2, FirstStation=21, LastStation=47, IsExist=true },
            //                    new Line{Code= 7 , BusLine=3, Area=(AreaEnum)2, FirstStation=37, LastStation=1, IsExist=true },
            //                    new Line{Code=  8, BusLine=4, Area=(AreaEnum)2, FirstStation=1, LastStation=3, IsExist=true },
            //                    new Line{Code=9  , BusLine=5, Area=(AreaEnum)2, FirstStation=1, LastStation=3, IsExist=true },
            //                    new Line{Code= 10 , BusLine=464, Area=(AreaEnum)2, FirstStation=9, LastStation=4, IsExist=true },
            //                    //

            //                    new Line{Code=11  , BusLine=567, Area=(AreaEnum)3, FirstStation=16, LastStation=5, IsExist=true },
            //                    new Line{Code=  12, BusLine=543, Area=(AreaEnum)3, FirstStation=13, LastStation=6, IsExist=true },
            //                    new Line{Code=13  , BusLine=6, Area=(AreaEnum)3, FirstStation=6, LastStation=7, IsExist=true },
            //                    new Line{Code=  14, BusLine=7, Area=(AreaEnum)3, FirstStation=26, LastStation=8, IsExist=true },
            //                    new Line{Code=15  , BusLine=8, Area=(AreaEnum)3, FirstStation=27, LastStation=21, IsExist=true },
            //                    new Line{Code= 16 , BusLine=464, Area=(AreaEnum)4, FirstStation=23, LastStation=1, IsExist=true },
            //                    new Line{Code=17  , BusLine=3, Area=(AreaEnum)4, FirstStation=28, LastStation=24, IsExist=true },
            //                    new Line{Code=  18, BusLine=541, Area=(AreaEnum)4, FirstStation=9, LastStation=28, IsExist=true },
            //                    new Line{Code=19  , BusLine=54, Area=(AreaEnum)4, FirstStation=18, LastStation=27, IsExist=true },
            //                    new Line{Code=  20, BusLine=1, Area=(AreaEnum)4, FirstStation=19, LastStation=5, IsExist=true },
            //                    new Line{Code=21  , BusLine=24, Area=(AreaEnum)5, FirstStation=4, LastStation=2, IsExist=true },
            //                    new Line{Code=  22, BusLine=679, Area=(AreaEnum)5, FirstStation=4, LastStation=5, IsExist=true },
            //                    new Line{Code=23  , BusLine=23, Area=(AreaEnum)5, FirstStation=37, LastStation=34, IsExist=true },
            //                    new Line{Code=  24, BusLine=564, Area=(AreaEnum)5, FirstStation=39, LastStation=23, IsExist=true },
            //                    new Line{Code=25  , BusLine=9, Area=(AreaEnum)5, FirstStation=9, LastStation=1, IsExist=true }
            //                };

            //    var AllBusStations = new List<BusStation>()
            //                {

            //                    new BusStation{StationCode=1  ,   IsExist=true, Latitude=32.183921, Longitude=34.917806, Name="Ben Yehuda", Address="Ben Yehuda 12", Accessibility=true},
            //                    new BusStation{StationCode= 2 ,   IsExist=true, Latitude=31.870034, Longitude=34.819541, Name="Herzel", Address="Herzel 12", Accessibility=true},
            //                    new BusStation{StationCode=  3,   IsExist=true, Latitude=31.984553, Longitude=34.782828, Name="shabazi", Address="shabazi 3", Accessibility=true},
            //                    new BusStation{StationCode=4  ,   IsExist=true, Latitude=31.888550, Longitude=34.7909040, Name="Bar Lev", Address="Bar Lev 43", Accessibility=true},
            //                    new BusStation{StationCode= 5 ,   IsExist=true, Latitude=31.914255, Longitude=34.807944, Name="Rakefet", Address="Rakefet 21", Accessibility=true},
            //                    new BusStation{StationCode=  6,   IsExist=true, Latitude=32.026047, Longitude=34.807561, Name="hagefen", Address="hagefen 1", Accessibility=true},
            //                    new BusStation{StationCode=7  ,   IsExist=true, Latitude=32.425697, Longitude=35.033724, Name="Kanaf", Address="Kanaf 23", Accessibility=true},
            //                    new BusStation{StationCode= 8 ,   IsExist=true, Latitude=32.422627, Longitude=34.920835, Name="Halevy", Address="Halevy 5", Accessibility=true},
            //                    new BusStation{StationCode=  9,   IsExist=true, Latitude=32.425167, Longitude=35.035408, Name="Trumpeldor", Address="Trumpeldor 78", Accessibility=true},
            //                    new BusStation{StationCode=10  ,   IsExist=true, Latitude=32.421197, Longitude=35.040302, Name="Kats", Address="Kats 5", Accessibility=true},
            //                    new BusStation{StationCode=  11,   IsExist=true, Latitude=32.442103, Longitude=35.047091, Name="Rotshild", Address="Rotshild 4", Accessibility=true},
            //                    new BusStation{StationCode=12  ,   IsExist=true, Latitude=32.560804, Longitude=34.961128, Name="Hazait", Address="Hazait 23", Accessibility=true},
            //                    new BusStation{StationCode=  13,   IsExist=true, Latitude=32.561361, Longitude=34.958195, Name="Hapardes", Address="Hapardes 32", Accessibility=true},
            //                    new BusStation{StationCode=14  ,   IsExist=true, Latitude=33.013069, Longitude=35.102114, Name="Perah", Address="Perah 1", Accessibility=true},
            //                    new BusStation{StationCode=  15,   IsExist=true, Latitude=33.074448, Longitude=35.295265, Name="Hagana", Address="Hagana 2", Accessibility=true},
            //                    new BusStation{StationCode=16  ,   IsExist=true, Latitude=33.007998, Longitude=35.270537, Name="Dvir", Address="Dvir 34", Accessibility=true},
            //                    new BusStation{StationCode=  17,   IsExist=true, Latitude=33.006947, Longitude=35.281855, Name="Ariel", Address="Ariel 21", Accessibility=true},
            //                    new BusStation{StationCode=18  ,   IsExist=true, Latitude=32.976412, Longitude=35.504043, Name="Hagibor", Address="Hagibor 46", Accessibility=true},
            //                    new BusStation{StationCode=  19,   IsExist=true, Latitude=33.020204, Longitude=35.148256, Name="Truman", Address="Truman 56", Accessibility=true},
            //                    new BusStation{StationCode=20  ,   IsExist=true, Latitude=32.937562, Longitude=35.361183, Name="Karny", Address="Karny 24", Accessibility=true},
            //                    new BusStation{StationCode=  21,   IsExist=true, Latitude=33.026994, Longitude=34.910842, Name="Roman", Address="Roman 81", Accessibility=true},
            //                    new BusStation{StationCode=22  ,   IsExist=true, Latitude=32.059086, Longitude=34.708337, Name="Hagefen", Address="Hagefen 13", Accessibility=true},
            //                    new BusStation{StationCode=  23,   IsExist=true, Latitude=32.072615, Longitude=34.791077, Name="Pilon", Address="Pilon 14", Accessibility=true},
            //                    new BusStation{StationCode=24  ,   IsExist=true, Latitude=31.398358, Longitude=34.746489, Name="Parpar", Address="Parpar 15", Accessibility=true},
            //                    new BusStation{StationCode=  25,   IsExist=true, Latitude=31.397747, Longitude=34.746050, Name="HaEtzel", Address="HaEtzel 26", Accessibility=true},
            //                    new BusStation{StationCode=26  ,   IsExist=true, Latitude=31.395101, Longitude=34.747424, Name="Doron", Address="Doron 14", Accessibility=true},
            //                    new BusStation{StationCode=  27,   IsExist=true, Latitude=31.392122, Longitude=34.748650, Name="Armon", Address="Armon 12", Accessibility=true},
            //                    new BusStation{StationCode=28  ,   IsExist=true, Latitude=31.394065, Longitude=34.748791, Name="Hamigdal", Address="Hamigdal 12", Accessibility=true},
            //                    new BusStation{StationCode=  29,   IsExist=true, Latitude=32.026119, Longitude=34.743063, Name="Turki", Address="Turki 14", Accessibility=true},
            //                    new BusStation{StationCode=30  ,   IsExist=true, Latitude=31.735511, Longitude=34.749105, Name="kanion", Address="kanion 32", Accessibility=true},
            //                    new BusStation{StationCode=  31,   IsExist=true, Latitude=31.735434, Longitude=34.749583, Name="Rubi", Address="Rubi 31", Accessibility=true},
            //                    new BusStation{StationCode= 32 ,   IsExist=true, Latitude=31.711082, Longitude=34.744586, Name="HaMelech", Address="HaMelech 13", Accessibility=true},
            //                    new BusStation{StationCode= 33 ,   IsExist=true, Latitude=31.712156, Longitude=34.739468, Name="Donald", Address="Donald 34", Accessibility=true},
            //                    new BusStation{StationCode= 34 ,   IsExist=true, Latitude=31.716725, Longitude=34.732568, Name="Jonathan", Address="Jonathan 32", Accessibility=true},
            //                    new BusStation{StationCode=35  ,   IsExist=true, Latitude=32.910362, Longitude=35.299742, Name="Dvora", Address="Dvora 23", Accessibility=true},
            //                    new BusStation{StationCode=  36,   IsExist=true, Latitude=32.913282, Longitude=35.301922, Name="Mapal", Address="Mapal 49", Accessibility=true},
            //                    new BusStation{StationCode=37  ,   IsExist=true, Latitude=33.026696, Longitude=35.250267, Name="Kineret", Address="Kineret 8", Accessibility=true},
            //                    new BusStation{StationCode=  38,   IsExist=true, Latitude=33.025094, Longitude=35.252544, Name="Anilevich", Address="Anilevich 7", Accessibility=true},
            //                    new BusStation{StationCode=39  ,   IsExist=true, Latitude=32.927907, Longitude=35.319294, Name="Hashomer", Address="Hashomer 23", Accessibility=true},
            //                    new BusStation{StationCode=  40,   IsExist=true, Latitude=33.006806, Longitude=35.259478, Name="Hayezira", Address="Hayezira 67", Accessibility=true},
            //                    new BusStation{StationCode=41  ,   IsExist=true, Latitude=32.833388, Longitude=35.247594, Name="Sapir", Address="Sapir 5", Accessibility=true},
            //                    new BusStation{StationCode=  42,   IsExist=true, Latitude=32.837056, Longitude=35.24829, Name="Shmork", Address="Shmork 28", Accessibility=false},
            //                    new BusStation{StationCode=43  ,   IsExist=true, Latitude=32.047463, Longitude=34.864658, Name="Nachum", Address="Nachum 38", Accessibility=false},
            //                    new BusStation{StationCode=  44,   IsExist=true, Latitude=32.460728, Longitude=35.054007, Name="Golomb", Address="Golomb 18", Accessibility=false},
            //                    new BusStation{StationCode=45  ,   IsExist=true, Latitude=32.461191, Longitude=35.053399, Name="Alon", Address="Alon 49", Accessibility=false},
            //                    new BusStation{StationCode=  46,   IsExist=true, Latitude=32.461896, Longitude=35.053225, Name="Shpigelman", Address="Shpigelman 37", Accessibility=false},
            //                    new BusStation{StationCode=47  ,   IsExist=true, Latitude=33.086246, Longitude=35.225712, Name="Hartum", Address="Hartum 5", Accessibility=false},
            //                    new BusStation{StationCode=  48,   IsExist=true, Latitude=33.088012, Longitude=35.224534, Name="Hayesod", Address="Hayesod 8", Accessibility=false},
            //                    new BusStation{StationCode=49  ,   IsExist=true, Latitude=33.087206, Longitude=35.227705, Name="Rotem", Address="Rotem 10", Accessibility=false},
            //                    new BusStation{StationCode=  50,   IsExist=true, Latitude=33.085835, Longitude=35.221639, Name="Savion", Address="Savion 4", Accessibility=false},
            //                    new BusStation{StationCode= 51 ,   IsExist=true, Latitude=32.455216, Longitude=35.055688, Name="Agamim", Address="Agamim 19", Accessibility=false},
            //                };

            //    var AllLineStations = new List<LineStation>()
            //                {
            //                    #region
            //                    new LineStation{LineCode=11, StationCode=6, StationNumberInLine=0, IsExist=true},
            //                    new LineStation{LineCode=12, StationCode=13, StationNumberInLine=0,  IsExist=true},
            //                    new LineStation{LineCode=13, StationCode=6, StationNumberInLine=0  ,IsExist=true},
            //                    new LineStation{LineCode=14, StationCode=26, StationNumberInLine=0  ,IsExist=true},
            //                    new LineStation{LineCode=15, StationCode=27, StationNumberInLine=0,  IsExist=true},
            //                    new LineStation{LineCode=16, StationCode=23, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=17, StationCode=28, StationNumberInLine=0  ,IsExist=true},
            //                    new LineStation{LineCode=18, StationCode=9, StationNumberInLine=0,  IsExist=true},
            //                    new LineStation{LineCode=19, StationCode=18, StationNumberInLine=0,  IsExist=true},
            //                    new LineStation{LineCode=20, StationCode=19, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=21, StationCode=4, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=22, StationCode=4, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=23, StationCode=37, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=24, StationCode=39, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=25, StationCode=9, StationNumberInLine=0 , IsExist=true},

            //                    new LineStation{LineCode=11, StationCode=5, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=12, StationCode=6, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=13, StationCode=7, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=14, StationCode=8, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=15, StationCode=21, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=16, StationCode=1, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=17, StationCode=24, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=18, StationCode=28, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=19, StationCode=27, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=20, StationCode=5, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=21, StationCode=2, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=22, StationCode=5, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=23, StationCode=34, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=24, StationCode=23, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=25, StationCode=1, StationNumberInLine=1 , IsExist=true},
            //    #endregion
            //                    #region
            //                    new LineStation{LineCode=1, StationCode=1, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=2, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=4, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=6, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=43, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=21, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=37, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=1, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=1, StationNumberInLine=0 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=9, StationNumberInLine=0 , IsExist=true},
            //                    //first station

            //                    new LineStation{LineCode=1, StationCode=39, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=39, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=39, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=39, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=39, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=39, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=39, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=35, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=35, StationNumberInLine=1 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=35, StationNumberInLine=1 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=8, StationNumberInLine=2 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=8, StationNumberInLine=2 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=8, StationNumberInLine=2 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=8, StationNumberInLine=2 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=8, StationNumberInLine=2 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=7, StationNumberInLine=2 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=7, StationNumberInLine=2 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=7, StationNumberInLine=2 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=7, StationNumberInLine=2 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=7, StationNumberInLine=2 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=9, StationNumberInLine=3 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=10, StationNumberInLine=3 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=11, StationNumberInLine=3 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=12, StationNumberInLine=3 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=13, StationNumberInLine=3 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=14, StationNumberInLine=3 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=15, StationNumberInLine=3 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=16, StationNumberInLine=3 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=17, StationNumberInLine=3 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=18, StationNumberInLine=3 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=30, StationNumberInLine=4 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=31, StationNumberInLine=4 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=32, StationNumberInLine=4 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=33, StationNumberInLine=4 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=34, StationNumberInLine=4 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=35, StationNumberInLine=4 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=36, StationNumberInLine=4 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=37, StationNumberInLine=4 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=38, StationNumberInLine=4 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=39, StationNumberInLine=4 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=35, StationNumberInLine=5 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=34, StationNumberInLine=5 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=33, StationNumberInLine=5 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=32, StationNumberInLine=5 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=31, StationNumberInLine=5 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=30, StationNumberInLine=5 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=35, StationNumberInLine=5 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=34, StationNumberInLine=5 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=33, StationNumberInLine=5 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=32, StationNumberInLine=5 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=20, StationNumberInLine=6 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=20, StationNumberInLine=6 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=20, StationNumberInLine=6 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=20, StationNumberInLine=6 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=20, StationNumberInLine=6 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=22, StationNumberInLine=6 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=22, StationNumberInLine=6 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=22, StationNumberInLine=6 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=22, StationNumberInLine=6 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=22, StationNumberInLine=6 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=25, StationNumberInLine=7 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=25, StationNumberInLine=7 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=25, StationNumberInLine=7 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=26, StationNumberInLine=7 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=26, StationNumberInLine=7 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=26, StationNumberInLine=7 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=27, StationNumberInLine=7 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=27, StationNumberInLine=7 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=27, StationNumberInLine=7 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=27, StationNumberInLine=7 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=26, StationNumberInLine=8 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=26, StationNumberInLine=8 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=26, StationNumberInLine=8 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=27, StationNumberInLine=8 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=27, StationNumberInLine=8 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=27, StationNumberInLine=8 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=25, StationNumberInLine=8 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=25, StationNumberInLine=8 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=25, StationNumberInLine=8 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=25, StationNumberInLine=8 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=28, StationNumberInLine=9 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=28, StationNumberInLine=9 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=28, StationNumberInLine=9 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=28, StationNumberInLine=9 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=28, StationNumberInLine=9 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=28, StationNumberInLine=9 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=28, StationNumberInLine=9 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=28, StationNumberInLine=9 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=28, StationNumberInLine=9 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=28, StationNumberInLine=9 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=2, StationNumberInLine=10 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=48, StationNumberInLine=10 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=48, StationNumberInLine=10 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=1, StationNumberInLine=10 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=1, StationNumberInLine=10 , IsExist=true},
            //                    new LineStation{LineCode=6, StationCode=47, StationNumberInLine=10 , IsExist=true},
            //                    new LineStation{LineCode=7, StationCode=1, StationNumberInLine=10 , IsExist=true},
            //                    new LineStation{LineCode=8, StationCode=3, StationNumberInLine=10 , IsExist=true},
            //                    new LineStation{LineCode=9, StationCode=3, StationNumberInLine=10 , IsExist=true},
            //                    new LineStation{LineCode=10, StationCode=4, StationNumberInLine=10 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=40, StationNumberInLine=11 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=40, StationNumberInLine=11 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=40, StationNumberInLine=11 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=41, StationNumberInLine=11 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=41, StationNumberInLine=11 , IsExist=true},

            //                    new LineStation{LineCode=1, StationCode=3, StationNumberInLine=12 , IsExist=true},
            //                    new LineStation{LineCode=2, StationCode=3, StationNumberInLine=12 , IsExist=true},
            //                    new LineStation{LineCode=3, StationCode=5, StationNumberInLine=12 , IsExist=true},
            //                    new LineStation{LineCode=4, StationCode=7, StationNumberInLine=12 , IsExist=true},
            //                    new LineStation{LineCode=5, StationCode=5, StationNumberInLine=12 , IsExist=true},
            //    #endregion
            // 
            //List < ConsecutiveStations > AllConsecutiveStations=new List<ConsecutiveStations>();
            //foreach (Line l in GetAllLines())
            //{
            //    List<BusStation> lst = GetStationsOfLine(l.Code).ToList();
            //    for (int i = 1; i < lst.Count; i++)
            //    {
            //        //AllConsecutiveStations.Add(new ConsecutiveStations
            //        //{
            //        //    StationCode1 = lst[i - 1].StationCode,
            //        //    StationCode2 = lst[i].StationCode,
            //        //    Distance = csDistance(lst[i - 1], lst[i]),
            //        //    DriveTime = csDt(csDistance(lst[i - 1], lst[i]))
            //        //});
            //        AddConsecutiveStations(new ConsecutiveStations
            //        {
            //            StationCode1 = lst[i - 1].StationCode,
            //            StationCode2 = lst[i].StationCode,
            //            Distance = csDistance(lst[i - 1], lst[i]),
            //            DriveTime = csDt(csDistance(lst[i - 1], lst[i]))
            //        });
            //    }
            //}

            //List<ConsecutiveStations> AllConsecutiveStations = (from Line l in GetAllLines()
            //                                                    from LineStation st in GetAllLineStations(l.Code)
            //                                                    where st.StationNumberInLine!=0
            //                                                    select new ConsecutiveStations
            //                                                    {
            //                                                        StationCode1 = bs1.StationCode,
            //                                                        StationCode2 = bs2.StationCode,
            //                                                        Distance = csDistance(bs1, bs2),
            //                                                        DriveTime = csDt(csDistance(bs1, bs2))
            //                                                    }).ToList();

            //    var AllUsers = new List<User>
            //                {
            //                    new User{ IsExist=true, Name="m", Password="m", IsManager=true}, //example
            //                    new User{ IsExist=true, Name="u", Password="u", IsManager=false}, //example
            //                    new User{ IsExist=true, Name="AAAA", Password="aaaa1111", IsManager=true},
            //                    new User{ IsExist=true, Name="aaaa", Password="aaaa2222", IsManager=true},
            //                    new User{ IsExist=true, Name="bbbb", Password="bbbb2222", IsManager=false},
            //                    new User{ IsExist=true, Name="cccc", Password="cccc3333", IsManager=false},
            //                    new User{ IsExist=true, Name="dddd", Password="dddd4444", IsManager=false},
            //                    new User{ IsExist=true, Name="eeee", Password="eeee5555", IsManager=false},
            //                    new User{ IsExist=true, Name="ffff", Password="ffff6666", IsManager=false},
            //                    new User{ IsExist=true, Name="gggg", Password="gggg7777", IsManager=false}
            //                };

            var AllLinesTrip = new List<LineTrip>
                                {
                                    new LineTrip{IsExist=true, LineCode=1, Start=new TimeSpan(08,00,00) },
                                    new LineTrip{IsExist=true, LineCode=1, Start=new TimeSpan(08,08,08) },
                                    new LineTrip{IsExist=true, LineCode=1, Start=new TimeSpan(08,15,15) },
                                    new LineTrip{IsExist=true, LineCode=1, Start=new TimeSpan(00,15,00) },
                                    new LineTrip{IsExist=true, LineCode=1, Start=new TimeSpan(09,12,00) },
                                    new LineTrip{IsExist=true, LineCode=1, Start=new TimeSpan(08,00,00) },
                                    new LineTrip{IsExist=true, LineCode=2, Start=new TimeSpan(08,00,00) },
                                    new LineTrip{IsExist=true, LineCode=2, Start=new TimeSpan(09,50,00) },
                                    new LineTrip{IsExist=true, LineCode=2, Start=new TimeSpan(13,15,00) },
                                    new LineTrip{IsExist=true, LineCode=2, Start=new TimeSpan(13,10,00) },
                                    new LineTrip{IsExist=true, LineCode=2, Start=new TimeSpan(16,30,00) },
                                    new LineTrip{IsExist=true, LineCode=2, Start=new TimeSpan(17,30,00) },
                                    new LineTrip{IsExist=true, LineCode=3, Start=new TimeSpan(09,00,00) },
                                    new LineTrip{IsExist=true, LineCode=3, Start=new TimeSpan(08,00,00) },
                                    new LineTrip{IsExist=true, LineCode=3, Start=new TimeSpan(13,10,00) },
                                    new LineTrip{IsExist=true, LineCode=3, Start=new TimeSpan(13,20,00) },
                                    new LineTrip{IsExist=true, LineCode=3, Start=new TimeSpan(13,30,00) },
                                    new LineTrip{IsExist=true, LineCode=3, Start=new TimeSpan(13,15,00) },
                                };
            foreach (LineTrip lt in AllLinesTrip)
                UpdateLineTrip(lt,lt.Start);

            //    //XMLTools.SaveListToXMLSerializer(AllBuses, BusesPath);
            //    XMLTools.SaveListToXMLSerializer(AllLines, LinesPath);
            //    XMLTools.SaveListToXMLSerializer(AllBusStations, BusStationsPath);
            //    XMLTools.SaveListToXMLSerializer(AllLineStations, LineStationsPath);
            //XMLTools.SaveListToXMLSerializer(AllConsecutiveStations, ConsecutiveStationsPath);
            //    XMLTools.SaveListToXMLSerializer(AllUsers, UsersPath);
            //    XMLTools.SaveListToXMLSerializer(AllLinesTrip, LineTripsPath);

        }

        public static double csDistance(BusStation bs1, BusStation bs2) //return distance between 2 stations
        {
            GeoCoordinate loc1 = new GeoCoordinate(bs1.Latitude, bs1.Longitude);
            GeoCoordinate loc2 = new GeoCoordinate(bs2.Latitude, bs2.Longitude);
            return loc1.GetDistanceTo(loc2) * (1 + r.NextDouble() / 2); //air-distance(in meters)*(1 to 1.5)
        }
        public static TimeSpan csDt(double distance) //return the drive time according to the given distance
        {
            return TimeSpan.FromSeconds(distance / (r.Next(50, 70) * 1 / 3.6));
        }

        #endregion //reset the XML files data(using one time!)

    }
}
