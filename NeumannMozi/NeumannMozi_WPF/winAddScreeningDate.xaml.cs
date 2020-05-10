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

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for winAddScreeningDate.xaml
    /// </summary>
    public partial class winAddScreeningDate : Window {
        public winAddScreeningDate(FilmData value) {
            InitializeComponent();
            this.passedFilm = value;
            lblTitle.Content = "Vetítés hozzáadása: "+passedFilm.Title;
        }
        public FilmData passedFilm { get; set; }

        edmNeumannMoziContainer MoziC = new edmNeumannMoziContainer();
        private void btnConfirmScreeningDate_Click(object sender, RoutedEventArgs e) {
            try {
                var newDate = DateTime.Parse(txtNewScreeningDate.Text);
                //MessageBox.Show("Jó a dátum");
                //MessageBox.Show(passedFilm.Id.ToString());
                var vetitInsert = new Vetites {
                    Kezdete = newDate,
                    TeremId = 1,
                    FilmId = passedFilm.Id
                };
                MoziC.VetitesSet.Add(vetitInsert);
                MoziC.SaveChanges();
                this.Close();
                MessageBox.Show("Sikeresen hozzáadtad a vetitési időpontot!");
            }
            catch (Exception) {
                MessageBox.Show("Nem jó dátumot adtál meg.");
                return;
            }
        }
    }
}
