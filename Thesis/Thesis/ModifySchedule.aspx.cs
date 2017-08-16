using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class ModifySchedule : System.Web.UI.Page
    {

      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                getSchedTable("");
            }
        }



        public void getSchedTable(String search)
        {
            ViewState["search"] = search;
            DataTable dt = new DataTable();
            dt = getSched(search);
            schedGrid.DataSource = dt;
            schedGrid.DataBind();
            schedPanel.Update();
        }



        public static DataTable getSched(String search)
        {


            String[] daysOfWeek = new String[] {"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"};
            using (ThesisDBEntities data = new ThesisDBEntities())
            {

            var settings = data.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
            int sys = settings.schoolYearStart;
            int sye = settings.schoolYearEnd;
            var sem=Int32.Parse(settings.semester);
            DataTable dt = new DataTable();
            dt.Columns.Add("schedID", typeof(int));
            dt.Columns.Add("roomNumber", typeof(string));
            dt.Columns.Add("days", typeof(string));
            dt.Columns.Add("time", typeof(string));
            dt.Columns.Add("subjectCode", typeof(string));
            dt.Columns.Add("section", typeof(string));
            dt.Columns.Add("instructor", typeof(string));
            dt.Columns.Add("numOfHours", typeof(string));

            DataRow dr;
                //var entity = new List() ;
                if (search.Trim().Length == 0)
                {
                   var entity = (from t in data.SemesterSchedules
                              where t.semester == sem && t.schoolYearStart == sys && t.schoolYearEnd == sye
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
                       dr["days"] = entityRow.days==null?"":daysOfWeek[Int32.Parse(entityRow.days) - 1];
                       dr["time"] = entityRow.time;
                       dr["section"] = entityRow.section;
                       dr["subjectCode"] = entityRow.subjectCode;
                       dr["instructor"] = entityRow.instructor;
                       dr["numOfHours"] = entityRow.numofHours;


                       dt.Rows.Add(dr);
                   }
                   return dt;

                }
                else
                {
                    if (search.ToLower().Equals("monday"))
                    {
                        search = "1";
                    }
                    if (search.ToLower().Equals("tuesday"))
                    {
                        search = "2";
                    }
                    if (search.ToLower().Equals("wednesday"))
                    {
                        search = "3";
                    }
                    if (search.ToLower().Equals("thursday"))
                    {
                        search = "4";
                    }
                    if (search.ToLower().Equals("friday"))
                    {
                        search = "5";
                    }
                    if (search.ToLower().Equals("saturday"))
                    {
                        search = "6";
                    }

                    var entity = (from t in data.SemesterSchedules
                              where ((t.semester == sem && t.schoolYearStart == sys && t.schoolYearEnd == sye) && (t.roomNumber.Equals(search) || t.days.Equals(search) || t.time.Equals(search) || t.subjectCode.Equals(search) || t.instructor.Equals(search) || t.section.Equals(search)))
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
                        dr["days"] = daysOfWeek[Int32.Parse(entityRow.days) - 1];
                        dr["time"] = entityRow.time;
                        dr["section"] = entityRow.section;
                        dr["subjectCode"] = entityRow.subjectCode;
                        dr["instructor"] = entityRow.instructor;
                        dr["numOfHours"] = entityRow.numofHours;


                        dt.Rows.Add(dr);
                    }
                    return dt;

                }
                
              


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

        protected void schedGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "onDelete")
            {
                int ID = Int32.Parse(e.CommandArgument.ToString());
                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    var entity = data.SemesterSchedules.FirstOrDefault(g => g.schedID==ID);
                    data.SemesterSchedules.Remove(entity);
                    data.SaveChanges();
                }
                getSchedTable("");
            }
            else if (e.CommandName == "onUpdate")
            {
                var ID = Int32.Parse(e.CommandArgument.ToString());
                
               ;
                using (ThesisDBEntities data = new ThesisDBEntities())
                {

                    var entity = data.SemesterSchedules.FirstOrDefault(x => x.schedID == ID);

                    resetEdit();
                    editRoomDDL.Items.Add(new ListItem("", ""));
                    var rooms = (from p in data.RoomLists
                                 select new
                                 {
                                     p.room
                                 });

                    //editDayDDL.Items.Add(new ListItem("", ""));
                    foreach (var room in rooms)
                    {
                        editRoomDDL.Items.Add(new ListItem(room.room, room.room));
                    }
                    editRoomID.Text = ID.ToString();
                    editRoomDDL.SelectedValue = entity.roomNumber==null?"1":entity.roomNumber;
                    editDayDDL.SelectedValue = entity.days==null?"1":entity.days;
                    if (entity.time == null)
                    {
                        editStartTxt.Text = String.Empty;
                        editEndTxt.Text = String.Empty;
                    }
                    else
                    {
                        string[] split = entity.time.Split('-');
                        editStartTxt.Text = split[0];
                        editEndTxt.Text = split[1];
                    }
                    editSubjectTxt.Text = entity.subjectCode == null ? String.Empty : entity.subjectCode;
                    editInstructorTxt.Text = entity.instructor == null ? String.Empty : entity.instructor;
                    editSectionTxt.Text = entity.section == null ? String.Empty : entity.section;
                    editNumTxt.Text = entity.numofHours == null ? String.Empty : entity.numofHours;
                    editSchedulePanel.Update();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#editScheduleModal').modal('show');", true);
                }
            }


           
        }


        protected void saveButton_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var time=startTxt.Text + "-" + endTxt.Text;

                if(subjectCodeTextBox.Text.Trim().Length==0){


                }
                else if(data.SemesterSchedules.Any(x=>x.roomNumber.Equals(roomDDL.SelectedValue)&&x.time.Equals(time) && x.days.Equals(ddlDay.SelectedValue) &&x.subjectCode.Equals(subjectCodeTextBox.Text))){

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "exist()", true);
                
                }
                else{
                    var settings = data.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                    SemesterSchedule schedule = new SemesterSchedule();
                    schedule.roomNumber=roomDDL.SelectedValue.Equals("")?null:roomDDL.SelectedValue;
                
                        if (ddlDay.SelectedValue.Equals("1"))
                        {
                            schedule.days = "1";
                            schedule.hasMonday = "1";
                        }
                        else if (ddlDay.SelectedValue.Equals("2"))
                        {
                            schedule.days = "2";
                            schedule.hasTuesday = "1";
                        }
                        else if (ddlDay.SelectedValue.Equals("3"))
                        {
                            schedule.days = "3";
                            schedule.hasWednesday = "1";
                        }
                        else if (ddlDay.SelectedValue.Equals("4"))
                        {
                            schedule.days = "4";
                            schedule.hasThursday = "1";
                        }
                        else if (ddlDay.SelectedValue.Equals("5"))
                        {
                            schedule.days = "5";
                            schedule.hasFriday = "true";
                        }
                        else if (ddlDay.SelectedValue.Equals("6"))
                        {
                            schedule.days = "6";
                            schedule.hasSaturday = "1";
                        }


                    schedule.time = time.Trim().Length == 0 ? null : startTxt.Text + "-" + endTxt.Text;
                    schedule.section = sectionTextBox.Text.Trim().Length==0 ? null : sectionTextBox.Text.ToUpper();
                    schedule.subjectCode = subjectCodeTextBox.Text.Trim().Length==0 ? null : subjectCodeTextBox.Text.ToUpper();
                    schedule.instructor = instructorTextBox.Text.Trim().Length==0? null : instructorTextBox.Text;
                    schedule.numofHours = numOfHoursTextBox.Text.Trim().Length == 0 ? null : numOfHoursTextBox.Text; 
                    schedule.semester = Int32.Parse(settings.semester);
                    schedule.schoolYearStart = settings.schoolYearStart;
                    schedule.schoolYearEnd = settings.schoolYearEnd;
                    schedule.durationStart = settings.durationStart;
                    schedule.durationEnd = settings.durationEnd;
                
                    data.SemesterSchedules.Add(schedule);
                    data.SaveChanges();
                    getSchedTable("");
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showSuccess()", true);
                }
                

            }
            
        }

        protected void btnAddSchedule_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

                reset();
                roomDDL.Items.Add(new ListItem("", ""));
                var rooms=(from p in entity.RoomLists
                          select new{
                            p.room 
                          });

                foreach (var room in rooms)
                {
                    roomDDL.Items.Add(new ListItem(room.room, room.room));
                }

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#addScheduleModal').modal('show');", true);

            }
            
        }



        protected void schedGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var search=ViewState["search"].ToString();
            schedGrid.PageIndex = e.NewPageIndex;
            getSchedTable(search);
        }

        protected void btnFindSchedule_Click(object sender, EventArgs e)
        {
            //ViewState["search"] = searchTextBox.Text;
            getSchedTable(searchTextBox.Text);
            //schedPanel.Update();
        }

        public void reset()
        {
            //roomDDL.SelectedIndex = 1;
            ddlDay.SelectedIndex = 0;
            startTxt.Text = String.Empty;
            endTxt.Text = String.Empty;
            subjectCodeTextBox.Text = String.Empty;
            sectionTextBox.Text = String.Empty;
            instructorTextBox.Text = String.Empty;
            numOfHoursTextBox.Text = String.Empty;
            roomDDL.Items.Clear();
            mdlSchedulePanel.Update();
        }


        public void resetEdit()
        {
            //roomDDL.SelectedIndex = 1;
            editRoomDDL.Items.Clear();
            //editDayDDL.Items.Clear();
            editSchedulePanel.Update();
        }

        protected void editBtn_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
           
                
                var id = Int32.Parse(editRoomID.Text);
                var data = entity.SemesterSchedules.FirstOrDefault(g => g.schedID==id);
                //data.days = editDayDDL.SelectedValue.Equals("") ? null : editDayDDL.SelectedValue;
                data.subjectCode=editSubjectTxt.Text.Trim().Length==0?null:editSubjectTxt.Text;
                data.instructor = editInstructorTxt.Text.Trim().Length == 0 ? null : editInstructorTxt.Text;
                data.numofHours = editNumTxt.Text.Trim().Length == 0 ? null : editNumTxt.Text;
                if (editStartTxt.Text.Trim().Length == 0 || editEndTxt.Text.Trim().Length == 0)
                {
                    data.time = null;
                }
                else
                {
                    data.time = editStartTxt.Text + "-" + editEndTxt.Text;
                }

                if (editDayDDL.SelectedValue.Equals("1"))
                {
                    data.days = "1";
                    data.hasMonday = "1";
                    data.hasTuesday = null;
                    data.hasWednesday = null;
                    data.hasThursday = null;
                    data.hasFriday = null;
                    data.hasSaturday = null;
                }
                else if (editDayDDL.SelectedValue.Equals("2"))
                {
                    data.days = "2";
                    data.hasMonday = null;
                    data.hasTuesday = "1";
                    data.hasWednesday = null;
                    data.hasThursday = null;
                    data.hasFriday = null;
                    data.hasSaturday = null;
                }
                else if (editDayDDL.SelectedValue.Equals("3"))
                {
                    data.days = "3";
                    data.hasMonday = null;
                    data.hasTuesday = null;
                    data.hasWednesday = "1";
                    data.hasThursday = null;
                    data.hasFriday = null;
                    data.hasSaturday = null;
                }
                else if (editDayDDL.SelectedValue.Equals("4"))
                {
                    data.days = "4";
                    data.hasMonday = null;
                    data.hasTuesday = null;
                    data.hasWednesday = null;
                    data.hasThursday = "1";
                    data.hasFriday = null;
                    data.hasSaturday = null;

                }
                else if (editDayDDL.SelectedValue.Equals("5"))
                {
                    data.days = "5";

                    data.hasMonday = null;
                    data.hasTuesday = null;
                    data.hasWednesday = null;
                    data.hasThursday = null;
                    data.hasFriday = "1";
                    data.hasSaturday = null;
                }
                else if (editDayDDL.SelectedValue.Equals("6"))
                {
                    data.days = "6";
                    data.hasMonday = null;
                    data.hasTuesday = null;
                    data.hasWednesday = null;
                    data.hasThursday = null;
                    data.hasFriday = null;
                    data.hasSaturday = "1";
                }
                else
                {
                    data.days = null;
                    data.hasMonday = null;
                    data.hasTuesday = null;
                    data.hasWednesday = null;
                    data.hasThursday = null;
                    data.hasFriday = null;
                    data.hasSaturday = null;
                }


                entity.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "edited()", true);
                getSchedTable("");
                
           
            }
        }
    }
}