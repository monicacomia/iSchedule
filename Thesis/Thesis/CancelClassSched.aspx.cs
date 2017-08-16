using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;

namespace Thesis
{
    public partial class CancelClassSched : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                //getCancelSchedTable(DateTime.Now.ToString());
            }
        }

        public void getCancelSchedTable(string cancelledSched)
        {

            DataTable dt = new DataTable();
            dt = getUsers(cancelClassDate.Text);

            cancelSchedGrid.DataSource = dt;
            cancelSchedGrid.DataBind();
            cancelSchedPanel.Update();

            //dt.Rows.Count
        }

        public static DataTable getUsers(string cancelledSched)
        {
            DateTime sched = DateTime.Parse(cancelledSched);
            var chosen = sched.Date;
            String day = chosen.DayOfWeek.ToString();
            String query = queryDay(day);

            DataTable dt = new DataTable();
            dt.Columns.Add("schedId", typeof(int));
            dt.Columns.Add("subjectCode", typeof(string));
            dt.Columns.Add("section", typeof(string));
            dt.Columns.Add("facultyName", typeof(string));
            dt.Columns.Add("time", typeof(string));
            dt.Columns.Add("room", typeof(string));
            //dt.Columns.Add("classType", typeof(string)); 

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                //var entity = (from t in data.SemesterSchedules
                //              select new
                //              {
                //                  t.schedID,
                //                  t.subjectCode,
                //                  t.section,
                //                  t.instructor,
                //                  t.time,
                //                  t.roomNumber
                //              });

                // if regularClassId EXIST in cancelClassSchedule w/ same CancelledDate and chosenDate ; save in cancelledSchedule/not found in dataGrid to be cancelled 
                // else retain the regularClass in Grid

                var entity = (from s in data.SemesterSchedules.Where(query, chosen.Date, "1")
                                where !data.CancelledClassSchedules.Any(c => c.schedID == s.schedID && EntityFunctions.TruncateTime(c.cancelledDate) == EntityFunctions.TruncateTime(chosen.Date))
                                orderby s.roomNumber, s.days ascending, s.time ascending
                                select new {

                                    s.schedID,
                                    s.subjectCode,
                                    s.section,
                                    s.instructor,
                                    s.time,
                                    s.roomNumber
                                }
                 );

                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["schedId"] = entityRow.schedID;
                    dr["subjectCode"] = entityRow.subjectCode;
                    dr["section"] = entityRow.section;
                    dr["facultyName"] = entityRow.instructor;
                    dr["time"] = entityRow.time;
                    dr["room"] = entityRow.roomNumber;

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static String queryDay(String day)
        {
            String query = null;
            switch (day)
            {
                case "Monday":
                    query = "(durationStart<=@0 && @0<=durationEnd) && hasMonday.Equals(@1)";
                    break;
                case "Tuesday":
                    query = "(durationStart<=@0 && @0<=durationEnd) && hasTuesday.Equals(@1)";
                    break;
                case "Wednesday":
                    query = "(durationStart<=@0 && @0<=durationEnd) && hasWednesday.Equals(@1)";
                    break;
                case "Thursday":
                    query = "(durationStart<=@0 && @0<=durationEnd) && hasThursday.Equals(@1)";
                    break;
                case "Friday":
                    query = "(durationStart<=@0 && @0<=durationEnd) && hasFriday.Equals(@1)";
                    break;
                case "Saturday":
                    query = "(durationStart<=@0 && @0<=durationEnd) && hasSaturday.Equals(@1)";
                    break;
            }
            return query;
        }


        protected void cancelSchedGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int classId = Convert.ToInt32(e.CommandArgument);
            
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                if (e.CommandName == "onCancel")
                {
                    SemesterSchedule regClass = data.SemesterSchedules.FirstOrDefault(t => t.schedID  == classId);
                    CancelledClassSchedule cancelledClass = new CancelledClassSchedule();

                    cancelledClass.schedID = regClass.schedID;
                    cancelledClass.cancelledDate = (DateTime.Parse(cancelClassDate.Text)).Date; // Check this from datePicker
                    cancelledClass.subjectCode = regClass.subjectCode;
                    cancelledClass.section = regClass.section;
                    cancelledClass.facultyName = regClass.instructor;
                    cancelledClass.time = regClass.time;
                    cancelledClass.room = regClass.roomNumber;
                    cancelledClass.dateFiled = DateTime.Now.Date;
                    cancelledClass.reason = "None"; // Create action button(Reason): modal for input reason (same as messaging) 

                    data.CancelledClassSchedules.Add(cancelledClass);
                    data.SaveChanges();

                    getCancelSchedTable(cancelClassDate.Text);
                    cancelSchedPanel.Update();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Nothing goes here..
        }

        protected void assignCancelDate_Click(object sender, EventArgs e)
        {
            getCancelSchedTable(cancelClassDate.Text);
            cancelSchedPanel.Update();

        }

        protected void cancelSchedGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cancelSchedGrid.PageIndex = e.NewPageIndex;
            getCancelSchedTable(cancelClassDate.Text);

        }

        protected void cancelSchedGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            for (int rowIndex = cancelSchedGrid.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = cancelSchedGrid.Rows[rowIndex];
                GridViewRow gvPreviousRow = cancelSchedGrid.Rows[rowIndex + 1];
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
    }
}