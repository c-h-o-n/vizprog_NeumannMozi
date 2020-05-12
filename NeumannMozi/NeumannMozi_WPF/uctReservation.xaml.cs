using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using NeumannMozi_DAL;

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for uctReservation.xaml
    /// </summary>
    public partial class uctReservation : UserControl {
        public uctReservation() {
            edmNeumannMoziContainer = new edmNeumannMoziContainer();
            InitializeComponent();
            GetRoomFromXml();
            
        }
        public uctReservation(FilmData choosenFilm) : this() {
            this.currentFilm = choosenFilm;
            GetAllFilmData();
        }
        #region CORE_VARIABLES
        private edmNeumannMoziContainer edmNeumannMoziContainer;
        private FilmData currentFilm;
        private List<RoomData> roomDatas = new List<RoomData>();
        List<Seat> seats;
        #endregion

        #region BUTTON_CLICK_EVENTS
        private void AddSeat_Click(object sender, RoutedEventArgs e) {
            // Szemleltetes celjabol somanak
            Button deleteme = sender as Button;
            Console.WriteLine(deleteme.Name.ToString() + " is pressed");
            foreach (var seat in seats) {
                if (deleteme.Name == seat.BtnName) {
                    Console.WriteLine(seat.Row + ". sor " + seat.Column + ". oszlop");
                }
            }
            //
            deleteme.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BD1222"));
        }
        #endregion

        #region DATABASE
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
        private void GetAllFilmData() {
            foreach (var x in edmNeumannMoziContainer.FilmSet) {
                if (this.currentFilm.Id == x.Id) {
                    currentFilm.Id = x.Id;
                    currentFilm.PosterImage = x.Poszter;
                    currentFilm.Title = x.Cim;
                    currentFilm.Director = x.Rendezo;
                    currentFilm.Cast = x.Szereplok;
                    currentFilm.Description = x.Leiras;
                    currentFilm.AgeRating = x.Korhatar;
                    currentFilm.Length = x.Hossz;
                    currentFilm.Category = x.Kategoria;
                    currentFilm.RoomNameForDates = GetRoomName(x.Id);
                    currentFilm.ScreeningDates = GetScreeningDates(x.Id);
                    currentFilm.ComboBoxSource = GetComboboxSource(x.Id);
                }
            }
            wpFilmCard1.DataContext = currentFilm;
            wpFilmCard2.DataContext = currentFilm;
        }
        #endregion

        #region MISC
        private void GetRoomFromXml() {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"D:\_temp delete me later\Neumann Mozi\Git Repo\NeumannMozi\NeumannMozi_WPF\mozi_termek.xml");
            foreach (XmlNode node in xmlDoc.DocumentElement) {
                roomDatas.Add(new RoomData { 
                                Id = int.Parse(node["terem_id"].InnerText),
                                Name = node.Attributes[0].Value,
                                Rows = int.Parse(node["sor"].InnerText),
                                Columns = int.Parse(node["oszlop"].InnerText)
                 });
            }
        }
        // Generate reservation buttons due to choosen room from combobox
        private void GenerateSeats(int rows, int columns) {
            seats = new List<Seat>();
            wpSeats.Rows = rows;
            wpSeats.Columns = columns;
            Console.WriteLine(rows + " x " + columns + " = " + columns*rows);
            int seatCounter = 0;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    seatCounter++;
                    Button addSeat = new Button();
                    addSeat.Content = seatCounter.ToString();
                    addSeat.Style = Resources["SeatButtonTheme"] as Style;
                    addSeat.Name = "btnSeat" + seatCounter.ToString();
                    addSeat.Click += AddSeat_Click;
                    wpSeats.Children.Add(addSeat);
                    seats.Add(new Seat { 
                                BtnName = addSeat.Name,       
                                Row = i+1,
                                Column = j+1 });
                }
            }
        }
        private void cbScreeningDates_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            wpSeats.Children.Clear();
            ComboBox cb = sender as ComboBox;
            string selectedRoomName = cb.SelectedItem.ToString();
            selectedRoomName = selectedRoomName.Substring(selectedRoomName.IndexOf("-")+2);

            Console.WriteLine(selectedRoomName);
            foreach (var x in roomDatas) {
                if (selectedRoomName == x.Name) {
                    GenerateSeats(x.Rows, x.Columns);
                }
            }
        }
        #endregion





        
        

    }
}
