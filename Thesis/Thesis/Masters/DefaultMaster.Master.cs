using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Thesis;

namespace ISchedule.Masters
{
    public partial class DefaultMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FACULTY_ID"] != null)
            {
                if (!IsPostBack)
                {
                    string date = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                    datelbl.Text = date;
                    getData();
                    getControls();
                }
                else
                {
                    getData();
                    getControls();
                }
            }
            else
            {
                Response.Redirect("~/LoginPage.aspx");
            }
        }

        private void getControls()
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
                {   //query to get user type
                    //var usertype = entity.Users.Where(p => p.ID == 2).Select(x=>x.user_type).FirstOrDefault(); 
                var usertype = Int32.Parse(Session["user_type"].ToString()); //for testing

                //var usertype = 1;
                if (usertype == 1)
                {
                    Menu.Controls.Add(new LiteralControl("<li><a class='active-menu' href='/Dashboard.aspx'><i class='fa fa-dashboard fa-3x'></i> Dashboard</a></li>"));
                    //Menu.Controls.Add(new LiteralControl("<li><a  href='" + Request.ApplicationPath + "/Messages.aspx'><i class='fa fa-envelope fa-3x'></i>Messages</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a href='/UserProfile.aspx'><i class='fa fa-file-o fa-3x''></i>User Profile</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a href='/MakeupForm.aspx'><i class='fa fa-file-o fa-3x'></i>Make Up Form</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a href='#'><i class='fa fa-calendar-o fa-3x'></i>Schedule</a><ul class='nav nav-second-level'><li><a href='/GetSchedule.aspx'>Get Schedule</a></li></li>"));
                    // Menu.Controls.Add(new LiteralControl("<li><a href='#'><i class='fa fa-folder-o fa-3x'></i>Schedule</a><ul class='nav nav-second-level'><li><a href='/GetSchedule.aspx'>Get Schedule</a></li><li><a href='/ModifySchedule.aspx'>Modify Schedule</a></li><li><a href='/UploadSchedule.aspx'>Upload Schedule</a></li></ul></li>"));
                }

                else if (usertype == 2)
                {
                    Menu.Controls.Add(new LiteralControl("<li><a class='active-menu' href='/Dashboard.aspx'><i class='fa fa-dashboard fa-3x'></i> Dashboard</a></li>"));
                    //Menu.Controls.Add(new LiteralControl("<li><a  href='" + Request.ApplicationPath + "/Messages.aspx'><i class='fa fa-envelope fa-3x'></i>Messages</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/UserProfile.aspx'><i class='fa fa-file-o fa-3x''></i>User Profile</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/UserManagement.aspx'><i class='fa fa-file-o fa-3x''></i>Allowed Users</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/MakeupForm.aspx'><i class='fa fa-file-o fa-3x'></i>Make Up Form</a></li>"));
                    // INSERT USERMANAGEMENT
                    Menu.Controls.Add(new LiteralControl("<li><a href='#'><i class='fa fa-calendar-o fa-3x'></i>Schedule</a><ul class='nav nav-second-level'><li><a href='/GetSchedule.aspx'>Get Schedule</a></li></li>"));

                }
                else if (usertype == 3)
                {
                    Menu.Controls.Add(new LiteralControl("<li><a class='active-menu' href='/Dashboard.aspx'><i class='fa fa-dashboard fa-3x'></i> Dashboard</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/UserProfile.aspx'><i class='fa fa-file-o fa-3x''></i>User Profile</a></li>"));
                    //Menu.Controls.Add(new LiteralControl("<li><a  href='" + Request.ApplicationPath + "/Messages.aspx'><i class='fa fa-envelope fa-3x'></i>Messages</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/MakeupForm.aspx'><i class='fa fa-file-o fa-3x'></i>Make Up Form</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/CancelClassSched.aspx'><i class='fa fa-file-o fa-3x'></i>Cancel Class</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/Announcements.aspx'><i class='fa fa-file-o fa-3x'></i>Announcement</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/Holidays.aspx'><i class='fa fa-file-o fa-3x'></i>Holidays</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/Rooms.aspx'><i class='fa fa-file-o fa-3x'></i>Rooms</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a href='#'><i class='fa fa-calendar-o fa-3x'></i>Schedule</a><ul class='nav nav-second-level'><li><a href='/GetSchedule.aspx'>Get Schedule</a></li></li>"));
                }

                else if (usertype == 4)
                {
                    Menu.Controls.Add(new LiteralControl("<li><a class='active-menu' href='/Dashboard.aspx'><i class='fa fa-dashboard fa-3x'></i> Dashboard</a></li>"));
                    //Menu.Controls.Add(new LiteralControl("<li><a  href='" + Request.ApplicationPath + "/Messages.aspx'><i class='fa fa-envelope fa-3x'></i>Messages</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/UserProfile.aspx'><i class='fa fa-file-o fa-3x''></i>User Profile</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/AccountProfiles.aspx'><i class='fa fa-file-o fa-3x'></i>Registered Users</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/UserManagement.aspx'><i class='fa fa-file-o fa-3x''></i>Allowed Users</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/MakeupForm.aspx'><i class='fa fa-file-o fa-3x'></i>Make Up Form</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/ApprovalPage.aspx'><i class='fa fa-check-square-o fa-3x'></i>Approval</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/CancelClassSched.aspx'><i class='fa fa-file-o fa-3x'></i>Cancel Class</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/Announcements.aspx'><i class='fa fa-file-o fa-3x'></i>Announcement</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/Reports.aspx'><i class='fa fa-bar-chart-o fa-3x'></i>Reports</a></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a href='#'><i class='fa fa-calendar-o fa-3x'></i>Schedule</a><ul class='nav nav-second-level'><li><a href='/GetSchedule.aspx'>Get Schedule</a></li><li><a href='/ModifySchedule.aspx'>Modify Schedule</a></li><li><a href='/UploadSchedule.aspx'>Upload Schedule</a></li></ul></li>"));
                    Menu.Controls.Add(new LiteralControl("<li><a  href='/CurrentSettings.aspx'><i class='glyphicon glyphicon-wrench'></i>Current Settings</a></li>"));
                }
            }
        }

        protected void getData()
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                var ID = Session["FACULTY_ID"].ToString();
                var data = entity.Users.Where(p => p.faculty_id.Equals(ID)).FirstOrDefault();
                if (data.image == null)
                {
                    profilePic.ImageUrl = "~/Assets/img/find_user.png";
                }
                else
                {
                    profilePic.ImageUrl = "~/ShowImage.ashx?id=" + ID;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/LoginPage.aspx");
        }
    }
}