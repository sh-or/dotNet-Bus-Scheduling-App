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
            st = bst; Rate = _Rate;
            timer = new TimeSpan(_Hours, _Minuts, _Seconds);
            rate.Text = _Rate.ToString();
            hours.Text = timer.Hours.ToString();
            minuts.Text = timer.Minutes.ToString();
            seconds.Text = timer.Seconds.ToString();
            stNum.Text = st.StationCode+"  "+ st.Name;
            if (Rate < 60)
                runingTime = new TimeSpan(0, Rate, 0);
            else //limited for running 15(+) minutes by 1 sec
            {
                runingTime = new TimeSpan(Rate%60, Rate/60, 0);
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
                Thread.Sleep(4000);
                bgw.ReportProgress(1);
            }
        }
        public void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            timer = timer.Add(runingTime);
            if (timer.Days > 0)
                timer = TimeSpan.Zero.Add(new TimeSpan(timer.Hours, timer.Minutes, timer.Seconds));
            hours.Text = timer.Hours.ToString();
            minuts.Text = timer.Minutes.ToString();
            seconds.Text = timer.Seconds.ToString();
            lineSimulation.ItemsSource = bl.GetAllStationLineTrips(st.StationCode, timer);
        }
        public void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void lineSimulation_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();
            if (header == "IsExist" || header == "StationCode" || header == "LineCode")
                e.Cancel = true;
        }

    }
}
