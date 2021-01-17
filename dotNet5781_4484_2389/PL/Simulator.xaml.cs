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
using System.Diagnostics;

namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        public static BackgroundWorker bgw;  // BackgroundWorker 
        public IBL bl;
        public BOBusStation st;
        //public int Hours, Minuts, Seconds;
        public int Rate;
        public TimeSpan timer;
        public bool isRun = true;
        public TimeSpan runingTime; //the progress time in 1 sec
        public Simulator(IBL ibl, BOBusStation bst, int _Hours, int _Minuts, int _Seconds, int _Rate)
        {
            InitializeComponent();
            bl = ibl;
            st = bst;
            //Hours = _Hours; Minuts = _Minuts; Seconds = _Seconds; Rate = _Rate;
            //while (_Hours.Length < 2)
            //    _Hours = "0"+_Hours;
            //while (_Minuts.Length < 2)
            //    _Minuts = "0" + _Minuts;
            //while (_Seconds.Length < 2)
            //    _Seconds = "0" + _Seconds;
            //hours.DataContext = _Hours; minuts.DataContext = _Minuts; seconds.DataContext = _Seconds;
            timer = new TimeSpan(_Hours, _Minuts, _Seconds);
            rate.DataContext = _Rate;
            stNum.Text = st.StationCode+"  "+ st.Name;
            if (Rate < 60)
                runingTime = new TimeSpan(0, 0, Rate);
            else //limited for running 15(+) minutes by 1 sec
            {
                runingTime = new TimeSpan(0, Rate%60, Rate/60);
            }


            lineSimulation.ItemsSource = bl.GetAllStationLineTrips(st.StationCode, timer);
            bgw = new BackgroundWorker(); //reset the backgrounder
            bgw.DoWork += bgw_DoWork;
            bgw.ProgressChanged += bgw_ProgressChanged;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.WorkerReportsProgress = true;
            bgw.RunWorkerAsync(st); 

        }
        public void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isRun)
            {
                Thread.Sleep(1000);
                bgw.ReportProgress(1);
            }
        }
        public void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            timer.Add(runingTime);
            lineSimulation.ItemsSource = bl.GetAllStationLineTrips(st.StationCode, timer);
        }
        public void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
