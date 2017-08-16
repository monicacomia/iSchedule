using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class SignInPage : System.Web.UI.Page
    {
        public void emptyValidationLabels()
        {
            //requiredFirstName.Text = String.Empty;
            //requiredFirstName.Visible = false;
            //requiredLastName.Text = String.Empty;
            //requiredLastName.Visible = false;
            //facultyIdLabel.Text = String.Empty;
            //facultyIdLabel.Visible = false;
            //emailLabel.Text = String.Empty;
            //emailLabel.Visible = false;
            //requiredPassword.Text = String.Empty;
            //requiredPassword.Visible = false;
            //confirmPasswordMatch.Text = String.Empty;
            //confirmPasswordMatch.Visible = false;
            //invalidDropdownSelection.Text = String.Empty;
            //invalidDropdownSelection.Visible = false;
            invalidLoginLabel.Text = String.Empty;
            invalidLoginLabel.Visible = true;
        }

        protected void logInButton_Click(object sender, EventArgs e)
        {
            //validate id and password entered
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var user = (from a in data.Users
                            where a.faculty_id == facultyIDTxtbox.Text
                            select new
                            {
                                a.faculty_id,
                                a.user_type,
                                a.password,
                                a.status
                            }).FirstOrDefault();

                if (user != null && user.status == true && BCrypt.CheckPassword(password.Text, user.password))
                {
                    //Store session variables
                    Session["FACULTY_ID"] = user.faculty_id;
                    Session["USER_TYPE"] = user.user_type;
                    //Session.Timeout = 1;

                    //then redirect to system
                    Response.Redirect("~/Dashboard.aspx");

                }
                else
                {
                    invalidLoginLabel.Text = "We could not find your Faculty ID registered in the database.";
                    invalidLoginLabel.Visible = true;
                }
            }

        }

        protected void forgotPasswordButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ForgotPassword.aspx");
        }
    }
}