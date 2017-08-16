using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class Announcements : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getAnnouncementTable();
            }
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
            var now = DateTime.Now;
            DataTable dt = new DataTable();
            dt.Columns.Add("announcementID", typeof(int));
            dt.Columns.Add("announcementMsg", typeof(string));
            dt.Columns.Add("expiryDate", typeof(string));

            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.Announcements
                              where ((t.dateCreated < now && now < t.expiryDate) && t.status == 1)
                              orderby t.dateCreated
                              select new
                              {
                                  t.announcementID,
                                  t.announcementMsg,
                                  t.expiryDate,
                                  t.dateCreated,

                              });
                foreach (var entityRow in entity)
                {

                    dr = dt.NewRow();
                    var timestamp=entityRow.dateCreated.ToString("MM/dd/yyyy hh:mm");
                    dr["announcementID"] = entityRow.announcementID;
                    dr["announcementMsg"] = WebUtility.HtmlDecode(entityRow.announcementMsg);
                    string date = entityRow.expiryDate.ToString();
                    string date2 = DateTime.Parse(date).ToString("MM/dd/yyyy hh:mm");
                    dr["expiryDate"] = date2;

                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }



        protected void submitBtn_Click(object sender, EventArgs e)
        {
            DateTime expDate = new DateTime();
            if (DateTime.TryParse(time.Text, out expDate))
            {
                if (expDate < DateTime.Now)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showInvalidMsg()", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "getContent()", true);
                    var msg = txtSummernote.Text;
                    using (ThesisDBEntities data = new ThesisDBEntities())
                    {
                        Announcement entity = new Announcement();
                        entity.announcementMsg = WebUtility.HtmlEncode(msg);
                        entity.createdBy = 1;
                        entity.dateCreated = DateTime.Now;
                        entity.status = 1;
                        entity.expiryDate = DateTime.Parse(time.Text);
                        data.Announcements.Add(entity);
                        data.SaveChanges();
                    }

                    getAnnouncementTable();
                    announcementPanel.Update();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showSuccessMsg()", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "input()", true);
            }
           
           
            
        }

        protected void announcementGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
             


             if (e.CommandName == "onStop")
             {
                 string ID = e.CommandArgument.ToString();
                 int x = Int32.Parse(ID);
                 using (ThesisDBEntities entity = new ThesisDBEntities())
                 {
                     var data = entity.Announcements.Where(p => p.announcementID == x).FirstOrDefault();
                     data.status = 2;
                     entity.SaveChanges();

                 }



             }

             getAnnouncementTable();
             announcementPanel.Update();




            
        }

        protected void announcementGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            announcementGrid.PageIndex = e.NewPageIndex;
            getAnnouncementTable();
        }

       
    }
}