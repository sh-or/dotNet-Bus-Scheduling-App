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
        private void TextBox_LettersAndNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Tab || e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            if (Char.IsDigit(c)) //allow digits (without Shift or Alt)
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
            if (Char.IsLetter(c))
                    return;

                //forbid signs (#,$, %, ...)
                e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        } //checking if the input contains digits/letters only

        private void userLogin_Click(object sender, RoutedEventArgs e)
        {
            BOUser u = new BOUser()
            {
                Name = iName.Text,
                IsExist = true,
                Password = iPassword.Text
            };

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

                if(u.Name.Length<4 || u.Password.Length<4)
                {
                    MessageBox.Show("ERROR!\nToo short name or password\nEdit and try again", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

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
