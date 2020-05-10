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
using NeumannMozi_DAL;

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for uctAdmin.xaml
    /// </summary>
    public partial class uctAdmin : UserControl {
        public uctAdmin() {
            edmNeumannMoziContainer = new edmNeumannMoziContainer();
            InitializeComponent();
            GetCurrentShowTimes();

        }

        #region CORE_VARIABLES
            private edmNeumannMoziContainer edmNeumannMoziContainer;
        #endregion


        #region BUTTON_CLICK_EVENTS
        private void btnAddMovie_Click(object sender, RoutedEventArgs e) {
            btnAddMovie.IsEnabled = false;
            winAddFilm winAddFilm = new winAddFilm();
            winAddFilm.Show();
        }

        private void btnAddScreeningDate_Click(object sender, RoutedEventArgs e) {
            //MessageBox.Show();
            var getFilm = ((Button)sender).Tag as FilmData;
            //MessageBox.Show(getFilm.Id.ToString());
            winAddScreeningDate winAddScreeningDate = new winAddScreeningDate(getFilm);
            winAddScreeningDate.Show();
        }
        private void btnMoreInfo_Click(object sender, RoutedEventArgs e) {

        }
        private void btnRemoveFilm_Click(object sender, RoutedEventArgs e) {
            var getFilm = ((Button)sender).Tag as FilmData;
            var msgBox = MessageBox.Show("Biztos törlöd a(z) "+getFilm.Title.ToString() + " filmet az adatbázisból?","Film törlése", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msgBox == MessageBoxResult.Yes) {
                var deleteRow = (from x in edmNeumannMoziContainer.FilmSet
                                 where x.Id.Equals(getFilm.Id)
                                 select x).FirstOrDefault();

                var deleteAllScreeningDates = (from x in edmNeumannMoziContainer.VetitesSet
                                               where x.FilmId.Equals(getFilm.Id)
                                               select x).ToList();

                foreach (var screeningDate in deleteAllScreeningDates) {
                    edmNeumannMoziContainer.VetitesSet.Remove(screeningDate);
                }
                edmNeumannMoziContainer.FilmSet.Remove(deleteRow);
                edmNeumannMoziContainer.SaveChanges();// itt a hiba :(
                MessageBox.Show("Sikeresen törölve.");
            }
            else {
                MessageBox.Show("Nemre ment");
            }
        }
        #endregion

        #region DATABASE

        private List<string> ScreeningDateData(int filmId) {
            List<string> vetitString = new List<string>();
            var currentDateTime = DateTime.Now;
            foreach (var x in edmNeumannMoziContainer.VetitesSet) {
                if (x.FilmId == filmId) {
                    if (x.Kezdete > currentDateTime) {
                        vetitString.Add(x.Kezdete.ToString());
                    }
                }
            }
            return vetitString;
        }
        List<FilmData> filmLista = new List<FilmData>();
        private void GetCurrentShowTimes() {
            foreach (var x in edmNeumannMoziContainer.FilmSet) {
                    filmLista.Add(new FilmData() {
                        Id = x.Id,
                        PosterImage = x.Poszter,
                        Title = x.Cim,
                        Director = x.Rendezo,
                        Cast = x.Szereplok,
                        Description = x.Leiras,
                        AgeRating = x.Korhatar,
                        Length = x.Hossz,
                        Category = x.Kategoria,
                        NumberOfSeats = 0,
                        ScreeningDates = ScreeningDateData(x.Id),
                    });
            }

            foreach (var filmData in filmLista) {
                if (filmData.Description.Length > 300) {
                    filmData.Description = filmData.Description.Substring(0, 300) + "...";
                }
            }
            ictrAdmin.ItemsSource = filmLista;
        }


        #endregion

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ComboBox cb = new ComboBox();
            cb = sender as ComboBox;
            ictrAdmin.ItemsSource = filmLista;
        }
    }
}
