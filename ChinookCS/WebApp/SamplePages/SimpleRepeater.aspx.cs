using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.SamplePages
{
    public partial class SimpleRepeater : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            //used to capture and handle errors from the
            //ODS control
            MessageUserControl.HandleDataBoundException(e);
        }
    }
}