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
            edmNeumannMoziContainer = new edmNeumannMoziContainer();
            InitializeComponent();
            GetRoomName();
            this.passedFilm = value;
            tbTitle.Text = "Vetítés hozzáadása\n" + passedFilm.Title;
        }

        #region CORE_VARIABLES
        public FilmData passedFilm { get; set; }
        private edmNeumannMoziContainer edmNeumannMoziContainer;
        #endregion

        #region BUTTON_CLICK_EVENTS
        private void btnConfirmScreeningDate_Click(object sender, RoutedEventArgs e) {
            try {
                var newDate = DateTime.Parse(txtNewScreeningDate.Text);
                //MessageBox.Show(passedFilm.Id.ToString());
                var vetitInsert = new Vetites {
                    Kezdete = newDate,
                    TeremId = GetSelectedRoomId(),
                    FilmId = passedFilm.Id
                };
                edmNeumannMoziContainer.VetitesSet.Add(vetitInsert);
                edmNeumannMoziContainer.SaveChanges();
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

        #region DATABASE
        private void GetRoomName() {
            var query = (from x in edmNeumannMoziContainer.TeremSet
                         select x.Nev).ToList();
            cbRoomName.ItemsSource = query;
        }
        #endregion

        private void cbRoomName_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Console.WriteLine();
            ComboBox cb = sender as ComboBox;
            RoomData room = new RoomData();
            Console.WriteLine(cb.SelectedValue);
        }
        private int GetSelectedRoomId() {
            foreach (var x in edmNeumannMoziContainer.TeremSet) {
                if (x.Nev == cbRoomName.SelectedItem.ToString()) {
                    return x.Id;
                }
            }
            return -1;
        }
    }
}
