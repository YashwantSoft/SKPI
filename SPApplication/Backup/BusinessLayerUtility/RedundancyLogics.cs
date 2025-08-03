using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayerUtility;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Net.Mail;

namespace BusinessLayerUtility
{
    public class RedundancyLogics
    {
        BusinessLayer objBL = new BusinessLayer();
        ToolTip objTT = new ToolTip();

        public int CI_CompanyId = 0;
        public static string Sex_Static;
        public static double SerumCreatinine;

        public static string EmailAddress_Static;
        public static string EmailPassword_Static;

           
        public static string SystemDateFormat;
        public static string DateFormatMMDDYYYY="MM/dd/yyyy";

        public static string PR_String;
        public static string DE_String;
        public static string SaleReceipt;

        public System.Globalization.RegionInfo objRegInfo = new RegionInfo("en-IN");

        public static string SupplierName;
        public static string CustomerName;

        public static int SupplierId;
        public static int CustomerId;
         

        public void Fill_Payment_Type(ComboBox cmb)
        {
            objBL.Query = "select PaymentMode from PaymentModeMaster";
            objBL.FillComboBox(cmb, "PaymentMode", "PaymentMode");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Supplier(ComboBox cmb)
        {
            objBL.Query = "select ID,SupplierName from Supplier where CancelTag=0";
            objBL.FillComboBox(cmb, "SupplierName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_PurchaseNo(ComboBox cmb, int Id)
        {
            objBL.Query = "select ID,PurchaseNo from Purchase where CancelTag=0 and SupplierId=" + Id + "";
            objBL.FillComboBox(cmb, "PurchaseNo", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_SaleNo(ComboBox cmb, int Id)
        {
            objBL.Query = "select ID,ID as [InvoiceNo] from Sale where CancelTag=0 and CustomerId=" + Id + "";
            objBL.FillComboBox(cmb, "InvoiceNo", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Customer(ComboBox cmb)
        {
            objBL.Query = "select ID,CustomerName from Customer where CancelTag=0";
            objBL.FillComboBox(cmb, "CustomerName", "ID");
            cmb.SelectedIndex = -1;
        }

        public bool ReturnSystemDateFormat()
        {
            SystemDateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            if (SystemDateFormat != "dd/MM/yyyy" && SystemDateFormat != "dd-MM-yyyy")
                return false;
            else
                return true;
        }

        public void EmailValidations()
        {
            objBL.Query = "select ID,EmailId,Password from EmailCredentials where CancelTag=0";
            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["EmailId"].ToString()) && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Password"].ToString()))
                {
                    //TableID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    EmailAddress_Static = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    EmailPassword_Static = ds.Tables[0].Rows[0]["Password"].ToString();
                }
            }
        }

        public void Fill_UOM(ComboBox cmb)
        {
            objBL.Query = "select ID,UnitOfMessurement from UOM where CancelTag=0";
            objBL.FillComboBox(cmb, "UnitOfMessurement", "ID");
        }

        public void Add_Tool_Tip(Button b1, Button b2, Button b3, Button b4)
        {
            objTT.SetToolTip(b1, "Save");
            objTT.SetToolTip(b2, "Clear");
            objTT.SetToolTip(b3, "Delete");
            objTT.SetToolTip(b4, "Exit");
        }

        public string MessageString(int MsgValue)
        {
            string MsgString = "";

            switch (MsgValue)
            {
                case 1:
                    MsgString = "Success";
                    break;
                case 2:
                    MsgString = "Warning";
                    break;
                case 3:
                    MsgString = "Question";
                    break;
                case 4:
                    MsgString = "Error";
                    break;
                case 5:
                    MsgString = "Stop";
                    break;
                case 6:
                    MsgString = "Hand";
                    break;
                case 7:
                    MsgString = "Saved Successfully";
                    break;
                case 8:
                    MsgString = "Updated Successfully";
                    break;
                case 9:
                    MsgString = "Deleted Successfully";
                    break;
                case 10:
                    MsgString = "Password changed successfully.";
                    break;
                case 11:
                    MsgString = "Error occured in change password.";
                    break;
                case 12:
                    MsgString = "Already exist.";
                    break;
                case 13:
                    MsgString = "Enter only numeric value.";
                    break;
                case 14:
                    MsgString = "Enter only text value.";
                    break;
                case 15:
                    MsgString = "Enter only text and numeric value.";
                    break;
                case 16:
                    MsgString = "Enter only decimal value.";
                    break;
                case 17:
                    MsgString = "Enter appropriate values";
                    break;
                case 18:
                    MsgString = "E-mail address format is not correct.";
                    break;
                case 19:
                    MsgString = "Enter user name or password";
                    break;
                case 20:
                    MsgString = "Invalid user name or password";
                    break;
                case 21:
                    MsgString = "Please enter correct system date e.g. dd/MM/yyyy";
                    break;
                case 22:
                    MsgString = "Report Generated Successfully";
                    break;
                case 23:
                    MsgString = "Price should be greater than cost.";
                    break;
                case 24:
                    MsgString = "Quantity should be less than available quantity.";
                    break;
                case 25:
                    MsgString = "Records are not found.";
                    break;
                case 26:
                    MsgString = "Do you want to exit?";
                    break;
                case 27:
                    MsgString = "Error occured.";
                    break;
                case 28:
                    MsgString = "Do you want to create and view report.?";
                    break;
                case 29:
                    MsgString = "Email sent successfully";
                    break;
            }
            return MsgString;
        }

        string MString = "", CapString = "";
        public void ShowMessage(int MsgValue, int CapValue)
        {
            MString = ""; CapString = "";
            MString = MessageString(MsgValue);
            CapString = MessageString(CapValue);

            if (CapString == "Success")
                MessageBox.Show(MString, CapString, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else if (CapString == "Warning")
                MessageBox.Show(MString, CapString, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (CapString == "Question")
                MessageBox.Show(MString, CapString, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else if (CapString == "Error")
                MessageBox.Show(MString, CapString, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (CapString == "Stop")
                MessageBox.Show(MString, CapString, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
                MessageBox.Show(MString, CapString, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public void ErrorMessge(string MsgValue)
        {
            MString = MsgValue;
            CapString = MessageString(4);
            MessageBox.Show(MString, CapString, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void NumericValue(object sender, KeyPressEventArgs e, TextBox tb)
        {
            if (tb.Text.Length == 0)
            {
                if (e.KeyChar == 32)
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            if (!(char.IsDigit(e.KeyChar)) && !char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == 32 && tb.Text.Length != 0)
                    e.Handled = false;
                else
                {
                    e.Handled = true;
                    ShowMessage(13, 4);
                }
            }
        }

        public void TxtValue(object sender, KeyPressEventArgs e, TextBox tb)
        {

            if (tb.Text.Length == 0)
            {
                if (e.KeyChar == 32)
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            if (!(char.IsLetter(e.KeyChar)) && !char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == 32 && tb.Text.Length != 0)
                    e.Handled = false;
                else
                {
                    e.Handled = true;
                    ShowMessage(14, 4);
                }
            }
        }

        public void TxtNumericValue(object sender, KeyPressEventArgs e, TextBox tb)
        {
            int a = tb.SelectionStart;
            if (a == 0)
            {
                if (e.KeyChar == 32)
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            if (!(char.IsLetter(e.KeyChar)) && !(char.IsDigit(e.KeyChar)) && !(char.IsControl(e.KeyChar)) && !(e.KeyChar == 46))
            {
                if (e.KeyChar == 32 && a != 0)
                    e.Handled = false;
                else
                {
                    e.Handled = true;
                    ShowMessage(15, 4);
                }
            }
        }

        public void FloatValue(object sender, KeyPressEventArgs e, TextBox tb)
        {
            if (tb.Text.Length == 0)
            {
                if (e.KeyChar == 32)
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            if (!(char.IsDigit(e.KeyChar)) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                ShowMessage(16, 4);
            }
            if (tb.Text.Contains(".") && e.KeyChar == 46)
            {
                e.Handled = true;
            }
        }

        public void MobileNumber_KeyPress(object sender, KeyPressEventArgs e, TextBox tb)
        {
            if (tb.Text.Length == 0)
            {
                if (e.KeyChar == 32)
                    e.Handled = true;
                else
                    e.Handled = false;
            }
            if (!(char.IsDigit(e.KeyChar)) && !char.IsControl(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '+')
            {
                e.Handled = true;
                ShowMessage(16, 4);
            }
        }


        public string PlanID = "", PlanDate = "", ClientId = "", ClientName = "", Address = "", MobileNumber = "";
        public string Occupation = "", IsReference = "", ReferenceId = "", ReferenceClientName = "", RefAddress = "", RefMobileNumber = "";
        public string RefEmailId = "", SiteAddress = "", PlanName = "", AreaInFt = "", AreaInMeter = "", PlanRate = "";
        public string Amount = "", FilePath = "", PaymentComplete = "";

        public void ClearAll_PlanDetails()
        {
            PlanID = ""; PlanDate = ""; ClientId = ""; ClientName = ""; Address = ""; MobileNumber = ""; FilePath = ""; PaymentComplete = "";
            Occupation = ""; IsReference = ""; ReferenceId = ""; ReferenceClientName = ""; RefAddress = ""; RefMobileNumber = "";
            RefEmailId = ""; SiteAddress = ""; PlanName = ""; AreaInFt = ""; AreaInMeter = ""; PlanRate = ""; Amount = "";
        }

        
        public string Return_Remove_Space(string strName)
        {
            strName = strName.Replace(" ", "");
            return strName;
        }

        public string RegNumber = "";

        public void Return_Registration_Number(string DoctorName)
        {
            RegNumber = "";
            DataSet ds = new DataSet();
            objBL.Query = "select ID,DoctorName,Address,MobileNumber,Gender,RegistrationNumber,Qualification,Specialists from Doctor where DoctorName='" + DoctorName + "' and CancelTag=0";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["RegistrationNumber"].ToString()))
                    RegNumber = ds.Tables[0].Rows[0]["RegistrationNumber"].ToString();
            }
        }

        public void Fill_Occupation(ComboBox cmb)
        {
            objBL.Query = "select ID,OccupationCaption from Occupation where CancelTag=0";
            objBL.FillComboBox(cmb, "OccupationCaption", "ID");
        }

        public string Return_Date_String_DDMMYYYY(string dt)
        {
            DateTime d;
            d = Convert.ToDateTime(dt);
            return d.ToString("dd/MM/yyyy");
        }

        //public string Return_Date_String_MMDDYYYY(DateTime dt)
        //{
        //    string ReturnDate;
        //    ReturnDate = dt.ToString(RedundancyLogics.SystemDateFormat);
        //    return ReturnDate;
        //}

        //public DateTime Return_Date_DDMMYYYY(string DateString)
        //{
        //    DateTime ReturnDate;
        //    string DDMMYYYY_String = DateTime.ParseExact(DateString, "dd/MM/yyyy", null).ToString(RedundancyLogics.SystemDateFormat);
        //    ReturnDate = Convert.ToDateTime(DDMMYYYY_String);
        //    return ReturnDate;
        //}

        //public DateTime Return_Date_MMDDYYYY(string DateString)
        //{
        //    DateTime ReturnDate;
        //    string MMDDYYYY_String = DateTime.ParseExact(DateString, RedundancyLogics.SystemDateFormat, null).ToString("dd/MM/yyyy");
        //    ReturnDate = Convert.ToDateTime(MMDDYYYY_String);
        //    return ReturnDate;
        //}

        //public string Return_Date_String_DDMMYYYY_withTime(DateTime dt)
        //{
        //    string ReturnDate;
        //    ReturnDate = dt.ToString("dd-MM-yyyy hh:mm:ss tt");
        //    return ReturnDate;
        //}

        //public DateTime AllDate(string DateString)
        //{
        //    return DateTime.Parse(DateString, new CultureInfo("en-CA"));
        //}

        public void Fill_Doctor(ComboBox cmb)
        {
            objBL.Query = "select ID,DoctorName from Doctor where CancelTag=0";
            objBL.FillComboBox(cmb, "DoctorName", "ID");
        }

        public string GetPath(string KeyName)
        {
            string RPath = "";
            RPath = ConfigurationManager.AppSettings["ServerPath"];

            if (!string.IsNullOrEmpty(RPath))
                RPath = RPath + ConfigurationManager.AppSettings[KeyName];

            return RPath;
        }

        public double BMI_Value_RL = 0, eGFRValue_RL = 0;
        public int CalculateYear_Age_RL = 0;

        public void CalculateBMI_Value(string Weight_F, string Height_F)
        {
            double HeightValue = 0, WeightValue = 0;

            if (Weight_F != "" && Height_F != "")
            {
                HeightValue = 0; WeightValue = 0;
                double.TryParse(Height_F, out HeightValue);
                double.TryParse(Weight_F, out WeightValue);
                HeightValue = HeightValue / 100;
                BMI_Value_RL = (WeightValue / (HeightValue * HeightValue));
                BMI_Value_RL = Math.Round(BMI_Value_RL, 2);

                if (Sex_Static == "Female")
                {
                    if (RedundancyLogics.SerumCreatinine != 0)
                        eGFRValue_RL = Convert.ToDouble(Convert.ToDouble((140 - Convert.ToInt32(CalculateYear_Age_RL)) * WeightValue) / (72 * RedundancyLogics.SerumCreatinine)) * 0.85;
                    else
                        eGFRValue_RL = 0;
                }
                else
                {
                    if (RedundancyLogics.SerumCreatinine != 0)
                        eGFRValue_RL = Convert.ToDouble(Convert.ToDouble((140 - Convert.ToInt32(CalculateYear_Age_RL)) * WeightValue) / (72 * RedundancyLogics.SerumCreatinine));
                    else
                        eGFRValue_RL = 0;
                }

                if (eGFRValue_RL != 0)
                    eGFRValue_RL = Math.Round(eGFRValue_RL, 2);
                else
                    eGFRValue_RL = 0;
            }
        }

        public int ReturnMaxID(string TableName)
        {
            int Maxid = 0;
            objBL.Query = "select Max(ID) from " + TableName + "";
            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                    Maxid = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            Maxid += 1;
            return Maxid;
        }

        public int ReturnMaxID_Fix(string TableName)
        {
            int Maxid = 0;
            objBL.Query = "select Max(ID) from " + TableName + "";
            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                    Maxid = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        
            return Maxid;
        }

        public void Fill_Staff(string Desgnation,ComboBox cmb)
        {
            DataSet ds = new DataSet();
            objBL.Query = "select S.ID,S.DesignationId,S.FullName,S.Gender,S.DOB,S.Age,S.BloodGroup,S.CurrentAddress,S.AsAbove,S.PermenentAddress,S.MobileNo1,S.MobileNo2,S.EmailId,S.Qualification,S.RegNo,S.Speciality,S.Experience,S.DateOfJoining from Staff S inner join DesignationMaster D on D.ID=S.DesignationId where S.CancelTag=0 and D.Designation='" + Desgnation + "'";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = ds.Tables[0];
                cmb.DisplayMember = "FullName";
                cmb.ValueMember = "ID";
            }
        }

        public int Fill_Staff_DesignationID(string Desgnation)
        {
            int DesignationId = 0;
            DataSet ds = new DataSet();
            objBL.Query = "select ID,Designation from DesignationMaster where CancelTag=0 and Designation='" + Desgnation + "'";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                DesignationId = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            else
                DesignationId = 0;

            return DesignationId;
        }

        public void Fill_Company(ComboBox cmb)
        {
            objBL.Query = "select ID,CompanyName from CompanyMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "CompanyName", "ID");
        }

        public void ClearExcelPath()
        {
            RL_ExcelFormatPath = "";
            RL_DestinationPath = "";
            Form_ReportFileName = "";
            Form_DestinationReportFilePath = "";
            Form_ExcelFileName = "";
            isPDF = false;
        }

        public string RL_ExcelFormatPath = "";
        public string RL_DestinationPath = "";

        public string Form_ReportFileName = "";
        public string Form_DestinationReportFilePath = "";
        public string Form_ExcelFileName = "";
        public bool isPDF = false;
        string CurrentDate_String = DateTime.Now.Date.ToString("dd-MM-yyyy");

        public string SetDateFormat_ForReport = "dd-MM-yyyy";
        public string SetDateFormat = "dd/MM/yyyy";

        public void Path_Comman()
        {
            if (isPDF == true)
            {
                if (!string.IsNullOrEmpty(Form_ExcelFileName))
                {
                    RL_ExcelFormatPath = GetPath("ExcelFormat") + Form_ExcelFileName;
                    FileInfo objFIExcel = new FileInfo(RL_ExcelFormatPath);
                    RL_DestinationPath = GetPath("ReportPath") + Form_DestinationReportFilePath + CurrentDate_String + @"\";
                    DirectoryInfo DI = new DirectoryInfo(RL_DestinationPath);
                    DI.Create();
                    RL_DestinationPath += Form_ReportFileName + CurrentDate_String + ".xlsx";

                    FileInfo objFIDelete = new FileInfo(RL_DestinationPath);
                    if (objFIDelete.Exists == true)
                        objFIDelete.Delete();
                    objFIExcel.CopyTo(RL_DestinationPath);
                }
            }
            else
            {
                RL_DestinationPath = GetPath("ReportPath") + Form_DestinationReportFilePath + CurrentDate_String + @"\";
                DirectoryInfo DI = new DirectoryInfo(RL_DestinationPath);
                DI.Create();
            }
        }

        public string String_To_Date(string CDate)
        {
            DateTime dt = Convert.ToDateTime(CDate);
            return dt.ToString(SetDateFormat);
        }

        public void  DeleteExcelFile()
        {
            if (!string.IsNullOrEmpty(RL_DestinationPath))
            {
                FileInfo fiDelete = new FileInfo(RL_DestinationPath);
                fiDelete.Delete();
            }
        }

        public string PDFFilePath = "";

        public string DrugScheduleReturn(string SendDose)
        {
            string ReturnDose = "";

            if (SendDose == "1-0-0")
                ReturnDose = "सकाळी १";
            else if (SendDose == "1-0-1")
                ReturnDose = "सकाळी १, रात्री १";
            else if (SendDose == "1-1-1")
                ReturnDose = "सकाळी १, दुपारी १, रात्री १";
            else if (SendDose == "1-1-0")
                ReturnDose = "सकाळी १, दुपारी १";
            else if (SendDose == "0-0-1")
                ReturnDose = "रात्री १";
            else if (SendDose == "0-1-0")
                ReturnDose = "दुपारी १";
            else if (SendDose == "0-1-0")
                ReturnDose = "सकाळी १, दुपारी १, संध्याकाळी १, रात्री १";
            else if (SendDose == "1-1-1-1-1")
                ReturnDose = "दार दोन तासाने १";
            else if (SendDose == "SOS")
                ReturnDose = "गरज वाटेल तेव्हा";

            return ReturnDose;
        }

        public string DurationScheduleReturn(string SendDose)
        {
            string ReturnDose = "";

            if (SendDose == "X 1 Day.")
                ReturnDose = "१ दिवस";
            else if (SendDose == "X 2 Days.")
                ReturnDose = "२ दिवस";
            else if (SendDose == "X 3 Days.")
                ReturnDose = "३ दिवस";
            else if (SendDose == "X 5 Days.")
                ReturnDose = "५ दिवस";
            else if (SendDose == "X 7 Days.")
                ReturnDose = "७ दिवस";
            else if (SendDose == "X 10 Days.")
                ReturnDose = "१० दिवस";
            else if (SendDose == "X 15 Days.")
                ReturnDose = "१५ दिवस";
            else if (SendDose == "X 3 weeks.")
                ReturnDose = "३ आठवडे";
            else if (SendDose == "X 7 Days.")
                ReturnDose = "७ दिवस";
            else if (SendDose == "X 1 1/2 month.")
                ReturnDose = "१/२ महिना";
            else if (SendDose == "X 2 months.")
                ReturnDose = "२ महिने";
            else if (SendDose == "X 3 months.")
                ReturnDose = "३ महिने";
            else if (SendDose == "X 6 months.")
                ReturnDose = "६ महिने";
            else if (SendDose == "X 6 months.")
                ReturnDose = "६ महिने";

            return ReturnDose;
        }

        public string RemarkReturn(string SendDose)
        {
            string ReturnDose = "";

            if (SendDose == "After Breakfast")
                ReturnDose = "नाश्त्यानंतर";
            else if (SendDose == "After Food")
                ReturnDose = "जेवल्यानंतर";
            else if (SendDose == "After Dinner")
                ReturnDose = "रात्री जेवल्यानंतर";
            else if (SendDose == "After Lunch")
                ReturnDose = "दुपारी जेवल्यानंतर";
            else if (SendDose == "Before Breakfast")
                ReturnDose = "नाश्त्याच्या आधी";
            else if (SendDose == "Before Food")
                ReturnDose = "जेवणाच्या आधी";
            else if (SendDose == "Before Dinner")
                ReturnDose = "रात्री जेवणाच्या आधी";
            else if (SendDose == "Before Lunch")
                ReturnDose = "दुपारी जेवणाच्या आधी";
            else if (SendDose == "On Empty Stomach")
                ReturnDose = "अनोश्या पोटी";
            else if (SendDose == "One Hour Before Food")
                ReturnDose = "१ तास जेवणाच्या आधी";
            else if (SendDose == "One Hour Before Sleep")
                ReturnDose = "१ तास झोपायच्या आधी";
            else if (SendDose == "15 min Before Food")
                ReturnDose = "जेवायच्या आधी १५ मिनिटे";
            else if (SendDose == "As and When Required")
                ReturnDose = "जास्त गरज वाटेल तेव्हा";

            return ReturnDose;
        }

        public string RoundUp_Value(string Value1)
        {
            return Convert.ToString(Convert.ToDouble(Math.Round(Convert.ToDouble(Value1))));
        }

        public void RL_Fill_Patient_ListBox(ListBox lb, string SearchText)
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,PatientRegistrationNumber,EntryDate,Salutation+' '+ FirstName+' '+ MiddleName+' '+ LastName as FullName,Sex,DOB,DrugAllergies,Age,IfYesToWhatDrugs,Occupation,Address,Email,MobileNumber,UserId from Patient where CancelTag=0 and  LastName like '%" + SearchText + "%' or FirstName like '%" + SearchText + "%' or MiddleName like '%" + SearchText + "%' order by FirstName desc";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "FullName";
                lb.ValueMember = "ID";
            }
        }

        public void Save_Category_Name(string CategoryName)
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                DataSet ds = new DataSet();
                objBL.Query = "select Category from CategoryMaster where Category='" + CategoryName + "'";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                    return;
                else
                {
                    objBL.Query = "insert into CategoryMaster(Category) Values('" + CategoryName + "')";
                    objBL.Function_ExecuteNonQuery();

                }
            }
        }

        public void Save_Company_Name(string Category)
        {
            if (!string.IsNullOrEmpty(Category))
            {
                DataSet ds = new DataSet();
                objBL.Query = "select CompanyName from CompanyMaster where CompanyName='" + Category + "'";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                    return;
                else
                {
                    objBL.Query = "insert into CompanyMaster(CompanyName) Values('" + Category + "')";
                    objBL.Function_ExecuteNonQuery();

                }
            }
        }

        public void Save_UnitOfMessurement(string UOM)
        {
            if (!string.IsNullOrEmpty(UOM))
            {
                DataSet ds = new DataSet();
                objBL.Query = "select UOM from UOMMaster where UOM='" + UOM  + "'";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                    return;
                else
                {
                    objBL.Query = "insert into UOMMaster(UOM) Values('" + UOM + "')";
                    objBL.Function_ExecuteNonQuery();

                }
            }
        }

        public double Price = 0, Cost = 0, ProfitMarginPer = 0, ProfitMarginAmount = 0;
       
        public void Calculate_ProfitMargin()
        {
            //profitMargin = ((_Price - _Cost) / _Price) * 100;
            
            //Price = 0; Cost = 0; ProfitMarginPer = 0; ProfitMarginAmount = 0;

            if (Price < Cost)
            {
                ShowMessage(23, 4);
                return;
            }
            else
            {
                ProfitMarginPer = Convert.ToDouble(Math.Round(Convert.ToDecimal(((Price - Cost) / Price) * 100),2));
                ProfitMarginAmount = Convert.ToDouble(Math.Round(Convert.ToDecimal(Price - Cost)));
            }
        }

        public void Fill_CategoryName(ComboBox cmb)
        {
            cmb.DataSource = null;
            DataSet ds = new DataSet();
            objBL.Query = "select Category from CategoryMaster";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = ds.Tables[0];
                cmb.DisplayMember = "Category";
            }
        }

        public void Fill_CompanyName(ComboBox cmb)
        {
            cmb.DataSource = null;
            DataSet ds = new DataSet();
            objBL.Query = "select CompanyName from CompanyMaster";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = ds.Tables[0];
                cmb.DisplayMember = "CompanyName";
            }
        }

        public void Fill_UOMName(ComboBox cmb)
        {
            cmb.DataSource = null;
            DataSet ds = new DataSet();
            objBL.Query = "select UOM from UOMMaster";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = ds.Tables[0];
                cmb.DisplayMember = "UOM";
            }
        }

        public bool CheckPrimaryAccount()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from BankDetails where PrimaryAccount='Yes'";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        public string BankName_Report = "", BankAddress_Report = "", AccountNumber_Report = "", AccountType_Report = "", AccountHolderName_Report = "", IFSCCode_Report = "";
        public void Account()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,PrimaryAccount,BankName,BankAddress,AccountNumber,AccountType,AccountHolderName,IFSCCode from BankDetails where PrimaryAccount='Yes'";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                BankName_Report = Convert.ToString(ds.Tables[0].Rows[0]["BankName"]);
                BankAddress_Report = Convert.ToString(ds.Tables[0].Rows[0]["BankAddress"]);
                AccountNumber_Report = Convert.ToString(ds.Tables[0].Rows[0]["AccountNumber"]);
                AccountType_Report = Convert.ToString(ds.Tables[0].Rows[0]["AccountType"]);
                AccountHolderName_Report = Convert.ToString(ds.Tables[0].Rows[0]["AccountHolderName"]);
                IFSCCode_Report = Convert.ToString(ds.Tables[0].Rows[0]["IFSCCode"]);
            }
        }

        public bool CheckExistFlag = false;
        public bool Checks_Record_Allready_Exist(int Id, string TableName)
        {
            if (TableName == "Supplier")
            {
                DataSet ds = new DataSet();
                objBL.Query = "select P.ID,P.SupplierId from Purchase P inner join Supplier S on S.ID=P.SupplierId where S.CancelTag=0 and P.CancelTag=0 and P.SupplierId=" + Id + "";

                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CheckExistFlag = true;
                }
                else
                    CheckExistFlag = false;

                return CheckExistFlag;
            }
            else if (TableName == "Item")
            {
                DataSet ds = new DataSet();
                objBL.Query = "select P.ID,P.ItemId from PurchaseTransaction P inner join Item I on I.ID=P.ItemId where I.CancelTag=0 and P.CancelTag=0 and P.ItemId=" + Id + "";

                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CheckExistFlag = true;
                }
                else
                    CheckExistFlag = false;

                return CheckExistFlag;
            }
            else if (TableName == "Customer")
            {
                DataSet ds = new DataSet();
                objBL.Query = "select P.ID,P.CustomerId from Sale P inner join Customer I on I.ID=P.CustomerId where I.CancelTag=0 and P.CancelTag=0 and P.CustomerId=" + Id + "";

                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CheckExistFlag = true;
                }
                else
                    CheckExistFlag = false;

                return CheckExistFlag;
            }
            else
                return CheckExistFlag;
        }

        public double Check_NullString(string Value)
        {
            double ReturnValue = 0;
            if (!string.IsNullOrEmpty(Convert.ToString(Value)))
                ReturnValue = Convert.ToDouble(Value);

            return ReturnValue;
        }

        public string Check_Null_String(string Value)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Value)))
                return Value;
            else
                return "";
        }

        public string Check_Null_String_RetrunZero(string Value)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Value)))
                return Value;
            else
                return "0";
        }

        public double qty_gst = 0;
        public void Calculate_GST(double Amount, double CGST, double SGST, double IGST)
        {
            CGSTAmount = (Amount * CGST) / 100;
            SGSTAmount = (Amount * SGST) / 100;
            IGSTAmount = (Amount * IGST) / 100;


            //Direct including GST

            //double GST = 0;
            //GST = Amount - (Amount * (100 / (100 + CGST + SGST)));

            //CGSTAmount = GST / qty_gst;
            //SGSTAmount = CGSTAmount;

            //SGSTAmount = Amount - (Amount * (100 / (100 + SGST)));
            //IGSTAmount = Amount - (Amount * (100 / (100 + IGST)));

        }

        public static double CGSTAmount = 0;
        public static double SGSTAmount = 0;
        public static double IGSTAmount = 0;

        public string CI_CompanyName = "", CI_Address = "", CI_ContactNo = "", CI_EmailId = "", CI_Website = "", CI_VAT = "", CI_CST = "", CI_GSTIN = "";
        public void FillCompanyData()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,CompanyName,Address,ContactNo,EmailId,WebSite,VAT,CST,GST,CGST,SGST,IGST from CompanyInformation where CancelTag=0 and ID="+ CI_CompanyId+"";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                CI_CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                CI_Address = ds.Tables[0].Rows[0]["Address"].ToString();
                CI_ContactNo = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                CI_EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                CI_Website = ds.Tables[0].Rows[0]["WebSite"].ToString();
                CI_VAT = ds.Tables[0].Rows[0]["VAT"].ToString();
                CI_CST = ds.Tables[0].Rows[0]["CST"].ToString();
                CI_GSTIN = ds.Tables[0].Rows[0]["GST"].ToString();

                if(!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CGST"])))
                    CGSTPer = Convert.ToDouble(ds.Tables[0].Rows[0]["CGST"].ToString());

                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["SGST"])))
                    SGSTPer = Convert.ToDouble(ds.Tables[0].Rows[0]["SGST"].ToString());

                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["IGST"])))
                    IGSTPer = Convert.ToDouble(ds.Tables[0].Rows[0]["IGST"].ToString());
            }
        }

        public double CGSTPer = 0, SGSTPer = 0, IGSTPer = 0;
        public string ID_Customer = "", CustomerName_Customer = "", DeliveryAddress_Customer = "", BillingAddress_Customer = "", ContactNumber_Customer = "", EmailId_Customer = "", VAT_Customer = "", CST_Customer = "", GST_Customer = "";
        
        public void GetCustomerRecords(int CustomerId)
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,CustomerName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Customer where CancelTag=0 and ID=" + CustomerId + " order by CustomerName desc";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ID_Customer = ds.Tables[0].Rows[0]["ID"].ToString();
                CustomerName_Customer = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                DeliveryAddress_Customer = ds.Tables[0].Rows[0]["Address"].ToString();
                BillingAddress_Customer = ds.Tables[0].Rows[0]["Address"].ToString();
                ContactNumber_Customer = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                EmailId_Customer = ds.Tables[0].Rows[0]["EmailId"].ToString();
                //VAT_Customer = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
                CST_Customer = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
                //GST_Customer = ds.Tables[0].Rows[0]["GST"].ToString();

                
            }
        }

        public string ID_Supplier_S = "", SupplierName_RL_S = "", Address_RL_S = "", MobileNumber_RL_S = "", EmailId_RL_S = "", GSTNumber_RL_S = "";
        
        public void GetSupplierRecords(int SupplierId_F)
        {
            DataSet ds = new DataSet();
            //objBL.Query = "select ID,CustomerName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Customer where CancelTag=0 and ID=" + CustomerId + " order by CustomerName desc";
            objBL.Query = "select ID,SupplierName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Supplier where CancelTag=0 and ID=" + SupplierId_F + " order by SupplierName desc";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ID_Supplier_S = ds.Tables[0].Rows[0]["ID"].ToString();
                SupplierName_RL_S = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                Address_RL_S = ds.Tables[0].Rows[0]["Address"].ToString();
                MobileNumber_RL_S = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                EmailId_RL_S = ds.Tables[0].Rows[0]["EmailId"].ToString();
                GSTNumber_RL_S = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
            }
        }

        public DateTime InvoiceDate_Bill, DateOfSupply_Bill, TransactionDate_Bill;
        public string ID_Bill = "", DCNo_Bill = "", JobNo_Bill = "", BillNo_Bill = "", PONo_Bill = "", CustomerId_Bill = "", CustomerName_Bill = "", NoteD_Bill = "", TaxPaybleOnReverseCharge_Bill = "", Total_Bill = "", FreightCharges_Bill = "", LoadingAndPackingCharges_Bill = "", InsuranceCharges_Bill = "", OtherCharges_Bill = "", InvoiceTotal_Bill = "", Naration_Bill = "", PaymentMode_Bill = "", BankId_Bill = "", BankName_Bill = "", AccountNumber_Bill = "", ChequeNumber_Bill = "", PartyBank_Bill = "", PartyBankAccountNumber_Bill = "", TransportationMode_Bill = "", VehicalNumber_Bill = "", PlaceOfSupply_Bill = "", BillStatus_Bill = "", PrintFlag_Bill = "", PrintCount_Bill = "";

        public void GetBill(int TableId)
        {
            DataSet dsDeliveryChallan = new DataSet();
            objBL.Query = "select DC.ID,DC.InvoiceDate,DC.DCNo,DC.JobNo,DC.BillNo,DC.PONo,DC.CustomerId,C.CustomerName,DC.NoteD,DC.TaxPaybleOnReverseCharge,DC.Total,DC.FreightCharges,DC.LoadingAndPackingCharges,DC.InsuranceCharges,DC.OtherCharges,DC.InvoiceTotal,DC.Naration,DC.PaymentMode,DC.BankId,DC.BankName,DC.AccountNumber,DC.TransactionDate,DC.ChequeNumber,DC.PartyBank,DC.PartyBankAccountNumber,DC.TransportationMode,DC.VehicalNumber,DC.DateOfSupply,DC.PlaceOfSupply,DC.BillStatus,DC.PrintFlag,DC.PrintCount from Sale DC inner join Customer C on C.ID=DC.CustomerId where DC.CancelTag=0 and C.CancelTag=0 and DC.Id=" + TableId + "";
            dsDeliveryChallan = objBL.ReturnDataSet();

            if (dsDeliveryChallan.Tables[0].Rows.Count > 0)
            {
                ID_Bill = dsDeliveryChallan.Tables[0].Rows[0]["ID"].ToString();
                InvoiceDate_Bill = Convert.ToDateTime(dsDeliveryChallan.Tables[0].Rows[0]["InvoiceDate"].ToString());
                DCNo_Bill = dsDeliveryChallan.Tables[0].Rows[0]["DCNo"].ToString();
                JobNo_Bill = dsDeliveryChallan.Tables[0].Rows[0]["JobNo"].ToString();
                BillNo_Bill = dsDeliveryChallan.Tables[0].Rows[0]["BillNo"].ToString();
                PONo_Bill = dsDeliveryChallan.Tables[0].Rows[0]["PONo"].ToString();
                CustomerId_Bill = dsDeliveryChallan.Tables[0].Rows[0]["CustomerId"].ToString();
                CustomerName_Bill = dsDeliveryChallan.Tables[0].Rows[0]["CustomerName"].ToString();
                NoteD_Bill = dsDeliveryChallan.Tables[0].Rows[0]["NoteD"].ToString();
                TaxPaybleOnReverseCharge_Bill = dsDeliveryChallan.Tables[0].Rows[0]["TaxPaybleOnReverseCharge"].ToString();
                Total_Bill = dsDeliveryChallan.Tables[0].Rows[0]["Total"].ToString();
                FreightCharges_Bill = dsDeliveryChallan.Tables[0].Rows[0]["FreightCharges"].ToString();
                LoadingAndPackingCharges_Bill = dsDeliveryChallan.Tables[0].Rows[0]["LoadingAndPackingCharges"].ToString();
                InsuranceCharges_Bill = dsDeliveryChallan.Tables[0].Rows[0]["InsuranceCharges"].ToString();
                OtherCharges_Bill = dsDeliveryChallan.Tables[0].Rows[0]["OtherCharges"].ToString();
                InvoiceTotal_Bill = dsDeliveryChallan.Tables[0].Rows[0]["InvoiceTotal"].ToString();
                //BankId_Bill = "", BankName_Bill = "", AccountNumber_Bill = "", ChequeNumber_Bill = "", PartyBank_Bill="",PartyBankAccountNumber_Bill="",
                
                Naration_Bill = dsDeliveryChallan.Tables[0].Rows[0]["Naration"].ToString();
                PaymentMode_Bill = dsDeliveryChallan.Tables[0].Rows[0]["PaymentMode"].ToString();
                TransactionDate_Bill = Convert.ToDateTime(dsDeliveryChallan.Tables[0].Rows[0]["TransactionDate"].ToString());
                BankId_Bill =  dsDeliveryChallan.Tables[0].Rows[0]["BankId"].ToString();
                BankName_Bill = dsDeliveryChallan.Tables[0].Rows[0]["BankName"].ToString();
                AccountNumber_Bill = dsDeliveryChallan.Tables[0].Rows[0]["AccountNumber"].ToString();
                TransactionDate_Bill = Convert.ToDateTime(dsDeliveryChallan.Tables[0].Rows[0]["TransactionDate"].ToString());
                ChequeNumber_Bill = dsDeliveryChallan.Tables[0].Rows[0]["ChequeNumber"].ToString();
                PartyBank_Bill = dsDeliveryChallan.Tables[0].Rows[0]["PartyBank"].ToString();
                PartyBankAccountNumber_Bill = dsDeliveryChallan.Tables[0].Rows[0]["PartyBankAccountNumber"].ToString();

                TransportationMode_Bill = dsDeliveryChallan.Tables[0].Rows[0]["TransportationMode"].ToString();
                VehicalNumber_Bill = dsDeliveryChallan.Tables[0].Rows[0]["VehicalNumber"].ToString();
                DateOfSupply_Bill = Convert.ToDateTime(dsDeliveryChallan.Tables[0].Rows[0]["DateOfSupply"].ToString());
                PlaceOfSupply_Bill = dsDeliveryChallan.Tables[0].Rows[0]["PlaceOfSupply"].ToString();
                BillStatus_Bill = dsDeliveryChallan.Tables[0].Rows[0]["BillStatus"].ToString();
                PrintFlag_Bill = dsDeliveryChallan.Tables[0].Rows[0]["PrintFlag"].ToString();
                PrintCount_Bill = dsDeliveryChallan.Tables[0].Rows[0]["PrintCount"].ToString();
            }
        }

        public string words(int numbers)
        {
            int number = numbers;
            if (number == 0) return "Zero Only";
            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
"Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
"Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
"Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    //if (h > 0 || i == 0) sb.Append("and ");
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd() + " Only";
        }


        public string Category = string.Empty, CompanyName = string.Empty, ItemName = string.Empty, BatchNumber = string.Empty, HSNCode = string.Empty, Contain = string.Empty, UOM = string.Empty;
        public string OutputLabel = string.Empty;

        public void Set_Label()
        {
            OutputLabel = //StringFormatSet("Category:", Category) + "\n" +
                          StringFormatSet("Manufracturer Name:", CompanyName) + "\n" +
                          StringFormatSet("Item Name:", ItemName) + "\n" +
                          StringFormatSet("Batch Number:", BatchNumber) + "\n" +
                          StringFormatSet("HSN Code:", HSNCode) + "\t" +StringFormatSet("UOM:", UOM);
        }

        public string StringFormatSet(string Value1,string Value2)
        {
            return String.Format("{0}\t{1}", Value1, Value2);
        }

        public double RoundUp_Function(string Value1)
        {
            double ValueRoundUp = 0;
            if (!string.IsNullOrEmpty(Value1))
            {
                ValueRoundUp = Convert.ToDouble(Value1);
                ValueRoundUp = Math.Round(ValueRoundUp, 2);
            }
            return ValueRoundUp;
        }

        public int ItemID_RL=0;
        public void Get_Item_Value(RichTextBox rtb)
        {
            DataSet ds = new DataSet();
           // objBL.Query = "select ID,Category,ManufracturerName,ItemName,BatchNumber,HSNCode,UOM,Price,Cost,MRP,ProfitMarginPer,ProfitMarginAmount,CGST,SGST,IGST from Item where CancelTag=0 and ID=" + ItemID_RL + "";
            objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Cost,I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ID=" + ItemID_RL + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                 
                //Category = ds.Tables[0].Rows[0]["Category"].ToString();
                CompanyName = ds.Tables[0].Rows[0]["ManufracturerName"].ToString();
                ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
                BatchNumber = ds.Tables[0].Rows[0]["BatchNumber"].ToString();
                HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                //Contain = ds.Tables[0].Rows[0]["Contain"].ToString();
                UOM = ds.Tables[0].Rows[0]["UOM"].ToString();
                Set_Label();
                rtb.Text = OutputLabel;
            }
        }

        public DateTime ExpiryDate_RL;
        public double AvailbleQuantity_RL = 0;

        public void AvailbleQuantity(int ItemId)
        {
            AvailbleQuantity_RL = 0;
            double PurchaseQuantity = 0, SalesQuantity = 0, AvailableQuantity = 0;
            DataSet dsPQ = new DataSet();
            objBL.Query = "select Quantity from ItemQuantity where CancelTag=0 and ItemID=" + ItemId + "";
            dsPQ = objBL.ReturnDataSet();
            if (dsPQ.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dsPQ.Tables[0].Rows[0][0].ToString()))
                {
                    PurchaseQuantity = Convert.ToDouble(dsPQ.Tables[0].Rows[0]["Quantity"].ToString());
                    //ExpiryDate_RL = Convert.ToDateTime(dsPQ.Tables[0].Rows[0]["ExpiryDate"].ToString());
                }
                else
                    PurchaseQuantity = 0;
            }
            else
                PurchaseQuantity = 0;

            //DataSet dsSQ = new DataSet();
            //objBL.Query = "select ExpiryDate,SaleQuantity from ItemSaleQuantity where CancelTag=0 and ItemID=" + ItemId + " and ExpiryDate > #" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "#";
            //dsSQ = objBL.ReturnDataSet();
            //if (dsSQ.Tables[0].Rows.Count > 0)
            //{
            //    if (!string.IsNullOrEmpty(dsSQ.Tables[0].Rows[0][0].ToString()))
            //        SalesQuantity = Convert.ToDouble(dsSQ.Tables[0].Rows[0]["SaleQuantity"].ToString());
            //    else
            //        SalesQuantity = 0;
            //}
            //else
            //    SalesQuantity = 0;

            AvailbleQuantity_RL = PurchaseQuantity ;
        }

        public int Return_Transaction_ID(string TableName)
        {
            int MaxID = 0;
            DataSet ds = new DataSet();
            objBL.Query = "select max(ID) from " + TableName + " where CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                    MaxID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            return MaxID;
        }

        public static string AccountNo = "";
        public static string BankAddress = "";
        public static string AccountType = "";
        public static string AccountHolderName = "";
        public static string IFSCCode = "";
        public static int BankID;
      
        public void GetBankDetails(int BankID)
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,BankName,BankAddress,AccountNumber,AccountType,AccountHolderName,IFSCCode from BankDetails where CancelTag=0 and ID=" + BankID + " ";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                BankID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                AccountNo = ds.Tables[0].Rows[0]["AccountNumber"].ToString();
                BankAddress = ds.Tables[0].Rows[0]["BankAddress"].ToString();
                AccountType = ds.Tables[0].Rows[0]["AccountType"].ToString();
                AccountHolderName = ds.Tables[0].Rows[0]["AccountHolderName"].ToString();
                IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
            }
        }

        public void GetBank(ComboBox cmb)
        {
            cmb.DataSource = null;
            DataSet ds = new DataSet();
            objBL.Query = "select ID,BankName,BankAddress,AccountNumber,AccountType,AccountHolderName,IFSCCode from BankDetails where CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = ds.Tables[0];
                cmb.DisplayMember = "BankName";
                cmb.ValueMember = "ID";
            }
        }

        public bool PendingFlag = false;
        
        public double Get_Pending_Details(string TableName, int ID)
        {
            double PendingAmount = 0;
            DataSet ds = new DataSet();
            if(TableName == "SupplierPendingAmount")
                objBL.Query = "select ID,SupplierId,PendingAmount from " + TableName + " where CancelTag=0 and SupplierId=" + ID + " ";
            else
                objBL.Query = "select ID,CustomerId,PendingAmount from " + TableName + " where CancelTag=0 and CustomerId=" + ID + " ";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ID"].ToString()))
                {
                    PendingFlag = true;
                    PendingAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["PendingAmount"].ToString());
                }
                else
                    PendingFlag = false;
            }
            else
                PendingFlag = false;

            return PendingAmount;
        }

        string PaymentMode = string.Empty;

        public void Set_PaymentMode_Details(ComboBox cmb, GroupBox gbChequeDetails, Label lblChequeNo, TextBox txtChequeNo)
        {
            //BANK DEPOSIT
            //CASH
            //CHEQUE
            //CREDIT
            //IMPS
            //MOBILE BANKING
            //NEFT
            //RTGS

            //AMAZON PAY
            //BANK DEPOSIT
            //CASH
            //CHARITY
            //CHEQUE
            //CREDIT
            //GOOGLE PAY
            //IMPS
            //MOBILE BANKING
            //NEFT
            //PHONE PAY
            //RTGS

            if (cmb.SelectedIndex > -1)
            {
                gbChequeDetails.Visible = false;
                lblChequeNo.Visible = false;
                txtChequeNo.Visible = false;
                txtChequeNo.Text = "";
                PaymentMode = cmb.Text;
                switch (PaymentMode)
                {
                    case "CASH":
                        gbChequeDetails.Visible = false;
                        break;
                    case "CREDIT":
                        gbChequeDetails.Visible = false;
                        break;
                    case "CHEQUE":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "Cheque Details";
                        lblChequeNo.Visible = true;
                        txtChequeNo.Visible = true;
                        break;
                    case "NEFT":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "NEFT Details";
                        break;
                    case "RTGS":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "RTGS Details";
                        break;
                    case "BANK DEPOSIT":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "Bank Deposite Details";
                        break;
                    case "IMPS":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "IMPS Details";
                        break;
                    case "MOBILE BANKING":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "MOBILE BANKING Details";
                        break;
                    case "AMAZON PAY":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "MOBILE BANKING Details";
                        break;
                    case "GOOGLE PAY":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "GOOGLE PAY";
                        break;
                    case "PHONE PAY":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "PHONE PAY";
                        break;
                        
                }
            }
        }

        public string Return_UOM(int ID_UOM)
        {
            string UOMString = string.Empty;

            DataSet ds = new DataSet();
            objBL.Query = "select ID,UnitOfMessurement from UOM where ID=" + ID_UOM + " and CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                UOMString = Convert.ToString(ds.Tables[0].Rows[0][1].ToString());
            }

            return UOMString;
        }


        public string Return_Date_String_DDMMYYYY(DateTime dt)
        {
            string ReturnDate;
            ReturnDate = dt.ToString("dd-MM-yyyy");
            return ReturnDate;
        }

        public string Return_Date_String_MMDDYYYY(DateTime dt)
        {
            string ReturnDate;
            ReturnDate = dt.ToString("MM/dd/yyyy");
            return ReturnDate;
        }

        public void DBBackup()
        {
            string DateF = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            string FileName = "Backup-" + DateF + ".mdb";

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Access files (*.mdb)|*.mdb|All files (*.*)|*.*";

            // Feed the dummy name to the save dialog
            sf.FileName = FileName;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                string savePath = Path.GetDirectoryName(sf.FileName);
                string FilePath = GetPath("DBPath");

                FileInfo FIDBFile = new FileInfo(FilePath);
                FileName = "" + savePath + "\\" + FileName;

                if (FIDBFile.Exists == true)
                {
                    FileInfo fiNew = new FileInfo(FileName);
                    fiNew.Delete();
                    FIDBFile.CopyTo(FileName);
                }
                else
                    FIDBFile.CopyTo(FileName);

                MessageBox.Show("Database backup successfully");
            }
        }

        public DialogResult ReturnDialogResult_Report()
        {
            DialogResult dr;
            dr = MessageBox.Show(MessageString(28), MessageString(3), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return dr;
        }

        public string EmailId_RL = "", Subject_RL = "", Body_RL = "", FilePath_RL = "";
        public void SendEMail()
        {
            try
            {
                EmailValidations();
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                MailAddress fromAddress = new MailAddress(RedundancyLogics.EmailAddress_Static);
                message.From = fromAddress;
                message.To.Add(EmailId_RL);
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(FilePath_RL);
                message.Attachments.Add(attachment);
                message.Subject = Subject_RL;
                message.IsBodyHtml = true;
                message.Body = Body_RL;
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(EmailAddress_Static, EmailPassword_Static);
                smtpClient.Send(message);
                //ShowMessage(29, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("err: " + ex.Message);
            }
        }
    }
}
