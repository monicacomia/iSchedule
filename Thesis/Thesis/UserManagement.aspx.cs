using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class UserManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getUsersTable();
            }
        }


        public void getUsersTable(){

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
        


        
        public static DataTable getUsers(){

            String[] positionList = { "Faculty", "Department Head", "Academics Assistant", "VP Academics" };

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("schoolID", typeof(string));
            dt.Columns.Add("firstName", typeof(string));
            dt.Columns.Add("lastName", typeof(string));
            dt.Columns.Add("position", typeof(string));

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity=(from t in data.AllowedUsers
                           select new
                           {
                               t.id,
                               t.faculty_id,
                               t.first_name,
                               t.last_name,
                               t.user_type
                           });
                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["ID"] = entityRow.id;
                    dr["schoolID"] = entityRow.faculty_id;
                    dr["firstName"] = entityRow.first_name;
                    dr["lastName"] = entityRow.last_name;   
                    dr["position"]=positionList[entityRow.user_type-1];


                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }



        public static DataTable getUsers(String searchWord)
        {
            String[] positionList = { "Faculty", "Department Head", "Academics Assistant", "VP Academics" };
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("schoolID", typeof(string));
            dt.Columns.Add("firstName", typeof(string));
            dt.Columns.Add("lastName", typeof(string));
            dt.Columns.Add("position", typeof(string));

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.AllowedUsers
                              where searchWord.Contains(t.faculty_id) || searchWord.Contains(t.first_name) || searchWord.Contains(t.last_name)
                              select new
                              {
                                  t.id,
                                  t.faculty_id,
                                  t.first_name,
                                  t.last_name,
                                  t.user_type
                              });
                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["ID"] = entityRow.id;
                    dr["schoolID"] = entityRow.faculty_id;
                    dr["firstName"] = entityRow.first_name;
                    dr["lastName"] = entityRow.last_name;
                    dr["position"] = positionList[entityRow.user_type - 1];
                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }






        protected void usersGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            usersGrid.PageIndex = e.NewPageIndex;
            getUsersTable();
            
        }

        protected void usersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "onDelete")
            {
                string ID = e.CommandArgument.ToString();
                
                using(ThesisDBEntities data=new ThesisDBEntities()){
                    var entity=data.AllowedUsers.FirstOrDefault(g=> g.faculty_id.Equals(ID));
                    data.AllowedUsers.Remove(entity);
                    data.SaveChanges();
                }
                getUsersTable();

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showInvalidMsg()", true);

            }
            else if(e.CommandName=="onUpdate")
            {
                string ID = e.CommandArgument.ToString();
                //int index = int.Parse(e.CommandArgument.ToString());
                //string ID = (string)this.usersGrid.DataKeys[index]["schoolID"];
                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    var selected = data.AllowedUsers.FirstOrDefault(g =>g.faculty_id.Equals(ID));
                    facultyIDLbl.Text = selected.id.ToString();
                    editSchoolIDTextBox.Text = selected.faculty_id;
                    editFirstNameTextBox.Text = selected.first_name;
                    editLastnameTextBox.Text = selected.last_name;
                    ddlPositionEdit.SelectedIndex=selected.user_type-1;

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#updateUserModal').modal('show');", true);
                }
            }

            



        }

        protected void addUserModalButton_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "addUserModal", "$('#addUserModal').modal();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //clearTextBox();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#addUserModal').modal('show');", true);
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
           using(ThesisDBEntities data = new ThesisDBEntities())
           {
            
               if(schoolIDTextBox.Text.Trim().Length==0)
               {

                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "enterFacultyID()", true);

               }
               else if(data.AllowedUsers.Any(x=>x.faculty_id.Equals(schoolIDTextBox.Text)))
               {
                   ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "exist()", true);
               }
               else{
                        

                            AllowedUser person = new AllowedUser();
                            person.faculty_id = schoolIDTextBox.Text;
                            person.first_name = firstNameTextBox.Text;
                            person.last_name = lastNameTextBox.Text;
                            person.user_type=Int32.Parse(ddlPosition.SelectedValue);

                            data.AllowedUsers.Add(person);
                            data.SaveChanges();

                        

                getUsersTable();
                resetAddModal();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showSuccessMsg()", true);
            }
            
            
           }
           
           
        }

        protected void usersGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = e.Row.FindControl("deleteButton") as LinkButton;
                LinkButton lb1 = e.Row.FindControl("updateButton") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb1);
            }
            
        }


        protected void searchUserButton_Click(object sender, EventArgs e)
        {
            String searchWord = searchTextBox.Text;
            if(searchWord.Equals(String.Empty)){
                getUsersTable();
            }
            else{
                getUsersTable(searchWord);
            }
            
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                //var schoolID=editSchoolIDTextBox.Text;
                //if (data.AllowedUsers.Any(x => x.faculty_id.Equals(editSchoolIDTextBox.Text)))
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "exist()", true);
                //}
                //else{
                var id = Int32.Parse(facultyIDLbl.Text);
                var entity = data.AllowedUsers.FirstOrDefault(g => g.id==id);

                entity.faculty_id = editSchoolIDTextBox.Text;
                entity.first_name = editFirstNameTextBox.Text;
                entity.last_name= editLastnameTextBox.Text;

                data.SaveChanges();
                getUsersTable();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showEditedMsg()", true);
                //}
            }
           
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            using (ExcelPackage package = new ExcelPackage())
            {

                ExcelWorksheet ws = package.Workbook.Worksheets.Add("User");

                //column headers
                ws.Cells["A1"].Value = "Faculty ID";
                ws.Cells["B1"].Value = "First Name";
                ws.Cells["C1"].Value = "Last Name";
                ws.Cells["D1"].Value = "Position";
                ws.Cells["A1:D1"].Style.Font.Bold = true;

                //set column width
                ws.Column(1).Width = 23;
                ws.Column(2).Width = 23;
                ws.Column(3).Width = 23;
                ws.Column(4).Width = 23;

                var position = ws.DataValidations.AddListValidation("D2:D1000");

                position.ShowErrorMessage=true;
                position.ErrorTitle = "An invalid feedback was entered";
                position.Error = "Please choose feedback from dropdown only.";

                position.Formula.Values.Add("Faculty");
                position.Formula.Values.Add("Department Head");
                position.Formula.Values.Add("Academics Assistant");
                position.Formula.Values.Add("VP Academics");
               

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=User_Template.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }
        }
        

        protected void btnUpload_Click(object sender, EventArgs e)
        {


            
            if (Uploader.HasFile && Path.GetExtension(Uploader.FileName) == ".xlsx")
            {
                using (var excel = new ExcelPackage(Uploader.PostedFile.InputStream))
                {

                    var ws = excel.Workbook.Worksheets.First();
                    int startRow = 2;
                    var lastRow = ws.Dimension.End.Row;
                    var lastCol = ws.Dimension.End.Column;

                    try
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            using (ThesisDBEntities entity = new ThesisDBEntities())
                            {
                                for (int i = startRow; i <= lastRow; i++)
                                {

                                    AllowedUser data = new AllowedUser();
                                    
                                    var id=ws.Cells[i, 1].Text;
                                    if(id.Equals("")){
                                        throw  new NullReferenceException();
                                    }
                                    else{
                                        data.faculty_id = ws.Cells[i, 1].Text;
                                    }
                                    
                                    //data.faculty_id = ws.Cells[i, 1].Text.Equals("") ? throw new NullReferenceException() : ws.Cells[i, 1].Text;
                                   
                                    data.first_name = ws.Cells[i, 2].Text.Equals("") ? null : ws.Cells[i, 2].Text;
                                    data.last_name = ws.Cells[i, 3].Text.Equals("") ? null : ws.Cells[i, 3].Text;
                                    if (ws.Cells[i, 4].Text.Equals("Faculty"))
                                    {
                                        data.user_type = 1;
                                    }
                                    else if (ws.Cells[i, 4].Text.Equals("Department Head"))
                                    {
                                        data.user_type = 2;
                                    }
                                    else if (ws.Cells[i, 4].Text.Equals("Academics Assistant"))
                                    {
                                        data.user_type = 3;
                                    }
                                    else if (ws.Cells[i, 4].Text.Equals("VP Academics"))
                                    {
                                        data.user_type = 4;
                                    }
                                    else 
                                    {
                                        throw new NullReferenceException();
                                    }

                                    string duplicateId = ws.Cells[i, 1].Text;
                                    if (!entity.AllowedUsers.Any(u => u.faculty_id.Equals(duplicateId)))
                                    {
                                        entity.AllowedUsers.Add(data);
                                    }
                                }

                                entity.SaveChanges();
                            }

                            transactionScope.Complete();


                        }
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "uploadSuccess()", true);
                        getUsersTable();
                        //Response.Redirect("UserManagement.aspx");


                    }
                    catch (NullReferenceException nre)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "empty()", true);
                    }
                    catch (TransactionAbortedException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "invalidFile()", true);
            }




        }

        protected void resetAddModal()
        {
            schoolIDTextBox.Text = String.Empty;
            firstNameTextBox.Text = String.Empty;
            lastNameTextBox.Text = String.Empty;
            ddlPosition.SelectedIndex = 0;
            mdlAddUserPanel.Update();
        }

       

       
    }
}