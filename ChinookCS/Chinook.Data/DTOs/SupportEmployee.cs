
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
    public class SupportEmployee
    {
        public string Name { get; set; }
        public int ClientCount { get; set; }
        public List<PlayListCustomer> ClientList { get; set; }
    }
}
