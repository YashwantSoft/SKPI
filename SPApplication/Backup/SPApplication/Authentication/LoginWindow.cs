using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;
using Microsoft.Win32;

namespace SPApplication
{
    public partial class LoginWindow : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();

        string UserName = "", Password = "";

        public LoginWindow()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            btnLogin.BackColor = objBL.GetBackgroundColor();
            btnLogin.ForeColor = objBL.GetForeColor();

            btnCancel.BackColor = objBL.GetBackgroundColor();
            btnCancel.ForeColor = objBL.GetForeColor();

            lblUserName.ForeColor = objBL.GetBackgroundColor();
            lblPassword.ForeColor = objBL.GetBackgroundColor();

            btnExit.BackColor = objBL.GetBackgroundColor();
            btnExit.ForeColor = objBL.GetForeColor();
            pbProductLogo.Image = BusinessResources.ProductLogo;
        }

        public string HKLM_GetString(string path, string key)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(path);
                if (rk == null) return "";
                return (string)rk.GetValue(key);
            }
            catch { return ""; }
        }

        public string FriendlyName()
        {
            string ProductName = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string CSDVersion = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");
            if (ProductName != "")
            {
                return (ProductName.StartsWith("Microsoft") ? "" : "Microsoft ") + ProductName +
                            (CSDVersion != "" ? " " + CSDVersion : "");
            }
            return "";
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {
            if (objRL.ReturnSystemDateFormat())
            {
                //objRL.FillColor(lblHeader);
                //string osVer = System.Environment.OSVersion.Version.ToString();
                //string MACAddress = string.Empty;

                //RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
                //RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
                //string pathName = (string)registryKey.GetValue("productName");
                //string pathName12 = (string)registryKey1.GetValue("CSDVersion");

                ////string MyMACAddress = "70:62:B8:2A:C7:FB";
                ////string MyMACAddress = "7062B82AC7FB";

                ////string MyMACAddress = "00:1A:73:FE:97:2C";
                ////string MyMACAddress = "28:E3:47:11:7F:2B";
                ////string MyMACAddress = "2A:E3:47:11:7F:2B";
                ////string MyMACAddress = "00:1E:68:17:49:67";

                ////string MyMACAddress = "DC:53:60:84:FE:72";
                //string MyMACAddress = "DC:4A:3E:A7:7D:81";
                ////DC-4A-3E-A7-7D-81
                ////string MyMACAddress = "001A73FE972C";

                ////if (pathName == "Windows 10 Enterprise") 
                //if (pathName == "Windows 10 Home Single") 
                //    MACAddress = objBL.GetMacAddress();
                //else
                //    MACAddress = objBL.GetMacAddressNew();

                //if (MyMACAddress != MACAddress)
                //{
                //    MessageBox.Show("You are not purchasing licence of this software");
                //    Application.Exit();
                //    return;
                //}
                //else
                //    ClearAll();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                this.Dispose();
                return;
            }
        }


        private void ClearAll()
        {
            objEP.Clear();
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtUserName.Focus();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtUserName.Text == "")
            {
                objEP.SetError(txtUserName, "Enter User Name");
                txtUserName.Focus();
                return true;
            }
            else if (txtPassword.Text == "")
            {
                objEP.SetError(txtPassword, "Enter Password");
                txtPassword.Focus();
                return true;
            }
            else
                return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginSuccess();
        }

        private void LoginSuccess()
        {
            if (!Validation())
            {
                UserName = ""; Password = "";
                UserName = txtUserName.Text;
                Password = txtPassword.Text;
                Password = BusinessLayer.Encrypt(Password, true);

                DataSet ds = new DataSet();
                objBL.Query = "select ID,UserName,Password from Login where CancelTag=0 and UserName='" + UserName + "' and Password='" + Password + "'";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["UserName"].ToString()) && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Password"].ToString()))
                    {
                        BusinessLayer.UserId_Static = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                        BusinessLayer.UserName_Static = ds.Tables[0].Rows[0]["UserName"].ToString();
                        Dashboard objForm = new Dashboard();
                        objForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        ClearAll();
                        objRL.ShowMessage(20, 4);
                        return;
                    }
                }
                else
                {
                    ClearAll();
                    objRL.ShowMessage(20, 4);
                    return;
                }
            }
            else
            {
                ClearAll();
                objRL.ShowMessage(19, 4);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LoginSuccess();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPassword.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
