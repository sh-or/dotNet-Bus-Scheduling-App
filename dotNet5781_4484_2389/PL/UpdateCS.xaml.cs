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
            st1 = bls.StationCode;

            InitializeComponent();
            try
            { 
                var lst = l.Stations.ToList();
                int index=lst.FindIndex(x => x.StationCode == bls.StationCode);
                if(index==0)
                {
                    MessageBox.Show("ERROR!  \nEdit and try again");
                    Close();
                }
                st2 = lst[index - 1].StationCode;
               _stations.Text = st1 + ", "+st2;
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR! " + ex.Message + "\nEdit and try again");
            }
        }

        private void Updating_Click(object sender, RoutedEventArgs e)
        {
            try
            {
           //    bl.con
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR! " + ex.Message + "\nEdit and try again");
            }
        }
    }
}
