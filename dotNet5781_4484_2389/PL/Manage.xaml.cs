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
         IBL bl;

        public Manage(IBL ibl)
        {
            bl = ibl;
            InitializeComponent();
            IEnumerable<BOBus> buses = bl.GetAllBuses();
            ListBuses.ItemsSource = buses;

        }


        private void Exit(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateBus(object sender, RoutedEventArgs e)
        {

        }

        private void addBus(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {

        }

        
            private void btUpdateGradeInCourse_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btUnRegisterCourse_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
