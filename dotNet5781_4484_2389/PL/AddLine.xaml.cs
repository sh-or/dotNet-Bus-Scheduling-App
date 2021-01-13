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
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        IBL bl;
        //public IEnumerable<string> getAllRegions()
        //{
        //    return Enum.GetValues(typeof(BO.AreaEnum))
        //          .Cast<BO.AreaEnum>()
        //         .Select(v => v.ToString())
        //         .ToList();
        //}
        public AddLine(IBL ibl)
        {
            bl = ibl;
            InitializeComponent();
            try
            {
                IEnumerable<int> lst = from x in bl.GetAllBusStations()
                                         select x.StationCode; //+name???
                _Area.ItemsSource = Enum.GetValues(typeof(BO.AreaEnum)); 
                _First.ItemsSource = lst;
                _Last.ItemsSource = lst;
            }
            catch (BLException ex)
            {
                MessageBox.Show(ex.Message);
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

        private void Adding_Click(object sender, RoutedEventArgs e)
        {
            BOLine l = new BOLine();
            l.IsExist = true;
            l.BusLine = int.Parse(_LineNumber.Text);
            l.Area = (AreaEnum)_Area.SelectedItem;
            l.FirstStation = (int)_First.SelectedItem;
            l.LastStation = (int)_Last.SelectedItem;
            //{
            //    BusLine = int.Parse(_LineNumber.Text),
            //    Area=(AreaEnum)_Area.SelectedItem,
            //    FirstStation=(int)_First.SelectedItem,
            //    LastStation = (int)_Last.SelectedItem
            //};
            try
            {
                bl.AddLine(l);
                MessageBox.Show($"Line {l.BusLine} was added successfuly");
                Close();
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR! " + ex.Message + "\nEdit and try again");
            }
        }
    }
}
