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
        public Simulator(IBL ibl, BOBusStation bst)
        {
            InitializeComponent();
            bl = ibl;
            st = bst;
            bgw = new BackgroundWorker(); //reset the care backgrounder
            bgw.DoWork += bgw_DoWork;
            bgw.ProgressChanged += bgw_ProgressChanged;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.WorkerReportsProgress = true;
            bgw.RunWorkerAsync(st); //send the current bus to care


            //lineSimulation.ItemsSource = bl.GetAllLineTrips(st.StationCode, DateTime.Now.TimeOfDay);
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
