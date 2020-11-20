using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_02_4484_2389;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusLine currentDisplayBusLine;
        private List<BusStation> allBusStations;
        private BusLineSystem allBusLines;

        public MainWindow()
        {
            InitializeComponent();
            allBusStations = new List<BusStation>();
            allBusLines = new BusLineSystem();
            { //restart 10 lines and 40 stations

                allBusLines.addLine(new BusLine(Area.Center, allBusStations));
                allBusLines.addLine(new BusLine(Area.Center, allBusStations));
                allBusLines.addLine(new BusLine(Area.Center, allBusStations));
                allBusLines.addLine(new BusLine(Area.Jerusalem, allBusStations));
                allBusLines.addLine(new BusLine(Area.Jerusalem, allBusStations));
                allBusLines.addLine(new BusLine(Area.South, allBusStations));
                allBusLines.addLine(new BusLine(Area.North, allBusStations));
                allBusLines.addLine(new BusLine(Area.North, allBusStations));
                allBusLines.addLine(new BusLine(Area.General, allBusStations));
                allBusLines.addLine(new BusLine(Area.General, allBusStations));

                allBusStations.Add(new BusStation("Halevy"));
                allBusStations.Add(new BusStation("Trumpeldor"));
                allBusStations.Add(new BusStation("Kats"));
                allBusStations.Add(new BusStation("Rotshild"));
                allBusStations.Add(new BusStation("Hazait"));
                allBusStations.Add(new BusStation("Hapardes"));
                allBusStations.Add(new BusStation("Perah"));
                allBusStations.Add(new BusStation("Hagana"));
                allBusStations.Add(new BusStation("Dvir"));
                allBusStations.Add(new BusStation("Ariel"));
                allBusStations.Add(new BusStation("Hagibor")); /////////////
                allBusStations.Add(new BusStation("Truman"));
                allBusStations.Add(new BusStation("Karny"));
                allBusStations.Add(new BusStation("Roman"));
                allBusStations.Add(new BusStation("Hagefen"));
                allBusStations.Add(new BusStation("Pilon"));
                allBusStations.Add(new BusStation("Parpar"));
                allBusStations.Add(new BusStation("HaEtzel"));
                allBusStations.Add(new BusStation("Doron"));
                allBusStations.Add(new BusStation("Armon"));
                allBusStations.Add(new BusStation("Hamigdal")); /////////////////
                allBusStations.Add(new BusStation("Turki"));
                allBusStations.Add(new BusStation("kanion"));
                allBusStations.Add(new BusStation("Rubi"));
                allBusStations.Add(new BusStation("HaMelech"));
                allBusStations.Add(new BusStation("Hafira"));
                allBusStations.Add(new BusStation("Laila"));
                allBusStations.Add(new BusStation("Ratzon"));
                allBusStations.Add(new BusStation("Donald"));
                allBusStations.Add(new BusStation("Moshe"));
                allBusStations.Add(new BusStation("Hacohen")); /////////////////
                allBusStations.Add(new BusStation("Avraham"));
                allBusStations.Add(new BusStation("Yzhak"));
                allBusStations.Add(new BusStation("Jonathan"));
                allBusStations.Add(new BusStation("Jakobs"));
                allBusStations.Add(new BusStation("Rachel"));
                allBusStations.Add(new BusStation("Miriam"));
                allBusStations.Add(new BusStation("Dvora"));
                allBusStations.Add(new BusStation("Mapal"));
                allBusStations.Add(new BusStation("Kineret"));

                allBusLines[1].addStation(100000, 0);
                allBusLines[1].addStation(100001, 0);
                allBusLines[2].addStation(100002, 0);
                allBusLines[2].addStation(100003, 0);
                allBusLines[3].addStation(100004, 0);
                allBusLines[3].addStation(100005, 0);
                allBusLines[4].addStation(100006, 0);
                allBusLines[4].addStation(100007, 0);
                allBusLines[5].addStation(100008, 0);
                allBusLines[5].addStation(100009, 0); ///
                allBusLines[6].addStation(100010, 0);
                allBusLines[6].addStation(100011, 0);
                allBusLines[7].addStation(100012, 0);
                allBusLines[7].addStation(100013, 0);
                allBusLines[8].addStation(100014, 0);
                allBusLines[8].addStation(100015, 0);
                allBusLines[9].addStation(100016, 0);
                allBusLines[9].addStation(100017, 0);
                allBusLines[10].addStation(100018, 0);
                allBusLines[10].addStation(100019, 0);
                allBusLines[1].addStation(100020, 0);
                allBusLines[1].addStation(100021, 0);
                allBusLines[2].addStation(100022, 0);
                allBusLines[2].addStation(100023, 0);
                allBusLines[3].addStation(100024, 0);
                allBusLines[3].addStation(100025, 0);
                allBusLines[4].addStation(100027, 0);
                allBusLines[4].addStation(100028, 0);
                allBusLines[5].addStation(100029, 0);
                allBusLines[5].addStation(100030, 0);
                allBusLines[6].addStation(100031, 0);
                allBusLines[6].addStation(100032, 0);
                allBusLines[7].addStation(100033, 0);
                allBusLines[7].addStation(100034, 0);
                allBusLines[8].addStation(100035, 0);
                allBusLines[8].addStation(100036, 0);
                allBusLines[9].addStation(100037, 0);
                allBusLines[9].addStation(100038, 0);
                allBusLines[10].addStation(100039, 0);
                allBusLines[10].addStation(100026, 0);

                allBusLines[2].addStation(100000, 0);

                allBusLines[2].addStation(100033, 0); //between 100001 to 100000

                allBusLines[2].addStation(100001, 0);
                allBusLines[3].addStation(100002, 0);
                allBusLines[3].addStation(100003, 0);
                allBusLines[4].addStation(100004, 0);
                allBusLines[4].addStation(100005, 0);
                allBusLines[5].addStation(100006, 0);
                allBusLines[5].addStation(100007, 0);
                allBusLines[6].addStation(100008, 0);
                allBusLines[6].addStation(100009, 0);
            }
            cbBusLines.ItemsSource = allBusLines;
            cbBusLines.DisplayMemberPath = " BusLineNum ";
            cbBusLines.SelectedIndex = 0;
        }

        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = allBusLines[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }


        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
               ShowBusLine((cbBusLines.SelectedValue as BusLine).line);
        }

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lbBusLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
