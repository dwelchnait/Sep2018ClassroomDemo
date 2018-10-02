using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.POCOs;
#endregion
namespace Chinook.Data.DTOs
{
    //AnAlbum is the DTO. It has structure (a set of data on each instance
    //   of the class.
    public class AnAlbum
    {
        public string artist { get; set; }
        public string title { get; set; }
        public List<Song> songs { get; set; }
    }
}
