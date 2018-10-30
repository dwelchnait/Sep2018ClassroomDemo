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
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {

                var results = from x in context.PlaylistTracks
                              where x.Playlist.UserName.Equals(username)
                                  && x.Playlist.Name.Equals(playlistname)
                              orderby x.TrackNumber
                              select new UserPlaylistTrack
                              {
                                  TrackID = x.TrackId,
                                  TrackNumber = x.TrackNumber,
                                  TrackName = x.Track.Name,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice
                              };

                return results.ToList();
            }
        }//eom

        //this method is an OLTP complex method
        //this method may alter multiple tracks
        //the method requires the design to properly design a
        //     solution BEFORE attempting to code.
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            //the using sets up the transaction environemnt
            //if the logic does not reach a .SaveChanges() method
            //   all work is rolled back.

            //a List<string> to be used to handle any number of errors
            //    generated while doing the transaction
            //all errors can then be returned to the MessageUserControl
            //    for display
            List<string> reasons = new List<string>();

            using (var context = new ChinookContext())
            {
                //code to go here
                //Part One
                //determine if a new playlist is needed
                //determine the tracknumber dependent of if a playlist
                //    already exists.
                Playlist exists = context.Playlists
                    .Where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                            && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase))
                    .Select(x => x).FirstOrDefault();
                // create an instance for PlaylistTrack
                PlaylistTrack newTrack = null;
                // initialize a local tracknumber
                int tracknumber = 0;
                if (exists == null)
                {
                    //this is a new playlist being created
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    exists = context.Playlists.Add(exists);
                    tracknumber = 1;
                }
                else
                {
                    //this is an exsiting playlist
                    //calculate the new proposed tracknumber
                    tracknumber = exists.PlaylistTracks.Count() + 1;
                    //business rule: track may only exists once on a playlist
                    //               it may exists on many different playlists
                    //.SingleOrDefault expects a single instance to be returned
                    newTrack = exists.PlaylistTracks.SingleOrDefault(
                        x => x.TrackId == trackid);
                    if (newTrack != null)
                    {
                        reasons.Add("Track already exists on the playlist.");
                    }

                }
                if (reasons.Count() > 0)
                {
                    //issue the BusinessRuleExpection(title, list of error strings)
                    throw new BusinessRuleException("Adding track to palylist",
                        reasons);
                }
                else
                {
                    //Part Two: Add the track

                    newTrack = new PlaylistTrack();
                    newTrack.TrackId = trackid;
                    newTrack.TrackNumber = tracknumber;
                    //whatabout the PlaylistId??
                    //NOte: the pkey for PlaylistId may not yet exists
                    //   using navigation one can let HashSet handle the expected
                    //   PlaylistId pkey value
                    exists.PlaylistTracks.Add(newTrack);
                    //at this point all records are in staged state
                    //physically add all data for the transaction to
                    //   the database and commit
                    context.SaveChanges();

                }
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 
                var exists = (from x in context.Playlists
                              where x.Name.Equals(playlistname)
                                 && x.UserName.Equals(username)
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Play list has been removed from the files.");
                }
                else
                {
                    PlaylistTrack moveTrack = (from x in exists.PlaylistTracks
                                               where x.TrackId == trackid
                                               select x).FirstOrDefault();
                    if (moveTrack == null)
                    {
                        throw new Exception("Play list track has been removed from the files.");
                    }
                    else
                    {
                        //create an instance pointer to be used to point
                        //to the other track involved in the move
                        PlaylistTrack otherTrack = null;

                        //direction
                        if (direction.Equals("up"))
                        {
                            //up
                            //recheck that the track is NOT the first track
                            //if so, throw an error; otherwise move the track
                            if (moveTrack.TrackNumber == 1)
                            {
                                throw new Exception("Play list track already at top.");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber - 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Switching track is missing");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }
                        }
                        else
                        {
                            //down
                            //recheck that the track is NOT the last track
                            //if so, throw an error; otherwise move the track
                            if (moveTrack.TrackNumber == exists.PlaylistTracks.Count)
                            {
                                throw new Exception("Play list track already at bottom.");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber + 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Switching track is missing");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }//eof up/down

                        //saving the changes to the data
                        //we are saving 2 different entities
                        //indicate the property to save for a
                        //  particular entity instance
                        context.Entry(moveTrack).Property(y => y.TrackNumber).IsModified = true;
                        context.Entry(otherTrack).Property(y => y.TrackNumber).IsModified = true;

                        //commit your changes
                        context.SaveChanges();
                    }
                }
            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
                //code to go here
                var exists = (from x in context.Playlists
                              where x.Name.Equals(playlistname)
                                 && x.UserName.Equals(username)
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Play list has been removed from the files.");
                }
                else
                {
                    //get a list of tracks that will be kept IN ORDER of TRACKNUMBER
                    //you do NOT know if the physical order is the same as the 
                    //   logical TrackNumber order
                    //.Any() allows you to search for an item in a list using a condition, returns true if found
                    //looking an item in ListA is inside ListB
                    //in this example we DO NOT want to find it thus !
                    var trackskept = exists.PlaylistTracks.
                        Where(tr => !trackstodelete.Any(tod => tod == tr.TrackId)).
                        OrderBy(tr => tr.TrackNumber).
                        Select(tr => tr);

                    //delete tracks
                    PlaylistTrack item = null;
                    foreach (var deletetrackid in trackstodelete)
                    {
                        item = exists.PlaylistTracks.
                            Where(tr => tr.TrackId == deletetrackid).
                            FirstOrDefault();
                        if (item != null)
                        {
                            exists.PlaylistTracks.Remove(item);
                        }

                    }

                    //renumber remaining tracks (tracks that were kept)
                    int number = 1;
                    foreach (var tkept in trackskept)
                    {
                        tkept.TrackNumber = number;
                        number++;
                        context.Entry(tkept).Property(y => y.TrackNumber).IsModified = true;
                    }

                    //commit work
                    context.SaveChanges();

                }
            }
        }//eom
    }
}
