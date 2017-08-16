using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;

namespace Thesis
{
    public partial class ForgotPassword1 : System.Web.UI.Page
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        //private String facultyIDGlobal;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void goBackLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
        private static string generateCode(int length)
        {
            var chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }
        protected void recoverPasswordButton_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var user = (from a in data.Users
                            where a.faculty_id == facultyIdTxtbox.Text
                            select new
                            {
                                a.faculty_id,
                                a.first_name,
                                a.last_name,
                                a.email_address
                            }).FirstOrDefault();

                (from p in data.ForgotPasswordTickets
                 where p.faculty_id == facultyIdTxtbox.Text
                 select p).ToList().ForEach(x => x.isUsed = true);

                if (user != null)
                {
                    Session["FP_FID"] = user.faculty_id.ToString();

                    string code = generateCode(10);

                    ForgotPasswordTicket ticket = new ForgotPasswordTicket();

                    ticket.faculty_id = Session["FP_FID"].ToString();
                    ticket.code_hash = BCrypt.HashPassword(code, BCrypt.GenerateSalt());
                    ticket.expiration_date = DateTime.Now.AddHours(24);
                    ticket.isUsed = false;

                    data.ForgotPasswordTickets.Add(ticket);
                    data.SaveChanges();

                    using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["emailAuthUser"], user.email_address))
                    {
                        mm.Subject = "Forgot Password";
                        mm.Body = String.Format("Hello {0},<br/>" + "<br/>iSchedule received a forgot password request in the system.<br/>"
                            + "<br/>Your system generated code is: <b>{1}</b><br/>" + "<br/>Take note that the code is case-sensitive and will expire in 24 hours. "
                            + "If you did not intend to reset your password, please ignore this e-mail. This is a system generated e-mail. Please do not reply.", user.first_name + " " + user.last_name, code);

                        mm.IsBodyHtml = true;

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

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), Guid.NewGuid().ToString(), "sentEmailMsg()", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The Faculty ID entered does not match any entry in the database!')", true);
                }
            }
            Response.Redirect("~/CodeConfirmation.aspx");
        }
    }
}