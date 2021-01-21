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
    /// Interaction logic for UpdateLine.xaml
    /// </summary>
    public partial class UpdateLine : Window
    {
        IBL bl;
        BOLine line;
        public UpdateLine(IBL ibl, BOLine l)
        {
            bl = ibl;
            line = l;
            InitializeComponent();
            _Area.ItemsSource = Enum.GetValues(typeof(BO.AreaEnum));
            _First.DataContext = l.FirstStation;
            _Last.DataContext = l.LastStation;
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

        private void Updating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BOLine l = line;
                l.BusLine = int.Parse(_LineNumber.Text);
                l.Area = (AreaEnum)_Area.SelectedItem;
                l.FirstStation = line.FirstStation;
                l.LastStation = line.LastStation;
                bl.UpdateLine(l);
                Close();
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR! " + ex.Message + "\nEdit and try again");
            }
            catch
            {
                MessageBox.Show("ERROR!\n" + "Missing input" + "\nEdit and try again");
            }
        }
    }
}
