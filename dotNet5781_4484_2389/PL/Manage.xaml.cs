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
using System.Windows.Shapes;
using BlAPI;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage : Window
    {
         IBL bl;

        public Manage(IBL ibl)
        {
            bl = ibl;
            InitializeComponent();
            IEnumerable<BOBus> buses = bl.GetAllBuses();
            ListBuses.ItemsSource = buses;
            IEnumerable<BOBusStation> busStations = bl.GetAllBusStations();
            ListBusStation.ItemsSource = busStations;
            IEnumerable<BOLine> line = bl.GetAllLines();
            ListLines.ItemsSource = line;
        }


        private void Exit(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateBus_Click(object sender, RoutedEventArgs e)
        {
            BOBus b = new BOBus();
            UpdateBus upB = new UpdateBus(bl, b);
            upB.ShowDialog();
        }

        private void addBus(object sender, RoutedEventArgs e)
        {
            AddBus addb = new AddBus(bl);
            addb.ShowDialog();
        }

        private void UpdateStation_Click(object sender, RoutedEventArgs e)
        {
            UpdateStation upS = new UpdateStation(bl);
            upS.ShowDialog();
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            AddStation adds = new AddStation(bl);
            adds.ShowDialog();
        }
        private void addline_Click(object sender, RoutedEventArgs e)
        {
            AddLine addl = new AddLine(bl);
            addl.ShowDialog();
        }

        private void UpdateLine_Click(object sender, RoutedEventArgs e)
        {
            UpdateLine upL = new UpdateLine(bl);
            upL.ShowDialog();
        }
        private void DeleteBus_Click(object sender, RoutedEventArgs e)
        {
           // bl.DeleteBus();
        }
        private void DeleteBusStation_Click(object sender, RoutedEventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
