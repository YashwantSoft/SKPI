using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Settings
{
    public partial class EmailCredentials : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        int TableID = 0;

        public EmailCredentials()
        {
            InitializeComponent();
            objBL.Set_Button_All_Report(btnSave, btnClear, btnExit, lblHeader, BusinessResources.LBL_HEADER_EMAILCREDENTIALS);
            btnSave.Text = BusinessResources.BTN_SAVE;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TableID != 0)
                objBL.Query = "update EmailCredentials set EmailId='" + txtEmailAddress.Text + "',[Password]='" + txtPassword.Text + "' where CancelTag=0 and ID=" + TableID + "";
            else
                objBL.Query = "insert into EmailCredentials(EmailId,[Password]) values('" + txtEmailAddress.Text + "','" + txtPassword.Text + "')";

            objBL.Function_ExecuteNonQuery();
            objRL.ShowMessage(1, 5);
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
            txtEmailAddress.Text = "";
            txtPassword.Text = "";
            TableID = 0;
        }

        private void Fill_Email_Creadentials()
        {
            ClearAll();
            objBL.Query = "select ID,EmailId,Password from EmailCredentials where CancelTag=0";
            DataSet ds = new DataSet();
            ds=objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EmailId"].ToString()) && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Password"].ToString()))
                {
                    TableID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    txtEmailAddress.Text = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                }
            }
        }

        private void EmailCredentials_Load(object sender, EventArgs e)
        {
            ClearAll();
            Fill_Email_Creadentials();
            txtEmailAddress.Focus();
        }

        private void txtEmailAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
    }
}
