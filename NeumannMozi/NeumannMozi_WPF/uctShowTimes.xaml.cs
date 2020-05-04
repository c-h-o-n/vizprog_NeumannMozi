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
    /// Interaction logic for uctShowTimes.xaml
    /// </summary>
    public partial class uctShowTimes : UserControl {
        public uctShowTimes() {
            InitializeComponent();


            Image finalImage = new Image();
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(@"C:\Users\Chon\Desktop\Spotify Album\19225849_1879843315601015_7390265863571418010_n.jpg");
            logo.EndInit();


            myPicture.Source = logo;

        }
    }
}
