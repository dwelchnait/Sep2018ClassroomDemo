using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Data.Entity;
using Chinook.Data.Entities;
#endregion

namespace ChinookSystem.DAL
{
    internal class ChinookContext:DbContext
    {
        public ChinookContext():base("ChinookDB")
        {

        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
    }
}
