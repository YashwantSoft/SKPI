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
using SPApplication.View;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace SPApplication
{
    public partial class LoginWindow : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        string UserName = "", Password = "";
        int RowCount = 18, AFlag = 0, RentCount = 0, SrNo = 1;
        double TotalAmount = 0;

        bool MH_Value = false;
        Microsoft.Office.Interop.Excel.Application myExcelApp;
        Microsoft.Office.Interop.Excel.Workbooks myExcelWorkbooks;
        Microsoft.Office.Interop.Excel.Workbook myExcelWorkbook;
        object misValue = System.Reflection.Missing.Value;

        string PDFReport = string.Empty;
        string ConcatQuery = string.Empty;
        string FacilityName = string.Empty;
        string Duration = string.Empty;

        public LoginWindow()
        {
            InitializeComponent();
            Set_Design();
        }

        private void Set_Design()
        {
            btnLogin.BackColor = objDL.GetBackgroundColor();
            btnLogin.ForeColor = objDL.GetForeColor();

            btnCancel.BackColor = objDL.GetBackgroundColor();
            btnCancel.ForeColor = objDL.GetForeColor();

            lblUserName.ForeColor = objDL.GetBackgroundColor();
            lblPassword.ForeColor = objDL.GetBackgroundColor();

            btnExit.BackColor = objDL.GetBackgroundColor();
            btnExit.ForeColor = objDL.GetForeColor();
            pbClientLogo.Image = BusinessResources.ProductLogo;
            pbClientLogo.Image = BusinessResources.ClientLogo;

            objDL.SetLabelDesign_ForeColor(lblContactDetails, BusinessResources.LBL_CONTACTDETAILS);
            objDL.SetLabelDesign_ForeColor(lblCopyRights, BusinessResources.LBL_COPYRIGHTS);
            objDL.SetLabelDesign_ForeColor(lblVersion, BusinessResources.LBL_VERSION);
            objDL.SetLabelDesign_ForeColor(lbHelp, BusinessResources.LBL_HELP);
            this.Icon = BusinessResources.ICOLogo;
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

                        SaveLogRecord();
                        //ExcelReport();

                        Dashboard objForm = new Dashboard();
                        //Dashboard1 objForm = new Dashboard1();
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

        private bool CheckExistLogRecord()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,LoginId,LoginDate,LoginTime,LogoutDate,LogoutTime from LogRecord where CancelTag=0 and LoginId=" + BusinessLayer.UserId_Static + " and LoginDate= #" + DateTime.Now.Date.ToShortDateString() + "# ";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void SaveLogRecord()
        {
            if (!CheckExistLogRecord())
            {
                objBL.Query = "insert into LogRecord(LoginId,LoginDate,LoginTime) values(" + BusinessLayer.UserId_Static + ",'" + DateTime.Now.Date.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "')";
                objBL.Function_ExecuteNonQuery();
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
            System.Windows.Forms.Application.Exit();
        }
        
        bool FlagCheck = false;

        private void CheckExist_Email_Sent()
        {
            EmailDueDateLockId = 0;
            DataSet ds = new DataSet();
            objBL.Query = "select ID,EntryDate,LockFlag from EmailDueDateLock where EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and LockFlag=1 and CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][0])))
                {
                    EmailDueDateLockId = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    FlagCheck = true;
                }
                else
                    FlagCheck = false;
            }
            else
                FlagCheck = false;

            if (!FlagCheck)
            {
                DataSet ds1=new DataSet();
                objBL.Query = "select ID,EntryDate,LockFlag from EmailDueDateLock where CancelTag=0";
                ds1=objBL.ReturnDataSet();
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ds1.Tables[0].Rows[0][0])))
                    {
                        EmailDueDateLockId = Convert.ToInt32(ds1.Tables[0].Rows[0][0].ToString());
                        FlagCheck = false;
                    }
                    else
                        FlagCheck = false;
                }
                else
                    FlagCheck = false;

                if (!FlagCheck && EmailDueDateLockId == 0)
                {
                    objBL.Query = "insert into EmailDueDateLock(EntryDate,LockFlag) values(" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + ",0)";
                    objBL.Function_ExecuteNonQuery();
                    EmailDueDateLockId = objRL.ReturnMaxID_Fix("EmailDueDateLock","ID");
                }
            }
        }

        private void ExcelReport()
        {
            CheckExist_Email_Sent();

            if (!FlagCheck && EmailDueDateLockId !=0)
            {
                try
                {
                    DataSet ds = new DataSet();
                    objBL.Query = "select PRE.ID,PRE.EntryDate,PRE.EntryTime,PRE.DepartmentId,L.UserName as [Department],PRE.PartId,PM.PartName as [Part Name],PRE.StartDate as [Start Date],PRE.IsCompressor,PRE.CopressorType as [Compressor Type],PRE.RenewarPeriod +'-'+ PRE.RenewarPeriodFor as [Period],PRE.ExpiryDate as [Expiry Date],PRE.StartReadingNo as [Start Reading],EndReadingNo as [End Reading],PRE.RenewalBy as [Renewal by],PRE.ContactNo as [Contact No],PRE.Naration from ((PartRenewalEntry PRE inner join PartMaster PM on PM.ID=PRE.PartId) inner join Login L on L.ID=PM.DepartmentId) where PRE.CancelTag=0 and L.CancelTag=0 and PM.CancelTag=0 and PRE.ExpiryDate >= #" + DateTime.Now.Date.AddDays(-30).ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#  order by PRE.ExpiryDate asc";
                    ds = objBL.ReturnDataSet();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        objRL.FillCompanyData();
                        myExcelApp = new Microsoft.Office.Interop.Excel.Application();
                        myExcelWorkbooks = myExcelApp.Workbooks;

                        objRL.FillCompanyData();
                        objRL.ClearExcelPath();
                        objRL.isPDF = true;


                        objRL.Form_ExcelFileName = "DueDateReportAll.xlsx";
                        objRL.Form_DestinationReportFilePath = "Part Due Date Report";
                        objRL.Form_ReportFileName = "Part Due Date Report All-" + DateTime.Now.Date.ToString("dd-MMM-yyyy");

                        objRL.Path_Comman();

                        myExcelWorkbook = myExcelWorkbooks.Open(objRL.RL_DestinationPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                        Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

                        myExcelWorksheet.get_Range("A3", misValue).Formula = "Find Date From Date " + objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(DateTime.Now.Date.AddDays(-30)));
                        myExcelWorksheet.get_Range("G4", misValue).Formula = objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(DateTime.Now.Date));
                        //myExcelWorksheet.get_Range("F7", misValue).Formula = BusinessLayer.UserName_Static;

                        //0 PM.ID,
                        //1 PRE.EntryDate,
                        //2 PRE.EntryTime,
                        //3 PRE.DepartmentId,
                        //4 L.UserName as [Department],
                        //5 PRE.PartId,
                        //6 PM.PartName as [Part Name],
                        //7 PRE.StartDate,
                        //8 PRE.IsCompressor,
                        //9 PRE.CopressorType,
                        //10 PRE.RenewarPeriod +'-'+ PRE.RenewarPeriodFor as [Period]
                        //11 PRE.ExpiryDate,
                        //12 PRE.StartReadingNo,
                        //13 EndReadingNo,
                        //14 PRE.RenewalBy,
                        //15 PRE.ContactNo
                        //16 PRE.Naration

                        myExcelWorksheet.get_Range("C3", misValue).Formula = "Date " + objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY)));
                        myExcelWorksheet.get_Range("G2", misValue).Formula = objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(DateTime.Now.Date));
                        myExcelWorksheet.get_Range("G3", misValue).Formula = BusinessLayer.UserName_Static;

                        RowCount = 5; SrNo = 1;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            AFlag = 2;
                            Fill_Merge_Cell("A", "A", misValue, myExcelWorksheet, SrNo.ToString());
                            Fill_Merge_Cell("B", "B", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(ds.Tables[0].Rows[i]["EntryDate"].ToString())));
                            AFlag = 0;
                            Fill_Merge_Cell("C", "C", misValue, myExcelWorksheet, ds.Tables[0].Rows[i]["Department"].ToString());
                            Fill_Merge_Cell("D", "D", misValue, myExcelWorksheet, ds.Tables[0].Rows[i]["Part Name"].ToString());
                            Fill_Merge_Cell("E", "E", misValue, myExcelWorksheet, ds.Tables[0].Rows[i]["Renewal by"].ToString());
                            Fill_Merge_Cell("F", "F", misValue, myExcelWorksheet, ds.Tables[0].Rows[i]["Contact No"].ToString());
                            AFlag = 2;
                            Fill_Merge_Cell("G", "G", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(ds.Tables[0].Rows[i]["Start Date"].ToString())));
                            Fill_Merge_Cell("H", "H", misValue, myExcelWorksheet, objRL.GetDateFormat_DDMMYYY(Convert.ToDateTime(ds.Tables[0].Rows[i]["Expiry Date"].ToString())));
                            RowCount++;
                            SrNo++;
                        }
                        myExcelWorkbook.Save();
                        PDFReport = objRL.RL_DestinationPath.Replace(".xlsx", ".pdf");

                        const int xlQualityStandard = 0;
                        myExcelWorkbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, PDFReport, xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
                        myExcelWorkbook.Close(true, misValue, misValue);
                        myExcelApp.Quit();

                        //objRL.ShowMessage(22, 1);

                        //DialogResult dr;
                        //dr = MessageBox.Show("Do you want to view this report", "Report View", MessageBoxButtons.YesNo);
                        //if (dr == DialogResult.Yes)
                        //    System.Diagnostics.Process.Start(objRL.RL_DestinationPath);

                       // System.Diagnostics.Process.Start(PDFReport);
                        //objRL.DeleteExcelFile();

                        //Email Sending code

                        string FileName = Path.GetFileName(PDFReport);
                        objRL.EmailBody = "I have send you Part/Equipment Due Date Report. Report sent from SKPI Apps on " + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATDDMMYYYY) + ".-" + FileName;
                        objRL.SendEmail_Details("nilesh@shreekhodiyarpet.com,info@shreekhodiyarpet.com,hr@shreekhodiyarpet.com,yashwant.it@shreekhodiyarpet.com", 36, PDFReport);

                        if (objRL.FlagEmail)
                        {
                            objBL.Query = "update EmailDueDateLock set EntryDate='" + DateTime.Now.Date.ToShortDateString() + "',LockFlag=1 where ID=" + EmailDueDateLockId + "";
                            objBL.Function_ExecuteNonQuery();
                            objRL.FlagEmail = false;
                        }

                        //Whats apps file sending code
                        //if (cbWhatsAppsNo.Checked)
                        //    objRL.WhatsAppsReportOpen(cbWhatsAppsNo.Text.ToString(), objRL.RL_DestinationPath);
                    }
                }
                catch (Exception ex1)
                {
                    objRL.ShowMessage(27, 4);
                    return;
                }
            }
        }

        int EmailDueDateLockId = 0;
        protected void Fill_Merge_Cell(string Cell1, string Cell2, object val1, Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet, string SetValue)
        {
            Cell1 = Cell1 + RowCount;
            Cell2 = Cell2 + RowCount;
            Microsoft.Office.Interop.Excel.Range AlingRange1 = myExcelWorksheet.get_Range(Cell1, Cell2);
            AlingRange1.Merge(val1);
            myExcelWorksheet.get_Range(Cell1, Cell2).Formula = SetValue;

            Microsoft.Office.Interop.Excel.Range AlingRange2 = myExcelWorksheet.get_Range(Cell1, Cell2);

            if (AFlag == 0)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            else if (AFlag == 2)
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            else
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;


            DrawBorder(AlingRange2);

            if (MH_Value == true)
            {
                AlingRange2.RowHeight = 40;
                AlingRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                AlingRange2.WrapText = true;
            }
        }
        protected void DrawBorder(Range Functionrange)
        {
            Borders borders = Functionrange.Borders;
            //borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;
            borders.Weight = 1D;
        }
    }
}
