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
    /// Interaction logic for uctAdmin.xaml
    /// </summary>
    public partial class uctAdmin : UserControl {
        public uctAdmin() {
            InitializeComponent();
        }

        private void btnAddMovie_Click(object sender, RoutedEventArgs e) {
            btnAddMovie.IsEnabled = false;
            winAddFilm winAddFilm = new winAddFilm();
            winAddFilm.Show();
            bool isclosed = false;
            
        }


    }
}
