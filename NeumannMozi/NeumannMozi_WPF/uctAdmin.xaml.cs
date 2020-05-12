using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            winAddScreeningDate.Owner = Window.GetWindow(this);
            winAddScreeningDate.Show();

        }
        private void btnMoreInfo_Click(object sender, RoutedEventArgs e) {

        }
        private void btnRemoveFilm_Click(object sender, RoutedEventArgs e) {
            var getFilm = ((Button)sender).Tag as FilmData;
            var msgBox = MessageBox.Show("Biztos törlöd a(z) " + getFilm.Title.ToString() + " filmet az adatbázisból?", "Film törlése", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                ReloadScreen();
            } else {
                MessageBox.Show("Nemre ment");
            }
        }
        #endregion

        #region DATABASE

        private List<string> GetScreeningDates(int filmId) {
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
        // Return screening dates and rooms in string list
        private List<string> GetComboboxSource(int filmId) {
            List<string> vetitString = new List<string>();
            var currentDateTime = DateTime.Now;
            foreach (var x in edmNeumannMoziContainer.VetitesSet) {
                if (x.FilmId == filmId) {
                    if (x.Kezdete > currentDateTime) {
                        vetitString.Add(x.Kezdete.ToString() + " - " + x.Terem.Nev);
                    }
                }
            }
            return vetitString;
        }

        private List<string> GetRoomName(int filmId) {
            List<string> roomName = new List<string>();
            var currentDateTime = DateTime.Now;
            foreach (var x in edmNeumannMoziContainer.VetitesSet) {
                if (x.FilmId == filmId) {
                    if (x.Kezdete > currentDateTime) {
                        roomName.Add(x.Terem.Nev);
                    }
                }
            }
            return roomName;
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
                    RoomNameForDates = GetRoomName(x.Id),
                    ScreeningDates = GetScreeningDates(x.Id),
                    ComboBoxSource = GetComboboxSource(x.Id)
                }) ;
            }
            ictrAdmin.ItemsSource = filmLista;
        }


        #endregion

        #region MISC
        private void ReloadScreen() {
            ((winMain)Application.Current.MainWindow).wpCurrentContent.Children.Clear();
            ((winMain)Application.Current.MainWindow).wpCurrentContent.Children.Add(new uctAdmin());
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var getFilm = ((ComboBox)sender).Tag as FilmData;
            ComboBox cb = sender as ComboBox;

            for (int i = 0; i < getFilm.ComboBoxSource.Count; i++) {
                Console.WriteLine(getFilm.ComboBoxSource[i] + " + " + getFilm.ScreeningDates[i] + " + " + getFilm.RoomNameForDates[i]);
            }            
            
        }
        #endregion
    }
}