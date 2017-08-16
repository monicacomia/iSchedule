using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class Rooms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                                groupDDL.Items.Add(new ListItem(item.grpName, item.grpID.ToString()));
                            }

                }
                getGroupTable();
                getRoomTable();
                
            }
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

                if(entity.RoomLists.Any(t=>t.room.Equals(RoomTxt.Text))){

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "invalidRoom()", true);
                }
                else if (RoomTxt.Text.Trim().Length == 0) {

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "inputRoom()", true);
                
                }
                else
                {

                    RoomList room = new RoomList();
                    room.room = RoomTxt.Text;
                    room.grp = Int32.Parse(groupDDL.SelectedValue);
                    entity.RoomLists.Add(room);
                    entity.SaveChanges();
                    //getGroupTable();
                    getRoomTable();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "addedRoom()", true);
                }
                
            }
        }

        public void getRoomTable()
        {

            DataTable dt = new DataTable();
            dt = getRoom();
            roomGrid.DataSource = dt;
            roomGrid.DataBind();
            roomPanel.Update();

        }

        public static DataTable getRoom()
        {



            DataTable dt = new DataTable();
            dt.Columns.Add("roomID", typeof(string));
            dt.Columns.Add("room", typeof(string));
            dt.Columns.Add("grp", typeof(string));



            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.RoomLists 

                              select new
                              {
                                  t.roomID,
                                  t.room,
                                  t.grp

                              });
                foreach (var entityRow in entity)
                {

                    //DateTime holiDate = (DateTime)entityRow.date;


                    dr = dt.NewRow();
                    dr["roomID"] = entityRow.roomID;
                    dr["room"] = entityRow.room;
                    dr["grp"] = data.Groups.Where(t=>t.grpID==entityRow.grp).Select(t=>t.grpName).FirstOrDefault();

                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }


       
     

        public void getGroupTable()
        {

            DataTable dt = new DataTable();
            dt = getGroup();
            grpGrid.DataSource = dt;
            grpGrid.DataBind();
            grpPanel.Update();

        }

        public static DataTable getGroup()
        {



            DataTable dt = new DataTable();
            dt.Columns.Add("grpID", typeof(int));
            dt.Columns.Add("grpName", typeof(string));
            


            DataRow dr;

            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = (from t in data.Groups
                              
                              select new
                              {
                                 t.grpID,
                                 t.grpName

                              });
                foreach (var entityRow in entity)
                {

                    //DateTime holiDate = (DateTime)entityRow.date;


                    dr = dt.NewRow();
                    dr["grpID"] = entityRow.grpID;
                    dr["grpName"] = entityRow.grpName;
                  
                    dt.Rows.Add(dr);
                }

            }

            return dt;


        }

      

        protected void btnGrp_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {

                if (entity.Groups.Any(t => t.grpName.Equals(grpTxt.Text)))
                {

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "invalidGrp()", true);
                }
                else if (grpTxt.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "inputGrp()", true);
                
                }
                else
                {
                    Group grp = new Group();
                    grp.grpName = grpTxt.Text;
                    entity.Groups.Add(grp);
                    entity.SaveChanges();
                    Response.Redirect("~/Rooms.aspx#group");
                    //getGroupTable();
                }

              

            }
            
        }

        

        protected void grpGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "onDelete")
            {
                var ID = Int32.Parse(e.CommandArgument.ToString());

                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    var entity = data.Groups.Where(x => x.grpID == ID).FirstOrDefault();

                    data.Groups.Remove(entity);
                    data.SaveChanges();
                }

                Response.Redirect("~/Rooms.aspx#group");

                

            }

            else if (e.CommandName == "onUpdate")
            {
                var ID = Int32.Parse(e.CommandArgument.ToString());

                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    var selected = data.Groups.FirstOrDefault(g => g.grpID==ID);
                    editGrpTxt.Text = selected.grpName;
                    grplbl.Text = selected.grpID.ToString();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#editGrpModal').modal('show');", true);
                }
            }


        }

        protected void roomGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            roomGrid.PageIndex = e.NewPageIndex;
            getRoomTable();
        }

        protected void roomGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "onDelete")
            {
                var ID = Int32.Parse(e.CommandArgument.ToString());

                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    var entity = data.RoomLists.Where(x => x.roomID == ID).FirstOrDefault();

                    data.RoomLists.Remove(entity);
                    data.SaveChanges();
                }
                getRoomTable();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "deletedRoom()", true);
            }


            else if (e.CommandName == "onUpdate")
            {
                string ID = e.CommandArgument.ToString();
                
                using (ThesisDBEntities data = new ThesisDBEntities())
                {
                    var selected = data.RoomLists.ToList().FirstOrDefault(g => g.roomID.ToString().Equals(ID));
                    editRoomTxt.Text = selected.room;
                    roomlbl.Text = selected.roomID.ToString();
                    var grp = (from p in data.Groups
                                select new
                                {
                                    p.grpID,
                                    p.grpName
                                });
                    editGroupDDL.Items.Clear();
                    foreach (var item in grp)
                    {
                       editGroupDDL.Items.Add(new ListItem(item.grpName, item.grpID.ToString()));
                    }
                    editGroupDDL.SelectedValue = selected.grp.ToString();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "jQuery('#editRoomModal').modal('show');", true);
                }
            }

           
        }

        protected void saveRoomButton_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities data = new ThesisDBEntities())
            {

                if (data.RoomLists.Any(t => t.room.Equals(editRoomTxt.Text)))
                {

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "invalidRoom()", true);
                }
                else if (editRoomTxt.Text.Trim().Length == 0)
                {

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "inputRoom()", true);

                }
                else
                {
                    var room = Int32.Parse(roomlbl.Text);
                    var entity = data.RoomLists.FirstOrDefault(g =>g.roomID==room);
                    entity.room = editRoomTxt.Text;
                    entity.grp = Int32.Parse(editGroupDDL.SelectedValue);
                    data.SaveChanges();
                    getRoomTable();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "editedRoom()", true);
                }
               
            }
            
        }

        protected void editGrpBtn_Click(object sender, EventArgs e)
        {


            using (ThesisDBEntities data = new ThesisDBEntities())
            {

                if (data.Groups.Any(t => t.grpName.Equals(editGrpTxt.Text)))
                {

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "invalidGrp()", true);
                }
                else if (editGrpTxt.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "inputGrp()", true);

                }

                else
                {
                    var id = Int32.Parse(grplbl.Text);
                    var entity = data.Groups.FirstOrDefault(g => g.grpID==id);
                    entity.grpName = editGrpTxt.Text;
                    data.SaveChanges();
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "editedRoom()", true);
                    Response.Redirect("~/Rooms.aspx#group");
                }

            }
            
            
        }
    }
}