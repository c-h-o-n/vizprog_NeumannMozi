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
            roomDatas = new List<RoomData>();
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
        private List<RoomData> roomDatas;
        private int selectedRoomId;
        private int selectedScreeningDateId;
        private List<Seat> seats;
        #endregion

        #region BUTTON_CLICK_EVENTS
        // Add seat to choosed seats
        private void AddSeat_Click(object sender, RoutedEventArgs e) {
            Button deleteme = sender as Button;
            foreach (var seat in seats) {
                if (deleteme.Name == seat.BtnName && seat.Available == true) {
                    seat.Res = !seat.Res;
                    // FRONTEND
                    if (seat.Res) {
                        deleteme.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BD1222"));
                    }
                    else {
                        deleteme.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#52DE57"));
                    }
                    //
                }
            }
        }
        private void btnImdb_Click(object sender, RoutedEventArgs e) {
            try {
                System.Diagnostics.Process.Start(currentFilm.ImdbLink);

            } catch (Exception) {
                MessageBox.Show("Az adatbázisban nincs a filmhez csatolható IMDb oldal");
            }
        }
        // Insert choosed seats to the db
        private void btnSendReservation_Click(object sender, RoutedEventArgs e) {
            InsertReservation();
            ((winMain)Application.Current.MainWindow).wpCurrentContent.Children.Clear();
            ((winMain)Application.Current.MainWindow).wpCurrentContent.Children.Add(new uctValidReservation());
        }


        #endregion

        #region DATABASE

            #region GET_DATA_FROM_DB
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
                        currentFilm.TrailerLink = x.ElozetesLink;
                        currentFilm.ImdbLink = x.ImdbLink;
                        currentFilm.RoomNameForDates = GetRoomName(x.Id);
                        currentFilm.ScreeningDates = GetScreeningDates(x.Id);
                        currentFilm.ComboBoxSource = GetComboboxSource(x.Id);
                    }
                }
                wpFilmCard.DataContext = currentFilm;
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
            private void GetSeatId() {
                    foreach (var x in edmNeumannMoziContainer.UlesSet) {
                        foreach (var s in seats) {
                            if (x.Sor == s.Row && x.Szam == s.Column && selectedRoomId == x.TeremId) {
                                s.SeatId = x.Id;
                            }
                        }
                    }
                }
            private void GetReservedSeats() {
                    GetSeatId();
                    foreach (Button x in wpSeats.Children) {
                        if (x is Button) {
                            foreach (var s in edmNeumannMoziContainer.Ules_foglalasSet) {
                                if (s.VetitesId == selectedScreeningDateId) {
                                    foreach (var seat in seats) {
                                        if (seat.SeatId == s.UlesId && x.Name == seat.BtnName) {
                                            seat.Available = false;
                                            x.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#707070"));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            #endregion

            #region GET_DATA_FROM_SCREEN
            private void GetSelectedScreeningDateId(string selectedScreeningDate) {
                    foreach (var x in edmNeumannMoziContainer.VetitesSet) {
                        if (x.Kezdete.ToString() == selectedScreeningDate && x.FilmId == currentFilm.Id) {
                            selectedScreeningDateId = x.Id;
                        }
                    }
                }
            #endregion

            #region INSERT_DATA_INTO_DB
            private void InsertReservation() {
                var reservationInsert = new Foglalas {
                    FelhasznaloId = winLogin.loginId,
                    Datum = DateTime.Now,
                    Foglalt = true,
                    FizetveVan = true,
                    Aktiv = true
                };
                foreach (var x in seats) {
                    if (x.Res) {
                        var seatReservationInsert = new Ules_foglalas {
                            Kategoria = "Tomi",
                            UlesId = x.SeatId,
                            VetitesId = selectedScreeningDateId,
                            Foglalas = reservationInsert
                        };
                        edmNeumannMoziContainer.Ules_foglalasSet.Add(seatReservationInsert);
                    }
                }
                edmNeumannMoziContainer.FoglalasSet.Add(reservationInsert);
                edmNeumannMoziContainer.SaveChanges();
            }
            #endregion

        #endregion

        #region COMBOBOX_EVENTS
        private void cbScreeningDates_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            wpSeats.Children.Clear();
            ComboBox cb = sender as ComboBox;
            // Get selected room name and screening date from combobox item value
            string selectedRoomName = cb.SelectedItem.ToString();
            selectedRoomName = selectedRoomName.Substring(selectedRoomName.IndexOf("-") + 2);

            string selectedScreeningDate = cb.SelectedItem.ToString();
            selectedScreeningDate = selectedScreeningDate.Substring(0, selectedScreeningDate.IndexOf("-") - 1);
            //
            // Generate room
            foreach (var x in roomDatas) {
                if (selectedRoomName == x.Name) {
                    selectedRoomId = x.Id;
                    GetSelectedScreeningDateId(selectedScreeningDate);
                    GenerateSeats(x.Rows, x.Columns);
                }
            }
        }
        // Disable changing combobox selected item with scroll wheel
        private void cbScreeningDates_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            MouseWheelEventArgs mouseArgs = (MouseWheelEventArgs)e;
            e.Handled = true;
            MouseWheelEventArgs args = new MouseWheelEventArgs(mouseArgs.MouseDevice, mouseArgs.Timestamp, mouseArgs.Delta);
            args.RoutedEvent = UIElement.MouseWheelEvent;
            args.Source = sender;
            ((winMain)Application.Current.MainWindow).svContent.RaiseEvent(args);
        }
        #endregion

        #region MISC
        // Get roomdata(id, name, rows, columns) from xml 
        private void GetRoomFromXml() {
            try {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("./../../mozi_termek.xml");
                foreach (XmlNode node in xmlDoc.DocumentElement) {
                    roomDatas.Add(new RoomData {
                        Id = int.Parse(node["terem_id"].InnerText),
                        Name = node.Attributes[0].Value,
                        Rows = int.Parse(node["sor"].InnerText),
                        Columns = int.Parse(node["oszlop"].InnerText)

                    });
                }
            } catch (Exception) {
                MessageBox.Show("Az XML fájl nem található.");
            }
        }
        
        // Generate reservation buttons due to choosen room from combobox
        private void GenerateSeats(int rows, int columns) {
            seats = new List<Seat>();
            wpSeats.Rows = rows;
            wpSeats.Columns = columns;
            int seatCounter = 0;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    seatCounter++;
                    Button addSeat = new Button();
                    addSeat.Content = seatCounter.ToString();
                    ResourceDictionary myResourceDictionary = new ResourceDictionary();
                    myResourceDictionary.Source = new Uri("ButtonTheme.xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
                    addSeat.Resources = myResourceDictionary;
                    addSeat.Style = (Style)FindResource("SeatButtonTheme");
                    addSeat.Name = "btnSeat" + seatCounter.ToString();
                    addSeat.Click += AddSeat_Click;
                    wpSeats.Children.Add(addSeat);
                    seats.Add(new Seat {
                        BtnName = addSeat.Name,
                        Row = i + 1,
                        Column = j + 1,
                        Available = true,
                        Res = false
                    }
                    );
                }
            }
            GetReservedSeats();
        }
        #endregion

        
    }
}
