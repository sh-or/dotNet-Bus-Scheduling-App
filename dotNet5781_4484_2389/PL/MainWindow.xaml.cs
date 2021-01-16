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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlAPI;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BlFactory.GetBl();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void userLogin_Click(object sender, RoutedEventArgs e)
        {
            BOUser u = new BOUser()
            {
                Name = iName.Text,
                IsExist = true,
                Password = iPassword.Text
            };

            //if......checking
            try
            {
                    u = bl.GetUser(u.Name,u.Password);
                    if (u.IsManager)
                    {
                        Manage manage = new Manage(bl);
                        manage.ShowDialog();
                    }
                    else
                    {
                        User userr = new User(bl);
                        userr.ShowDialog();
                    }
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR!\n" + ex.Message + "\nEdit and try again");
            }



            //Manage manage = new Manage(bl);
            //manage.ShowDialog();
        }

        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BOUser u = new BOUser()
                {
                    Name = iNameN.Text,
                    IsExist = true,
                    IsManager = (bool)IsManageN.IsChecked,
                    Password = iPasswordN.Text
                };

                //if......checking

                bl.AddUser(u);
                if (u.IsManager)
                {
                    Manage manageN = new Manage(bl);
                    manageN.ShowDialog();
                }
                else
                {
                    User userN = new User(bl);
                    userN.ShowDialog();
                }
            }
            catch (BLException ex)
            {
                MessageBox.Show("ERROR!\n" + ex.Message + "\nEdit and try again");
            }
        }
    }
}
