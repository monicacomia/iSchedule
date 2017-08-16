using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getData();
            }

            else
            {
                getData();
            }
            
        }



        protected void getData()
        {

            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                var ID = Session["FACULTY_ID"].ToString();
                var data = entity.Users.Where(p => p.faculty_id.Equals(ID)).FirstOrDefault();
                if (data.image == null)
                {
                    img.ImageUrl = "~/Assets/img/find_user.png";
                }
                else
                {
                    img.ImageUrl = "~/ShowImage.ashx?id=" + ID;
                }

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                var ID = Session["FACULTY_ID"].ToString();
                var data = entity.Users.Where(p => p.faculty_id.Equals(ID)).FirstOrDefault();
                FileUpload imgBox = (FileUpload)imgUpload;
                Byte[] imgByte = null;
                if ((imgBox.HasFile && imgBox.PostedFile != null) && (Path.GetExtension(imgBox.FileName) == ".jpg" || Path.GetExtension(imgBox.FileName) == ".png"))
                {
                    //To create a PostedFile
                    HttpPostedFile File = imgUpload.PostedFile;
                    //Create byte Array with file len
                    imgByte = new Byte[File.ContentLength];
                    //force the control to load data in array
                    File.InputStream.Read(imgByte, 0, File.ContentLength);
                    data.image = imgByte;
                    entity.SaveChanges();
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "invalid()", true);
                }
                
            }
            //getData();
            //imgPanel.Update();

            
        }

        protected void btnPassword_Click(object sender, EventArgs e)
        {   String ID=Session["FACULTY_ID"].ToString();
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                if (password1.Text.Trim().Length < 6 || password1.Text.Trim().Length > 20)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "length()", true);
                }

                else
                {
                    var password = data.Users.Where(p => p.faculty_id.Equals(ID)).Select(p => p.password).FirstOrDefault();
                    var pass = password.ToString();
                    var old = oldpassword.Text;
                    var changepass = password1.Text;
                    if (BCrypt.CheckPassword(old, pass))
                    {


                        var user = data.Users.Where(p => p.faculty_id.Equals(ID)).FirstOrDefault();
                        string mySalt = BCrypt.GenerateSalt();
                        user.password = BCrypt.HashPassword(changepass, mySalt);
                        data.SaveChanges();
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showSuccessMsg()", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "showFailMsg()", true);
                    }
                }

                
            }
        }
    }
}