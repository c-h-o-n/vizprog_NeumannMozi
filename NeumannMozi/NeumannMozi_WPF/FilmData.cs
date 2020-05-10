using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeumannMozi_WPF {
    class FilmData {
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
        public string ScreeningDates { get; set; }
    }
}
