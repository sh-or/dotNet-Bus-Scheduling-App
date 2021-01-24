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

        public User(IBL ibl) //window c-tor
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
                var AllStationsNumbers = from BOBusStation x in bl.GetAllBusStations()
                                         select x.StationCode;
                _StartStation.ItemsSource = AllStationsNumbers;
                _DestinationStation.ItemsSource = AllStationsNumbers;
                _StartStation.SelectedItem=1;
                _DestinationStation.SelectedItem=1;
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR!\n" + ex.Message);
            }
        }

        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            if (Char.IsDigit(c)) //allow digits (without Shift or Alt)
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        } //checking if the input contains digits only

        private void RefreshLinesAndStations(object sender, EventArgs e)
        {
            ListBusStation.ItemsSource = bl.GetAllBusStations();
            ListLines.ItemsSource = bl.GetAllLines();
        }

        private void ListLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LineStations.ItemsSource = (ListLines.SelectedItem as BOLine).Stations;
        }

        #region Station
        private void ListBusStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationLines.ItemsSource = (ListBusStation.SelectedItem as BOBusStation).Lines;
        }
        private void TSimulator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BOBusStation st = ListBusStation.SelectedItem as BOBusStation;
                int h = int.Parse(Hours.Text), m = int.Parse(Minuts.Text), s = int.Parse(Seconds.Text);
                if (h > 23 || m > 59 || s > 59)
                    MessageBox.Show("ERROR!\nWrong time format input");
                else
                {
                    Simulator sml = new Simulator(bl, st, h, m, s, int.Parse(Rate.Text));
                    //stop BGW in closing!!
                    sml.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("ERROR!\nMissing input\nEdit and try again");

            }
        }
        private void _StartStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_DestinationStation.SelectedItem != null)
                refreshLinesRout();
        }
        private void _DestinationStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_StartStation.SelectedItem!=null)
                refreshLinesRout();
        }
        private void refreshLinesRout()
        {
            int st1 = (int)_StartStation.SelectedItem;
            int st2 = (int)_DestinationStation.SelectedItem;
            ListLinesRoute.ItemsSource = bl.SearchRoute(st1, st2);
        }
        private void ListLinesRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListLinesRoute.SelectedItem != null)
                LinesRoutStations.ItemsSource = (ListLinesRoute.SelectedItem as BOLine).Stations;
            else
                LinesRoutStations.ItemsSource = null;
        }

        private void LinesRoutStations_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();
            if (header == "Distance")
                e.Cancel = true;
        }
        private void ListLinesRoute_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();
            if (header == "Code")
                e.Cancel = true;
            if (header == "IsExist")
                e.Cancel = true;
            if (header == "Stations")
                e.Cancel = true;
        }
        #endregion

        private void LineStations_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();
            if (header == "Distance")
                e.Cancel = true;
        }
    }
}

