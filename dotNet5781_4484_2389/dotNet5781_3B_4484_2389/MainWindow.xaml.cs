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


namespace dotNet5781_3B_4484_2389
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static BackgroundWorker bgw;  // BackgroundWorker
        public static BackgroundWorker bgw1;  // BackgroundWorker
        public static List<Bus1> lst = new List<Bus1>(); //global buses list
        public Random r = new Random(DateTime.Now.Millisecond);
        public static ObservableCollection<Bus1> buses = new ObservableCollection<Bus1>();  //the buses collection
        public static int numOfKm { get; set; } //input from the user
        public MainWindow()
        {
            InitializeComponent();
            restart(); //restart buses, include 3 asked
            busesLB.ItemsSource = buses;
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

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseBus_Click(object sender, RoutedEventArgs e) //choosing bus for a ride
        {
            Window1 win1 = new Window1();
            Bus1 b = (sender as Button).DataContext as Bus1;
            //win1.var = b;
            win1.ShowDialog();
            if (b.isReady(numOfKm))
            {
                bgw = new BackgroundWorker();
                bgw.DoWork += bgw_DoWork;
                bgw.ProgressChanged += bgw_ProgressChanged;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;

                  bgw.WorkerReportsProgress = true;
                bgw.RunWorkerAsync(b); //sending the current bus
            }
            else
            { //appropriate message:
                if (b.status == (Status)2)
                    MessageBox.Show("The bus need care");
                else if (b.status == (Status)3)
                    MessageBox.Show("The bus need refuel");
                else if ((numOfKm + b.kmOfLastCare) > 20000) //the km from last care
                    MessageBox.Show("The bus will pass the permitted kilometerage before care");
                else if ((numOfKm + b.kmOfLastRefuel) > 1200) //the km from last refuel
                    MessageBox.Show("There is no enough fuel for this ride");
            }
            //busesLB.Items.Refresh();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            int num = numOfKm; //save the current distance for this ride
            Bus1 b = (Bus1)e.Argument; //get current bus
            //b.status = (Status)4; //="InDrive"
            //busesLB.Items.Refresh(); //to show the new status in the list
            //bgw.ReportProgress.
            bgw.ReportProgress(4); //timer?
            System.Threading.Thread.Sleep((num / r.Next(20, 51)) * 60 * 100); //wait 0.1 sec(=sleep(100)) for 1 minute of ride
            //System.Threading.Thread.Sleep(3000); //example 3 sec

            //update the changes from the ride: (after the ride)
            b.Kilometerage += num;
            b.kmOfLastCare += num;
            b.kmOfLastRefuel += num;
            if (b.kmOfLastCare > 18500) //checking km from last care
                b.status = (Status)2; //= need care 
            else if (b.kmOfLastRefuel > 1000) //checking fuel
                b.status = (Status)3; //= need refuel 
            else
                b.status = (Status)1; //= ready
        }  //going on ride
        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           // Bus1 b = e.Argument as Bus1; //get current bus
           // b.status = (Status)4; //="InDrive"
            busesLB.Items.Refresh(); //to show the new status in the list
            //timer?
        } 
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            busesLB.Items.Refresh(); //to show the changes in the list
        } //finishing ride

        private void GoCare_Click(object sender, RoutedEventArgs e)
        {
            Bus1 b = (sender as Button).DataContext as Bus1;
            bgw1 = new BackgroundWorker();
            bgw1.DoWork += bgw1_DoWork;
            bgw1.ProgressChanged += bgw1_ProgressChanged;
            bgw1.RunWorkerCompleted += bgw1_RunWorkerCompleted;

            //  bgw1.WorkerReportsProgress = true;
            bgw1.RunWorkerAsync(b);
        }
        private void bgw1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bus1 b = (Bus1)e.Argument; //get current bus
            b.status = (Status)5; //="InCare"
            busesLB.Items.Refresh(); //to show the new status in the list
            System.Threading.Thread.Sleep(144 * 100); //wait 24 hours of care
            //System.Threading.Thread.Sleep(3000); //example 3 sec
            // bgw.ReportProgress(1); //timer?

            //update the changes from the ride: (after the ride)
            b.lastCare = DateTime.Now;
            b.kmOfLastCare = 0;

        //    if ((b.kmOfLastCare) > 18500) //checking km from last care
              //  b.kmOfLastCare   //= need care 
         //   else if (b.kmOfLastRefuel > 1000) //checking fuel
                b.kmOfLastRefuel = 0;//=need refuel
          
        }
        private void bgw1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //timer?
        }
        private void bgw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

    private void GoRefuel_Click(object sender, RoutedEventArgs e)
        {

        }


    }



}

