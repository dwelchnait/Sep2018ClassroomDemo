using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;  //ODS
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Track_Find(int trackid)
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.Find(trackid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_GetByAlbumId(int albumid)
        {
            using (var context = new ChinookContext())
            {
                var results = from aRowOn in context.Tracks
                              where aRowOn.AlbumId.HasValue
                              && aRowOn.AlbumId == albumid
                              select aRowOn;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, string arg)
        {
            if (string.IsNullOrEmpty(tracksby))
            {
                tracksby = "";
            }
            if (string.IsNullOrEmpty(arg))
            {
                arg = "";
            }
            int argid = 0;
            string argstring = "zyxzz";
            if (tracksby.Equals("Genre") || tracksby.Equals("MediaType"))
            {
                argid = int.Parse(arg);
            }
            else
            {
                argstring = arg.Trim();
            }
            using (var context = new ChinookContext())
            {


                var results = (from x in context.Tracks
                                   //where (tracksby.Equals("Genre") && x.GenreId == argid)
                                   // || (tracksby.Equals("MediaType") && x.MediaTypeId == argid)

                               where tracksby.Equals("Genre") ? x.GenreId == argid :
                                     tracksby.Equals("MediaType") ? x.MediaTypeId == argid : false
                               //    
                               select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              }).Union(from x in context.Tracks
                                           //where (tracksby.Equals("Artist") && x.Album.Artist.Name.Contains(argstring))
                                           //   || (tracksby.Equals("Album") && x.Album.Title.Contains(argstring))
                                       where tracksby.Equals("Artist") ? x.Album.Artist.Name.Contains(argstring):
                                             tracksby.Equals("Album") ? x.Album.Title.Contains(argstring) : false
                                       select new TrackList
                                       {
                                           TrackID = x.TrackId,
                                           Name = x.Name,
                                           Title = x.Album.Title,
                                           ArtistName = x.Album.Artist.Name,
                                           MediaName = x.MediaType.Name,
                                           GenreName = x.Genre.Name,
                                           Composer = x.Composer,
                                           Milliseconds = x.Milliseconds,
                                           Bytes = x.Bytes,
                                           UnitPrice = x.UnitPrice
                                       });

                return results.ToList();
            }
        }//eom
    }
}

