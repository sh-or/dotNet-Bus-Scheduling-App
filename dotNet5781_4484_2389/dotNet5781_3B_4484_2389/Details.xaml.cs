using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        public Details(Bus1 b) //get the current bus
        {
            InitializeComponent(); 
            tb1.DataContext = b; //lcn
            tb2.DataContext = b;//.Status; //Status
            tb3.DataContext = b;//.Kilometerage; //kms
            tb4.DataContext = b; //start
            tb5.DataContext = b; //km care
            tb6.DataContext = b; //date care
            tb7.DataContext = b; //km refuel
        }

        private void careB_Click(object sender, RoutedEventArgs e)  //care button
        {
            Bus1 b = ((MainWindow)Application.Current.MainWindow).findBus(tb1.Text);
            ((MainWindow)Application.Current.MainWindow).caring(b);
        }

        private void refuelB_Click(object sender, RoutedEventArgs e)  //refuel button
        {
            Bus1 b = ((MainWindow)Application.Current.MainWindow).findBus(tb1.Text);
            ((MainWindow)Application.Current.MainWindow).refueling(b);
        }
    }
}
