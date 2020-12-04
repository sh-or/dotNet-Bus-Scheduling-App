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
            tb1.DataContext = b;
            tb2.DataContext = b;
            tb3.DataContext = b;
            tb4.DataContext = b;
            tb5.DataContext = b;
            tb6.DataContext = b;
            tb7.DataContext = b;
        }

        private void careB_Click(object sender, RoutedEventArgs e)
        {
            //Bus1 b = (sender as Button).DataContext as Bus1;
            //MainWindow.bgw1 = new BackgroundWorker(); //reset the care backgrounder
            //MainWindow.bgw1.DoWork += MainWindow.bgw1_DoWork;
            //MainWindow.bgw1.ProgressChanged += MainWindow.bgw1_ProgressChanged;
            //MainWindow.bgw1.RunWorkerCompleted += MainWindow.bgw1_RunWorkerCompleted;
            //MainWindow.bgw1.WorkerReportsProgress = true;
            //MainWindow.bgw1.RunWorkerAsync(b); //send the current bus to care
            //if (b.kmOfLastRefuel > 1000) //checking fuel
            //    MainWindow.bgw2.RunWorkerAsync(b); //send the current bus to refuel
        }

        private void refuelB_Click(object sender, RoutedEventArgs e)
        {
            //Bus1 b = (sender as Button).DataContext as Bus1;
            //bgw2 = new BackgroundWorker(); //reset the refuel backgrounder
            //bgw2.DoWork += bgw2_DoWork;
            //bgw2.ProgressChanged += bgw2_ProgressChanged;
            //bgw2.RunWorkerCompleted += bgw2_RunWorkerCompleted;
            //bgw2.WorkerReportsProgress = true;
            //bgw2.RunWorkerAsync(b); //send the current bus to refuel
        }
    }
}
