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
using System.Threading;
using BlAPI;
using BO;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        public static BackgroundWorker bgw;  // BackgroundWorker 
        IBL bl;
        BOBusStation st;
        public string Hours, Minuts, Seconds;
        public int Rate;
        public Simulator(IBL ibl, BOBusStation bst, string _Hours, string _Minuts, string _Seconds, int _Rate)
        {
            InitializeComponent();
            bl = ibl;
            st = bst;
            Hours = _Hours; Minuts = _Minuts; Seconds = _Seconds; Rate = _Rate;
            hours.DataContext = _Hours;
            minuts.DataContext = _Minuts; seconds.DataContext = _Seconds;
            rate.DataContext = _Rate;
            stNum.Text = st.StationCode+"  "+ st.Name;
            bgw = new BackgroundWorker(); //reset the backgrounder
            bgw.DoWork += bgw_DoWork;
            bgw.ProgressChanged += bgw_ProgressChanged;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.WorkerReportsProgress = true;
            bgw.RunWorkerAsync(st); 

            lineSimulation.ItemsSource = bl.GetAllStationLineTrips(st.StationCode, new TimeSpan(8,30,0));
        }
        public void bgw_DoWork(object sender, DoWorkEventArgs e)
        {

        }
        public void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        public void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
