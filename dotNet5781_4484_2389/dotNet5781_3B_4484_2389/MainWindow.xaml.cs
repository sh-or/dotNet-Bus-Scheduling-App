using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
// < !---< Label Content = "--" HorizontalAlignment = "Center" Grid.Row = "1" VerticalAlignment = "Center" Grid.Column = "4" Height = "26" Width = "92" Grid.ColumnSpan = "3" />              
// < Label Content = "--" HorizontalAlignment = "Center" Grid.Row = "1" VerticalAlignment = "Center" Grid.Column = "5" Height = "26"  Width = "100" Grid.ColumnSpan = "3" /> -->
//<TextBlock Text="{Binding Path=kmOfLastRefuel}" Grid.Column="5" HorizontalAlignment="Center" />
//< TextBlock Text = "{Binding Path=LastCare}" Grid.Column = "6" HorizontalAlignment = "Left" />

//<TextBlock Text="{Binding Path=kmOfLastCare}" Grid.Column="4" HorizontalAlignment="Center" />

namespace dotNet5781_3B_4484_2389
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static BackgroundWorker bgw;  // BackgroundWorker drive
        public static BackgroundWorker bgw1;  // BackgroundWorker care
        public static BackgroundWorker bgw2;  // BackgroundWorker refuel
        public Random r = new Random(DateTime.Now.Millisecond);
        public static ObservableCollection<Bus1> buses = new ObservableCollection<Bus1>();  //the buses collection
        //public static int numOfBuses() { get{ return buses.Count; } set{ } }; //sum of buses in the collection

        public static int numOfKm { get; set; } //input from the user
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                restart(); //restart buses, include 3 asked
                busesLB.ItemsSource = buses;
                sumBuses.DataContext = buses;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }
        public void restart()//restart buses, include 3 asked
        {
            Bus1 bs;
            for (int i = 0; i < 10; i++)
            {
                bs = new Bus1(10000000 + i, DateTime.Today, DateTime.Today);
                buses.Add(bs);
            }
            buses.Add(new Bus1(10000010, DateTime.Today.AddMonths(-15), DateTime.Today.AddMonths(-15))); //time from care
            buses.Add(new Bus1(10000011, DateTime.Today.AddMonths(-6), DateTime.Today.AddMonths(-6), 19800, 0, 19800)); //km of care
            buses.Add(new Bus1(10000012, DateTime.Today.AddMonths(-6), DateTime.Today.AddMonths(-6), 0, 1100, 1100)); //close to refuel
        }
        private void AddBus_Click(object sender, RoutedEventArgs e)  //add bus
        {
            AddBus addB = new AddBus();
            addB.Closed += AddB_Closed;
            addB.ShowDialog();
        }

        private void AddB_Closed(object sender, EventArgs e)  //add the new bus to list buses
        {
            Bus1 bus = ((AddBus)sender).bs;
            try
            {
                if (isBusExist(bus.showLicenseNum)) //checking if the bus is allready exist
                    MessageBox.Show("ERROR! This license number is allready exist in the system", "", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    buses.Add(bus);
                    busesLB.Items.Refresh();
                    MessageBox.Show("The bus was added successfully");
                }
            }
            catch (Exception)
            { return; }
        }
        public bool isBusExist(string lcn) //checking if bus exist in the list by its license number
        {
            foreach (Bus1 b in buses)
                if (b.showLicenseNum == lcn)
                    return true;
            return false;
        }
        public Bus1 findBus(string lcn) //return bus from the list by its license number
        {
            foreach (Bus1 b in buses)
                if (b.showLicenseNum == lcn)
                    return b;
            throw new Exception("Bus was not found");
        }
        
        public void ChooseBus_Click(object sender, RoutedEventArgs e) //choosing bus for a ride
        {
            Window1 win1 = new Window1();
            Bus1 b = (sender as Button).DataContext as Bus1;
            win1.ShowDialog();
            if (b.isReady(numOfKm))
            {
                bgw = new BackgroundWorker(); //reset the drive backgrounder
                bgw.DoWork += bgw_DoWork;
                bgw.ProgressChanged += bgw_ProgressChanged;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                bgw.WorkerReportsProgress = true;
                //(sender as Button).IsEnabled = false;
                bgw.RunWorkerAsync(b); //sending the current bus to drive
                //bgw.RunWorkerAsync(sender as Button); //sending the sender details
            }
            else
            { //appropriate message:
                if (b.status == (Status)2)
                    MessageBox.Show("The bus need care", "", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (b.status == (Status)3)
                    MessageBox.Show("The bus need refuel", "", MessageBoxButton.OK, MessageBoxImage.Error);
                else if ((numOfKm + b.kmOfLastCare) > 20000) //the km from last care
                    MessageBox.Show("The bus will pass the permitted kilometerage before care", "", MessageBoxButton.OK, MessageBoxImage.Error);
                else if ((numOfKm + b.kmOfLastRefuel) > 1200) //the km from last refuel
                    MessageBox.Show("There is no enough fuel for this ride", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void bgw_DoWork(object sender, DoWorkEventArgs e) //going on ride
        {
            int num = numOfKm; //save the current distance for this ride
            if(num>0)
                MessageBox.Show("The bus left to the ride!");
            Bus1 b = (Bus1)e.Argument; //get current bus
            //Bus1 b = (e.Argument as Button).DataContext as Bus1;//get current bus

            b.status = (Status)4; //="InDrive"
                                  //     busesLB.Background = SolidColorBrush.ColorProperty.Name
            b.isAvailable = false; //not available
            for (int i = (num / r.Next(20, 51)) * 6; i > 0; i--)
            {
                b.timerAct = "Coming back in " + bgwTimer(i);
                System.Threading.Thread.Sleep(1000); //wait 0.1 sec(=sleep(100)) for 1 minute of ride
                bgw.ReportProgress(4); //present changes
            }
            //System.Threading.Thread.Sleep(3000); //example 3 sec
            //update the changes from the ride: (after the ride)
            b.Kilometerage += num;
            b.kmOfLastCare += num;
            b.kmOfLastRefuel += num;
            b.Fuel = 1 - b.kmOfLastRefuel / 1200.0;
            if (b.kmOfLastCare > 18500) //checking km from last care
                b.status = (Status)2; //= need care 
            else if (b.kmOfLastRefuel > 1000) //checking fuel
                b.status = (Status)3; //= need refuel 
            else
                b.status = (Status)1; //= ready 
            b.isAvailable = true; //available
            b.timerAct = "";
        }
        public void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           // busesLB.Background = Brushes.Blue;

            busesLB.Items.Refresh(); //to show the new status in the list
        }
        public void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //finishing ride
        {
            busesLB.Items.Refresh(); //to show the changes in the list
        }

        public void GoCare_Click(object sender, RoutedEventArgs e) //going to care
        {
            Bus1 b = (sender as Button).DataContext as Bus1;
            caring(b);
        }
        public void caring(Bus1 b) //the care(helper func for the ditails window)
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
            Bus1 b = (Bus1)e.Argument; //get current bus
            b.status = (Status)5; //="InCare"
            b.isAvailable = false; //not available
            for (int i = 144; i > 0; i--)
            {
                b.timerAct = "Coming back in " + bgwTimer(i);
                System.Threading.Thread.Sleep(1000); //wait 24 hours of care
                bgw1.ReportProgress(5); //present changes
            }

            //update the changes from the ride: (after the ride)
            b.lastCare = DateTime.Now;
            b.kmOfLastCare = 0;
            b.status = (Status)1; //= Ready  (if not-will go to refuel...)
            b.isAvailable = true; //available
            b.timerAct = "";
            if (b.kmOfLastRefuel > 1000) //checking fuel
            //bgw2.RunWorkerAsync(b); //send the current bus to refuel
            {
                b.Fuel = 1;
                b.kmOfLastRefuel = 0;
            }
        }
        public void bgw1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            busesLB.Items.Refresh(); //to show the new status in the list
            //timer?
        }
        public void bgw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            busesLB.Items.Refresh(); //to show the new status in the list
        }

        public void GoRefuel_Click(object sender, RoutedEventArgs e) //going to refuel
        {
            Bus1 b = (sender as Button).DataContext as Bus1;
            refueling(b);
        }
        public void refueling(Bus1 b) //the refuel(helper func for the ditails window)
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
            Bus1 b = (Bus1)e.Argument; //get current bus
            b.status = (Status)6; //="InRefuel"
            b.isAvailable = false; //not available
            double tmp = b.Fuel;
            for(int i=12;i>0;i--)
            {
                b.timerAct = "Coming back in "+bgwTimer(i);
                System.Threading.Thread.Sleep(1000); //wait 2 hours of refuel
                b.Fuel += (1 - tmp) / 12.0;
                bgw2.ReportProgress(1); //present changes
            }
            //update the changes: (after refuel)
            b.kmOfLastRefuel = 0;
            b.Fuel = 1200.0;
            b.status = (Status)1; //= Ready 
            b.isAvailable = true; //available
            b.timerAct = "";
        }
        public void bgw2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            busesLB.Items.Refresh(); //to show the new status in the list
        }
        public void bgw2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) 
        {
            busesLB.Items.Refresh(); //to show the new status in the list
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
                str += "0" + i%60;
            else str += i % 60;
            return str;
        }
        private void busesLB_MouseDoubleClick(object sender, MouseButtonEventArgs e) //double click for details
        {
            Bus1 b = (sender as ListBox).SelectedValue as Bus1;
            Details details = new Details(b); //send the current bus
            details.ShowDialog();
        }
    }

}

