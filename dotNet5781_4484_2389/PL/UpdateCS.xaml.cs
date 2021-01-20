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
    /// Interaction logic for UpdateCS.xaml
    /// </summary>
    public partial class UpdateCS : Window
    {
        IBL bl;
        int st1, st2;
        public UpdateCS(IBL ibl, BOLineStation bls, BOLine l)
        {
            bl = ibl;
            st2 = bls.StationCode;

            InitializeComponent();
            try
            { 
                var lst = l.Stations.ToList();
                int index=lst.FindIndex(x => x.StationCode == bls.StationCode);
                //if (index == 0)
                //{
                //    MessageBox.Show("Cannot edit first station's data");
                //}
                //else
                //{
                    st1 = lst[index - 1].StationCode;
                    _stations.Text = st1 + ", " + st2;
                //}
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR! " + ex.Message + "\nEdit and try again");
            }
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


        private void Updating_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan dTime;
            double km;
            int n;
            if (!double.TryParse(_Distance.Text,out km) || int.TryParse(_DriveTime.Text, out n) || !TimeSpan.TryParse(_DriveTime.Text, out dTime))
            {
                MessageBox.Show("ERROR! \nWrong input format" + "\nEdit and try again");
                return;
            }
            try
            {
                BOConsecutiveStations cs = new BOConsecutiveStations()
                {
                    StationCode1 = st1,
                    StationCode2 = st2,
                    Distance = km,
                    DriveTime= dTime
                };
                bl.UpdateConsecutiveStations(cs);
                Close();
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR! " + ex.Message + "\nEdit and try again");
            }
        }
    }
}
