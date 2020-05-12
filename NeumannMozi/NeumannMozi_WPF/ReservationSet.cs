using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeumannMozi_WPF {
    class ReservationSet {

        public int userId { get; set; }
        public DateTime resDate { get; set; }
        public bool foglalt { get; set; }
        public bool fizetve { get; set; }
        public bool aktiv { get; set; }

    }
}
