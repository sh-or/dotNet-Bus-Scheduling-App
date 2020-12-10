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

namespace dotNet5781_3B_4484_2389
{
    /// <summary>
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        public int licenseNum;
        public DateTime beginning;
        public DateTime lastCare;
        public int kmOfLastCare;
        public int kmOfLastRefuel;
        public int kilometerage;
        public Bus1 bs;

            public AddBus()
        {
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

        //private void licenceNum_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    licenseNum = int.Parse(text.Text);
        //}

        //private void km_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    kilometerage = int.Parse(text.Text);
        //}

        //private void KmFromLastCare_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    kmOfLastCare = int.Parse(text.Text);
        //}

        //private void KmFromLastRefuel_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    kmOfLastRefuel = int.Parse(text.Text);
        //}

        //private void licenceNum_TextInput(object sender, TextCompositionEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    licenseNum = int.Parse(text.Text);
        //}

        //private void km_TextInput(object sender, TextCompositionEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    kilometerage = int.Parse(text.Text);
        //}

        //private void KmFromLastRefuel_TextInput(object sender, TextCompositionEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    kmOfLastCare = int.Parse(text.Text);
        //}

        //private void KmFromLastCare_TextInput(object sender, TextCompositionEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    kmOfLastRefuel = int.Parse(text.Text);
        //}
        private void addBusButton_Click(object sender, RoutedEventArgs e)
        {
            licenseNum= int.Parse(licenceNum.Text);
            beginning = DateTime.Parse(begin.Text);
            lastCare= DateTime.Parse(care.Text); // DisplayDateEnd="12.07.2020"
            kmOfLastCare = int.Parse(KmFromLastCare.Text);
            kmOfLastRefuel = int.Parse(KmFromLastRefuel.Text);
            kilometerage = int.Parse(km.Text);

            if (licenseNum < 1000000)
            {
                MessageBox.Show("ERROR! The license number is too short", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if((licenseNum>9999999 && beginning.Year < 2018) || (licenseNum <10000000 && beginning.Year >= 2018))
            {
                MessageBox.Show("ERROR! The license number doesn't fit the beginnig date", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bs = new Bus1(licenseNum, beginning, lastCare, kmOfLastCare, kmOfLastRefuel, kilometerage);
            this.Close();
        }

    }
}

