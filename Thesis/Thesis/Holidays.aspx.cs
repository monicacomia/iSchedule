using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class Holidays : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getHolidayTable();
            }
        }




        public void getHolidayTable()
        {

            DataTable dt = new DataTable();
            dt = getHolidays();
            holidayGrid.DataSource = dt;
            holidayGrid.DataBind();
            holidayPanel.Update();
            
        }

        public static DataTable getHolidays()
        {

            

            DataTable dt = new DataTable();
            dt.Columns.Add("holidayID", typeof(int));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("date", typeof(string));

            var date = DateTime.Parse("May 01 2017");
            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                //var entity = (from t in data.Holidays
                              
                //              select new
                //              {
                //                  t.holidayID,
                //                  t.description,
                //                  t.date
                                  
                //              });
                var yr=DateTime.Now.Year.ToString();
                var entity = data.Holidays.ToList().Select(x=>new{
                                date=DateTime.Parse(x.date+" ,"+yr),
                                description=x.description,
                                holidayID=x.holidayID,
                                dateName=x.date
                             }).OrderBy(x=>x.date);

                                       

                foreach (var entityRow in entity)
                {

                    //DateTime holiDate = (DateTime)entityRow.date;
                    

                    dr = dt.NewRow();
                    dr["holidayID"] = entityRow.holidayID;
                    dr["description"] = entityRow.description;
                    dr["date"] = entityRow.dateName;

                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            var txt = holidayTxt.Text;


           

            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                DateTime value=new DateTime();
                var search = entity.Holidays.ToList().Where(x => x.date.Equals(dateTxt.Text)).FirstOrDefault();
                if(holidayTxt.Text.Trim().Length==0 || dateTxt.Text.Trim().Length==0){
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showError()", true);
                }

                else if (!DateTime.TryParse(dateTxt.Text,out value))
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showError()", true);
                }

                else if (search != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showError()", true);
                }
                else
                {
                    Holiday holiday = new Holiday();
                    holiday.description = holidayTxt.Text;
                    DateTime d = new DateTime();
                    //if(DateTime.TryParseExact(dateTxt.Text,"dd-MM-yyyy", new CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out d))
                    //{
                    //    holiday.date=d;
                    //}
                    //if (DateTime.TryParse(dateTxt.Text, out d))
                    //{
                    //    holiday.date=d;
                    //}
                    holiday.date = dateTxt.Text;
                    entity.Holidays.Add(holiday);
                    entity.SaveChanges();

                    holidayTxt.Text = String.Empty;
                    dateTxt.Text = String.Empty;
                    getHolidayTable();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showSuccess()", true);
                }


                
            }
            


        }

        protected void holidayGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "onDelete")
            {
                var ID = Int32.Parse(e.CommandArgument.ToString());

                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    var entity = data.Holidays.Where(x => x.holidayID == ID).FirstOrDefault();
                    
                    data.Holidays.Remove(entity);
                    data.SaveChanges();
                }

                getHolidayTable();

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showInvalidMsg()", true);

            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Uploader.HasFile && Path.GetExtension(Uploader.FileName) == ".xlsx")
            {
                using (var excel = new ExcelPackage(Uploader.PostedFile.InputStream))
                {

                    var ws = excel.Workbook.Worksheets.First();
                    int startRow = 1;
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
                                   
                                    Holiday data = new Holiday();
                                    data.description = ws.Cells[i, 2].Text;
                                    data.date = ws.Cells[i, 1].Text;
                                    entity.Holidays.Add(data);
                                }

                                entity.SaveChanges();
                            }

                            transactionScope.Complete();


                        }
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showSuccess()", true);
                        getHolidayTable();
                        //Response.Redirect("UserManagement.aspx");


                    }
                    catch (NullReferenceException nre)
                    {
                        
                    }
                    catch (TransactionAbortedException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }
            }
            else
            {

               
            }
        }

       
    }
}