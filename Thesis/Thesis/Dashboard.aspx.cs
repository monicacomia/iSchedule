using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getData();
                getMakeUpTable();
                getAnnouncementTable();
                getHolidayTable();
                getPendingTable();
            }





        }




        public void getData()
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                var settings=entity.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                var id=Session["FACULTY_ID"].ToString();
                var data = entity.Users.Where(x=>x.faculty_id.Equals(id)).FirstOrDefault();
                name.Text = data.first_name;
                var makeup = entity.MakeupClassSchedules.Where(x => x.faculty_id.Equals(id)&&x.semester.Equals(settings.semester)).Count();
                mkpCount.Text = makeup.ToString()+" Make-up Counter";
                var pending = entity.PendingClassSchedules.Where(x => x.faculty_id.Equals(id)).Count();
                pendingCount.Text = pending.ToString() + " Pending Approval";
                

                //msgCount.Text = data.ToString()+" New Message";
            }
        }


        public void getMakeUpTable()
        {
            DataTable dt = new DataTable();
            dt = getMakeUps();
            makeupGrid.DataSource = dt;
            makeupGrid.DataBind();
            //makeUpPanel.Update();
          
        }

        public DataTable getMakeUps()
        {

            var id = Session["FACULTY_ID"].ToString();   
            DataTable dt = new DataTable();
            dt.Columns.Add("makeUpID", typeof(int));
            dt.Columns.Add("absentDate", typeof(string));
            dt.Columns.Add("subjectCode", typeof(string));
            dt.Columns.Add("section", typeof(string));
            dt.Columns.Add("room", typeof(string));
            dt.Columns.Add("time", typeof(string));
            dt.Columns.Add("makeUpDate", typeof(string));
            dt.Columns.Add("status", typeof(string));
            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = data.MakeupClassSchedules.ToList().Where(t=>t.faculty_id.Equals(id) && DateTime.Parse(t.makeupDate.ToString("d")) >= DateTime.Parse(DateTime.Now.ToString("d"))).Select(t=>new{
                                  t.makeupClassID,
                                  t.subjectCode,
                                  t.section,
                                  t.room,
                                  t.time,
                                  t.makeupDate,
                                  t.absentDate
                             });   
                              

                //data.MakeupClassSchedules.ToList().Where(t=>t.faculty_id.Equals(id) && t.makeupDate.ToString()

                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["makeUpID"] = entityRow.makeupClassID;
                    dr["absentDate"] = entityRow.absentDate.ToString("MM/dd/yyyy");
                    dr["subjectCode"] = entityRow.subjectCode;
                    dr["section"] = entityRow.section;
                    dr["room"] = entityRow.room;
                    dr["time"] = entityRow.time;
                    dr["makeUpDate"] = entityRow.makeupDate.ToString("MMM dd");
                    dr["status"] = "Approved";
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public void getHolidayTable()
        {
            DataTable dt = new DataTable();
            dt = getHoliday();
            holidayGrid.DataSource = dt;
            holidayGrid.DataBind();
            //announcementPanel.Update();
        }

        public static DataTable getHoliday()
        {
           
            DataTable dt = new DataTable();
            dt.Columns.Add("holiday", typeof(string));

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
               var now=DateTime.Now.ToString("MMM");
               var entity = data.Holidays.Where(t => t.date.Contains(now));
                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                   
                    dr["holiday"] = WebUtility.HtmlDecode("<b>"+entityRow.date+" : "+entityRow.description+"</b>");
                    dt.Rows.Add(dr);
                }

            }

            return dt;
        }
        public void getAnnouncementTable()
        {

            DataTable dt = new DataTable();
            dt = getAnnouncements();
            announcementGrid.DataSource = dt;
            announcementGrid.DataBind();
            announcementPanel.Update();
            
        }



        public static DataTable getAnnouncements()
        {
            var now=DateTime.Now;
            DataTable dt = new DataTable();
            //dt.Columns.Add("announcementID", typeof(int));
            dt.Columns.Add("announcementMsg", typeof(string));

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {

               

                //var query = data.Holidays.ToList().Select(x => new
                //{
                //    x.date,
                //    description=x.description,
                //    when=x.date
                //});

                //var currentDate = DateTime.Now;
                //var year = DateTime.Now.ToString("yyyy");
                //var thirtyDaysFromNow=DateTime.Now.AddDays(30);               
                //var holidays=query.Where(x=>DateTime.Now<x.date.AddYears(currentDate.Year-x.date.Year) && x.date.AddYears(currentDate.Year-x.date.Year)<=thirtyDaysFromNow && x.date.ToString("yyyy").Equals(year));

                //foreach (var item in holidays)
                //{
                //    dr = dt.NewRow();
                //    dr["announcementMsg"] =WebUtility.HtmlDecode("<b>Upcoming Holiday: "+item.description+" ("+item.date.ToString("MMMM dd")+")</b>");
                //    dt.Rows.Add(dr);
                    
                //}



                var entity = (from t in data.Announcements
                              where ((t.dateCreated<now && now<t.expiryDate) && t.status==1)
                              orderby t.dateCreated
                              select new
                              {
                                
                                 t.announcementMsg,
                                 t.dateCreated
                                  
                              });
                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    var timestamp = entityRow.dateCreated.ToString("MM/dd/yyyy hh:mm");
                    //dr["announcementID"] = entityRow.announcementID;
                    dr["announcementMsg"] =WebUtility.HtmlDecode(entityRow.announcementMsg)+" "+"("+timestamp+")";
                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }


        

        protected void makeupGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void pdfButton_Click(object sender, EventArgs e)
        {
            
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {



                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    var id = Session["FACULTY_ID"].ToString();   
                    string sem = null;
                    String start = null;
                    String end = null;


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
                    start = settings.schoolYearStart.ToString();
                    end = settings.schoolYearEnd.ToString();


                    string today = DateTime.Now.ToString("MM/dd/yyyy hh:mm");
                    var pdfDoc = new Document(PageSize.LETTER, 10f, 10f, 20f, 0f);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    var path = Server.MapPath("~/Assets/img/logo.jpg");
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(path);
                    img.ScaleToFit(140f, 120f);
                    img.Alignment = Element.ALIGN_CENTER;

                    pdfDoc.Open();
                    pdfDoc.Add(img);
                    Chunk linebreak = new Chunk(new LineSeparator());
                    pdfDoc.Add(linebreak);


                    Paragraph title = new Paragraph("List of Make-up Class Conducted");
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);

                    Paragraph details = new Paragraph(sem+ " SY " + start + "-" + end);
                    details.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(details);
                    pdfDoc.Add(new Paragraph(" "));


                    var data = (from t in entity.MakeupClassSchedules
                                where (t.faculty_id.Equals(id) && t.semester.Equals(settings.semester) && t.schoolYearStart == settings.schoolYearStart && t.schoolYearEnd == settings.schoolYearEnd)

                                select new
                                {
                                    t.makeupDate,
                                    t.subjectCode,
                                    t.section,
                                    t.room,
                                    t.time,
                                    t.absentDate

                                });

                    PdfPTable table = new PdfPTable(6);
                    PdfPCell cell0 = new PdfPCell(new Phrase("Make-up Date"));
                    PdfPCell cell1 = new PdfPCell(new Phrase("Subject Code"));
                    PdfPCell cell2 = new PdfPCell(new Phrase("Section"));
                    PdfPCell cell3 = new PdfPCell(new Phrase("Room Number"));
                    PdfPCell cell4 = new PdfPCell(new Phrase("Time"));
                    PdfPCell cell5 = new PdfPCell(new Phrase("Date of Absence"));

                    cell0.HorizontalAlignment = 1;
                    cell1.HorizontalAlignment = 1; //centered==1
                    cell2.HorizontalAlignment = 1;
                    cell3.HorizontalAlignment = 1;
                    cell4.HorizontalAlignment = 1;
                    cell5.HorizontalAlignment = 1;

                    table.AddCell(cell0);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                    table.AddCell(cell4);
                    table.AddCell(cell5);

                    foreach (var item in data)
                    {
                        PdfPCell date = new PdfPCell(new Phrase(item.makeupDate.ToString("MMM dd")));
                        PdfPCell cellSubjectCode = new PdfPCell(new Phrase(item.subjectCode));
                        PdfPCell cellSection = new PdfPCell(new Phrase(item.section));
                        PdfPCell cellRoomNumber = new PdfPCell(new Phrase(item.room));
                        PdfPCell cellTime = new PdfPCell(new Phrase(item.time));
                        PdfPCell absent = new PdfPCell(new Phrase(item.absentDate.ToString("MM/dd/yyyy")));

                        date.HorizontalAlignment = 1;
                        cellSubjectCode.HorizontalAlignment = 1;
                        cellSection.HorizontalAlignment = 1;
                        cellRoomNumber.HorizontalAlignment = 1;
                        cellTime.HorizontalAlignment = 1;
                        absent.HorizontalAlignment=1;

                        table.AddCell(date);
                        table.AddCell(cellSubjectCode);
                        table.AddCell(cellSection);
                        table.AddCell(cellRoomNumber);
                        table.AddCell(cellTime);
                        table.AddCell(absent);

                    }


                    pdfDoc.Add(table);

                    pdfDoc.Add(new Paragraph("Generated on:" + today));
                    pdfDoc.Close();


                    byte[] b = stream.ToArray();
                    stream.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Makeup.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(b);
                    Response.End();
                }
            }
        }

        protected void announcementGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            announcementGrid.PageIndex = e.NewPageIndex;
            getAnnouncementTable();
        }




        public void getPendingTable()
        {
            DataTable dt = new DataTable();
            dt = getPending();
            pendingGrid.DataSource = dt;
            pendingGrid.DataBind();
            //makeUpPanel.Update();

        }


        public DataTable getPending()
        {

            var id = Session["FACULTY_ID"].ToString();
            DataTable dt = new DataTable();
            dt.Columns.Add("pendingClassID", typeof(int));
            dt.Columns.Add("absentDate", typeof(string));
            dt.Columns.Add("subjectCode", typeof(string));
            dt.Columns.Add("section", typeof(string));
            dt.Columns.Add("room", typeof(string));
            dt.Columns.Add("time", typeof(string));
            dt.Columns.Add("assignedDate", typeof(string));
            dt.Columns.Add("status", typeof(string));
            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.PendingClassSchedules
                              where t.faculty_id.Equals(id)//t.assignedDate >= DateTime.Now)

                              select new
                              {
                                  t.pendingClassID,
                                  t.subjectCode,
                                  t.section,
                                  t.room,
                                  t.duration,
                                  t.assignedDate,
                                  t.absentDate

                              });
                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["pendingClassID"] = entityRow.pendingClassID;
                    dr["absentDate"] = entityRow.absentDate.ToString("MM/dd/yyyy");
                    dr["subjectCode"] = entityRow.subjectCode;
                    dr["section"] = entityRow.section;
                    dr["room"] = entityRow.room;
                    dr["time"] = entityRow.duration;
                    dr["assignedDate"] = entityRow.assignedDate.ToString("MMM dd") ;
                    dr["status"] = "Pending Approval";
                    dt.Rows.Add(dr);
                }

            }

            return dt;
        }

        protected void pendingGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pendingGrid.PageIndex = e.NewPageIndex;
        }

        protected void makeupClassBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MakeupForm.aspx");
        }



    }
}