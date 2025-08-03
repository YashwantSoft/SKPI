using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Management;
using System.Drawing;

namespace BusinessLayerUtility
{
    public class BusinessLayer
    {
        public OleDbCommand objCmd;
        public OleDbConnection objCon;
        public string conString = null;
        public string Query = "";
        public OleDbDataAdapter da;
        public static string UserName_Static;
        public static int UserId_Static;

        public void Connect()
        {
            //conString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\198.168.2.3\Yashwant\Projects\Surya Hospital\SPApplication\Database\MDAppsDB.mdb";

            conString = System.Configuration.ConfigurationManager.ConnectionStrings["MyCon"].ToString();

            //conString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\GRAVITY-PC\Yashwant\Projects\Surya Hospital\SPApplication\Database\MDAppsDB.mdb";
            
            //conString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Yashwant\Projects\Surya Hospital\SPApplication\Database\MDAppsDB.mdb";
            //conString = System.Configuration.ConfigurationManager.ConnectionStrings["MyCon"].ToString();
            objCon = new OleDbConnection(conString);
            objCon.Open();
        }

      

        public Color GetRowSelectionColor()
        {
            //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
            Color color = System.Drawing.ColorTranslator.FromHtml("#1C2A4F");
            return color;
        }

        public int Function_ExecuteNonQuery()
        {
            RedundancyLogics objRL = new RedundancyLogics();
            int Result = 0;
            try
            {
                Connect();
                objCmd = new OleDbCommand(Query, objCon);
                Result = objCmd.ExecuteNonQuery();
                objCon.Close();
            }
            catch (Exception ex1) {objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
            return Result;
        }

        public DataSet ReturnDataSet()
        {
            RedundancyLogics objRL = new RedundancyLogics();
            DataSet ds = new DataSet();
            try
            {
                Connect();
                objCmd = new OleDbCommand(Query, objCon);
                da = new OleDbDataAdapter(objCmd);
                da.Fill(ds);
                objCon.Close();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); objCon.Close(); }

            return ds;
        }

        public DataTable ReturnDataTable()
        {
            RedundancyLogics objRL = new RedundancyLogics();
            DataTable dt = new DataTable();
            try
            {
                Connect();
                objCmd = new OleDbCommand(Query, objCon);
                da = new OleDbDataAdapter(objCmd);
                da.Fill(dt);
                objCon.Close();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }

            return dt;
        }

        public string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }

        public string GetMacAddressNew()
        {
            string macAddresses = string.Empty;

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            string MACAddress = "";

            foreach (ManagementObject mo in moc)
            {
                if (mo["MacAddress"] != null)
                {
                    MACAddress = mo["MacAddress"].ToString();
                }
            }

            //Yashwant Machine Address-: 00:27:0E:0C:4E:81
            //Pushkaraj Sir Machine Address-: 
            //Laptop Address-: 00:1D:72:25:FE:5D

            macAddresses = MACAddress;
            return macAddresses;
            //if (MACAddress != "00:26:22:CF:4F:61")
            //{
            //    //objMUC.ShowMessageBox(52, 9);
            //    Application.Exit();
            //}
        }

