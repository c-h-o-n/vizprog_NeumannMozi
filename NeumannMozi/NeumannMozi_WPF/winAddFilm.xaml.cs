using Microsoft.Win32;
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

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for AddFilm.xaml
    /// </summary>
    public partial class winAddFilm : Window {
        public winAddFilm() {
            InitializeComponent();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e) {
            // Get image from file dialog
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(.jpg)|*.jpg|(.png)|*png";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true) {
                txtPosterImagePath.Text = dialog.FileName.ToString();
            }

            //TODO: insert data to Filmset
        }
    }
}
