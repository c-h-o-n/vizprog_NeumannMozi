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
        public edmNeumannMoziContainer edmNeumannMoziContainer;
        public uctShowTimes() {
            // Create edm(database) object
            edmNeumannMoziContainer = new edmNeumannMoziContainer();
            InitializeComponent();

            GetCurrentShowTimes();
        }

        private string vetitIdok(int filmId) {
            var vetitString = "";
            var currentDateTime = DateTime.Now;
            foreach (var x in edmNeumannMoziContainer.VetitesSet) {
                if (x.FilmId == filmId) {
                    if (x.Kezdete > currentDateTime) {
                        vetitString += x.Kezdete.ToString() + "\n";
                    }
                }
            }
            return vetitString;
        }

        private void GetCurrentShowTimes() {

            var filmLista = new List<FilmData>();
            foreach (var x in edmNeumannMoziContainer.FilmSet) {
                if (vetitIdok(x.Id).Length > 0) {
                    filmLista.Add(new FilmData() {
                        Id = x.Id,
                        PosterImage = x.Poszter,
                        Title = x.Cim,
                        Director = x.Rendezo,
                        Cast = x.Szereplok,
                        Description = "Leírás ",//x.Leiras.Substring(0, 10) + "...", //TODO: temporary
                        AgeRating = x.Korhatar,
                        Length = x.Hossz,
                        Category = x.Kategoria,
                        ScreeningDates = vetitIdok(x.Id),
                    });
                }
            }
            ictrCurrentShowTimes.ItemsSource = filmLista;
        }

        private void btnFilmCard_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Open next uct");
        }
    }

}
