using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class ApprovalPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getPendingSchedTable();
        }

        public void getPendingSchedTable()
        {

            DataTable dt = new DataTable();
            dt = getUsers();

            pendingSchedGrid.DataSource = dt;
            pendingSchedGrid.DataBind();
            pendingSchedPanel.Update();

            //dt.Rows.Count
        }

        public static DataTable getUsers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("pendingClassId", typeof(int));
            dt.Columns.Add("subjCode", typeof(string));
            dt.Columns.Add("section", typeof(string));
            dt.Columns.Add("facultyName", typeof(string));
            dt.Columns.Add("dateFiled", typeof(DateTime));
            dt.Columns.Add("assignedDate", typeof(DateTime));
            dt.Columns.Add("assignedDay", typeof(string));
            dt.Columns.Add("room", typeof(string));
            dt.Columns.Add("duration", typeof(string));
            dt.Columns.Add("numHours", typeof(string));
            dt.Columns.Add("reason", typeof(string));
            dt.Columns.Add("absentDate", typeof(DateTime));
            //dt.Columns.Add("classType", typeof(string)); 

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.PendingClassSchedules
                              select new
                              {
                                  t.pendingClassID,
                                  t.subjectCode,
                                  t.section,
                                  t.facultyName,
                                  t.dateFiled,
                                  t.assignedDate,
                                  t.duration,
                                  t.numHours,
                                  t.room,
                                  t.reason,
                              });
                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["pendingClassId"] = entityRow.pendingClassID;
                    dr["subjCode"] = entityRow.subjectCode;
                    dr["section"] = entityRow.section;
                    dr["facultyName"] = entityRow.facultyName;
                    dr["dateFiled"] = entityRow.dateFiled;
                    dr["assignedDate"] = entityRow.assignedDate;
                    dr["duration"] = entityRow.duration;
                    dr["numHours"] = entityRow.numHours;
                    dr["room"] = entityRow.room;
                    dr["reason"] = entityRow.reason;

                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }

        protected void pendingSchedGrid_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void approveButton_Click(object sender, EventArgs e)
        {
            // Nothing goes here...
        }

        protected void pendingSchedGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

           int classId = Convert.ToInt32(e.CommandArgument);
           using (ThesisDBEntities data = new ThesisDBEntities())
           {
                if (e.CommandName == "onApprove")
                {

                    var settings = data.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                    PendingClassSchedule pendingSched = data.PendingClassSchedules.FirstOrDefault(t => t.pendingClassID == classId);
                    MakeupClassSchedule makeupClass = new MakeupClassSchedule();

                    makeupClass.makeupClassID = pendingSched.pendingClassID;
                    makeupClass.absentDate = pendingSched.absentDate;
                    makeupClass.subjectCode = pendingSched.subjectCode;
                    makeupClass.faculty_id = pendingSched.faculty_id;
                    makeupClass.section = pendingSched.section;
                    makeupClass.facultyName = pendingSched.facultyName;
                    makeupClass.dateFiled = pendingSched.dateFiled;
                    makeupClass.makeupDate = pendingSched.assignedDate;
                    //makeupClass.makeupDay = pendingSched.assignedDay; //to follow
                    makeupClass.room = pendingSched.room;
                    makeupClass.time = pendingSched.duration;
                    makeupClass.numHours = pendingSched.numHours;
                    makeupClass.reason = pendingSched.reason;
                    makeupClass.approvedBy = 1; // change this accdg to usertype
                    makeupClass.dateApproved = DateTime.Now;
                    //makeupClass.remarks = remarksTxtbox.Text; // create a modal after btnApprove_click to update remarks column //IF NULL, DEFAULT TO NONE.
                    makeupClass.remarks = "Approved"; // to follow: create remarks modal if needed
                    makeupClass.semester = settings.semester;
                    makeupClass.schoolYearStart = settings.schoolYearStart;
                    makeupClass.schoolYearEnd = settings.schoolYearEnd;

                    data.MakeupClassSchedules.Add(makeupClass); // This will add pending class to MakeupClassSchedule Table
                    data.PendingClassSchedules.Remove(pendingSched); // This will remove pending class from PendingClassSchedule Table
                    data.SaveChanges();

                    getPendingSchedTable();
                    pendingSchedPanel.Update();
                    //Creat growl message after btnApprove click
                    
                    // FUNCTION HERE: Send e-mail for APPROVED classes
                    sendEmail(1, pendingSched);

                } else if (e.CommandName == "onDisapprove"){

                    PendingClassSchedule unapprovedMakeup = data.PendingClassSchedules.FirstOrDefault(t => t.pendingClassID == classId);
                    data.PendingClassSchedules.Remove(unapprovedMakeup);
                    data.SaveChanges();
                    getPendingSchedTable();
                    pendingSchedPanel.Update();


                    //Creat growl message after btnDisapprove click

                    // FUNCTION HERE: Send e-mail for DISAPPROVED classes
                    sendEmail(2, unapprovedMakeup);
                }
            }
        }

        public void sendEmail(int emailType, PendingClassSchedule pcs)
        {
            using (ThesisDBEntities data = new ThesisDBEntities()){
                User user = (from a in data.Users
                                where a.faculty_id == pcs.faculty_id
                                select a).FirstOrDefault();

                using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["emailAuthUser"], user.email_address))
                {
                    mm.IsBodyHtml = true;

                    if (emailType == 1)
                    {
                        mm.Subject = String.Format("Make-up class approval for {0}", pcs.subjectCode);
                        mm.Body = String.Format("Hello {0},<br/>" + "<br/>Your request for a make-up class filed on {1} has been <b>approved</b>.<br/>"
                        + "<br/>Details of your filed make-up class is as follows:<br/>" + "<br/>Instructor: <b>{2}</b><br/>Subject/Section: <b>{3}</b>"
                        + "<br/>Date: <b>{4}</b><br/>Time: <b>{5}</b><br/>" + "<br/><br/>This is a system generated e-mail. Please do not reply.",
                        user.first_name, pcs.dateFiled, pcs.facultyName, pcs.subjectCode + " - " + pcs.section, pcs.assignedDate, pcs.duration);
                    }
                    else if (emailType == 2)
                    {
                        mm.Subject = String.Format("Make-up class disapproval for {0}", pcs.subjectCode);
                        mm.Body = String.Format("Hello {0},<br/>" + "<br/>Your request for a make-up class filed on {1} has been <b>disapproved</b>.<br/>"
                        + "<br/>Details of your filed make-up class is as follows:<br/>" + "<br/>Instructor: <b>{2}</b><br/>Subject/Section: <b>{3}</b>"
                        + "<br/>Date: <b>{4}</b><br/>Time: <b>{5}</b><br/><br/>Remarks: <b>{6}</b><br/>" + "<br/><br/>This is a system generated e-mail. Please do not reply.",
                        user.first_name, pcs.dateFiled, pcs.facultyName, pcs.subjectCode + " - " + pcs.section, pcs.assignedDate, pcs.duration, "REMARKS"); // insert remarks once made
                    }
                
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["emailAuthUser"], ConfigurationManager.AppSettings["emailAuthPassword"]);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;

                    try
                    {
                        smtp.Send(mm);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "unsentEmailMsg()", true);
                    }
                    finally
                    {
                        mm.Dispose();
                    }
                }
            }

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "sentEmailMsg()", true);
        }

        protected void disapproveButton_Click(object sender, EventArgs e)
        {
            //Nothing goes here...
        }

    }
}