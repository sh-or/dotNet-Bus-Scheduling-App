
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Device.Location;



namespace DS
{
    public static class DataSource
    {
        public static Random r = new Random(DateTime.Now.Millisecond);

        public static List<Bus> AllBuses;
        public static List<Line> AllLines;
        public static List<BusStation> AllBusStations;
        public static IEnumerable<ConsecutiveStations> AllConsecutiveStations;
        public static List<LineStation> AllLineStations;

        static DataSource() //c-tor of DS
        {
            AllBuses = new List<Bus>()
            {
                new Bus{LicenseNumber=12345678, LicensingDate =new DateTime(2020, 12, 29), Kilometerage=2500, KmFromLastRefuel=600, Fuel=(1200-600)/1200, KmFromLastCare=2500, DateOfLastCare=new DateTime(2020, 12, 29),  Status=(StatusEnum)1, Driver="Yosef", IsExist=true},
                new Bus{LicenseNumber=23456789, LicensingDate =new DateTime(2019, 1, 9), Kilometerage=125000, KmFromLastRefuel=850, Fuel=(1200-850)/1200, KmFromLastCare=19800, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)2, Driver="Shlomi", IsExist=true},
                new Bus{LicenseNumber=34567890, LicensingDate =new DateTime(2018, 10, 6), Kilometerage=34500, KmFromLastRefuel=0, Fuel=(1200-0)/1200, KmFromLastCare=34500, DateOfLastCare=new DateTime(2018, 10, 6),  Status=(StatusEnum)2, Driver="Yakov", IsExist=true},
                new Bus{LicenseNumber=45678901, LicensingDate =new DateTime(2018, 12, 27), Kilometerage=600, KmFromLastRefuel=600, Fuel=(1200-600)/1200, KmFromLastCare=600, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)1, Driver="Dan", IsExist=true},
                new Bus{LicenseNumber=56789012, LicensingDate =new DateTime(2019, 12, 27), Kilometerage=18000, KmFromLastRefuel=1150, Fuel=(1200-1150)/1200, KmFromLastCare=9000, DateOfLastCare=new DateTime(2020, 7, 24),  Status=(StatusEnum)3, Driver="Noam", IsExist=true},
                new Bus{LicenseNumber=1234567, LicensingDate =new DateTime(2012, 4, 27), Kilometerage=25600, KmFromLastRefuel=700, Fuel=(1200-700)/1200, KmFromLastCare=10777, DateOfLastCare=new DateTime(2020, 8, 14),  Status=(StatusEnum)1, Driver="Yosi", IsExist=true},
                new Bus{LicenseNumber=2345678, LicensingDate =new DateTime(2015, 8, 10), Kilometerage=62500, KmFromLastRefuel=100, Fuel=(1200-100)/1200, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 8, 14),  Status=(StatusEnum)1, Driver="Dina", IsExist=true},
                new Bus{LicenseNumber=3456789, LicensingDate =new DateTime(2015, 6, 9), Kilometerage=25009, KmFromLastRefuel=120, Fuel=(1200-120)/1200, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)1, Driver="Yair", IsExist=true},
                new Bus{LicenseNumber=4567890, LicensingDate =new DateTime(2016, 10, 23), Kilometerage=2500, KmFromLastRefuel=1190, Fuel=(1200-1190)/1200, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 7, 14),  Status=(StatusEnum)3, Driver="Ori", IsExist=true},
                new Bus{LicenseNumber=5678901, LicensingDate =new DateTime(2016, 10, 27), Kilometerage=25001, KmFromLastRefuel=1110, Fuel=(1200-1110)/1200, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 7, 17),  Status=(StatusEnum)3, Driver="Itamar", IsExist=true},
                new Bus{LicenseNumber=6789012, LicensingDate =new DateTime(2017, 4, 27), Kilometerage=25001, KmFromLastRefuel=100, Fuel=(1200-100)/1200, KmFromLastCare=14302, DateOfLastCare=new DateTime(2020, 7, 24),  Status=(StatusEnum)1, Driver="Ron", IsExist=true},
                new Bus{LicenseNumber=7890123, LicensingDate =new DateTime(2017, 7, 21), Kilometerage=25001, KmFromLastRefuel=180, Fuel=(1200-180)/1200, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 7, 26),  Status=(StatusEnum)1, Driver="Yosef", IsExist=true},
                new Bus{LicenseNumber=8901234, LicensingDate =new DateTime(2017, 10, 22), Kilometerage=25001, KmFromLastRefuel=10, Fuel=(1200-10)/1200, KmFromLastCare=1589, DateOfLastCare=new DateTime(2020, 9, 25),  Status=(StatusEnum)1, Driver="Dan", IsExist=true},
                new Bus{LicenseNumber=90123456, LicensingDate =new DateTime(2018, 11, 23), Kilometerage=25001, KmFromLastRefuel=800, Fuel=(1200-800)/1200, KmFromLastCare=1600, DateOfLastCare=new DateTime(2020, 6, 13),  Status=(StatusEnum)1, Driver="Moshe", IsExist=true},
                new Bus{LicenseNumber=67890123, LicensingDate =new DateTime(2019, 9, 27), Kilometerage=25001, KmFromLastRefuel=800, Fuel=(1200-800)/1200, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)1, Driver="Dan", IsExist=true},
                new Bus{LicenseNumber=78901234, LicensingDate =new DateTime(2019, 8, 27), Kilometerage=25200, KmFromLastRefuel=650, Fuel=(1200-650)/1200, KmFromLastCare=12000, DateOfLastCare=new DateTime(2020, 9, 14),  Status=(StatusEnum)1, Driver="Aharon", IsExist=true},
                new Bus{LicenseNumber=89012345, LicensingDate =new DateTime(2019, 7, 27), Kilometerage=24001, KmFromLastRefuel=567, Fuel=(1200-567)/1200, KmFromLastCare=1300, DateOfLastCare=new DateTime(2020, 6, 14),  Status=(StatusEnum)1, Driver="Dvir", IsExist=true},
                new Bus{LicenseNumber=90123456, LicensingDate =new DateTime(2019, 6, 25), Kilometerage=35001, KmFromLastRefuel=586, Fuel=(1200-586)/1200, KmFromLastCare=1500, DateOfLastCare=new DateTime(2020, 5, 7),  Status=(StatusEnum)1, Driver="Yosef", IsExist=true},
                new Bus{LicenseNumber=98765432, LicensingDate =new DateTime(2019, 5, 24), Kilometerage=27005, KmFromLastRefuel=543, Fuel=(1200-543)/1200, KmFromLastCare=19800, DateOfLastCare=new DateTime(2020, 5, 4),  Status=(StatusEnum)2, Driver="Yehuda", IsExist=true},
                new Bus{LicenseNumber=87654321, LicensingDate =new DateTime(2019, 4, 23), Kilometerage=25009, KmFromLastRefuel=234, Fuel=(1200-234)/1200, KmFromLastCare=19800, DateOfLastCare=new DateTime(2020, 4, 19),  Status=(StatusEnum)2, Driver="Yoni", IsExist=true},
                new Bus{LicenseNumber=76543210, LicensingDate =new DateTime(2019, 3, 8), Kilometerage=25099, KmFromLastRefuel=1180, Fuel=(1200-1180)/1200, KmFromLastCare=11800, DateOfLastCare=new DateTime(2020, 3, 22),  Status=(StatusEnum)3, Driver="Michael", IsExist=true},

            };

            AllLines = new List<Line>()
            {
                //10 lines with 10 stations
                new Line{Code=ConfigurationClass.LineCode, BusLine=10, Area=(AreaEnum)1, FirstStation=1, LastStation=3, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=23, Area=(AreaEnum)1, FirstStation=2, LastStation=3, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=21, Area=(AreaEnum)1, FirstStation=4, LastStation=5, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=1, Area=(AreaEnum)1, FirstStation=6, LastStation=7, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=2, Area=(AreaEnum)1, FirstStation=43, LastStation=5, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=17, Area=(AreaEnum)2, FirstStation=21, LastStation=47, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=3, Area=(AreaEnum)2, FirstStation=37, LastStation=1, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=4, Area=(AreaEnum)2, FirstStation=1, LastStation=3, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=5, Area=(AreaEnum)2, FirstStation=1, LastStation=3, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=464, Area=(AreaEnum)2, FirstStation=9, LastStation=4, IsExist=true },
                //
                
                new Line{Code=ConfigurationClass.LineCode, BusLine=567, Area=(AreaEnum)3, FirstStation=16, LastStation=5, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=543, Area=(AreaEnum)3, FirstStation=13, LastStation=6, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=6, Area=(AreaEnum)3, FirstStation=6, LastStation=7, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=7, Area=(AreaEnum)3, FirstStation=26, LastStation=8, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=8, Area=(AreaEnum)3, FirstStation=27, LastStation=21, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=464, Area=(AreaEnum)4, FirstStation=23, LastStation=1, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=3, Area=(AreaEnum)4, FirstStation=28, LastStation=24, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=541, Area=(AreaEnum)4, FirstStation=9, LastStation=28, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=54, Area=(AreaEnum)4, FirstStation=18, LastStation=27, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=1, Area=(AreaEnum)4, FirstStation=19, LastStation=5, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=24, Area=(AreaEnum)5, FirstStation=4, LastStation=2, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=679, Area=(AreaEnum)5, FirstStation=4, LastStation=5, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=23, Area=(AreaEnum)5, FirstStation=37, LastStation=34, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=564, Area=(AreaEnum)5, FirstStation=39, LastStation=23, IsExist=true },
                new Line{Code=ConfigurationClass.LineCode, BusLine=9, Area=(AreaEnum)5, FirstStation=9, LastStation=1, IsExist=true }
            };

            AllBusStations = new List<BusStation>()
            {

                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.183921, Longitude=34.917806, Name="Ben Yehuda", Address="Ben Yehuda 12", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.870034, Longitude=34.819541, Name="Herzel", Address="Herzel 12", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.984553, Longitude=34.782828, Name="shabazi", Address="shabazi 3", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.888550, Longitude=34.7909040, Name="Bar Lev", Address="Bar Lev 43", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.914255, Longitude=34.807944, Name="Rakefet", Address="Rakefet 21", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.026047, Longitude=34.807561, Name="hagefen", Address="hagefen 1", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.425697, Longitude=35.033724, Name="Kanaf", Address="Kanaf 23", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.422627, Longitude=34.920835, Name="Halevy", Address="Halevy 5", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.425167, Longitude=35.035408, Name="Trumpeldor", Address="Trumpeldor 78", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.421197, Longitude=35.040302, Name="Kats", Address="Kats 5", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.442103, Longitude=35.047091, Name="Rotshild", Address="Rotshild 4", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.560804, Longitude=34.961128, Name="Hazait", Address="Hazait 23", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.561361, Longitude=34.958195, Name="Hapardes", Address="Hapardes 32", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.013069, Longitude=35.102114, Name="Perah", Address="Perah 1", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.074448, Longitude=35.295265, Name="Hagana", Address="Hagana 2", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.007998, Longitude=35.270537, Name="Dvir", Address="Dvir 34", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.006947, Longitude=35.281855, Name="Ariel", Address="Ariel 21", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.976412, Longitude=35.504043, Name="Hagibor", Address="Hagibor 46", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.020204, Longitude=35.148256, Name="Truman", Address="Truman 56", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.937562, Longitude=35.361183, Name="Karny", Address="Karny 24", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.026994, Longitude=34.910842, Name="Roman", Address="Roman 81", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.059086, Longitude=34.708337, Name="Hagefen", Address="Hagefen 13", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.072615, Longitude=34.791077, Name="Pilon", Address="Pilon 14", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.398358, Longitude=34.746489, Name="Parpar", Address="Parpar 15", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.397747, Longitude=34.746050, Name="HaEtzel", Address="HaEtzel 26", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.395101, Longitude=34.747424, Name="Doron", Address="Doron 14", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.392122, Longitude=34.748650, Name="Armon", Address="Armon 12", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.394065, Longitude=34.748791, Name="Hamigdal", Address="Hamigdal 12", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.026119, Longitude=34.743063, Name="Turki", Address="Turki 14", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.735511, Longitude=34.749105, Name="kanion", Address="kanion 32", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.735434, Longitude=34.749583, Name="Rubi", Address="Rubi 31", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.711082, Longitude=34.744586, Name="HaMelech", Address="HaMelech 13", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.712156, Longitude=34.739468, Name="Donald", Address="Donald 34", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=31.716725, Longitude=34.732568, Name="Jonathan", Address="Jonathan 32", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.910362, Longitude=35.299742, Name="Dvora", Address="Dvora 23", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.913282, Longitude=35.301922, Name="Mapal", Address="Mapal 49", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.026696, Longitude=35.250267, Name="Kineret", Address="Kineret 8", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.025094, Longitude=35.252544, Name="Anilevich", Address="Anilevich 7", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.927907, Longitude=35.319294, Name="Hashomer", Address="Hashomer 23", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.006806, Longitude=35.259478, Name="Hayezira", Address="Hayezira 67", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.833388, Longitude=35.247594, Name="Sapir", Address="Sapir 5", Accessibility=true},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.837056, Longitude=35.24829, Name="Shmork", Address="Shmork 28", Accessibility=false},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.047463, Longitude=34.864658, Name="Nachum", Address="Nachum 38", Accessibility=false},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.460728, Longitude=35.054007, Name="Golomb", Address="Golomb 18", Accessibility=false},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.461191, Longitude=35.053399, Name="Alon", Address="Alon 49", Accessibility=false},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.461896, Longitude=35.053225, Name="Shpigelman", Address="Shpigelman 37", Accessibility=false},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.086246, Longitude=35.225712, Name="Hartum", Address="Hartum 5", Accessibility=false},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.088012, Longitude=35.224534, Name="Hayesod", Address="Hayesod 8", Accessibility=false},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.087206, Longitude=35.227705, Name="Rotem", Address="Rotem 10", Accessibility=false},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=33.085835, Longitude=35.221639, Name="Savion", Address="Savion 4", Accessibility=false},
                new BusStation{StationCode=ConfigurationClass.StationCode, Latitude=32.455216, Longitude=35.055688, Name="Agamim", Address="Agamim 19", Accessibility=false},
            };

            AllLineStations = new List<LineStation>()
            {
                #region
                new LineStation{LineCode=1, StationCode=1, StationNumberInLine=0},
                new LineStation{LineCode=2, StationCode=2, StationNumberInLine=0},
                new LineStation{LineCode=3, StationCode=4, StationNumberInLine=0},
                new LineStation{LineCode=4, StationCode=6, StationNumberInLine=0},
                new LineStation{LineCode=5, StationCode=43, StationNumberInLine=0},
                new LineStation{LineCode=6, StationCode=21, StationNumberInLine=0},
                new LineStation{LineCode=7, StationCode=37, StationNumberInLine=0},
                new LineStation{LineCode=8, StationCode=1, StationNumberInLine=0},
                new LineStation{LineCode=9, StationCode=1, StationNumberInLine=0},
                new LineStation{LineCode=10, StationCode=9, StationNumberInLine=0},
                //first station

                new LineStation{LineCode=1, StationCode=39, StationNumberInLine=1},
                new LineStation{LineCode=2, StationCode=39, StationNumberInLine=1},
                new LineStation{LineCode=3, StationCode=39, StationNumberInLine=1},
                new LineStation{LineCode=4, StationCode=39, StationNumberInLine=1},
                new LineStation{LineCode=5, StationCode=39, StationNumberInLine=1},
                new LineStation{LineCode=6, StationCode=39, StationNumberInLine=1},
                new LineStation{LineCode=7, StationCode=39, StationNumberInLine=1},
                new LineStation{LineCode=8, StationCode=35, StationNumberInLine=1},
                new LineStation{LineCode=9, StationCode=35, StationNumberInLine=1},
                new LineStation{LineCode=10, StationCode=35, StationNumberInLine=1},

                new LineStation{LineCode=1, StationCode=8, StationNumberInLine=2},
                new LineStation{LineCode=2, StationCode=8, StationNumberInLine=2},
                new LineStation{LineCode=3, StationCode=8, StationNumberInLine=2},
                new LineStation{LineCode=4, StationCode=8, StationNumberInLine=2},
                new LineStation{LineCode=5, StationCode=8, StationNumberInLine=2},
                new LineStation{LineCode=6, StationCode=7, StationNumberInLine=2},
                new LineStation{LineCode=7, StationCode=7, StationNumberInLine=2},
                new LineStation{LineCode=8, StationCode=7, StationNumberInLine=2},
                new LineStation{LineCode=9, StationCode=7, StationNumberInLine=2},
                new LineStation{LineCode=10, StationCode=7, StationNumberInLine=2},

                new LineStation{LineCode=1, StationCode=9, StationNumberInLine=3},
                new LineStation{LineCode=2, StationCode=10, StationNumberInLine=3},
                new LineStation{LineCode=3, StationCode=11, StationNumberInLine=3},
                new LineStation{LineCode=4, StationCode=12, StationNumberInLine=3},
                new LineStation{LineCode=5, StationCode=13, StationNumberInLine=3},
                new LineStation{LineCode=6, StationCode=14, StationNumberInLine=3},
                new LineStation{LineCode=7, StationCode=15, StationNumberInLine=3},
                new LineStation{LineCode=8, StationCode=16, StationNumberInLine=3},
                new LineStation{LineCode=9, StationCode=17, StationNumberInLine=3},
                new LineStation{LineCode=10, StationCode=18, StationNumberInLine=3},

                new LineStation{LineCode=1, StationCode=30, StationNumberInLine=4},
                new LineStation{LineCode=2, StationCode=31, StationNumberInLine=4},
                new LineStation{LineCode=3, StationCode=32, StationNumberInLine=4},
                new LineStation{LineCode=4, StationCode=33, StationNumberInLine=4},
                new LineStation{LineCode=5, StationCode=34, StationNumberInLine=4},
                new LineStation{LineCode=6, StationCode=35, StationNumberInLine=4},
                new LineStation{LineCode=7, StationCode=36, StationNumberInLine=4},
                new LineStation{LineCode=8, StationCode=37, StationNumberInLine=4},
                new LineStation{LineCode=9, StationCode=38, StationNumberInLine=4},
                new LineStation{LineCode=10, StationCode=39, StationNumberInLine=4},

                new LineStation{LineCode=1, StationCode=35, StationNumberInLine=5},
                new LineStation{LineCode=2, StationCode=34, StationNumberInLine=5},
                new LineStation{LineCode=3, StationCode=33, StationNumberInLine=5},
                new LineStation{LineCode=4, StationCode=32, StationNumberInLine=5},
                new LineStation{LineCode=5, StationCode=31, StationNumberInLine=5},
                new LineStation{LineCode=6, StationCode=30, StationNumberInLine=5},
                new LineStation{LineCode=7, StationCode=35, StationNumberInLine=5},
                new LineStation{LineCode=8, StationCode=34, StationNumberInLine=5},
                new LineStation{LineCode=9, StationCode=33, StationNumberInLine=5},
                new LineStation{LineCode=10, StationCode=32, StationNumberInLine=5},

                new LineStation{LineCode=1, StationCode=20, StationNumberInLine=6},
                new LineStation{LineCode=2, StationCode=20, StationNumberInLine=6},
                new LineStation{LineCode=3, StationCode=20, StationNumberInLine=6},
                new LineStation{LineCode=4, StationCode=20, StationNumberInLine=6},
                new LineStation{LineCode=5, StationCode=20, StationNumberInLine=6},
                new LineStation{LineCode=6, StationCode=22, StationNumberInLine=6},
                new LineStation{LineCode=7, StationCode=22, StationNumberInLine=6},
                new LineStation{LineCode=8, StationCode=22, StationNumberInLine=6},
                new LineStation{LineCode=9, StationCode=22, StationNumberInLine=6},
                new LineStation{LineCode=10, StationCode=22, StationNumberInLine=6},

                new LineStation{LineCode=1, StationCode=25, StationNumberInLine=7},
                new LineStation{LineCode=2, StationCode=25, StationNumberInLine=7},
                new LineStation{LineCode=3, StationCode=25, StationNumberInLine=7},
                new LineStation{LineCode=4, StationCode=26, StationNumberInLine=7},
                new LineStation{LineCode=5, StationCode=26, StationNumberInLine=7},
                new LineStation{LineCode=6, StationCode=26, StationNumberInLine=7},
                new LineStation{LineCode=7, StationCode=27, StationNumberInLine=7},
                new LineStation{LineCode=8, StationCode=27, StationNumberInLine=7},
                new LineStation{LineCode=9, StationCode=27, StationNumberInLine=7},
                new LineStation{LineCode=10, StationCode=27, StationNumberInLine=7},

                new LineStation{LineCode=1, StationCode=26, StationNumberInLine=8},
                new LineStation{LineCode=2, StationCode=26, StationNumberInLine=8},
                new LineStation{LineCode=3, StationCode=26, StationNumberInLine=8},
                new LineStation{LineCode=4, StationCode=27, StationNumberInLine=8},
                new LineStation{LineCode=5, StationCode=27, StationNumberInLine=8},
                new LineStation{LineCode=6, StationCode=27, StationNumberInLine=8},
                new LineStation{LineCode=7, StationCode=25, StationNumberInLine=8},
                new LineStation{LineCode=8, StationCode=25, StationNumberInLine=8},
                new LineStation{LineCode=9, StationCode=25, StationNumberInLine=8},
                new LineStation{LineCode=10, StationCode=25, StationNumberInLine=8},

                new LineStation{LineCode=1, StationCode=28, StationNumberInLine=9},
                new LineStation{LineCode=2, StationCode=28, StationNumberInLine=9},
                new LineStation{LineCode=3, StationCode=28, StationNumberInLine=9},
                new LineStation{LineCode=4, StationCode=28, StationNumberInLine=9},
                new LineStation{LineCode=5, StationCode=28, StationNumberInLine=9},
                new LineStation{LineCode=6, StationCode=28, StationNumberInLine=9},
                new LineStation{LineCode=7, StationCode=28, StationNumberInLine=9},
                new LineStation{LineCode=8, StationCode=28, StationNumberInLine=9},
                new LineStation{LineCode=9, StationCode=28, StationNumberInLine=9},
                new LineStation{LineCode=10, StationCode=28, StationNumberInLine=9},
                                                
                new LineStation{LineCode=1, StationCode=2, StationNumberInLine=10},
                new LineStation{LineCode=2, StationCode=48, StationNumberInLine=10},
                new LineStation{LineCode=3, StationCode=48, StationNumberInLine=10},
                new LineStation{LineCode=4, StationCode=1, StationNumberInLine=10},
                new LineStation{LineCode=5, StationCode=1, StationNumberInLine=10},
                new LineStation{LineCode=6, StationCode=47, StationNumberInLine=10},
                new LineStation{LineCode=7, StationCode=1, StationNumberInLine=10},
                new LineStation{LineCode=8, StationCode=3, StationNumberInLine=10},
                new LineStation{LineCode=9, StationCode=3, StationNumberInLine=10},
                new LineStation{LineCode=10, StationCode=4, StationNumberInLine=10},

                new LineStation{LineCode=1, StationCode=40, StationNumberInLine=11},
                new LineStation{LineCode=2, StationCode=40, StationNumberInLine=11},
                new LineStation{LineCode=3, StationCode=40, StationNumberInLine=11},
                new LineStation{LineCode=4, StationCode=41, StationNumberInLine=11},
                new LineStation{LineCode=5, StationCode=41, StationNumberInLine=11},

                new LineStation{LineCode=1, StationCode=3, StationNumberInLine=12},
                new LineStation{LineCode=2, StationCode=3, StationNumberInLine=12},
                new LineStation{LineCode=3, StationCode=5, StationNumberInLine=12},
                new LineStation{LineCode=4, StationCode=7, StationNumberInLine=12},
                new LineStation{LineCode=5, StationCode=5, StationNumberInLine=12},
#endregion
            };

            AllConsecutiveStations = from bs1 in AllBusStations
                                     from bs2 in AllBusStations
                                     select new ConsecutiveStations { StationCode1 = bs1.StationCode,
                                         StationCode2 = bs2.StationCode,
                                         Distance = csDistance(bs1, bs2),
                                         DriveTime = csDt(csDistance(bs1, bs2)) };
        }

        public static double csDistance(BusStation bs1, BusStation bs2)
        {
            GeoCoordinate loc1 = new GeoCoordinate(bs1.Latitude, bs1.Longitude);
            GeoCoordinate loc2 = new GeoCoordinate(bs2.Latitude, bs2.Longitude);
            return loc1.GetDistanceTo(loc2) * (1 + r.NextDouble() / 2); //air-distance(in meters)*(1 to 1.5)
        }
         public static TimeSpan csDt(double distance)
        {
            return TimeSpan.FromSeconds(distance / (r.Next(50, 70) * 1 / 3.6));
        }



            //public static List<WindDirection> directions;
            //static DataSource()
            //{
            //    directions = new List<WindDirection>();
            //}
    }
}
