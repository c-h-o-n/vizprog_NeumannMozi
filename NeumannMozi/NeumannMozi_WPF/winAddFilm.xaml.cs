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
using NeumannMozi_DAL;
using System.IO;

namespace NeumannMozi_WPF {
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
                //var img = File.ReadAllBytes(dialog.FileName);
                txtPosterImagePath.Text = dialog.FileName.ToString();
            }
            //TODO: insert data to Filmset
        }

        edmNeumannMoziContainer MoziC = new edmNeumannMoziContainer();
        private void btnSend_Click(object sender, RoutedEventArgs e) {

            byte[] bytes = null;
            try {
                int n = Int32.Parse(txtLength.Text);
                bytes = File.ReadAllBytes(txtPosterImagePath.Text);
            }
            catch (Exception) {// ha nem számot ír a hosszhoz // nem lehet a fájlt feldolgozni
                MessageBox.Show("Hibás adatok!");
                return;
            }
            var filmInsert = new Film {
                Cim = txtTitle.Text,
                Rendezo = txtDirector.Text,
                Szereplok = txtCast.Text,
                Leiras = txtDescription.Text,
                Hossz = Int32.Parse(txtLength.Text),
                Korhatar = Int32.Parse(cboAgeRating.Text),
                Kategoria = txtCategory.Text,
                ElozetesLink = txtTrailerLink.Text,
                ImdbLink = txtImdb.Text,
                Poszter = bytes
            };
            MoziC.FilmSet.Add(filmInsert);
            MoziC.SaveChanges();
            MessageBox.Show("Sikeresen hozzáadtad a filmet");
        }
    }
}
