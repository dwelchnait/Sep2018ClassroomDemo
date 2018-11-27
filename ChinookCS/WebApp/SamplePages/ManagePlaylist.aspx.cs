using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using Chinook.Data.POCOs;
using WebApp.Security;
#endregion

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Administrators") ||
                    User.IsInRole("Customers"))
                {
                    SecurityController sysmgr = new SecurityController();
                    int? customerid = sysmgr.GetCurrentUserCustomerId(User.Identity.Name);
                  
                }
                else
                {
                    //redirect ot a page that states no authorization for the requested action
                    Response.Redirect("~/Default.aspx");

                }
            }
            else
            {
                //redieect to login page
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                //code to go here
                int id = int.Parse(ArtistDDL.SelectedValue);
                SearchArgID.Text = id.ToString();
                TracksBy.Text = "Artist";
                TracksSelectionList.DataBind(); //causes the ODS to execute
            },"Track Search","Please select from the following list to add to your playlist.");
            


        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                //code to go here
                int id = int.Parse(MediaTypeDDL.SelectedValue);
                SearchArgID.Text = id.ToString();
                TracksBy.Text = "MediaType";
                TracksSelectionList.DataBind(); //causes the ODS to execute
            }, "Track Search", "Please select from the following list to add to your playlist.");
        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                //code to go here
                int id = int.Parse(GenreDDL.SelectedValue);
                SearchArgID.Text = id.ToString();
                TracksBy.Text = "Genre";
                TracksSelectionList.DataBind(); //causes the ODS to execute
            }, "Track Search", "Please select from the following list to add to your playlist.");
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                //code to go here
                int id = int.Parse(AlbumDDL.SelectedValue);
                SearchArgID.Text = id.ToString();
                TracksBy.Text = "Album";
                TracksSelectionList.DataBind(); //causes the ODS to execute
            }, "Track Search", "Please select from the following list to add to your playlist.");
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                /* use the MessageUserControl method .ShowInfo("title","message")*/
                MessageUserControl.ShowInfo("Required data",
                    "Play list name is required to add a track.");
            }
            else
            {
                //collect the needed data
                string playlistname = PlaylistName.Text;
                //string username = User.Identity.Name; //comes from security
                string username = User.Identity.Name; // "HansenB";
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> results = sysmgr.List_TracksForPlaylist(
                        playlistname, username);
                    PlayList.DataSource = results;
                    PlayList.DataBind();
                }, "Playlist Tracks", "See current tracks on playlist below.");
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

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                /* use the MessageUserControl method .ShowInfo("title","message")*/
                MessageUserControl.ShowInfo("Required data", 
                    "Play list name is required to add a track.");
            }
            else
            {
                //collect the needed data
                string playlistname = PlaylistName.Text;
                //string username = User.Identity.Name; //comes from security
                string username = "HansenB";
                //obtain the TrackId from the ListView
                //CommandArgument is an object
                int trackid = int.Parse(e.CommandArgument.ToString());
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(playlistname, username, trackid);
                    List<UserPlaylistTrack> results = sysmgr.List_TracksForPlaylist(
                        playlistname, username);
                    PlayList.DataSource = results;
                    PlayList.DataBind();
                },"Adding a Track","Track has been added to the playlist.");
            }
           
        }

    }
}