using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class CodeConfirmation : System.Web.UI.Page
    {
        private String facultyIDGlobal;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void goBackLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
        protected void codeConfirmButton_Click(object sender, EventArgs e)
        {
            facultyIDGlobal = Session["FP_FID"].ToString();
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var ticket = (from u in data.ForgotPasswordTickets
                              where u.faculty_id == facultyIDGlobal && u.isUsed == false
                              select u).FirstOrDefault();

                if (ticket != null && ticket.isUsed == false && BCrypt.CheckPassword(codeConfirmTextbox.Text, ticket.code_hash) && ticket.expiration_date >= DateTime.Now)
                {
                    ticket.isUsed = true;
                    data.SaveChanges();

                    invalidCodeLabel.Visible = false;
                    Response.Redirect("~/ChangePassword.aspx");
                }
                else
                {
                    invalidCodeLabel.Text = "The code you entered does not match with the ID in the database.";
                    invalidCodeLabel.Visible = true;
                }
            }


        }
    }
}