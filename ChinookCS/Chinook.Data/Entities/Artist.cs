using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion
namespace Chinook.Data.Entities
{
    [Table("Artists")]
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(120,ErrorMessage ="Name is limited to 120 characters")]
        public string Name { get; set; }

        //navigation properties
        public virtual ICollection<Album> Albums { get; set; }
    }
}
