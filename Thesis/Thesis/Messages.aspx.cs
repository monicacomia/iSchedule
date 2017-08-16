using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class Messages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getInboxTable();
                Visibility();
                GetColor();
            }

            
        }


        protected void GetColor()
        {

            foreach (GridViewRow row in inboxGrid.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    
                    if (Int32.Parse(inboxGrid.DataKeys[row.RowIndex]["isRead"].ToString()) == 0)
                    {
                        

                        var newColor = Color.FromArgb(1, 79, 179);
                        row.ForeColor = Color.White;
                        row.BackColor = newColor;

                    }
                    else
                    {
                        row.ForeColor = Color.Black;
                        row.BackColor = Color.Gainsboro;
                    }
                    

                }
            }

        }


        protected void Visibility()
        {
            var visibilityFlag = false;
            foreach (GridViewRow row in inboxGrid.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked == true)
                    {
                        visibilityFlag = true;
                        

                    }
             
                }
            }

            if (visibilityFlag == true)
            {
                readBtn.Visible = true;
                deleteBtn.Visible = true;
            }

            else
            {
                readBtn.Visible = false;
                deleteBtn.Visible = false;
            }

            visibilityPanel.Update();
        }


        public void getInboxTable()
        {

            DataTable dt = new DataTable();
            dt = getInbox();
            inboxGrid.DataSource = dt;
            inboxGrid.DataBind();



            //GetColor();
            //Visibility();
            inboxPanel.Update();
        }


        public static DataTable getInbox()
        {
           
            DataTable dt = new DataTable();
            dt.Columns.Add("msgID", typeof(int));
            dt.Columns.Add("hash", typeof(string));
            dt.Columns.Add("sender", typeof(string));
            dt.Columns.Add("subjectAndMsg", typeof(string));
            dt.Columns.Add("date", typeof(string));
            dt.Columns.Add("isRead", typeof(int));
            dt.Columns.Add("isDeleted", typeof(int));
            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.Messagings
                              where (t.receiver==2 && t.isInbox==1 && t.isDeleted==0)
                              orderby t.sent_datetime
                              group t by t.sender_receiver_datetime into newGrp  
                              select new
                              {
                                   Messaging=newGrp.Key,
                                   msgID=newGrp.Select(i=>i.msgID),
                                   sender=newGrp.Select(i=>i.sender),
                                   subj = newGrp.Select(i => i.subject),
                                   msg = newGrp.Select(i => i.sender_message),
                                   date = newGrp.Select(i => i.sent_datetime),
                                   isRead = newGrp.Select(i => i.isRead),
                                   isDeleted = newGrp.Select(i => i.isDeleted)
                                   
                                  //t.sender_receiver_datetime,
                                  //t.sender,
                                  //t.subject,
                                  //t.sender_message,
                                  //t.sent_datetime,
                                  //t.isRead,
                                  //t.isDeleted
                              });

                //var query = data.Messagings.Where(n => n.receiver == 2 && n.isInbox == 1 && n.isDeleted == 0).GroupBy(n => n.sender_receiver_datetime).Select(n => new { Messaging=n.Key, msgID=n.Select(i => i.msgID), sender =n.Select(i => i.sender), subj = n.Select(i => i.subject), msg = n.Select(i => i.sender_message), date = n.Select(i => i.sent_datetime), isRead = n.Select(i => i.isRead), isDeleted = n.Select(i => i.isDeleted) });
                //var query2 = query.OrderByDescending(n => n.date);


                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["msgID"] = entityRow.msgID.LastOrDefault();
                    dr["hash"] = entityRow.Messaging;
                    dr["sender"] = entityRow.sender.LastOrDefault();
                    dr["subjectAndMsg"] = entityRow.subj.LastOrDefault()+"-"+entityRow.msg.LastOrDefault();
                    string date = entityRow.date.LastOrDefault().ToString();
                    string date2 = DateTime.Parse(date).ToString("MMM dd yyyy");


                    dr["date"] = date2;
                    dr["isRead"] = entityRow.isRead.LastOrDefault();
                    dr["isDeleted"] = entityRow.isDeleted.LastOrDefault();
                    


                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }


        public void getMessagesTable(string hash)
        {

            DataTable dt = new DataTable();
            dt = getMessages(hash);
            replyGrid.DataSource = dt;
            replyGrid.DataBind();

            
        }


        public static DataTable getMessages(string hash)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("msgID", typeof(int));
            dt.Columns.Add("hash", typeof(string));
            dt.Columns.Add("msg", typeof(string));
            dt.Columns.Add("datetime", typeof(string));
            
            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.Messagings
                              where (t.sender_receiver_datetime.Equals(hash))
                              orderby t.sent_datetime ascending
                              select new
                              {
                                  t.msgID,
                                  t.sender_receiver_datetime,
                                  t.sender,
                                  t.receiver,
                                  t.sender_message,
                                  t.sent_datetime,
                                 
                              });



                foreach (var entityRow in entity)
                {
                    dr = dt.NewRow();
                    dr["msgID"] = entityRow.msgID;
                    dr["hash"] = hash;


                    if (entityRow.receiver==2 || entityRow.sender==2) //if the inbox is you
                    {
                        dr["msg"] = entityRow.sender+":"+entityRow.sender_message;
                          
                    }

                    
                    string date = entityRow.sent_datetime.ToString();
                    string date2 = DateTime.Parse(date).ToString("mm/dd/yyyy hh:mm");

                    dr["datetime"] = date2;

                    
                  



                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }



        protected void composeBtn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#composeModal').modal('show');", true);
        }

        protected void sendButton_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                Messaging entity = new Messaging();
                entity.author=1;
                entity.receiver=2;
                entity.subject=subjectTxt.Text;
                entity.sender_receiver_datetime = 1 + "_" + 2 + "_" + DateTime.Now.ToString("mmddyyyyhhmm");
                entity.sender = 1;
                entity.sender_message = msgTxt.Text;
                entity.author_sentdatetime = DateTime.Now;
                entity.sent_datetime = DateTime.Now;
                entity.isRead = 0;
                entity.isInbox = 1;
                entity.isInTrash = 0;
                entity.isDeleted = 0;
                entity.isInArchive = 0;

                data.Messagings.Add(entity);
                data.SaveChanges();
            }
        }

        protected void selectDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl = sender as DropDownList;
            int flag = 0;
            if(ddl.SelectedValue.Equals("All")){
                flag = 1;
            }
            else if (ddl.SelectedValue.Equals("None"))
            {
                flag = 2;
            }
            else if (ddl.SelectedValue.Equals("Read"))
            {
                flag = 3;
            }
            else if (ddl.SelectedValue.Equals("Unread"))
            {
                flag = 4;
            }

            foreach(GridViewRow row in inboxGrid.Rows){

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (flag == 1)
                    {
                        chkRow.Checked = true;
                        
                        
                    }
                    else if (flag==2)
                    {
                        chkRow.Checked = false;
                       
                        
                        
                    }
                    else if (flag==3)
                    {
                        if (Int32.Parse(inboxGrid.DataKeys[row.RowIndex]["isRead"].ToString()) == 1)
                        {
                            chkRow.Checked = true;
                            
                            
                        }
                        if (Int32.Parse(inboxGrid.DataKeys[row.RowIndex]["isRead"].ToString()) == 0)
                        {
                            chkRow.Checked = false;
                           

                        }

                        

                    }
                    else if (flag==4)
                    {
                        if (Int32.Parse(inboxGrid.DataKeys[row.RowIndex]["isRead"].ToString()) == 0)
                        {
                            chkRow.Checked = true;
                           
                            
                        }
                        if (Int32.Parse(inboxGrid.DataKeys[row.RowIndex]["isRead"].ToString()) == 1)
                        {
                            chkRow.Checked = false;
                            
                            
                        }
                    }
                }
            }

            GetColor();
            Visibility();
            //visibilityPanel.Update();
        }

        protected void readBtn_Click(object sender, EventArgs e)
        {

            using(ThesisDBEntities entity=new ThesisDBEntities()){

                foreach (GridViewRow row in inboxGrid.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        if (chkRow.Checked == true)
                        {
                            if (Int32.Parse(inboxGrid.DataKeys[row.RowIndex]["isRead"].ToString()) == 0)
                            {
                                var hash = inboxGrid.DataKeys[row.RowIndex]["hash"].ToString();
                                (from p in entity.Messagings where p.sender_receiver_datetime.Equals(hash) select p).ToList().ForEach(x => x.isRead = 1);
                                entity.SaveChanges();


                            }
                        }
                        

                    }
                }



            }


            getInboxTable();
            GetColor();
            Visibility();




        }

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

                foreach (GridViewRow row in inboxGrid.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        if (chkRow.Checked == true)
                        {
                            if (Int32.Parse(inboxGrid.DataKeys[row.RowIndex]["isDeleted"].ToString()) == 0)
                            {
                                var hash = inboxGrid.DataKeys[row.RowIndex]["hash"].ToString();
                                (from p in entity.Messagings where p.sender_receiver_datetime.Equals(hash) select p).ToList().ForEach(x => x.isDeleted = 1);
                                entity.SaveChanges();


                            }
                        }


                    }
                }



            }


            getInboxTable();
            GetColor();
            Visibility();
        }

        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {
            
            //var visibilityFlag = false;
            //foreach (GridViewRow row in inboxGrid.Rows)
            //{

            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
            //        if (chkRow.Checked == true )
            //        {
            //           //visibilityFlag = true;
            //            row.BackColor = System.Drawing.Color.Gainsboro;
                        
            //        }
            //        else
            //        {
            //            row.BackColor = System.Drawing.Color.White;
                        
            //    }

                
            //}
            GetColor();
            Visibility();
            

        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            getInboxTable();
            GetColor();
            Visibility();
        }

        protected void viewBtn_Click(object sender, EventArgs e)
        {

        }

        protected void inboxGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "onView")
            {
                string ID = e.CommandArgument.ToString();
                ViewState["hash"] = ID;

                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    var entity = data.Messagings.Where(p => p.sender_receiver_datetime.Equals(ID)).OrderByDescending(t=>t.sent_datetime).FirstOrDefault();
                    entity.isRead = 1;
                    data.SaveChanges();
                    headerlbl.Text = entity.subject;
                    
                }

                GetColor();
                Visibility();
                getInboxTable();
                getMessagesTable(ID);

                

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#replyModal').modal('show');", true);
                //



            }
        }

        protected void replyBtn_Click(object sender, EventArgs e)
        {
            var hash = ViewState["hash"].ToString();


            using (ThesisDBEntities entity = new ThesisDBEntities())
            {


                var info = entity.Messagings.Where(p => p.sender_receiver_datetime.Equals(hash)).OrderBy(t=>t.sent_datetime).FirstOrDefault();

                Messaging data = new Messaging();
                data.author = info.author;
                data.subject = info.subject;
                data.author_sentdatetime = info.author_sentdatetime;
                data.isDeleted = 0;
                data.isInArchive = 0;
                data.isInbox = 0;
                data.isInTrash = 0;
                data.isRead = 0;
                data.receiver = Int32.Parse(info.sender.ToString());
                data.sender = info.receiver;
                data.sender_message = replyMsg.Text;
                data.sender_receiver_datetime = hash;
                data.sent_datetime = DateTime.Now;
                entity.Messagings.Add(data);
                entity.SaveChanges();
                //data.receiver = info.sender;
                //data.sender
                
            }


            GetColor();
            Visibility();
            getMessagesTable(hash);
            getInboxTable();

        }

        protected void replyGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void replyGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.BorderStyle = BorderStyle.None;
            }
        }

        
    }
}