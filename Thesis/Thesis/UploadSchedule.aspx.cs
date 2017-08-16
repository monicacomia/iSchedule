using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class UploadSchedule1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

                    
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                var data = entity.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                
                lblSchoolYear.Text = data.schoolYearStart.ToString() + "-" + data.schoolYearEnd.ToString();
                if (data.semester.Equals("1"))
                {
                    lblSemester.Text = "1st";
                }
                else if (data.semester.Equals("2"))
                {
                    lblSemester.Text = "2nd";
                }
                else if (data.semester.Equals("3"))
                {
                    lblSemester.Text = "3rd";
                }


                lblDuration.Text = data.durationStart.ToString("yyyy-MM-dd") + " to " + data.durationEnd.ToString("yyyy-MM-dd");




                getSchedTable();
            }
        }



        public void getSchedTable()
        {

            DataTable dt = new DataTable();
            dt = getSched();

            if (dt == null)
            {
                btnUpload.Visible = true;
                Uploader.Visible = true;
            }
            else if(dt.Rows.Count==0){
                btnUpload.Visible = true;
                Uploader.Visible = true;
            }
            else
            {
                Uploader.Visible = false;
                btnUpload.Visible = false;
                templateBtn.Visible = false;
                schedGrid.DataSource = dt;
                schedGrid.DataBind();
                schedPanel.Update();
            }

            
            //dt.Rows.Count
        }


        public static DataTable getSched()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("schedID", typeof(int));
            dt.Columns.Add("roomNumber", typeof(string));
            dt.Columns.Add("days", typeof(string));
            dt.Columns.Add("time", typeof(string));
            //dt.Columns.Add("endTime", typeof(string));
            dt.Columns.Add("subjectCode", typeof(string));
            dt.Columns.Add("section", typeof(string));
            dt.Columns.Add("instructor", typeof(string));
            //dt.Columns.Add("numOfHours", typeof(string));
           

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.SemesterSchedules
                              orderby t.roomNumber, t.days ascending, t.time ascending
                              select new
                              {
                                  t.schedID,
                                  t.roomNumber,
                                  t.days,
                                  t.time,
                                  t.subjectCode,
                                  t.section,
                                  t.instructor,
                                  t.numofHours
                              });
                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["schedID"] = entityRow.schedID;
                    dr["roomNumber"] = entityRow.roomNumber;
                    string words = "";
                    if (entityRow.days.Contains("1"))
                    {
                        words += "Mon ";
                    }
                    if (entityRow.days.Contains("2"))
                    {
                        words += "Tue ";
                    }
                    if (entityRow.days.Contains("3"))
                    {
                        words += "Wed ";
                    }
                    if (entityRow.days.Contains("4"))
                    {
                        words += "Thu ";
                    }
                    if (entityRow.days.Contains("5"))
                    {
                        words += "Fri ";
                    }
                    if (entityRow.days.Contains("6"))
                    {
                        words += "Sat ";
                    }


                    dr["days"] = words;

                    //use .Value for timespan
                   
                    //DateTime time2 = DateTime.Today.Add(entityRow.endTime.Value);
                    //DateTime time = DateTime.Today.Add(entityRow.startTime.Value);



                    dr["time"] = entityRow.time;
                    //dr["endTime"] =

                    dr["section"] = entityRow.section;
                    dr["subjectCode"] = entityRow.subjectCode;
                    dr["instructor"] = entityRow.instructor;
                    //dr["numOfHours"] = entityRow.numofHours;


                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }



        protected void btnUpload_Click(object sender, EventArgs e)
        {

            var schoolYear=lblSchoolYear.Text.ToString().Split('-');
            var duration = lblDuration.Text.ToString();
            if (Uploader.HasFile && Path.GetExtension(Uploader.FileName) == ".xlsx")
            {
                using (var excel = new ExcelPackage(Uploader.PostedFile.InputStream))
                {

                    var ws = excel.Workbook.Worksheets.First();
                    int startRow = 2;
                    //var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column]---first row,first column,first row,last column //translation
                    //foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column]) 
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

                                    SemesterSchedule data = new SemesterSchedule();
                                    data.roomNumber = ws.Cells[i, 1].Text.Equals("") ? null : ws.Cells[i, 1].Text;

                                    String schoolDays = ws.Cells[i, 2].Text;
                                    schoolDays = Regex.Replace(schoolDays, @"\s", "");



                                    String daysCombination = "";

                                    if (schoolDays.ToLower().Contains("mon"))
                                    {
                                        daysCombination += "1";
                                        data.hasMonday = "1";
                                    }
                                    if (schoolDays.ToLower().Contains("tue"))
                                    {
                                        daysCombination += "2";
                                        data.hasTuesday = "1";
                                    }
                                    if (schoolDays.ToLower().Contains("wed"))
                                    {
                                        daysCombination += "3";
                                        data.hasWednesday = "1";
                                    }
                                    if (schoolDays.ToLower().Contains("thu"))
                                    {
                                        daysCombination += "4";
                                        data.hasThursday = "1";
                                    }
                                    if (schoolDays.ToLower().Contains("fri"))
                                    {
                                        daysCombination += "5";
                                        data.hasFriday = "1";
                                    }

                                    if (schoolDays.ToLower().Contains("sat"))
                                    {
                                        daysCombination += "6";
                                        data.hasSaturday = "1";
                                    }

                                    data.days = daysCombination;
                                    data.time = ws.Cells[i, 3].Value==null? null : ws.Cells[i, 3].Value.ToString();
                                    data.subjectCode = ws.Cells[i, 4].Text;
                                    data.section = ws.Cells[i, 5].Text;
                                    data.instructor = ws.Cells[i, 6].Text.Equals("") ? null : ws.Cells[i, 6].Text;
                                    data.numofHours = ws.Cells[i, 7].Text.Equals("") ? null : ws.Cells[i, 7].Text;
                                    data.schoolYearStart = Int32.Parse(schoolYear[0]);
                                    data.schoolYearEnd = Int32.Parse(schoolYear[1]);
                                    data.durationStart = DateTime.Parse(duration.Substring(0, 10));
                                    data.durationEnd = DateTime.Parse(duration.Substring(14, 10));
                                    if (lblSemester.Text.Equals("1st"))
                                    {
                                        data.semester = 1;
                                    }
                                    else if (lblSemester.Text.Equals("2nd"))
                                    {
                                        data.semester = 2;
                                    }
                                    else if (lblSemester.Text.Equals("3rd"))
                                    {
                                        data.semester = 3;
                                    }

                                    entity.SemesterSchedules.Add(data);
                                }

                                entity.SaveChanges();
                            }

                            transactionScope.Complete();
                            

                        }
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showSuccessMsg()", true);
                        getSchedTable();
                        schedPanel.Update();
                        Response.Redirect("UploadSchedule.aspx");


                    }
                    catch (NullReferenceException nre)
                    {
                        Console.WriteLine(nre);
                    }
                    catch (TransactionAbortedException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }                         
                    
                    
                }
            }
            else
            {
                
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showErrorMsg()", true);
            }
        }

        protected void schedGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            for (int rowIndex = schedGrid.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = schedGrid.Rows[rowIndex];
                GridViewRow gvPreviousRow = schedGrid.Rows[rowIndex + 1];
                for (int cellCount = 0; cellCount < 2; cellCount++)
                {
                    if (gvRow.Cells[cellCount].Text == gvPreviousRow.Cells[cellCount].Text)
                    {
                        if (gvPreviousRow.Cells[cellCount].RowSpan < 2)
                        {
                            gvRow.Cells[cellCount].RowSpan = 2;
                        }
                        else
                        {
                            gvRow.Cells[cellCount].RowSpan =
                                gvPreviousRow.Cells[cellCount].RowSpan + 1;
                           
                        }
                        gvPreviousRow.Cells[cellCount].Visible = false;
                    }
                }
            }





        }

        protected void templateBtn_Click(object sender, EventArgs e)
        {
            using (ExcelPackage package = new ExcelPackage())
            {

                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Schedule");

                //column headers
                ws.Cells["A1"].Value = "Room Number";
                ws.Cells["B1"].Value = "Days";
                ws.Cells["C1"].Value = "Time";
                ws.Cells["D1"].Value = "Subject Code";
                ws.Cells["E1"].Value = "Section";
                ws.Cells["F1"].Value = "Instructor";
                ws.Cells["G1"].Value = "Number of Hours";
                ws.Cells["A1:G1"].Style.Font.Bold = true;

                //set column width
                ws.Column(1).Width = 15;
                ws.Column(2).Width = 15;
                ws.Column(3).Width = 15;
                ws.Column(4).Width = 15;
                ws.Column(5).Width = 15;
                ws.Column(6).Width = 15;
                ws.Column(7).Width = 15;

                //dropdown list of days
                var days = ws.DataValidations.AddListValidation("B2:B1000");
                
                //days.ShowErrorMessage=true;
                //days.ErrorTitle = "An invalid feedback was entered";
                //days.Error = "Please choose feedback from dropdown only.";

                days.Formula.Values.Add("Monday");
                days.Formula.Values.Add("Tuesday");
                days.Formula.Values.Add("Wednesday");
                days.Formula.Values.Add("Thursday");
                days.Formula.Values.Add("Friday");
                days.Formula.Values.Add("Saturday");
                
                //dropdown list of time
                var time = ws.DataValidations.AddListValidation("C2:C1000");
                time.Formula.Values.Add("0730-1100");
                time.Formula.Values.Add("1100-1430");
                time.Formula.Values.Add("1430-1800");
                time.Formula.Values.Add("1800-2130");
                time.Formula.Values.Add("--For PE Classes--");
                time.Formula.Values.Add("0900-1100");
                time.Formula.Values.Add("1000-1200");
                time.Formula.Values.Add("1200-1400");
                time.Formula.Values.Add("1445-1645");

                //time.Formula.ExcelFormula"=time<>
                //time.ShowErrorMessage=true;
                //time.ErrorTitle = "An invalid feedback was entered";
                //time.Error = "Please choose feedback from dropdown only.";
               


                


                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=Template.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(package.GetAsByteArray());
                Response.End();
            }
        }

        protected void schedGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            schedGrid.PageIndex = e.NewPageIndex;
            getSchedTable();
        }

       
    }
}