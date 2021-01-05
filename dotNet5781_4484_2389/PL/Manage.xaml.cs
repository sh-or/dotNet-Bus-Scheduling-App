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
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage : Window
    {
        static IBL bl;
        public Manage()
        {
            InitializeComponent();
            IEnumerable <BOBus> buses = bl.GetAllBuses();
            busesLB.ItemsSource = buses;

        }



        private void UpdateBus(object sender, RoutedEventArgs e)
        {

        }

        private void addBus(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {

        }
    }
}
