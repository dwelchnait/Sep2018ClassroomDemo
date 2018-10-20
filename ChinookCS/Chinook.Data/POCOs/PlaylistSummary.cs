using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Data.POCOs
{
    public class PlaylistSummary
    {
        public string name { get; set; }
        public int trackcount { get; set; }
        public decimal cost { get; set; }
        public double? storage { get; set; }
    }
}
