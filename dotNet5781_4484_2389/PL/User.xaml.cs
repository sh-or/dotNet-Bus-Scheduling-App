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
using BO;
using BlAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        IBL bl;

        public User(IBL ibl)
        {
            bl = ibl;
            InitializeComponent();
            //IEnumerable<BOBusStation> busStations = bl.GetAllBusStations();
            try
            {
                ListBusStation.ItemsSource = bl.GetAllBusStations();
                //IEnumerable<BOLine> line = bl.GetAllLines();
                ListLines.ItemsSource = bl.GetAllLines();
                StationLines.ItemsSource = (ListBusStation.SelectedItem as BOBusStation).Lines;
                LineStations.ItemsSource = (ListLines.SelectedItem as BOLine).Stations;
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR!\n" + ex.Message);
            }
        }

        private void RefreshLinesAndStations(object sender, EventArgs e)
        {
            ListBusStation.ItemsSource = bl.GetAllBusStations();
            ListLines.ItemsSource = bl.GetAllLines();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateStation_Click(object sender, RoutedEventArgs e)
        {
            BOBusStation bs = (sender as Button).DataContext as BOBusStation;
            UpdateStation upS = new UpdateStation(bl,bs);
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
            UpdateLine upL = new UpdateLine(bl,l);
            upL.Closed += RefreshLinesAndStations;
            upL.ShowDialog();
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

        private void ListLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LineStations.ItemsSource = (ListLines.SelectedItem as BOLine).Stations;
        }

        private void ListBusStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationLines.ItemsSource = (ListBusStation.SelectedItem as BOBusStation).Lines;
        }

        private void TSimulator_Click(object sender, RoutedEventArgs e)
        {
            BOBusStation st = ListBusStation.SelectedItem as BOBusStation;
            
            Simulator s = new Simulator(bl, st, Hours.Text, Minuts.Text, Seconds.Text, int.Parse(Rate.Text));
            //stop BGW in closing!!
            s.ShowDialog();
        }

        private void LineStations_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();
            if (header == "Distance")
                e.Cancel = true;
        }

    }
}

