using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Thesis
{
    //TODO
        //FIX MODAL OPEN/CLOSE ISSUE
    public partial class UserProfiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getUsersTable();
            }

            //Needs to populate dropdown according to user role
        }
        public void getUsersTable()
        {
            DataTable dt = new DataTable();
            dt = getUsers();
            usersGrid.DataSource = dt;
            usersGrid.DataBind();
            usersPanel.Update();
            //dt.Rows.Count
        }

        public void getUsersTable(String searchWord)
        {
            DataTable dt = new DataTable();
            dt = getUsers(searchWord);
            usersGrid.DataSource = dt;
            usersGrid.DataBind();
            usersPanel.Update();
            //dt.Rows.Count
        }
        public static DataTable getUsers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("user_id", typeof(int));
            dt.Columns.Add("faculty_id", typeof(string));
            dt.Columns.Add("first_name", typeof(string));
            dt.Columns.Add("last_name", typeof(string));
            dt.Columns.Add("email_address", typeof(string));
            dt.Columns.Add("user_type", typeof(string));

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.Users
                              where t.status == true
                              select new
                              {
                                  t.user_id,
                                  t.faculty_id,
                                  t.first_name,
                                  t.last_name,
                                  t.email_address,
                                  t.user_type
                              });
                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["user_id"] = entityRow.user_id;
                    dr["faculty_id"] = entityRow.faculty_id;
                    dr["first_name"] = entityRow.first_name;
                    dr["last_name"] = entityRow.last_name;
                    dr["email_address"] = entityRow.email_address;
                    switch (entityRow.user_type)
                    {
                        case 1:
                            dr["user_type"] = "Faculty";
                            break;
                        case 2:
                            dr["user_type"] = "Department Head";
                            break;
                        case 3:
                            dr["user_type"] = "Academics Assistant";
                            break;
                        case 4:
                            dr["user_type"] = "VP Academics";
                            break;
                    }
                    
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        protected void usersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "onDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);

                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    var selected = data.Users.FirstOrDefault(g => g.user_id.Equals(ID));

                    //nameToDeleteLabel.Text = selected.first_name + " " + selected.last_name;
                    //idToDeleteLabel.Text = selected.faculty_id;
                    
                }
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#deleteUserModal').modal('show');", true);
                
            }
            else if (e.CommandName == "onUpdate")
            {
                populateEditForm(e.CommandArgument.ToString());
                
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#updateUserModal').modal('show');", true);
            }
        }
        public void deleteUser(User entity)
        {

        }
        public void populateEditForm(string ID)
        {
            facultyId.Text = ID;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var selected = data.Users.FirstOrDefault(g => g.faculty_id.Equals(facultyId.Text));
                dropdownAccount.SelectedValue = selected.user_type.ToString();
                firstName.Text = selected.first_name;
                lastName.Text = selected.last_name;
                email.Text = selected.email_address;
            }
        }

        public static DataTable getUsers(String searchWord)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("user_id", typeof(int));
            dt.Columns.Add("faculty_id", typeof(string));
            dt.Columns.Add("first_name", typeof(string));
            dt.Columns.Add("last_name", typeof(string));
            dt.Columns.Add("email_address", typeof(string));
            dt.Columns.Add("user_type", typeof(int));

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.Users
                              where (searchWord.Contains(t.faculty_id.ToString()) || searchWord.Contains(t.first_name.ToString()) || searchWord.Contains(t.last_name.ToString())) && t.status == true
                              select new
                              {
                                  t.user_id,
                                  t.faculty_id,
                                  t.first_name,
                                  t.last_name,
                                  t.email_address,
                                  t.user_type
                              });
                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["user_id"] = entityRow.user_id;
                    dr["faculty_id"] = entityRow.faculty_id;
                    dr["first_name"] = entityRow.first_name;
                    dr["last_name"] = entityRow.last_name;
                    dr["email_address"] = entityRow.email_address;
                    switch (entityRow.user_type)
                    {
                        case 1:
                            dr["user_type"] = "Faculty";
                            break;
                        case 2:
                            dr["user_type"] = "Department Head";
                            break;
                        case 3:
                            dr["user_type"] = "Academics Assistant";
                            break;
                        case 4:
                            dr["user_type"] = "VP Academics";
                            break;
                    }

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        protected void searchUserButton_Click(object sender, EventArgs e)
        {
            //getUsersTable(searchTextBox.Text);
        }

        protected void usersGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkButtonDelete = e.Row.FindControl("deleteButton") as LinkButton;
                LinkButton linkButtonUpdate = e.Row.FindControl("updateButton") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(linkButtonDelete);
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(linkButtonUpdate);
            }
        }
        protected void resetRegButton_Click(object sender, EventArgs e)
        {
            emptyValidationLabels();
            populateEditForm(facultyId.Text);
        }

        public void emptyValidationLabels()
        {
            firstNameLabel.Text = String.Empty;
            firstNameLabel.Visible = false;
            lastNameLabel.Text = String.Empty;
            lastNameLabel.Visible = false;
            emailLabel.Text = String.Empty;
            emailLabel.Visible = false;
            invalidDropdownSelection.Text = String.Empty;
            invalidDropdownSelection.Visible = false;
        }

        public bool validateInput()
        {
            bool isValid = true;

            //Empty out labels
            emptyValidationLabels();

            if (firstName.Text == String.Empty)
            {
                firstNameLabel.Text = "First name field is required.";
                firstNameLabel.Visible = true;
                isValid = false;
            }
            if (lastName.Text == String.Empty)
            {
                lastNameLabel.Text = "Last name field is required.";
                lastNameLabel.Visible = true;
                isValid = false;
            }
            if (email.Text == String.Empty)
            {
                emailLabel.Text = "Email address field is required.";
                emailLabel.Visible = true;
                isValid = false;
            }
            Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match matchEmail = regexEmail.Match(email.Text);
            if (!matchEmail.Success)
            {
                emailLabel.Text = "Please enter a valid email address.";
                emailLabel.Visible = true;
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

        protected void editUserButton_Click(object sender, EventArgs e)
        {
            if (validateInput())
            {
                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    User person = (from x in data.Users
                                   where x.faculty_id == facultyId.Text
                                   select x).First();

                    person.first_name = firstName.Text;
                    person.last_name = lastName.Text;
                    person.email_address = email.Text;
                    person.user_type = Convert.ToInt32(dropdownAccount.SelectedValue);
                    person.date_modified = DateTime.Now;
                    person.modifiedBy = Session["FACULTY_ID"].ToString();
                    person.status = true;

                    data.SaveChanges();

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "editedMsg()", true);
                }
            }
        }

        protected void deleteYesBtn_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from u in data.Users
                                  where u.faculty_id.Equals(idToDeleteLabel.Text)
                                  select u).FirstOrDefault();

                entity.status = false;
                data.SaveChanges();
            }
            getUsersTable();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "deletedMsg()", true);
        }

        
    }
}