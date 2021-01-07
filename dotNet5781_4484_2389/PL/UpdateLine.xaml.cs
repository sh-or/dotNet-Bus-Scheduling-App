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
using BlAPI;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdateLine.xaml
    /// </summary>
    public partial class UpdateLine : Window
    {
        IBL bl;
        public UpdateLine(IBL ibl)
        {
            bl = ibl;
            InitializeComponent();
        }
    }
}
