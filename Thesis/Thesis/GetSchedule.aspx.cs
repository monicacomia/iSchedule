using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Web.UI.HtmlControls;
using iTextSharp.tool.xml;
using iTextSharp.text.pdf.draw;
using System.Data.Objects;
//using iTextSharp.tool.xml;


namespace Thesis
{
    public partial class GetSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                DateTime today = DateTime.Today.Date;
                String day=today.DayOfWeek.ToString();
                dateTextBox.Text = today.ToString("yyyy-MM-dd");
                ViewState["date"] = today;
                ViewState["dayOfWeek"] = day;
                ViewState["room"] = "0";
                getSchedTable(today,day,"0");
                schedPanel.Update();
                var usertype=Int32.Parse(Session["USER_TYPE"].ToString());
                bindDropDown();

                if (usertype == 1 || usertype == 2)
                {
                    pdfButton.Visible = false;
                }


            }


        }

        public void bindDropDown()
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

                var data = (from p in entity.Groups
                            select new
                            {
                                p.grpID,
                                p.grpName
                            });

                foreach (var item in data)
                {
                    ddlFloors.Items.Add(new System.Web.UI.WebControls.ListItem(item.grpName, item.grpID.ToString()));
                }

            }
        }

        public void getSchedTable(DateTime chosen, String day,String room)
        {

            DataTable dt = new DataTable();
            dt = getSched(chosen,day,room);
            schedGrid.DataSource = dt;
            schedGrid.DataBind();

        }

      

        
        

        public static DataTable getSched(DateTime chosen, String day,String room)
        {
            //int sys = Int32.Parse(schoolYearStart);
            //int sye = Int32.Parse(schoolYearEnd);
            DataTable dt = new DataTable();

            
            if (day.Equals("Sunday"))
            {
                return dt;
            }

            dt.Columns.Add("schedID", typeof(int));
            dt.Columns.Add("roomNumber", typeof(string));
            //dt.Columns.Add("days", typeof(string));
            dt.Columns.Add("startTime", typeof(string));
            dt.Columns.Add("time", typeof(string));
            dt.Columns.Add("subjectCode", typeof(string));
            dt.Columns.Add("section", typeof(string));
            dt.Columns.Add("instructor", typeof(string));
            dt.Columns.Add("numOfHours", typeof(string));

            dt.Columns.Add("start", typeof(string));
            dt.Columns.Add("middle", typeof(string));
            dt.Columns.Add("end", typeof(string));
            dt.Columns.Add("remarks", typeof(string));



            DataRow dr;
            String query = queryDay(day);
            

            using (ThesisDBEntities data = new ThesisDBEntities())
            {


                //var combi = (from p in data.SemesterSchedules.Where(query, chosen.Date, "true") where !data.CancelledClassSchedules.Any(x => x.schedID == p.schedID) select new { p.schedID, p.subjectCode }).Union((from m in data.MakeupClassSchedules select new { m.makeUpClassID, m.subjectCode }));

                var combi = (from p in data.SemesterSchedules.Where(query, chosen.Date, "1")
                             where !data.CancelledClassSchedules.Any(x => x.schedID == p.schedID && EntityFunctions.TruncateTime(x.cancelledDate)==EntityFunctions.TruncateTime(chosen.Date))
                             select new { p.schedID, p.subjectCode, p.roomNumber,p.time,p.section,p.instructor,p.numofHours,remarks=""}
                             );
                var combiMakeup = combi.Union(from m in data.MakeupClassSchedules.Where(x => EntityFunctions.TruncateTime(x.makeupDate) == EntityFunctions.TruncateTime(chosen.Date)) select new { schedID = m.makeupClassID, m.subjectCode, roomNumber = m.room, time = m.time, m.section, instructor = m.facultyName, numofHours = m.numHours, remarks="Make-Up" });

                var combiMakeupOrdered = (from p in combiMakeup
                                          orderby p.roomNumber, p.time ascending
                                          select new
                                          {
                                              p.schedID,
                                              p.subjectCode,
                                              p.roomNumber,
                                              p.time,
                                              p.section,
                                              p.instructor,
                                              p.numofHours,
                                              p.remarks
                                          });


                List<String> listOfRooms=new List<string>();
                listOfRooms=getRooms(room);

                //var filteredRooms = combiMakeup.Where(t => listOfRooms.Contains(t.roomNumber)).ToList();

                                 
                foreach (var entityRow in combiMakeupOrdered)
                {
                    if(room.Equals("0")){

                        dr = dt.NewRow();
                        dr["schedID"] = entityRow.schedID;
                        dr["roomNumber"] = entityRow.roomNumber;
                        //dr["days"] = entityRow.days;
                        //DateTime time = DateTime.Today.Add(entityRow.startTime.Value);
                        //DateTime time2 = DateTime.Today.Add(entityRow.endTime.Value);

                        dr["time"] = entityRow.time;
                        // dr["endTime"] = time2.ToString("hh:mm tt");


                        dr["section"] = entityRow.section;
                        dr["subjectCode"] = entityRow.subjectCode;
                        dr["instructor"] = entityRow.instructor;
                        dr["numOfHours"] = entityRow.numofHours;
                        dr["start"] = "";
                        dr["middle"] = "";
                        dr["end"] = "";
                        dr["remarks"] = entityRow.remarks;
                        //create a column for remarks

                        dt.Rows.Add(dr);


                    }
                    else{

                        foreach (var item in listOfRooms)
                        {
                            if (item.Equals(entityRow.roomNumber))
                            {
                                dr = dt.NewRow();
                                dr["schedID"] = entityRow.schedID;
                                dr["roomNumber"] = entityRow.roomNumber;
                                //dr["days"] = entityRow.days;
                                //DateTime time = DateTime.Today.Add(entityRow.startTime.Value);
                                //DateTime time2 = DateTime.Today.Add(entityRow.endTime.Value);

                                dr["time"] = entityRow.time;
                                // dr["endTime"] = time2.ToString("hh:mm tt");


                                dr["section"] = entityRow.section;
                                dr["subjectCode"] = entityRow.subjectCode;
                                dr["instructor"] = entityRow.instructor;
                                dr["numOfHours"] = entityRow.numofHours;
                                dr["start"] = "";
                                dr["middle"] = "";
                                dr["end"] = "";
                                dr["remarks"] = entityRow.remarks;
                                //create a column for remarks

                                dt.Rows.Add(dr);
                            }
                    }

                    }
                    
                    
                   
                }

            }

            return dt;


        }

        public static List<String> getRooms(String value){

            List<String> grp = new List<string>();
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                int val=Int32.Parse(value);

                var data=entity.RoomLists.Where(x=>x.grp==val).Select(x=>x.room);

                foreach (var item in data)
                {
                    grp.Add(item);
                }
                //if (value.Equals("1"))
                //{
                //    grp=
                //}
                //else if (value.Equals("2"))
                //{
                //    grp = new List<string>() { "407", "501", "502", "503", "504", "505", "506", "601", "602" };
                //}
                //else if (value.Equals("3"))
                //{
                //    grp = new List<string>() { "CINTIQ", "706", "CL1", "CL2", "MMA1", "MMA2", "MMA3", "iMAC" };
                //}
                //else if (value.Equals("4"))
                //{
                //    grp = new List<string>() { "AUDITORIUM", "FM", "TZ-A", "TZ-B" };
                //}
            

            }
           
            
            

            return grp;
        }


        public static String queryDay(String day)
        {
            String query = null;
            switch(day){
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

      

        protected void searchSchedButton_Click(object sender, EventArgs e)
        {
             var chosen=DateTime.Parse(dateTextBox.Text);
            String day = chosen.DayOfWeek.ToString();
            var room = ddlFloors.SelectedValue;
            ViewState["date"]=chosen;
            ViewState["dayOfWeek"] = day;
            ViewState["room"] =ddlFloors.SelectedValue;

            getSchedTable(chosen.Date, day,room);
            schedPanel.Update();
            //pdfButton.Visible = true;
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showVisible()", true);
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

        protected void pdfButton_Click(object sender, EventArgs e)
        {

            string sem = null;
            String start=null;
            String end=null;
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

                
                var settings = entity.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                if (settings.semester.Equals("1"))
                {
                    sem = "1st Sem";
                }
                else if (settings.semester.Equals("2"))
                {
                    sem = "2nd Sem";
                }
                else if (settings.semester.Equals("3"))
                {
                    sem = "3rd Sem";
                }
                start=settings.schoolYearStart.ToString();
                end=settings.schoolYearEnd.ToString();
            }



            var chosen = DateTime.Today.Date;
            String day = chosen.DayOfWeek.ToString();
            string today=DateTime.Now.ToString("MM/dd/yyyy hh:mm");
            String output = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    schedGrid.RenderControl(hw);
                    
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.LEGAL, 5f, 5f, 20f, 5f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);


                    var path = Server.MapPath("~/Assets/img/logo.jpg");
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(path);
                    img.ScaleToFit(140f, 120f);
                    img.Alignment = Element.ALIGN_CENTER;
                    


                    pdfDoc.Open();
                    pdfDoc.Add(img);
                    Chunk linebreak = new Chunk(new LineSeparator());
                    pdfDoc.Add(linebreak);
                    pdfDoc.Add(new Paragraph(" "));
                    Paragraph title=new Paragraph(sem+" SY "+start+"-"+end);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);

                    Chunk glue = new Chunk(new VerticalPositionMark());
                    Paragraph p = new Paragraph("Date: " + output);
                    p.Add(new Chunk(glue));
                    p.Add(ddlFloors.SelectedItem.Text);
                    pdfDoc.Add(p);
                    //pdfDoc.Add(new Paragraph("Date: "+ output));
                    pdfDoc.Add(new Paragraph(" "));
                    


                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    
                    pdfDoc.Add(new Paragraph("Generated on:"+ today));
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Schedule.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
            	


           


        
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void schedGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            schedGrid.PageIndex = e.NewPageIndex;
            var date=DateTime.Parse(ViewState["date"].ToString()); 
            var day=ViewState["dayOfWeek"].ToString();
            var room = ViewState["room"].ToString();
            getSchedTable(date,day,room);
        }
    }
}