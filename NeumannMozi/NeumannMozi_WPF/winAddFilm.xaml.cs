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

        #region CORE_VARIABLES
        edmNeumannMoziContainer MoziC = new edmNeumannMoziContainer();
        #endregion

        #region BUTTON_CLICK_EVENTS
        // Choose poster image
        private void btnAddImage_Click(object sender, RoutedEventArgs e) {
            // Get image from file dialog
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = " Képek |*.jpg;*.png|Minden|*.*";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true) {
                txtPosterImagePath.Text = dialog.FileName.ToString();
            }
            
        }
        // Insert new film into DB
        private void btnSend_Click(object sender, RoutedEventArgs e) {
            InsertFilmIntoDB();

            // Close film adding window
            this.Close();
            // Reload wrappanel's content in winMain
            ((winMain)Application.Current.MainWindow).wpCurrentContent.Children.Clear();
            ((winMain)Application.Current.MainWindow).wpCurrentContent.Children.Add(new uctAdmin());
            ((winMain)Application.Current.MainWindow).svContent.ScrollToBottom();
        }
        #endregion

        #region DATABASE
        private void InsertFilmIntoDB() {
            byte[] bytes = null;
            try {
                int n = Int32.Parse(txtLength.Text);
                bytes = File.ReadAllBytes(txtPosterImagePath.Text);
            } catch (Exception) {// ha nem számot ír a hosszhoz // nem lehet a fájlt feldolgozni
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
        }
        #endregion
    }
}
