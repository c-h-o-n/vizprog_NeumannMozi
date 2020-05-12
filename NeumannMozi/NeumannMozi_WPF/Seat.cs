using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeumannMozi_WPF {
    class Seat {
        public int SeatId { get; set; }
        public string BtnName { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Available { get; set; }
        public bool Res { get; set; }

    }
}
