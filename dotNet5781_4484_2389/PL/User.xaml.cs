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
                var AllStationsNumbers = from BOBusStation x in bl.GetAllBusStations()
                                         select x.StationCode;
                _StartStation.ItemsSource = AllStationsNumbers;
                _DestinationStation.ItemsSource = AllStationsNumbers;
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

        private void ListBusStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationLines.ItemsSource = (ListBusStation.SelectedItem as BOBusStation).Lines;
        }

        private void TSimulator_Click(object sender, RoutedEventArgs e)
        {
            BOBusStation st = ListBusStation.SelectedItem as BOBusStation;
            //checking for int.Parse(Hours.Text), int.Parse(Minuts.Text), int.Parse(Seconds.Text)!!
            //24,60,60
            Simulator s = new Simulator(bl, st, int.Parse(Hours.Text), int.Parse(Minuts.Text), int.Parse(Seconds.Text), int.Parse(Rate.Text));
            //stop BGW in closing!!
            s.ShowDialog();
        }

        private void _StartStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var AllStationsNumbers = from BOBusStation x in bl.GetAllBusStations()
                                     select x.StationCode;
            _StartStation.ItemsSource = AllStationsNumbers;
        }

        private void _DestinationStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var AllStationsNumbers = from BOBusStation x in bl.GetAllBusStations()
                                     select x.StationCode;
            _DestinationStation.ItemsSource = AllStationsNumbers;
        }

        private void ListLinesRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int st1 = (int)_StartStation.SelectedItem;
            int st2 = (int)_DestinationStation.SelectedItem;
            ListLinesRoute.ItemsSource=bl.SearchRoute(st1, st2);
        }
    }
}

