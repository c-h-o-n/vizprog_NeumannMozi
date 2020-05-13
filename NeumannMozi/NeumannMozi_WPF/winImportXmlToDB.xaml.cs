using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using NeumannMozi_DAL;

namespace NeumannMozi_WPF {
    /// <summary>
    /// Interaction logic for winImportXmlToDB.xaml
    /// </summary>
    public partial class winImportXmlToDB : Window {
        private List<RoomData> roomDatas;
        private edmNeumannMoziContainer edmNeumannMoziContainer;
        public winImportXmlToDB() {
            roomDatas = new List<RoomData>();
            edmNeumannMoziContainer = new edmNeumannMoziContainer();
            InitializeComponent();
            GetRoomFromXml();
        }

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
                ictrlRooms.ItemsSource = roomDatas;
            } catch (Exception) {
                MessageBox.Show("Az XML fájl nem található.");
            }
        }



        private void btnSyncXmlToDB_Click(object sender, RoutedEventArgs e) {
            try {
                foreach (var room in roomDatas) {
                    bool roomExist = false;
                    foreach (var x in edmNeumannMoziContainer.TeremSet) {
                        if (x.Id == room.Id && x.Nev == room.Name) {
                            roomExist = true;
                        }
                    }
                    if (!roomExist) {
                        edmNeumannMoziContainer.TeremSet.Add(new Terem {
                            Id = room.Id,
                            Nev = room.Name,
                            UlesekSzama = (room.Rows * room.Columns),
                            TakaritaniKell = false
                        });
                    }

                    for (int rows = 0; rows < room.Rows; rows++) {
                        for (int columns = 0; columns < room.Columns; columns++) {
                            bool seatExist = false;
                            foreach (var x in edmNeumannMoziContainer.UlesSet) {
                                if (x.Sor == rows + 1 && x.Szam == columns + 1 && x.TeremId == room.Id) {
                                    seatExist = true;
                                }
                            }
                            if (!seatExist) {
                                edmNeumannMoziContainer.UlesSet.Add(new Ules {
                                    Sor = rows + 1,
                                    Szam = columns + 1,
                                    TeremId = room.Id
                                });
                            }
                        }
                    }

                }
                edmNeumannMoziContainer.SaveChanges();
                this.Close();
                MessageBox.Show("Az adatbázis szinkronizáció sikeres volt.");
            } catch (Exception ex) {
                MessageBox.Show("Hiba lépett fel az adatbázis szinkronizációnál:\n " + ex);
            }
            

        }
    }
}
