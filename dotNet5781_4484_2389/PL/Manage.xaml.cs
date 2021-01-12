

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
            //IEnumerable<BOBus> buses = bl.GetAllBuses();
            ListBuses.ItemsSource = bl.GetAllBuses();
            //IEnumerable<BOBusStation> busStations = bl.GetAllBusStations();
            ListBusStation.ItemsSource = bl.GetAllBusStations();
            //IEnumerable<BOLine> line = bl.GetAllLines();
            ListLines.ItemsSource = bl.GetAllLines();
            StationLines.ItemsSource = (ListBusStation.SelectedItem as BOBusStation).Lines;
            LineStations.ItemsSource = (ListLines.SelectedItem as BOLine).Stations;
        }

        private void RefreshBuses(object sender, EventArgs e)
        {
            ListBuses.ItemsSource = bl.GetAllBuses();
        }
        private void RefreshLinesAndStations(object sender, EventArgs e)
        {
            ListBusStation.ItemsSource = bl.GetAllBusStations();
            ListLines.ItemsSource = bl.GetAllLines();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateBus_Click(object sender, RoutedEventArgs e)
        {
            BOBus b = (sender as Button).DataContext as BOBus;
            UpdateBus upB = new UpdateBus(bl, b);
            upB.ShowDialog();
        }

        private void addBus(object sender, RoutedEventArgs e)
        {
            AddBus addb = new AddBus(bl);
            addb.Closed += RefreshBuses;
            addb.ShowDialog();
        }

        private void UpdateStation_Click(object sender, RoutedEventArgs e)
        {
            BOBusStation bs = (sender as Button).DataContext as BOBusStation;
            UpdateStation upS = new UpdateStation(bl, bs);
            upS.Closed += RefreshLinesAndStations;
            upS.ShowDialog();
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            AddStation adds = new AddStation(bl);
            adds.Closed += RefreshLinesAndStations;
            adds.ShowDialog();
        }
        private void addline_Click(object sender, RoutedEventArgs e)
        {
            AddLine addl = new AddLine(bl);
            addl.Closed += RefreshLinesAndStations;
            addl.ShowDialog();
        }

        private void UpdateLine_Click(object sender, RoutedEventArgs e)
        {
            BOLine l = (sender as Button).DataContext as BOLine;
            UpdateLine upL = new UpdateLine(bl, l);
            upL.Closed += RefreshLinesAndStations;
            upL.ShowDialog();
        }


        private void dataGridView1_CellClick(object sender, RoutedEventArgs e)
        {

        }


        private void ListLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LineStations.ItemsSource = (ListLines.SelectedItem as BOLine).Stations;
        }

        private void ListBusStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationLines.ItemsSource = (ListBusStation.SelectedItem as BOBusStation).Lines;
        }

        private void DeleteBus_Click(object sender, RoutedEventArgs e)
        {
            BOBus b = (sender as Button).DataContext as BOBus;
            bl.DeleteBus(b.LicenseNumber);
            ListBuses.ItemsSource = bl.GetAllBuses();  //refresh
        }

        private void DeleteBusStation_Click(object sender, RoutedEventArgs e)
        {
            BOBusStation bs = (sender as Button).DataContext as BOBusStation;
            bl.DeleteBusStation(bs.StationCode);
            ListBusStation.ItemsSource = bl.GetAllBusStations();  //refresh
            ListLines.ItemsSource = bl.GetAllLines();  //refresh
        }

        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            BOLine l = (sender as Button).DataContext as BOLine;
            bl.DeleteLine(l.Code);
            ListBusStation.ItemsSource = bl.GetAllBusStations();  //refresh
            ListLines.ItemsSource = bl.GetAllLines();  //refresh
        }



        private void DeleteStationInLine_Click(object sender, RoutedEventArgs e)
        {
            BOLine l = ListLines.SelectedItem as BOLine;
            BOLineStation sl = (sender as Button).DataContext as BOLineStation;
            bl.DeleteStationInLine(l, sl.StationCode);
            ListBusStation.ItemsSource = bl.GetAllBusStations();  //refresh
            ListLines.ItemsSource = bl.GetAllLines();  //refresh
        }



        private void AddStationInLine_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}