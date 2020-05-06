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
using NeumannMozi_DAL; //adatbazis

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for uctShowTimes.xaml
    /// </summary>
    public partial class uctShowTimes : UserControl {
        private edmNeumannMoziContainer edmNeumannMoziContainer;
        public uctShowTimes() {
            // Create edm(database) object
            edmNeumannMoziContainer = new edmNeumannMoziContainer();
            InitializeComponent();

            GetCurrentShowTimes();


        }

        // TODO: azokat a filmeket (ezek az adatok kellenek lent) amiknek van olyan vetítés kezdete dátumuk ami korábbi a mai dátumnál továbbá a vetítés kezdete dátumait 
        private void GetCurrentShowTimes() {

            var currentDateTime = DateTime.Now; //jelenlegi datumot lekeri

            var query = (from x in edmNeumannMoziContainer.FilmSet
                         join vetit in edmNeumannMoziContainer.VetitesSet on x.Id equals vetit.FilmId
                         where vetit.Kezdete > currentDateTime
                         select new {
                             Title = x.Cim,
                             Director = x.Rendezo,
                             Cast = x.Szereplok,
                             Description = x.Leiras,
                             Length = x.Hossz,
                             AgeRating = x.Korhatar,
                             PosterLink = x.PoszterLink,
                             Category = x.Kategoria,
                             ScreeningDates = vetit.Kezdete,
                             TeremId = vetit.TeremId //többit igy hozza lehet adni vetites tablabol ha kell
                         }).ToList();

            ictrFilmCard.ItemsSource = query;
        }
    }
}
