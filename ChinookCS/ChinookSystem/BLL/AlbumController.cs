using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;  //ODS
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Album_Find(int albumid)
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(albumid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_GetByArtistId(int artistid)
        {
            using (var context = new ChinookContext())
            {
                var results = from aRowOn in context.Albums
                              where aRowOn.ArtistId.Equals(artistid)
                              select aRowOn;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert,false)]
        public int Album_Add(Album item)
        {
            using (var context = new ChinookContext())
            {
                item = context.Albums.Add(item);
                context.SaveChanges();
                return item.AlbumId;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int Album_Update(Album item)
        {
            using (var context = new ChinookContext())
            {
                item.ReleaseLabel = string.IsNullOrEmpty(item.ReleaseLabel) ? null :
                    item.ReleaseLabel;
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int Album_Delete(Album item)
        {
            return Album_Delete(item.AlbumId);
        }

        public int Album_Delete(int albumid)
        {
            using (var context = new ChinookContext())
            {
                var existing = context.Albums.Find(albumid);
                if (existing == null)
                {
                    throw new Exception("Album does not exist on file.");
                }
                context.Albums.Remove(existing);
                return context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AnAlbum> Album_GetAlbumAndSongs()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums
                              where x.Tracks.Count() > 24
                              select new AnAlbum
                              {
                                  artist = x.Artist.Name,
                                  title = x.Title,
                                  songs = (from y in x.Tracks
                                           select new Song
                                           {
                                               songname = y.Name,
                                               length = y.Milliseconds / 60000 + ":" +
                                                          (y.Milliseconds % 60000) / 1000
                                           }).ToList()
                              };
                return results.ToList();
            }
        }
    }
}

