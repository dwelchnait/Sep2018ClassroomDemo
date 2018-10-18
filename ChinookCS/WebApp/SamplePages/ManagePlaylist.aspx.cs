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

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {

            //code to go here

        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //code to go here
           
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void TracksSelectionList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //code to go here
           
        }

    }
}