using FusionCharts.Charts;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                getBarCharts();
                getColumnCharts();
                getLineCharts();
                getDownloadTable();
            }
           

        }



        protected void getBarCharts()
        {


            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

                string sem = null;
                var settings = entity.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                if (settings.semester.Equals("1"))
                {
                    sem = "1st";
                }
                else if (settings.semester.Equals("2"))
                {
                    sem = "2nd";
                }
                else if (settings.semester.Equals("3"))
                {
                    sem = "3rd";
                }

                StringBuilder xmlData = new StringBuilder();

                xmlData.AppendFormat("<chart caption='Top 5 Faculty Member conducting Make-up Classes' subCaption='{0} Sem {1}-{2}'  placevaluesinside='1' yaxisname='Number of makeups' exportEnabled='1'>",sem,settings.schoolYearStart.ToString(),settings.schoolYearEnd.ToString());


                var query = entity.MakeupClassSchedules.Where(n=>n.semester.Equals(settings.semester) && n.schoolYearStart==settings.schoolYearStart && n.schoolYearEnd==n.schoolYearEnd).GroupBy(n => n.facultyName).Select(n => new { FacultyName = n.Key, FacultyCount = n.Count() });
                var query2 = query.OrderByDescending(n => n.FacultyCount).Take(5);


                //var query = entity.MakeupClassSchedules.GroupBy(n => n.facultyName).Select(n => new { FacultyName = n.Key, FacultyCount = n.Count() });
                //var query2= query.OrderByDescending(n=>n.FacultyCount).Take(5);

                foreach (var data in query2)
                {
                    var random = new Random();
                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    xmlData.AppendFormat("<set label='{0}' value='{1}' color='{2}'/>", data.FacultyName, data.FacultyCount.ToString(),color);
                }


               xmlData.AppendFormat("</chart>");
               Chart factoryOutput = new Chart("bar2d", "myChart", "500", "350", "xml", xmlData.ToString());
               Literal1.Text = factoryOutput.Render();
                

            }

            
        }



        protected void getColumnCharts()
        {

            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

                string sem = null;
                var settings = entity.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                if (settings.semester.Equals("1"))
                {
                    sem = "1st";
                }
                else if (settings.semester.Equals("2"))
                {
                    sem = "2nd";
                }
                else if (settings.semester.Equals("3"))
                {
                    sem = "3rd";
                }

                StringBuilder xmlData = new StringBuilder();
                xmlData.AppendFormat("<chart caption='Top 5 Reason for conducting Make-up Classes' subCaption='{0} Sem {1}-{2}'  placevaluesinside='1' yaxisname='Number of makeups' xaxisname='Reasons' exportEnabled='1' >", sem, settings.schoolYearStart.ToString(), settings.schoolYearEnd.ToString());
                var query = entity.MakeupClassSchedules.Where(n => n.semester.Equals(settings.semester) && n.schoolYearStart == settings.schoolYearStart && n.schoolYearEnd == n.schoolYearEnd).GroupBy(n => n.reason).Select(n => new { ReasonName = n.Key, ReasonCount = n.Count() });
                var query2 = query.OrderByDescending(n => n.ReasonCount).Take(5);
                foreach (var data in query2)
                {
                    var random = new Random();
                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    xmlData.AppendFormat("<set label='{0}' value='{1}' color='{2}'/>", data.ReasonName, data.ReasonCount.ToString(),color);
                }


                xmlData.AppendFormat("</chart>");
                Chart factoryOutput = new Chart("column2d", "ColumnChart", "500", "350", "xml", xmlData.ToString());
                Literal2.Text = factoryOutput.Render();
               

            }
        }


        protected void getLineCharts()
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

                string sem = null;
                var settings = entity.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                if (settings.semester.Equals("1"))
                {
                    sem = "1st";
                }
                else if (settings.semester.Equals("2"))
                {
                    sem = "2nd";
                }
                else if (settings.semester.Equals("3"))
                {
                    sem = "3rd";
                }




                StringBuilder xmlData = new StringBuilder();

                string currentYear = DateTime.Now.ToString("yyyy");



                xmlData.AppendFormat("<chart caption='Make-up classes conducted' subCaption='{0} Sem {1}-{2}' yaxisname='Number of makeups' xaxisname='Months' exportEnabled='1' >", sem, settings.schoolYearStart.ToString(), settings.schoolYearEnd.ToString());


                var query = entity.MakeupClassSchedules.AsEnumerable().Where(n => n.makeupDate.ToString("yyyy").Equals(currentYear)).GroupBy(n => n.makeupDate.ToString("MMM")).Select(n => new { Month = n.Key, MonthCount = n.Count() });
                //var query2 = query.OrderByDescending(n => n.ReasonCount).Take(5);



                var avg = 0;
                foreach (var data in query)
                {
                    avg += data.MonthCount;
                    xmlData.AppendFormat("<set label='{0}' value='{1}'/>", data.Month, data.MonthCount.ToString());
                }

                avg = avg / 12;

                xmlData.AppendFormat("<trendlines>");
                xmlData.AppendFormat("<line startvalue='{0}' color='#1aaf5d' displayvalue='Average Make-up classes' valueonright='1' thickness='2' />",avg);
                xmlData.AppendFormat("</trendlines>");
                xmlData.AppendFormat("</chart>");


                
                Chart factoryOutput = new Chart("line", "LineChart", "500", "350", "xml", xmlData.ToString());
                Literal3.Text = factoryOutput.Render();
               
                
            }
        }

      

       


        protected void getDownloadTable()
        {
            DataTable dt = new DataTable();
            dt = getDownload();
            downloadGrid.DataSource = dt;
            downloadGrid.DataBind();
            downloadPanel.Update();
        }


        public static DataTable getDownload()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("file", typeof(string));
            

            DataRow dr;

            dr = dt.NewRow();
            dr["ID"] = 1;
            dr["file"] = "Number of Make-ups by Faculty in Selected Term and School Year";
                    
             dt.Rows.Add(dr);

             dr = dt.NewRow();
             dr["ID"] = 2;
             dr["file"] = "Make-ups in Selected Year";
             dt.Rows.Add(dr);

            return dt;

        }



        protected void downloadGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "onDownload")
            {
               
                string ID = e.CommandArgument.ToString();
                int x = Int32.Parse(ID);


                if (x == 1)
                {
                    DropDownList ddl = (DropDownList)downloadGrid.Rows[0].FindControl("semDDL");
                    var sem = ddl.SelectedValue;
                    TextBox txt = (TextBox)downloadGrid.Rows[0].FindControl("schoolYearStart");
                    TextBox txt2 = (TextBox)downloadGrid.Rows[0].FindControl("schoolYearEnd");

                    if (txt.Text.Length!=0 && txt2.Text.Length!=0)
                    {
                        var schoolYearStart = Int32.Parse(txt.Text.ToString());
                        var schoolYearEnd = Int32.Parse(txt2.Text.ToString());
                        createPDF(x, sem, schoolYearStart, schoolYearEnd);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "incomplete()", true);
                    }



                    
                }
                else if (x == 2)
                {
                    TextBox txt = (TextBox)downloadGrid.Rows[1].FindControl("schoolYearStart");
                    if (txt.Text.Length != 0)
                    {
                        var year = Int32.Parse(txt.Text.ToString());
                        createPDF(x, "", 0, year);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "incomplete()", true);
                    }

                    
                      
                }

            }
        }





        private void createPDF(int p,string sem,int schoolYearStart,int schoolYearEnd)
        {


            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

               

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    
                    var doc = new Document(PageSize.LETTER, 10f, 10f, 20f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(doc, stream);
                    var path = Server.MapPath("~/Assets/img/logo.jpg");
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(path);
                    img.ScaleToFit(140f, 120f);
                    img.Alignment = Element.ALIGN_CENTER;
                    //img.SetAbsolutePosition(50f, 20f);
                    
                    doc.Open();

                    doc.Add(img);
                    doc.Add(Chunk.NEWLINE);
                    doc.Add(Chunk.NEWLINE);
                    Chunk linebreak=new Chunk(new LineSeparator());
                    doc.Add(linebreak);
                   
                    //add stuff to itext document
                    doc.Add(new Paragraph(" "));

                    if (p == 1)
                    {
                        //PdfPTable table = new PdfPTable(1);
                        //PdfPCell cell = new PdfPCell(new Phrase("List of Faculty with Make-ups"));
                        //cell.Colspan = 1;
                        //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        //table.AddCell(cell);
                        Paragraph title = new Paragraph("List of Faculty Makeup Counter");
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);
                        String semWord=null;
                        if(sem.Equals("1")){
                            semWord="1st Sem";
                        }
                        else if(sem.Equals("2")){
                            semWord="2nd Sem";
                        }
                        else if(sem.Equals("3")){
                            semWord="3rd Sem";
                        }

                        Paragraph details = new Paragraph(semWord+" SY "+schoolYearStart + "-" + schoolYearEnd);
                        details.Alignment = Element.ALIGN_CENTER;
                        doc.Add(details);
                        doc.Add(new Paragraph(" "));
                        
                        var query = entity.MakeupClassSchedules.Where(n => n.semester.Equals(sem) && n.schoolYearStart == schoolYearStart  && n.schoolYearEnd ==schoolYearEnd ).GroupBy(n => n.facultyName).Select(n => new { FacultyName = n.Key, FacultyCount = n.Count() });
                        var query2 = query.OrderByDescending(n => n.FacultyCount).Take(5);

                        foreach (var data in query)
                        {
                            // doc.Add(new Paragraph(data.Month + ":" + data.MonthCount));

                            
                            doc.Add(new Paragraph(data.FacultyName + ":" + data.FacultyCount));


                            //PdfPCell cellOne = new PdfPCell(new Phrase(data.FacultyName+": "+data.FacultyCount));
                            //cellOne.
                            //table.AddCell(cellOne);
                            
                            //cellOne.HorizontalAlignment = 2;
                            //cellTwo.HorizontalAlignment = 2;

                            //table.AddCell(cellOne);
                            //table.AddCell(cellTwo);
                            
                        }


                        //doc.Add(table);





                    }



                    if (p == 2)
                    {

                        //PdfPTable table = new PdfPTable(2);
                        
                        //PdfPCell cell = new PdfPCell(new Phrase("Make-ups in "+schoolYearEnd));
                        
                        //cell.Colspan = 3;
                        //cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        //cell.Border = Rectangle.NO_BORDER;
                        //table.AddCell(cell);
                        Paragraph title = new Paragraph("Make-ups in "+schoolYearEnd);
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);


                        var yearStr = schoolYearEnd.ToString();


                        //doc.Add(new Paragraph("Make-ups in 2017 "));

                        var query = entity.MakeupClassSchedules.AsEnumerable().Where(n => n.makeupDate.ToString("yyyy").Equals(yearStr)).GroupBy(n => n.makeupDate.ToString("MMMM")).Select(n => new { Month = n.Key, MonthCount = n.Count() });

                        foreach (var data in query)
                        {
                           // doc.Add(new Paragraph(data.Month + ":" + data.MonthCount));
                            doc.Add(new Paragraph(data.Month + ":" + data.MonthCount));


                            //PdfPCell cellOne = new PdfPCell(new Phrase(data.Month));
                            //PdfPCell cellTwo = new PdfPCell(new Phrase(data.MonthCount.ToString()));
                            //cellOne.HorizontalAlignment = 2;
                            //cellTwo.HorizontalAlignment = 2;
                            //cellOne.Border = Rectangle.NO_BORDER;
                            //cellTwo.Border = Rectangle.NO_BORDER;
                            //table.AddCell(cellOne);
                            //table.AddCell(cellTwo);
                            
                            //cellOne.Border(Rectangle.NO_BORDER);
                            //cellOne.setBackgroundColor(new Color(255, 255, 45));

                            //cellTwo.setBorder(Rectangle.BOX);

                            //table.AddCell(data.Month);
                            //table.AddCell(data.MonthCount.ToString());
                        }


                        //doc.Add(table);
                    }


                    var chosen = DateTime.Today.Date;
                    string today = DateTime.Now.ToString("MM/dd/yyyy hh:mm");
                    Paragraph gen = new Paragraph("Generated On:" + today);
                    gen.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(gen);
                    doc.Close();
                    
                    
                    
                    
                    
                    byte[] b = stream.ToArray();
                    stream.Close();
                    
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=iScheduleReport.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(b);
                    Response.End();

                   

                }






               


            }


        }

        protected void downloadGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            



            foreach (GridViewRow item in downloadGrid.Rows)
            {
                
                int id =Int32.Parse(downloadGrid.DataKeys[item.RowIndex].Value.ToString());
               
                    if (id == 1)
                    {

                    }
                    else if (id == 2)
                    {



                        DropDownList ddl = (DropDownList)item.FindControl("semDDL");
                        TextBox txt = (TextBox)item.FindControl("schoolYearEnd");
                        Label lbl = (Label)item.FindControl("toLabel");
                        ddl.Visible = false;
                        txt.Visible = false;
                        lbl.Visible = false;
                    }

                    LinkButton lbn = item.FindControl("downloadBtn") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lbn);
              }


            
        }
   
        
    }
}