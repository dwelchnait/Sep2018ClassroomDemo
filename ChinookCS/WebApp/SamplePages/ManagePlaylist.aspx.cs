using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using Chinook.Data.POCOs;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            //code to go here
            MessageUserControl.TryRun(() =>
            {
                TracksBy.Text = "Artist";
                SearchArgID.Text = ArtistDDL.SelectedValue;
                TracksSelectionList.DataBind();
            }, "Tracks by Artist", "Add an track to your playlist by clicking on the + (plus sign).");
        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {
            //code to go here
            MessageUserControl.TryRun(() =>
            {
                TracksBy.Text = "MediaType";
                SearchArgID.Text = MediaTypeDDL.SelectedValue;
                TracksSelectionList.DataBind();
            }, "Tracks by Media Type", "Add an track to your playlist by clicking on the + (plus sign).");
        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            //code to go here
            MessageUserControl.TryRun(() =>
            {
                TracksBy.Text = "Genre";
                SearchArgID.Text = GenreDDL.SelectedValue;
                TracksSelectionList.DataBind();
            }, "Tracks by Genre", "Add an track to your playlist by clicking on the + (plus sign).");
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            //code to go here
            MessageUserControl.TryRun(() =>
            {
                TracksBy.Text = "Album";
                SearchArgID.Text = AlbumDDL.SelectedValue;
                TracksSelectionList.DataBind();
            }, "Tracks by Album", "Add an track to your playlist by clicking on the + (plus sign).");
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data",
                    "Playlist Name is required to retreive tracks.");
            }
            else
            {
                string username = "HansenB";
                string playlistname = PlaylistName.Text;
                MessageUserControl.TryRun(() => {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> results =
                    sysmgr.List_TracksForPlaylist(playlistname, username);
                    if (results.Count() == 0)
                    {
                        MessageUserControl.ShowInfo("Check playlist name");
                    }
                    PlayList.DataSource = results;
                    PlayList.DataBind();
                });
            }

        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Warning", "You must have a playlist showing. Fetch your playlist.");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Warning", "You must have a playlist name. Enter your playlist name.");
                    //optionally you might wish to empty the Playlist GridView
                }
                else
                {
                    //check that a single track has been checked
                    //if so, extract the needed data from the select GridViewRow
                    //the TrackId is a hidden column on the GridViewRow
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //access the control on the selected GridViewRow
                        //pointer to the CheckBox
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //is the CheckBox on
                        if (playlistselection.Checked)
                        {
                            rowselected++; //counter for number of checked items
                            //save necessary data for moving a track
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    } //eofor

                    //how many tracks were checked
                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select one track to move.");
                    }
                    else
                    {
                        //is the selected track the 1st track
                        if (tracknumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Warning", "Track cannot be moved down.");
                        }
                        else
                        {
                            //move the track.
                            MoveTrack(trackid, tracknumber, "down");
                        }
                    }
                }
            }
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Warning", "You must have a playlist showing. Fetch your playlist.");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Warning", "You must have a playlist name. Enter your playlist name.");
                    //optionally you might wish to empty the Playlist GridView
                }
                else
                {
                    //check that a single track has been checked
                    //if so, extract the needed data from the select GridViewRow
                    //the TrackId is a hidden column on the GridViewRow
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //access the control on the selected GridViewRow
                        //pointer to the CheckBox
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //is the CheckBox on
                        if (playlistselection.Checked)
                        {
                            rowselected++; //counter for number of checked items
                            //save necessary data for moving a track
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    } //eofor

                    //how many tracks were checked
                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select one track to move.");
                    }
                    else
                    {
                        //is the selected track the 1st track
                        if (tracknumber == 1)
                        {
                            MessageUserControl.ShowInfo("Warning", "Track cannot be moved up.");
                        }
                        else
                        {
                            //move the track.
                            MoveTrack(trackid, tracknumber, "up");
                        }
                    }
                }
            }
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
            MessageUserControl.TryRun(() => {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack("HansenB", PlaylistName.Text, trackid,
                    tracknumber, direction);
                List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, "HansenB");
                PlayList.DataSource = info;
                PlayList.DataBind();
            }, "Moved", "Track has been moved " + direction);
        }

        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Enter a play list name");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Playlist has no tracks to delete.");
                }
                else
                {
                    //gather all select rows
                    List<int> trackstodelete = new List<int>();
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        if (playlistselection.Checked)
                        {
                            rowselected++;
                            trackstodelete.Add(int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text));
                        }
                    }
                    //was at least one track selected
                    if (rowselected == 0)
                    {
                        MessageUserControl.ShowInfo("Warning", "You must select at least one track to delete");
                    }
                    else
                    {
                        //send the list of tracks to the BLL to delete
                        MessageUserControl.TryRun(() =>
                        {
                            PlaylistTracksController sysmgr = new PlaylistTracksController();
                            sysmgr.DeleteTracks("HansenB", PlaylistName.Text, trackstodelete);
                            List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, "HansenB");
                            PlayList.DataSource = info;
                            PlayList.DataBind();
                        }, "Removed", "Tracks have been removed");
                    }
                }
            }
        }

        protected void TracksSelectionList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //this method will only execute if the user has press
            //    the plus sign on a visible row from the display

            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("PlayList Name", "You must supply a playlist name.");
            }
            else
            {
                //via security one can obtain the username
                string username = "HansenB";
                string playlistname = PlaylistName.Text;

                //the trackid is attached to each ListView row
                //   via the CommandArgument parameter
                //Access to the trackid is done via the
                //   ListViewCommandEventArgs e parameter
                //The e parameter is treated as an object
                //Some e parameter need to be cast as strings
                int trackid = int.Parse(e.CommandArgument.ToString());

                //all requred data can now be sent to the BLL
                //    for further processing
                //user friendly error handling
                MessageUserControl.TryRun(() =>
                {
                    //connect to your BLL
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(playlistname, username, trackid);
                    //code to retreive the up to date playlist and tracks
                    //    for refreshing the playlist track list
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                }, "Track Added", "The track has been addded, check your list below");
            }
        }

    }
}