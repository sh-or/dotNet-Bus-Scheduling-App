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
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        IBL bl;
        public AddBus(IBL ibl)
        {
            bl = ibl;
            InitializeComponent();
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
            try
            {
                BOBus b = new BOBus()
                {
                    IsExist = true,
                    LicenseNumber = int.Parse(_LicenseNumber.Text),
                    LicensingDate = DateTime.Parse(_LicensingDate.ToString()),
                    DateOfLastCare = DateTime.Parse(_DateCare.ToString()),
                    KmFromLastCare = int.Parse(_KmCare.Text),
                    KmFromLastRefuel = int.Parse(_KmRefuel.Text),
                    Kilometerage = int.Parse(_Kilometerage.Text),
                    Driver = _DName.Text
                };
                if (b.LicenseNumber < 1000000) //license number is shorter than 7 digits
                {
                    MessageBox.Show("ERROR! The license number is too short", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bl.AddBus(b);
                MessageBox.Show($"Bus {b.LicenseNumber} was added successfuly");
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
