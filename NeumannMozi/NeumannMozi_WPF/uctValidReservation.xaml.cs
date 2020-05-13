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

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for uctValidReservation.xaml
    /// </summary>
    public partial class uctValidReservation : UserControl {
        public uctValidReservation() {
            // Set parent stackpanel alignment to center
            ((winMain)Application.Current.MainWindow).wpCurrentContent.VerticalAlignment = VerticalAlignment.Center;
            InitializeComponent();
        }
    }
}
