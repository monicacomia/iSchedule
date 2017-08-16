using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.Data;
using System.Globalization;
using Thesis;
using System.Data.Objects;

namespace Thesis
{
    public partial class MakeupForm : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FACULTY_ID"] != null)
            {
                if (!IsPostBack)
                {
                    string date = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm");
                    getUserData();
                    getStartTimeDDL();
             
                    //getProfessors();
                    //getPeSchedProfessors();
                    
                    //if (Convert.ToInt32(Session["USER_TYPE"]) == 3)
                    //{

                    //    fac_id.Visible = true;
                    //    pe_fac_id.Visible = true;
                    //    facultyId_DDL.Visible = true;
                    //    peFacultyIdDDL.Visible = true;
                    //}
                    //else {
                    //    fac_id.Visible = false;
                    //    pe_fac_id.Visible = false;
                    //    facultyId_DDL.Visible = false;
                    //    peFacultyIdDDL.Visible = false;
                    //}
                }
                else
                {
                    getUserData();
                }
            }
            else
            {
                Response.Redirect("~/LoginPage.aspx#signIn");
            }
        }

        protected void getUserData()
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                var ID = Session["FACULTY_ID"].ToString();
                var data = entity.Users.Where(p => p.faculty_id.Equals(ID)).FirstOrDefault();
                
                facultyId.Text = data.faculty_id;
                facultyName.Text = data.last_name + ", " + data.first_name;
            }
        }

        // Get Data for Registered Faculty Users
        public static DataTable getFacultyId()
        {
            DataTable dtTable = new DataTable();
            DataRow drRow;

            dtTable.Columns.Add("value", typeof(string));
            dtTable.Columns.Add("text", typeof(string));

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var myEntitySet = (from t in data.Users
                                   select new
                                   {
                                       value = t.faculty_id,
                                       text = t.faculty_id + "-" + t.last_name + ", " + t.first_name
                                   }).OrderBy(t => t.value);

                foreach (var myEntityRow in myEntitySet)
                {
                    drRow = dtTable.NewRow();
                    drRow["value"] = myEntityRow.value.ToString();
                    drRow["text"] = myEntityRow.text.ToString();
                    dtTable.Rows.Add(drRow);
                }
            }
            return dtTable;
        }

        //private void getProfessors()
        //{
        //    facultyId_DDL.DataSource = getFacultyId();
        //    facultyId_DDL.DataTextField = "text";
        //    facultyId_DDL.DataValueField = "value";
        //    facultyId_DDL.DataBind();

        //    facultyId_DDL.SelectedValue.Equals("0");
        //}

        //private void getPeSchedProfessors()
        //{
        //    peFacultyIdDDL.DataSource = getFacultyId();
        //    peFacultyIdDDL.DataTextField = "text";
        //    peFacultyIdDDL.DataValueField = "value";
        //    peFacultyIdDDL.DataBind();

        //    facultyId_DDL.SelectedValue.Equals("0");
        //}

        public void clearTextField() {
            absentDate.Text = string.Empty;
            subjCodeTxtbox.Text = string.Empty;
            sectionTxtbox.Text = string.Empty;
            makeUpClassDate.Text = string.Empty;
            reasonTxtbox.Text = string.Empty;
        }

        private bool SavePreValidation()
        {
            bool requiredValid = true;

            if (absentDate.Text.Trim().Length == 0)
                requiredValid = false;

            if (subjCodeTxtbox.Text.Trim().Length == 0)
                requiredValid = false;

            if (sectionTxtbox.Text.Trim().Length == 0)
                requiredValid = false;

            if (makeUpClassDate.Text.Trim().Length == 0)
                requiredValid = false;

            if (reasonTxtbox.Text.Trim().Length == 0)
                requiredValid = false;
           
            return (requiredValid);
        }

        private bool SavePreValidationPeClasses()
        {
            bool requiredValid = true;

            //if (absentDate.Text.Trim().Length == 0)
            //    requiredValid = false;

            if (peSubjectCode.Text.Trim().Length == 0)
                requiredValid = false;

            if (peSection.Text.Trim().Length == 0)
                requiredValid = false;

            if (peMakeupClassDate.Text.Trim().Length == 0)
                requiredValid = false;

            if (peReason.Text.Trim().Length == 0)
                requiredValid = false;

            return (requiredValid);
        }


        //private bool SavePreValidationRecordExists(String absentDate, String subjCode, String section)
        //{
        //    bool recordExistValid = true;

        //    using (ThesisDBEntities data = new ThesisDBEntities())
        //    {
        //        var isFiled = (data.PendingClassSchedules.ToList().Where(p => p.absentDate.ToString("MMMM DD YYYY").Equals(absentDate) && 
        //                         p.subjectCode.Equals(subjCode) && p.section.Equals(section)).Any());

        //        if (isFiled == false)
        //        {
        //            recordExistValid = false;
        //        }
        //        else {
        //            recordExistValid = true;
        //        }

        //    }
        //    return (recordExistValid);
        //}

        protected void PendingSchedButton_Click(object sender, EventArgs e)
        {
            if (SavePreValidation())
            {
                //if (SavePreValidationRecordExists(absentDate.Text, subjCodeTxtbox.Text, sectionTxtbox.Text))
                //{
                    using (ThesisDBEntities data = new ThesisDBEntities())
                    {
                        PendingClassSchedule sched = new PendingClassSchedule();

                        // --- Faculty ID From Session or accgdg to logged in User
                        var ID = Session["FACULTY_ID"].ToString();
                        var faculty = data.Users.Where(p => p.faculty_id.Equals(ID)).FirstOrDefault();
                        string facultyName = faculty.first_name + " " + faculty.last_name;

                        // --- Faculty ID From DropDownList
                        //var facultyPicker = data.Users.Where(p => p.faculty_id.Equals(facultyId_DDL.SelectedValue)).FirstOrDefault();
                        //string facultyPickerName = faculty.first_name + " " + faculty.last_name;

                        sched.absentDate = DateTime.Parse(absentDate.Text);

                        //if (Convert.ToInt32(Session["USER_TYPE"]) == 3)
                        //{
                        //    //---ASSISTANT ACADEMICS 
                        //    sched.faculty_id = facultyId_DDL.SelectedValue; // Should be dropdown for Faculty Users: Get From Users Table
                        //    sched.facultyName = facultyPickerName;
                        //}
                        //else
                        //{
                            sched.faculty_id = faculty.faculty_id;
                            sched.facultyName = facultyName; // Change accdg to logged in user
                        //}

                        sched.dateFiled = DateTime.Now;
                        sched.subjectCode = subjCodeTxtbox.Text;
                        sched.section = sectionTxtbox.Text;
                        sched.assignedDate = DateTime.Parse(makeUpClassDate.Text).Date; //Add day and time in one textfield
                        sched.duration = makeupTimeDDL.SelectedValue;
                        sched.numHours = "3hrs And 30mins";
                        sched.room = makeupRoomDDL.SelectedValue;
                        sched.reason = reasonTxtbox.Text;

                        data.PendingClassSchedules.Add(sched);
                        data.SaveChanges();

                        clearTextField();
                    }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "makeupSuccess()", true);
                //} else {
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "filedMakeup()", true);
                //}
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "validInput()", true);
            }
           
        }

        public List<String> getAvailableSched(string makeupClassDate, string makeupTime){

            List<String> availableRoom = new List<string>();

            DateTime date = DateTime.Parse(makeupClassDate);
            var chosen = date;
            String day = chosen.DayOfWeek.ToString();
            String query = queryDay(day);

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var combi = (from p in data.SemesterSchedules.Where(query, chosen.Date, "1")
                             where !data.CancelledClassSchedules.Any(x => x.schedID == p.schedID && EntityFunctions.TruncateTime(x.cancelledDate) == EntityFunctions.TruncateTime(chosen.Date))
                             select new { p.schedID, p.roomNumber, p.time }
                 );
                 
                var combiMakeup = combi.Union(from m in data.MakeupClassSchedules
                                              where EntityFunctions.TruncateTime(m.makeupDate) == EntityFunctions.TruncateTime(chosen.Date)
                                              select new
                                              {
                                                  schedID = m.makeupClassID,
                                                  roomNumber = m.room,
                                                  m.time,
                                              });

                var roomList = from r in data.RoomLists select r.room;

                foreach (var roomNum in roomList)
                {
                    var available = combiMakeup.Where(x=> x.roomNumber.Equals(roomNum) && x.time.Equals(makeupTime)).FirstOrDefault();
                    if (available == null) {
                        availableRoom.Add(roomNum);
                    }
                }
            }
            return availableRoom;
        }

        public List<String> getAvailablePE_Sched(string makeupClassDate, string makeupTime)
        {

            List<String> availableRoom = new List<string>();

            DateTime date = DateTime.Parse(makeupClassDate);
            var chosen = date;
            String day = chosen.DayOfWeek.ToString();
            String query = queryDay(day);

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var combi = (from p in data.SemesterSchedules.Where(query, chosen.Date, "1")
                             where !data.CancelledClassSchedules.Any(x => x.schedID == p.schedID && EntityFunctions.TruncateTime(x.cancelledDate) == EntityFunctions.TruncateTime(chosen.Date))
                             select new { p.schedID, p.roomNumber, p.time }
                 );

                var combiMakeup = combi.Union(from m in data.MakeupClassSchedules
                                              where EntityFunctions.TruncateTime(m.makeupDate) == EntityFunctions.TruncateTime(chosen.Date)
                                              select new
                                              {
                                                  schedID = m.makeupClassID,
                                                  roomNumber = m.room,
                                                  m.time,
                                              });

                var roomList = from r in data.PE_RoomList select r.rooms;

                foreach (var roomNum in roomList)
                {
                    var available = combiMakeup.Where(x => x.roomNumber.Equals(roomNum) && x.time.Equals(makeupTime)).FirstOrDefault();
                    if (available == null)
                    {
                        availableRoom.Add(roomNum);
                    }
                }
            }
            return availableRoom;
        }


        private void getAvailableRoom(string makeupClassDate, string time)
        {
            makeupRoomDDL.DataSource = getAvailableSched(makeupClassDate, time);
            makeupRoomDDL.DataBind();
           
        }

        private void getPE_AvailableRoom(string makeupClassDate, string time)
        {
            //List<String> roomsAvailable = new getAvailableSched(makeupClassDate, time);
            peMakeupRoomDDL.DataSource = getAvailablePE_Sched(makeupClassDate, time);
            peMakeupRoomDDL.DataBind();

        }

        protected void btnChkSched_Click(object sender, EventArgs e)
        {
            DateTime holiday = DateTime.Parse(makeUpClassDate.Text);
            string formattedDate = holiday.ToString("MMMM dd");
            
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                    var settings = data.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                    string startDate = settings.durationStart.ToString();
                    string endDate = settings.durationEnd.ToString();
                    
                    // Query Check for a Holiday
                    var isHoliday = (from h in data.Holidays
                                      where h.date.Equals(formattedDate)
                                      select h).Any();


                        if( (settings.durationStart <= holiday && holiday <= settings.durationEnd) == true ){
                        //if (isHoliday == true || (settings.durationStart <= holiday && holiday <= settings.durationEnd) == false)

                            if (isHoliday == true || (holiday.ToString("dddd").Equals("Sunday")) || (holiday <= DateTime.Now == true))
                                {
                                    makeupRoomDDL.Enabled = false;
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "invalidDates()", true);
                                }
                                else
                                {
                                    makeupRoomDDL.Enabled = true;
                                    getAvailableRoom(makeUpClassDate.Text, makeupTimeDDL.SelectedValue);
                                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "success()", true);
                                }
                        } else {
                            makeupRoomDDL.Enabled = false;
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "isEndStartTerm()", true);
                        }
            }
            chckClassSchedUpdatePanel.Update();
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

        protected void pePendingSchedBtn_Click(object sender, EventArgs e)
        {
            if (SavePreValidationPeClasses())
            {
                //if(SavePreValidationRecordExists(peAbsentDate.Text,peSubjectCode.Text, peSection.Text)){
                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    PendingClassSchedule sched = new PendingClassSchedule();

                    // --- Faculty ID From Session or accgdg to logged in User
                    var ID = Session["FACULTY_ID"].ToString();
                    var faculty = data.Users.Where(p => p.faculty_id.Equals(ID)).FirstOrDefault();
                    string facultyName = faculty.first_name + " " + faculty.last_name;

                    // --- Faculty ID From DropDownList
                    //var facultyPicker = data.Users.Where(p => p.faculty_id.Equals(peFacultyIdDDL.SelectedValue)).FirstOrDefault();
                    //string facultyPickerName = faculty.first_name + " " + faculty.last_name;

                    sched.absentDate = DateTime.Parse(peAbsentDate.Text);

                    //if (Convert.ToInt32(Session["USER_TYPE"]) == 3)
                    //{
                    //    //---ASSISTANT ACADEMICS 
                    //    sched.faculty_id = peFacultyIdDDL.Text; // Should be dropdown for Faculty Users: Get From Users Table
                    //    sched.facultyName = "GET THIS IN DROPDOWN VALUE in USERS TABLE WHERE ID == SELECTED VALUE ";
                    //}
                    //else
                    //{
                    sched.faculty_id = faculty.faculty_id;
                    sched.facultyName = facultyName; // Change accdg to logged in user
                    //}
                    
                    sched.dateFiled = DateTime.Now;
                    sched.subjectCode = peSubjectCode.Text;
                    sched.section = peSection.Text;
                    sched.assignedDate = DateTime.Parse(peMakeupClassDate.Text).Date; //Add day and time in one textfield
                    //Create conditional statement for checking End Time: Should not be less than start Time
                    sched.duration = peMakeupStartTimeDDL.SelectedValue + "-" + peMakeupEndTimeDDL.SelectedValue;
                    sched.numHours = "3hrs And 30mins";
                    sched.room = peMakeupRoomDDL.SelectedValue;
                    sched.reason = peReason.Text;

                    data.PendingClassSchedules.Add(sched);
                    data.SaveChanges();
                }
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "makeupSuccess()", true);
                //} else {
                //     ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "filedMakeup()", true);
                //}
            } else {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "validInput()", true);
            }
        }

        protected void peChkSchedBtn_Click(object sender, EventArgs e)
        {
            //CHECK FOR END TIME DDL NOT LESS THAN THE START TIME DDL

            DateTime holiday = DateTime.Parse(peMakeupClassDate.Text);
            string formattedDate = holiday.ToString("MMMM dd");

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var settings = data.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                string startDate = settings.durationStart.ToString();
                string endDate = settings.durationEnd.ToString();

                // Query Check for a Holiday
                var isHoliday = (from h in data.Holidays
                                 where h.date.Equals(formattedDate)
                                 select h).Any();


                if ((settings.durationStart <= holiday && holiday <= settings.durationEnd) == true)
                {
                    //if (isHoliday == true || (settings.durationStart <= holiday && holiday <= settings.durationEnd) == false)

                    if (isHoliday == true || (holiday.ToString("dddd").Equals("Sunday")))
                    {
                        peMakeupRoomDDL.Enabled = false;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "invalidDates()", true);
                    }
                    else
                    {
                        peMakeupRoomDDL.Enabled = true;
                        getPE_AvailableRoom(peMakeupClassDate.Text, peMakeupStartTimeDDL.SelectedValue + "-" + peMakeupEndTimeDDL.SelectedValue);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "success()", true);
                    }
                }
                else
                {
                    peMakeupRoomDDL.Enabled = false;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "isEndStartTerm()", true);
                }
            }
            peSchedUpdatePanel.Update();

        }
   
        private void getStartTimeDDL()
        {
            peMakeupStartTimeDDL.DataSource = getOpenTime();
            peMakeupStartTimeDDL.DataTextField = "text";
            peMakeupStartTimeDDL.DataValueField = "value";
            peMakeupStartTimeDDL.DataBind();

            peMakeupStartTimeDDL.SelectedValue.Equals("0");
        }

        private void getEndTimeDDL(int startTime)
        {
            peMakeupEndTimeDDL.DataSource = getOpenEndTime(startTime);
            peMakeupEndTimeDDL.DataTextField = "text";
            peMakeupEndTimeDDL.DataValueField = "value";
            peMakeupEndTimeDDL.DataBind();

            peMakeupEndTimeDDL.SelectedValue.Equals("0");
        }

        public static DataTable getOpenTime()
        {
            DataTable dtTable = new DataTable();
            DataRow drRow;

            dtTable.Columns.Add("value", typeof(int));
            dtTable.Columns.Add("text", typeof(string));

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var myEntitySet = (from t in data.OpenTimes
                                   select new
                                   {
                                       value = t.timeValue,
                                       text = t.timeText
                                   });

                foreach (var myEntityRow in myEntitySet)
                {
                    drRow = dtTable.NewRow();
                    drRow["value"] = myEntityRow.value.ToString();
                    drRow["text"] = myEntityRow.text.ToString();
                    dtTable.Rows.Add(drRow);
                }
            }
            return dtTable;
        }

        public static DataTable getOpenEndTime(int startTime)
        {
            DataTable dtTable = new DataTable();
            DataRow drRow;

            dtTable.Columns.Add("value", typeof(int));
            dtTable.Columns.Add("text", typeof(string));

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var myEntitySet = (from t in data.OpenTimes
                                   where t.timeValue > startTime
                                   select new
                                   {
                                       value = t.timeValue,
                                       text = t.timeText
                                   });

                foreach(var myEntityRow in myEntitySet)
                {
                    drRow = dtTable.NewRow();
                    drRow["value"] = myEntityRow.value.ToString();
                    drRow["text"] = myEntityRow.text.ToString();
                    dtTable.Rows.Add(drRow);
                }
            }
            return dtTable;
        }

        protected void peMakeupStartTimeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            peMakeupEndTimeDDL.Items.Clear();
            getEndTimeDDL(Convert.ToInt32(peMakeupStartTimeDDL.SelectedValue));
            startTimeUpdatePanel.Update();
        }

    }
}