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
        public static BackgroundWorker bgw1;  // BackgroundWorker care
        public static BackgroundWorker bgw2;  // BackgroundWorker refuel

        public Manage(IBL ibl)
        {
            bl = ibl;
            InitializeComponent();
            ListBuses.ItemsSource = bl.GetAllBuses();
            ListBusStation.ItemsSource = bl.GetAllBusStations();
            ListLines.ItemsSource = bl.GetAllLines();
            var AllLinesNumbers = from BOLine l in bl.GetAllLines()
                                  select l.Code;
            LineChoose.ItemsSource = AllLinesNumbers;
            ListLineTrips.ItemsSource = bl.GetAllLineTrips((int)LineChoose.SelectedItem);

            //StationLines.ItemsSource = (ListBusStation.SelectedItem as BOBusStation).Lines;
            //LineStations.ItemsSource = (ListLines.SelectedItem as BOLine).Stations;
        }


        private void RefreshBuses(object sender, EventArgs e)
        {
            ListBuses.ItemsSource = bl.GetAllBuses();
        }
        private void RefreshLinesAndStations(object sender, EventArgs e)
        {
            ListBusStation.ItemsSource = bl.GetAllBusStations();
            ListLines.ItemsSource = bl.GetAllLines();
        }
        private void RefreshLineTrips(object sender, EventArgs e)
        {
            ListLineTrips.ItemsSource = bl.GetAllLineTrips((int)LineChoose.SelectedItem);

        }

        private void UpdateBus_Click(object sender, RoutedEventArgs e)
        {
            BOBus b = (sender as Button).DataContext as BOBus;
            UpdateBus upB = new UpdateBus(bl, b);
            upB.Closed += RefreshBuses;
            upB.ShowDialog();
        }

        private void addBus(object sender, RoutedEventArgs e)
        {
            AddBus addb = new AddBus(bl);
            addb.Closed += RefreshBuses;
            addb.ShowDialog();
        }

        private void UpdateStation_Click(object sender, RoutedEventArgs e)
        {
            BOBusStation bs = (sender as Button).DataContext as BOBusStation;
            UpdateStation upS = new UpdateStation(bl, bs);
            upS.Closed += RefreshLinesAndStations;
            upS.ShowDialog();
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            AddStation adds = new AddStation(bl);
            adds.Closed += RefreshLinesAndStations;
            adds.ShowDialog();
        }
        private void addline_Click(object sender, RoutedEventArgs e)
        {
            AddLine addl = new AddLine(bl);
            addl.Closed += RefreshLinesAndStations;
            addl.ShowDialog();
        }

        private void UpdateLine_Click(object sender, RoutedEventArgs e)
        {
            BOLine l = (sender as Button).DataContext as BOLine;
            UpdateLine upL = new UpdateLine(bl, l);
            upL.Closed += RefreshLinesAndStations;
            upL.ShowDialog();
        }


        private void dataGridView1_CellClick(object sender, RoutedEventArgs e)
        {

        }


        private void ListLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LineStations.ItemsSource = (ListLines.SelectedItem as BOLine).Stations;
        }

        private void ListBusStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationLines.ItemsSource = (ListBusStation.SelectedItem as BOBusStation).Lines;
        }

        private void DeleteBus_Click(object sender, RoutedEventArgs e)
        {
            BOBus b = (sender as Button).DataContext as BOBus;
            try
            {
                bl.DeleteBus(b.LicenseNumber);
                ListBuses.ItemsSource = bl.GetAllBuses();  //refresh
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR!\n" + ex.Message + "\nEdit and try again");
            }
        }

        private void DeleteBusStation_Click(object sender, RoutedEventArgs e)
        {
            BOBusStation bs = (sender as Button).DataContext as BOBusStation;
            try
            {
                bl.DeleteBusStation(bs.StationCode);
                ListBusStation.ItemsSource = bl.GetAllBusStations();  //refresh
                ListLines.ItemsSource = bl.GetAllLines();  //refresh
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR!\n" + ex.Message + "\nEdit and try again");
            }
        }


        private void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            BOLine l = (sender as Button).DataContext as BOLine;
            try
            {
                bl.DeleteLine(l.Code);
                ListBusStation.ItemsSource = bl.GetAllBusStations();  //refresh
                ListLines.ItemsSource = bl.GetAllLines();  //refresh
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR!\n" + ex.Message + "\nEdit and try again");
            }
        }


        private void DeleteStationInLine_Click(object sender, RoutedEventArgs e)
        {
            BOLine l = ListLines.SelectedItem as BOLine;
            BOLineStation sl = (sender as Button).DataContext as BOLineStation;
            try
            {
                bl.DeleteStationInLine(l, sl.StationCode);
                ListBusStation.ItemsSource = bl.GetAllBusStations();  //refresh
                ListLines.ItemsSource = bl.GetAllLines();  //refresh
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR! \n" + ex.Message + "\nEdit and try again");
            }
        }

        private void AddStationInLine_Click(object sender, RoutedEventArgs e)
        {
            BOLine l = ListLines.SelectedItem as BOLine;
            AddStationInLine addsl = new AddStationInLine(bl, l);
            addsl.Closed += RefreshLinesAndStations;
            addsl.ShowDialog();
        }

        public void GoCare_Click(object sender, RoutedEventArgs e) //going to care
        {
            BOBus b = (sender as Button).DataContext as BOBus;
            caring(b);
        }
        public void caring(BOBus b) //the care(helper func for the ditails window)
        {
            bgw1 = new BackgroundWorker(); //reset the care backgrounder
            bgw1.DoWork += bgw1_DoWork;
            bgw1.ProgressChanged += bgw1_ProgressChanged;
            bgw1.RunWorkerCompleted += bgw1_RunWorkerCompleted;
            bgw1.WorkerReportsProgress = true;
            bgw1.RunWorkerAsync(b); //send the current bus to care
        }
        public void bgw1_DoWork(object sender, DoWorkEventArgs e)
        {
            BOBus b = (BOBus)e.Argument; //get current bus
            b.Status = (StatusEnum)5; //="InCare"
            bl.UpdateBus(b);
            //button not enable?
            //b.isAvailable = false; //not available
            for (int i = 144; i > 0; i--)
            {
                //b.timerAct = "Coming back in " + bgwTimer(i);
                System.Threading.Thread.Sleep(1000); //wait 24 hours of care
                bgw1.ReportProgress(5); //present changes
            }

            //update the changes from the ride: (after the ride)
            b.DateOfLastCare = DateTime.Now;
            b.KmFromLastCare = 0;
            b.Status =(StatusEnum)1; //= Ready  (if not-will go to refuel...)
            //b.isAvailable = true; //available
            //b.timerAct = "";
            if (b.KmFromLastRefuel > 1000) //checking fuel
            //bgw2.RunWorkerAsync(b); //send the current bus to refuel
            {
                b.Fuel = 1;
                b.KmFromLastRefuel = 0;
            }
            bl.UpdateBus(b);
        }
        public void bgw1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ListBuses.ItemsSource = bl.GetAllBuses(); //to show the new Status in the list
        }
        public void bgw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ListBuses.ItemsSource = bl.GetAllBuses(); //to show the new Status in the list
        }

        public void GoRefuel_Click(object sender, RoutedEventArgs e) //going to refuel
        {
            BOBus b = (sender as Button).DataContext as BOBus;
            refueling(b);
        }
        public void refueling(BOBus b) //the refuel(helper func for the ditails window)
        {
            bgw2 = new BackgroundWorker(); //reset the refuel backgrounder
            bgw2.DoWork += bgw2_DoWork;
            bgw2.ProgressChanged += bgw2_ProgressChanged;
            bgw2.RunWorkerCompleted += bgw2_RunWorkerCompleted;
            bgw2.WorkerReportsProgress = true;
            bgw2.RunWorkerAsync(b); //send the current bus to refuel
        }
        public void bgw2_DoWork(object sender, DoWorkEventArgs e)
        {
            BOBus b = (BOBus)e.Argument; //get current bus
            b.Status = (StatusEnum)6; //="InRefuel"
            bl.UpdateBus(b);
            //button not enable?
            //b.isAvailable = false; //not available
            double tmp = b.Fuel;
            for (int i = 12; i > 0; i--)
            {
                //b.timerAct = "Coming back in " + bgwTimer(i);
                System.Threading.Thread.Sleep(1000); //wait 2 hours of refuel
                b.Fuel += (1 - tmp) / 12.0;
                bl.UpdateBus(b);
                bgw2.ReportProgress(1); //present changes
            }
            //update the changes: (after refuel)
            b.KmFromLastRefuel = 0;
            b.Fuel = 1;
            b.Status = (StatusEnum)1; //= Ready 
            bl.UpdateBus(b);
            //b.isAvailable = true; //available
            //b.timerAct = "";
        }
        public void bgw2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ListBuses.ItemsSource = bl.GetAllBuses();  //to show the new Status in the list
        }
        public void bgw2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ListBuses.ItemsSource = bl.GetAllBuses();  //to show the new Status in the list
        }

        public static string bgwTimer(int i) //describe the time that left to the end of the act
        {
            string str;
            if (i >= 60)
            {
                if (i / 60 / 10 != 0)
                    str = i / 60 + ":";
                else str = "0" + i / 60 + ":";
            }
            else str = "00:";
            if (i % 60 < 10)
                str += "0" + i % 60;
            else str += i % 60;
            return str;
        }

        private void LineStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //update cs (window)
        }

        private void LineStations_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();
            if (header == "Distance")
                e.Cancel = true;
            if (header == "BusLine")
                e.Cancel = true;
            if (header == "Arrive")
                e.Cancel = true;
            if (header == "IsExist")
                e.Cancel = true;
        }

        private void LineChoose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListLineTrips.ItemsSource =bl.GetAllLineTrips((int)LineChoose.SelectedItem);
            BOLine l = bl.GetLine((int)LineChoose.SelectedItem);
            BusLineOftrip.Text = l.BusLine.ToString();
            Destination.Text = bl.GetBusStation(l.LastStation).Name;
        }

        private void DeleteLineTrip_Click(object sender, RoutedEventArgs e)
        {
            BOLineTrip lt = (sender as Button).DataContext as BOLineTrip;
            try
            {
                bl.DeleteLineTrip(lt.LineCode, lt.Start);
                ListLineTrips.ItemsSource = bl.GetAllLineTrips((int)LineChoose.SelectedItem);  //refresh

            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR!\n" + ex.Message + "\nEdit and try again");
            }
        }

        private void UpdateLineTrip_Click(object sender, RoutedEventArgs e)
        {
            BOLineTrip lt = (sender as Button).DataContext as BOLineTrip;
            UpdateLineTrip upLt = new UpdateLineTrip(bl, lt);
            upLt.Closed += RefreshLineTrips;
            upLt.ShowDialog();
        }
    }
}