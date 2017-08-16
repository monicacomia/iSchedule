using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Thesis
{
    public partial class UploadSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getDefaultValues();
            }
        }

        public void getDefaultValues()
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
                var data = entity.CurrentSettings.OrderByDescending(p => p.ID).FirstOrDefault();
                if (data == null)
                {
                    ddlSemester.Enabled = true;
                    schoolYearStart.ReadOnly = false;
                    schoolYearEnd.ReadOnly = false;
                    durationStart.ReadOnly = false;
                    durationEnd.ReadOnly = false;
                    editCurrentSettingsButton.Visible = false;
                    confirmButton.Visible = true;
                }
                else
                {
                    ddlSemester.SelectedIndex = int.Parse(data.semester);
                    schoolYearStart.Text = data.schoolYearStart.ToString();
                    schoolYearEnd.Text = data.schoolYearEnd.ToString();
                    schoolYearEnd.Enabled = false;
                    schoolYearStart.Enabled = false;
                    durationStart.Text = data.durationStart.ToString("yyyy-MM-dd");
                    durationEnd.Text = data.durationEnd.ToString("yyyy-MM-dd");
                    durationStart.Enabled = false;
                    durationEnd.Enabled = false;
                }
            }
        }

        protected void editCurrentSettingsButton_Click(object sender, EventArgs e)
        {
            editCurrentSettingsButton.Visible = false;
            ddlSemester.Enabled = true;
            //schoolYearTextBox.ReadOnly = false;
            schoolYearStart.Enabled = true;
            schoolYearEnd.Enabled = true;
            //durationTextBox.ReadOnly = false;
            durationStart.Enabled = true;
            durationEnd.Enabled = true;
            confirmButton.Visible = true;
            cancelButton.Visible = true;
        }

        protected void confirmButton_Click(object sender, EventArgs e)
        {
            using (ThesisDBEntities entity = new ThesisDBEntities())
            {
               
                CurrentSetting data = new CurrentSetting();
                data.semester = ddlSemester.SelectedValue;
                data.schoolYearStart = short.Parse(schoolYearStart.Text);
                data.schoolYearEnd = short.Parse(schoolYearEnd.Text);
                data.durationStart = DateTime.Parse(durationStart.Text);
                data.durationEnd = DateTime.Parse(durationEnd.Text);
                data.dateModified = DateTime.Now;
                data.modifiedBy = 1;

                entity.CurrentSettings.Add(data);
                entity.SaveChanges();
            }

            returnStart();
            
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            returnStart();

        }

        public void returnStart()
        {
            getDefaultValues();
            editCurrentSettingsButton.Visible = true;
            confirmButton.Visible = false;
            cancelButton.Visible = false;
            ddlSemester.Enabled = false;
            schoolYearStart.Enabled = false;
            schoolYearEnd.Enabled = false;
            //durationTextBox.ReadOnly = true;
            durationStart.Enabled = false;
            durationEnd.Enabled = false;
        }

       
        

    }
}