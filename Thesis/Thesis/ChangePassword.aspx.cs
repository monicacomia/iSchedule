using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;

namespace Thesis
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private String facultyIDGlobal;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void goBackLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
        protected void passwordButton_Click(object sender, EventArgs e)
        {
            facultyIDGlobal = Session["FP_FID"].ToString();
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var user = (from a in data.Users
                            where a.faculty_id == facultyIDGlobal
                            select a).FirstOrDefault();

                if (user != null)
                {
                    if (validatePassword())
                    {
                        user.password = BCrypt.HashPassword(passwordTextbox.Text, BCrypt.GenerateSalt());
                        user.date_modified = DateTime.Now;
                        data.SaveChanges();
                    }
                }
            }
        }

        public bool validatePassword()
        {
            bool isValid = true;

            if (passwordTextbox.Text == String.Empty)
            {
                passwordLabel.Text = "Password field is required.";
                passwordLabel.Visible = true;
                isValid = false;
            }
            Regex regexPassword = new Regex(@"^(\w{6,20})$");
            Match matchPassword = regexPassword.Match(passwordTextbox.Text);
            if (!matchPassword.Success)
            {
                passwordLabel.Text = "Please enter a password between 6-20 characters.";
                passwordLabel.Visible = true;
                isValid = false;
            }
            if (confirmPasswordTextbox.Text != passwordTextbox.Text)
            {
                confirmPasswordLabel.Text = "The password entered does not match.";
                confirmPasswordLabel.Visible = true;
                isValid = false;
            }
            return isValid;
        }
    }
}