        //Password Encription Decrypt
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",
                                                             typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey",
                                                         typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public void FillComboBox(ComboBox cmb,string DisplayMember,string ValueMember)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cmb.DataSource = ds.Tables[0];
                    cmb.DisplayMember = DisplayMember;
                    cmb.ValueMember = ValueMember;
                }
            }
            catch (Exception ex1) { MessageBox.Show(ex1.ToString()); }
            finally { GC.Collect(); }
        }


        public void Button_Regular_Format_Test(Button btn, string btnText)
        {
            btn.Text = btnText;
            btn.Font = new Font(BusinessResources.FONT_NAME, 12, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = System.Drawing.ColorTranslator.FromHtml(BusinessResources.COL_LBL_HEADER);
        }

        public void Master_Form_Format(Form frm)
        {
            //frm.BackColor = System.Drawing.ColorTranslator.FromHtml("#9C27B0");
            frm.BackColor = Color.White;
            frm.ControlBox = false;
            //frm.Font = new Font(BusinessResources.FONT_NAME, 10, FontStyle.Regular);
            frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            frm.MinimizeBox = false;
            frm.MaximizeBox = false;
            frm.ShowInTaskbar = false;
            frm.ShowIcon = false;
            //frm.Size = new System.Drawing.Size(900, 600);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Text = "";
        }

        public void Button_Add(Button btn)
        {
            btn.BackColor = GetBackgroundColor();
            btn.Text = BusinessResources.BTN_PLUS;
            btn.Font = new Font(BusinessResources.FONT_NAME, 9, FontStyle.Bold);
        }

        public Color GetBackgroundColor()
        {
            //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
            Color color = System.Drawing.ColorTranslator.FromHtml(BusinessResources.BACKGROUND_COLOUR); //System.Drawing.ColorTranslator.FromHtml("#1C2A4F");
            return color;
        }

        public Color GetForeColor()
        {
            //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");System.Drawing.Color.White;// 
            Color color = System.Drawing.ColorTranslator.FromHtml(BusinessResources.FORE_COLOUR); 
            return color;
        }

        public void Set_Design_All(Form frm, Label lbl, Button btnSave, Button btnClear, Button btnDelete, Button btnExit, string LableText)
        {
            //Master_Form_Format(frm);
            //Label_Header(lbl, LableText);
            //Set_Button_All(btnSave, btnClear, btnDelete, btnExit);

            btnSave.BackColor = GetBackgroundColor();
            btnSave.ForeColor = GetForeColor();
            btnSave.Text = BusinessResources.BTN_SAVE;
            btnSave.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnSave.Size = new System.Drawing.Size(80, 30);

            btnClear.BackColor = GetBackgroundColor();
            btnClear.ForeColor = GetForeColor();
            btnClear.Text = BusinessResources.BTN_CLEAR;
            btnClear.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnClear.Size = new System.Drawing.Size(80, 30);

            btnDelete.BackColor = GetBackgroundColor();
            btnDelete.ForeColor = GetForeColor();
            btnDelete.Text = BusinessResources.BTN_DELETE;
            btnDelete.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnDelete.Size = new System.Drawing.Size(80, 30);

            btnExit.BackColor = GetBackgroundColor();
            btnExit.ForeColor = GetForeColor();
            btnExit.Text = BusinessResources.BTN_EXIT;
            btnExit.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnExit.Size = new System.Drawing.Size(80, 30);

            lbl.BackColor = GetBackgroundColor();
            lbl.ForeColor = GetForeColor();
            lbl.Text = LableText.ToString();

            DataGridView dgv = frm.Controls.Find("dataGridView1", true).FirstOrDefault() as DataGridView;
            if(dgv  !=null)
                dgv.DefaultCellStyle.SelectionBackColor = GetRowSelectionColor();
        }

        public void Set_Button_All_Report(Button btnReport, Button btnClear, Button btnExit, Label lblHeader, string LableText)
        {
            btnReport.BackColor = GetBackgroundColor();
            btnReport.ForeColor = GetForeColor();
            btnReport.Text = BusinessResources.BTN_REPORT;
            btnReport.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnReport.Size = new System.Drawing.Size(80, 30);

            btnClear.BackColor = GetBackgroundColor();
            btnClear.ForeColor = GetForeColor();
            btnClear.Text = BusinessResources.BTN_CLEAR;
            btnClear.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnReport.Size = new System.Drawing.Size(80, 30);

            btnExit.BackColor = GetBackgroundColor();
            btnExit.ForeColor = GetForeColor();
            btnExit.Text = BusinessResources.BTN_EXIT;
            btnExit.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnExit.Size = new System.Drawing.Size(80, 30);

            lblHeader.BackColor = GetBackgroundColor();
            lblHeader.ForeColor = GetForeColor();
            lblHeader.Text = LableText.ToString();
        }

        public void Set_Report_Design(Form frm, Label lbl, Button btnReport, Button btnClear, Button btnExit, string LableText)
        {
            Master_Form_Format(frm);
            Set_Button_All_Report(btnReport, btnClear, btnExit, lbl, LableText);
        }

        public void Set_List_Desing(Form frm, Label lbl, Button btnExit, string LableText)
        {
            //Master_Form_Format(frm);
            //Label_Header(lbl, LableText);
            //Set_Button_All_List(btnExit);

            btnExit.BackColor = GetBackgroundColor();
            btnExit.ForeColor = GetForeColor();
            btnExit.Text = "E&XIT";
            btnExit.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            lbl.BackColor = GetBackgroundColor();
            lbl.ForeColor = GetForeColor();
            lbl.Text = LableText.ToString();
        }
    }
}
