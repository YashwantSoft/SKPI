using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Authentication
{
    public partial class ChangePassword : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        RedundancyLogics objRL = new RedundancyLogics();
        ErrorProvider objEP = new ErrorProvider();
        DesignLayer objDL = new DesignLayer();

        public ChangePassword()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            objDL.SetDesign3Buttons(this, lblHeader, btnSave, btnClear, btnExit, BusinessResources.LBL_HEADER_CHANGEPASSWORD);
            btnSave.Text = BusinessResources.BTN_SAVE;
        }

        private void ClearAll()
        {
            txtUserName.Text = "";
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtUserName.Text = BusinessLayer.UserName_Static;
            txtCurrentPassword.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtUserName.Text == "")
            {
                objEP.SetError(txtUserName, "Enter User Name.");
                return true;
            }
            else if (txtCurrentPassword.Text == "")
            {
                txtCurrentPassword.Focus();
                objEP.SetError(txtCurrentPassword, "Enter Current Password.");
                return true;
            }
            else if (txtNewPassword.Text == "")
            {
                txtNewPassword.Focus();
                objEP.SetError(txtNewPassword, "Enter New Password.");
                return true;
            }
            else if (txtConfirmPassword.Text == "")
            {
                txtConfirmPassword.Focus();
                objEP.SetError(txtConfirmPassword, "Enter Confirm Password.");
                return true;
            }
            else
                return false;
        }

        private void SavePassword()
        {
            if (!Validation())
            {
                if (Success_Current_Password())
                {
                    if (txtNewPassword.Text == txtConfirmPassword.Text)
                    {
                        PasswordEncripted = "";
                        PasswordEncripted = txtNewPassword.Text;
                        PasswordEncripted = BusinessLayer.Encrypt(PasswordEncripted, true);
                        objBL.Query = "update Login set [Password]='" + PasswordEncripted + "' where ID=" + BusinessLayer.UserId_Static + "";
                        int Result = objBL.Function_ExecuteNonQuery();
                        if (Result > 0)
                        {
                            objRL.ShowMessage(10, 1);
                            ClearAll();
                        }
                        else
                        {
                            ClearAll();
                            objRL.ShowMessage(11, 4);
                            return;
                        }
                    }
                    else
                    {
                        ClearAll();
                        objRL.ShowMessage(22, 4);
                        return;
                    }
                }
                else
                {
                    ClearAll();
                    objRL.ShowMessage(17, 4);
                    return;
                }
            }
            else
            {
                ClearAll();
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                SavePassword();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        string PasswordEncripted = "";

        private bool Success_Current_Password()
        {
            bool FlagReutrn = false;

            if (txtCurrentPassword.Text != "" && txtUserName.Text != "")
            {
                DataSet ds = new DataSet();
                
                PasswordEncripted = BusinessLayer.Encrypt(txtCurrentPassword.Text, true);
                objBL.Query = "select ID,UserName,Password from Login where CancelTag=0 and UserName='" + txtUserName.Text + "' and Password='" + PasswordEncripted + "'";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                    FlagReutrn = true;
                else
                    FlagReutrn = false;
            }
            else
                FlagReutrn = false;

            return FlagReutrn;
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtCurrentPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNewPassword.Focus();
        }

        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtConfirmPassword.Focus();
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
    }
}
