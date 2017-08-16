using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

     
        protected void loginButton_Click(object sender, EventArgs e)
        {
            String ID = TextBox1.Text;
            String password = TextBox2.Text;


            using (ThesisDBEntities data = new ThesisDBEntities())
            {
                var entity = data.Users.FirstOrDefault(g => g.schoolID.Equals(ID) && g.password.Equals(password) && g.status==1);

                if (entity == null)
                {
                    //Invalid login
                }
                else
                {
                    Session["userID"] = entity.ID;
                    Session["firstName"] = entity.firstName;
                    Session["lastName"] = entity.lastName;
                    Response.Redirect("Dashboard.aspx");
                    //go to a specific page
                }


            }


        }

        
    }
}