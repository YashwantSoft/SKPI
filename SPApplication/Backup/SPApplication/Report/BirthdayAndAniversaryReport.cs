using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SPApplication
{
    public partial class BirthdayAndAniversaryReport : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();

        bool FlagToday = false;

        public BirthdayAndAniversaryReport()
        {
            InitializeComponent();
            objBL.Set_Report_Design(this, lblHeader, btnReport, btnClear, btnExit, BusinessResources.LBL_HEADER_BIRTHDAYREPORT);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll()
        {
            objEP.Clear();
            cbToday.Checked = true;
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            cmbReportType.SelectedIndex = -1;
            dataGridView1.DataSource = null;
            lblCount.Text = "";
            dtpFromDate.Focus();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (cmbReportType.SelectedIndex == -1)
            {
                objEP.SetError(cmbReportType, "Select Report Type");
                return true;
            }
            else if (dtpToDate.Value.Date < dtpFromDate.Value.Date)
            {
                objEP.SetError(dtpToDate, "Select Proper Date");
                return true;
            }
            else
                return false;
        }

        private void Report()
        {
            if (!Validation())
            {
                DataSet ds = new DataSet();
                if (cmbReportType.Text == "Birthday")
                    if (FlagToday == true)
                        objBL.Query = "select ID,PatientRegistrationNumber,EntryDate,Salutation,LastName,FirstName,MiddleName,Sex,DOB,DrugAllergies,Age,IfYesToWhatDrugs,Occupation,Address,Email,MobileNumber,Weight,Height,BMI,UserId from Patient where CancelTag=0 and Month(DOB)=" + dtpFromDate.Value.Month + " and Day(DOB)=" + dtpFromDate.Value.Day + " and CancelTag=0";
                    else
                        objBL.Query = "select ID,PatientRegistrationNumber,EntryDate,Salutation,LastName,FirstName,MiddleName,Sex,DOB,DrugAllergies,Age,IfYesToWhatDrugs,Occupation,Address,Email,MobileNumber,Weight,Height,BMI,UserId from Patient where CancelTag=0 and Month(DOB) between " + dtpFromDate.Value.Month + " and " + dtpToDate.Value.Month + " and Day(DOB) between " + dtpFromDate.Value.Day + " and " + dtpToDate.Value.Day + "";

                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblCount.Text = "Total Count-" + ds.Tables[0].Rows.Count.ToString();
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    //dataGridView1.Columns[1].Visible = false;
                    //dataGridView1.Columns[2].Visible = false;

                    dataGridView1.Columns[3].Width = 120;
                    dataGridView1.Columns[4].Width = 120;
                    dataGridView1.Columns[5].Width = 200;
                    dataGridView1.Columns[6].Width = 50;
                    dataGridView1.Columns[7].Width = 100;
                    dataGridView1.Columns[8].Width = 50;
                    dataGridView1.Columns[9].Width = 120;
                    dataGridView1.Columns[10].Width = 150;
                    dataGridView1.Columns[11].Width = 120;
                }
                else
                {
                    MessageBox.Show("Record not found");
                    return;
                }
            }
        }

        private void BirthdayAndAniversaryReport_Load(object sender, EventArgs e)
        {
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            ClearAll();
        }
        
        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToday.Checked == true)
            {
                FlagToday = true;
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
            else
            {
                FlagToday = false;
                dtpFromDate.Value = DateTime.Now.Date;
                dtpToDate.Value = DateTime.Now.Date;
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Do yo want to send birthday Email.", "Delete Record", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                SendMail();
            }
        }

        RedundancyLogics objRL = new RedundancyLogics();
        private void SendMail()
        {
            try
            {
                objRL.EmailValidations();
                MailMessage mail = new MailMessage();

                //Google Credentials
                SmtpClient SmtpServer = new SmtpClient("webmail.mechlean.com", 587);
                //SmtpClient SmtpServer = new SmtpClient("webmail.mechlean.com", 587);

                SmtpServer.UseDefaultCredentials = false;
                
                //mail.From = new MailAddress("rahul.gadhe@mechlean.com");
                mail.From = new MailAddress(RedundancyLogics.EmailAddress_Static);
        
                mail.To.Add(RedundancyLogics.EmailAddress_Static);
                //mail.To.Add("rahul.gadhe@mechlean.com");

                mail.Subject = "Birthday Wishes";

                mail.Body = "Many many happy returns of the day";
                mail.Body = mail.Body.Replace("@", " " + Environment.NewLine);

                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(ReportDestination);
                //mail.Attachments.Add(attachment);

                //SmtpServer.Credentials = new System.Net.NetworkCredential("rahul.gadhe@mechlean.com", "rahul.532");
                SmtpServer.Credentials = new System.Net.NetworkCredential(RedundancyLogics.EmailAddress_Static.ToString(), RedundancyLogics.EmailPassword_Static.ToString());
                
                SmtpServer.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                SmtpServer.Send(mail);
                MessageBox.Show("Mail Send");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpToDate.Focus();
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbToday.Focus();
        }

        private void cbToday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbReportType.Focus();
        }

        private void cmbReportType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnReport.Focus();
        }
    }
}
