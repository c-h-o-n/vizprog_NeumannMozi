using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NeumannMozi_WPF {
    public class FilmData {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public int AgeRating { get; set; }
        public string TrailerLink { get; set; }
        public string ImdbLink { get; set; }
        public byte[] PosterImage { get; set; }
        public string Category { get; set; }
        public List<string> ScreeningDates { get; set; }
        public List<string> RoomNameForDates { get; set; }
        public List<string> ComboBoxSource { get; set; }
    }
}
