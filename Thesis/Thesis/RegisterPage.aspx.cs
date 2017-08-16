using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            unauthorizedUserLabel.Visible = false;
        }

        public void emptyValidationLabels()
        {
            requiredFirstName.Text = String.Empty;
            requiredFirstName.Visible = false;
            requiredLastName.Text = String.Empty;
            requiredLastName.Visible = false;
            facultyIdLabel.Text = String.Empty;
            facultyIdLabel.Visible = false;
            emailLabel.Text = String.Empty;
            emailLabel.Visible = false;
            requiredPassword.Text = String.Empty;
            requiredPassword.Visible = false;
            confirmPasswordMatch.Text = String.Empty;
            confirmPasswordMatch.Visible = false;
            invalidDropdownSelection.Text = String.Empty;
            invalidDropdownSelection.Visible = false;
            //invalidLoginLabel.Text = String.Empty;
            //invalidLoginLabel.Visible = true;
        }
        public bool validateInput()
        {
            bool isValid = true;

            //Empty out labels
            emptyValidationLabels();

            if (firstName.Text == String.Empty)
            {
                requiredFirstName.Text = "First name field is required.";
                requiredFirstName.Visible = true;
                isValid = false;
            }
            if (lastName.Text == String.Empty)
            {
                requiredLastName.Text = "Last name field is required.";
                requiredLastName.Visible = true;
                isValid = false;
            }
            if (facultyId.Text == String.Empty)
            {
                facultyIdLabel.Text = "Faculty ID field is required.";
                facultyIdLabel.Visible = true;
                isValid = false;
            }
            //else
            //{
            //    Regex regexFacultyID = new Regex(@"^(\d{9})$");
            //    Match matchFacultyID = regexFacultyID.Match(facultyId.Text);
            //    if (!matchFacultyID.Success)
            //    {
            //        facultyIdLabel.Text = "Please enter a valid faculty ID.";
            //        facultyIdLabel.Visible = true;
            //        isValid = false;
            //    }
            //}
            if (email.Text == String.Empty)
            {
                emailLabel.Text = "Email address field is required.";
                emailLabel.Visible = true;
                isValid = false;
            }
            Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regexEmail.Match(email.Text);
            if (!match.Success)
            {
                emailLabel.Text = "Please enter a valid email address.";
                emailLabel.Visible = true;
                isValid = false;

            }
            if (regPass.Text == String.Empty)
            {
                requiredPassword.Text = "Password field is required.";
                requiredPassword.Visible = true;
                isValid = false;
            }
            Regex regexPassword = new Regex(@"^(\w{6,20})$");
            Match matchPassword = regexPassword.Match(regPass.Text);
            if (!matchPassword.Success)
            {
                requiredPassword.Text = "Please enter a password between 6-20 characters.";
                requiredPassword.Visible = true;
                isValid = false;
            }
            if (confirmPass.Text != regPass.Text)
            {
                confirmPasswordMatch.Text = "The password entered does not match.";
                confirmPasswordMatch.Visible = true;
                isValid = false;
            }
            if (dropdownAccount.SelectedValue.Equals("0"))
            {
                invalidDropdownSelection.Text = "Please select a user type.";
                invalidDropdownSelection.Visible = true;
                isValid = false;
            }

            return isValid;
        }


        protected void registerButton_Click(object sender, EventArgs e)
        {
            if (validateInput())
            {
                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    int utype = Convert.ToInt32(dropdownAccount.SelectedValue);
                    if (data.AllowedUsers.Any(u => u.faculty_id.Equals(facultyId.Text) && u.user_type == utype))
                    {
                        if (!data.Users.Any(u => u.faculty_id.Equals(facultyId.Text)))
                        {
                            User person = new User();

                            person.first_name = firstName.Text;
                            person.last_name = lastName.Text;
                            person.faculty_id = facultyId.Text.Trim();
                            person.email_address = email.Text;
                            string mySalt = BCrypt.GenerateSalt();
                            person.password = BCrypt.HashPassword(regPass.Text, mySalt);
                            person.user_type = Convert.ToInt32(dropdownAccount.SelectedValue);
                            person.date_created = DateTime.Now;
                            person.status = true;

                            data.Users.Add(person);
                            data.SaveChanges();

                            emptyCreateUserFields();
                        }
                        else
                        {
                            unauthorizedUserLabel.Text = "The user you are trying to add already has an account on this system.";
                            unauthorizedUserLabel.Visible = true;
                        }
                    }
                    else
                    {
                        unauthorizedUserLabel.Text = "You are not authorized to create an account in this system.";
                        unauthorizedUserLabel.Visible = true;
                    }
                }
            }

        }
        protected void resetRegButton_Click(object sender, EventArgs e)
        {
            emptyValidationLabels();
            emptyCreateUserFields();
        }

        public void emptyCreateUserFields()
        {
            facultyId.Text = String.Empty;
            firstName.Text = String.Empty;
            lastName.Text = String.Empty;
            dropdownAccount.SelectedIndex = 0;
            email.Text = String.Empty;
            regPass.Text = String.Empty;
            confirmPass.Text = String.Empty;
        }

        protected void goBackLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
    }
}