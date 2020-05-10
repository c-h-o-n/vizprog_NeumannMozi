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

        }
        private void btnMoreInfo_Click(object sender, RoutedEventArgs e) {

        }
        private void btnRemoveFilm_Click(object sender, RoutedEventArgs e) {

        }
        #endregion

        #region DATABASE
        private void GetCurrentShowTimes() {
            var query = (from x in edmNeumannMoziContainer.FilmSet
                         join vetit in edmNeumannMoziContainer.VetitesSet on x.Id equals vetit.FilmId
                         orderby vetit.Kezdete
                         select new {
                             id = x.Id,
                             PosterImage = x.Poszter,
                             Title = x.Cim,
                             Director = x.Rendezo,
                             Cast = x.Szereplok,
                             Description = x.Leiras.Substring(0, 300) + "...", //TODO: temporary
                             AgeRating = x.Korhatar,
                             Length = x.Hossz,
                             Category = x.Kategoria,
                             ScreeningDates = vetit.Kezdete,
                             TeremId = vetit.TeremId //többit igy hozza lehet adni vetites tablabol ha kell
                         }).ToList();

            ictrAdmin.ItemsSource = query;
        }

        #endregion



    }
}
