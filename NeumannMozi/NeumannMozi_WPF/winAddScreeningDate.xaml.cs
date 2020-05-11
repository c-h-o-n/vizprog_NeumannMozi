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
            tbTitle.Text = "Vetítés hozzáadása\n" + passedFilm.Title;
        }

        #region CORE_VARIABLES
        public FilmData passedFilm { get; set; }
        private edmNeumannMoziContainer MoziC = new edmNeumannMoziContainer();
        #endregion

        #region BUTTON_CLICK_EVENTS
        private void btnConfirmScreeningDate_Click(object sender, RoutedEventArgs e) {
            try {
                var newDate = DateTime.Parse(txtNewScreeningDate.Text);
                //MessageBox.Show(passedFilm.Id.ToString());
                var vetitInsert = new Vetites {
                    Kezdete = newDate,
                    TeremId = 1,
                    FilmId = passedFilm.Id
                };
                MoziC.VetitesSet.Add(vetitInsert);
                MoziC.SaveChanges();
                this.Close();
                ((winMain)Application.Current.MainWindow).wpCurrentContent.Children.Clear();
                ((winMain)Application.Current.MainWindow).wpCurrentContent.Children.Add(new uctAdmin());
            }
            catch (Exception) {
                MessageBox.Show("Nem jó dátumot adtál meg.");
                return;
            }
        }
        #endregion
    }
}
