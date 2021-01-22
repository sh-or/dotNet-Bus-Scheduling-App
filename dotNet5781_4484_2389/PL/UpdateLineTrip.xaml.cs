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
    /// Interaction logic for UpdateLineTrip.xaml
    /// </summary>
    public partial class UpdateLineTrip : Window
    {
        IBL bl;
        BOLineTrip linetrip;
        public UpdateLineTrip(IBL ibl, BOLineTrip lt)
        {
            bl = ibl;
            linetrip = lt;
            InitializeComponent();
            _LineCode.DataContext = lt.LineCode;
            _Start.Text = lt.Start.ToString();
        }

        private void Updating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan start;
                int n;
                if (int.TryParse(_Start.Text, out n) || !TimeSpan.TryParse(_Start.Text, out start))
                {
                    MessageBox.Show("ERROR! \nWrong input format" + "\nEdit and try again");
                    return;
                }
                bl.UpdateLineTrip(linetrip, start);
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
