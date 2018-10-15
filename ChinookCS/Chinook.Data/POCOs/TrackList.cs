using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Data.POCOs
{
    public class TrackList
    {
        public int TrackID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string MediaName { get; set; }
        public string GenreName { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
