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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        IBL bl;
        public AddStation(IBL ibl)
        {
            bl = ibl;
            InitializeComponent();
        }
        private void TextBox_onlyDouble_PreviewKeyDown(object sender, KeyEventArgs e)
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

            if (Char.IsDigit(c) || c == 190) //allow digits or dot (without Shift or Alt)
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        } //checking if the input contains digits only

        private void Adding_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                BOBusStation b = new BOBusStation();
                b.IsExist = true;
                b.Latitude = double.Parse(_Latitude.Text);
                b.Longitude = double.Parse(_Longitude.Text);
                b.Name = _Name.Text;
                b.Address = _Address.Text; 
                b.Accessibility = (bool)_Accessibility.IsChecked;
                bl.AddBusStation(b);
                MessageBox.Show($"Station {b.Name} was added successfuly");
                Close();
            }
            catch(BLException ex)
            {
                MessageBox.Show("ERROR! " + ex.Message+"\nEdit and try again");
            }
            catch
            {
                MessageBox.Show("ERROR!\n" + "Missing input" + "\nEdit and try again");
            }
        }
    }
}
