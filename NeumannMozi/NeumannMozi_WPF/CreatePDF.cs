using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using NeumannMozi_DAL;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
namespace NeumannMozi_WPF {
    // ide van hanyva
    public class CreatePDF {
        private edmNeumannMoziContainer edmNeumannMoziContainer;
        public CreatePDF() {
            edmNeumannMoziContainer = new edmNeumannMoziContainer();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.pdf)|*.pdf";
            saveFileDialog.ShowDialog();

            using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create)) {
                Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 42, 35);
                
                PdfWriter.GetInstance(pdfDoc, stream);

                pdfDoc.Open();
                PdfPTable table = new PdfPTable(3);

                PdfPCell cell = new PdfPCell(new Phrase("Filmek"));
                cell.BorderColor = BaseColor.WHITE;

                cell.Colspan = 3;

                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                table.AddCell(cell);


                List<FilmData> filmDatas = GetCurrentShowTimes();
                foreach (var film in filmDatas) {
                    byte[] byt = (byte[])film.PosterImage;
                    Image img = Image.GetInstance(byt);
                    img.ScaleAbsolute(115, 165);
                    table.AddCell(img);
                    //pdfDoc.Add(img);
                    Paragraph title = new Paragraph(film.Title);
                    table.AddCell(title);

                    //pdfDoc.Add(new Paragraph(film.Title));
                    string dates = "";
                    foreach (var date in film.ScreeningDates) {
                        dates += date + "\n";
                        //pdfDoc.Add(new Paragraph(date));
                    }
                    table.AddCell(new Paragraph(dates));
                }
                pdfDoc.Add(table);
                pdfDoc.Close();
                stream.Close();
            }
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
            int dateCounter = 0;
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
        private List<FilmData> GetCurrentShowTimes() {
            var result = new List<FilmData>();
            foreach (var x in edmNeumannMoziContainer.FilmSet) {
                if (GetScreeningDates(x.Id).Count > 0) {
                    result.Add(new FilmData() {
                        Id = x.Id,
                        PosterImage = x.Poszter,
                        Title = x.Cim,
                        RoomNameForDates = GetRoomName(x.Id),
                        ScreeningDates = GetScreeningDates(x.Id),
                    });
                }
            }
            return result;
        }
    }
}

