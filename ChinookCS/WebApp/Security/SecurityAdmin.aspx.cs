using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Security
{
    public partial class SecurityAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Administrators"))
                {
                    MessageUserControl.ShowInfo("Success", "You may continue");
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
        protected void CheckForExceptions(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void AddRole_Click(object sender, EventArgs e)
        {
            if (EmployeeListForRoles.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Manage User Role","Select an employee from the Add User Role employees.");
            }
            else if (RoleList.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Manage User Role", "Select a role from the Add User Role roles.");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    SecurityController sysmgr = new SecurityController();
                    sysmgr.AddUserRole(EmployeeListForRoles.SelectedValue, RoleList.SelectedValue);
                }, "Success", "User Role created");
            }
        }

        protected void DeleteRole_Click(object sender, EventArgs e)
        {
            if (EmployeeListForRoles.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Manage User Role", "Select an employee from the Add User Role employees.");
            }
            else if (RoleList.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Manage User Role", "Select a role from the Add User Role roles.");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    SecurityController sysmgr = new SecurityController();
                    sysmgr.RemoveUserRole(EmployeeListForRoles.SelectedValue, RoleList.SelectedValue);
                }, "Success", "User Role removed");
            }
        }
    }
}