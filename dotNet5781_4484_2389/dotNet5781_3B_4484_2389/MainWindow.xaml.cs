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

namespace dotNet5781_3B_4484_2389
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Bus1> lst = new List<Bus1>(); //global buses list
        public Random r = new Random(DateTime.Now.Millisecond);


        public ObservableCollection<Bus1> Users;

        public MainWindow()
        {
            InitializeComponent();
            restart(); //restart buses, include 3 asked
            //list.ItemsSource = lst;


            Users = new ObservableCollection<Bus1>()
            {
            new Bus1(10000010, DateTime.Today.AddMonths(-15), DateTime.Today.AddMonths(-15)), //time from care
            new Bus1(10000011, DateTime.Today.AddMonths(-6), DateTime.Today.AddMonths(-6), 19800, 0, 19800), //km of care
            new Bus1(10000012, DateTime.Today.AddMonths(-6), DateTime.Today.AddMonths(-6), 0, 1100, 1100) //close to refuel};
            };
            list.DataContext = Users;
        }
        public void restart()//restart buses, include 3 asked
        {
            Bus1 bs;
            for (int i = 0; i < 10; i++)
            {
                bs = new Bus1(10000000 + i, DateTime.Today, DateTime.Today);
                lst.Add(bs);
            }
            lst.Add(new Bus1(10000010, DateTime.Today.AddMonths(-15), DateTime.Today.AddMonths(-15))); //time from care
            lst.Add(new Bus1(10000011, DateTime.Today.AddMonths(-6), DateTime.Today.AddMonths(-6), 19800, 0, 19800)); //km of care
            lst.Add(new Bus1(10000012, DateTime.Today.AddMonths(-6), DateTime.Today.AddMonths(-6), 0, 1100, 1100)); //close to refuel
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddBus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseBus_Click(object sender, RoutedEventArgs e)
        {
            int kmToDrive = r.Next(1201);
            if ((sender as Bus1).isReady(kmToDrive))
            {
                //תהליכון
                //status+show?->timer->km+kmRefuel+kmCare->status->show?
            }
            else
            {
                //message
                //show? for status...
            }

        }

        private void cmdDeleteUser_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }


    public class User
    {

        public string Name { get; set; }
        public int Age { get; set; }

    }
}

//< ListBox.ItemTemplate >
//    < DataTemplate >
//        < StackPanel >
//            < TextBlock Text = "djmk" />

//             < TextBlock Text = "{Binding Path=Description}" />

//              < TextBlock Text = "{Binding Path=Priority}" />

//           </ StackPanel >

//       </ DataTemplate >

//   </ ListBox.ItemTemplate >
