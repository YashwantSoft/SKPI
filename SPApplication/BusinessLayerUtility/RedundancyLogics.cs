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
using System.Diagnostics;

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
        public static string DateFormatMMDDYYYY = "MM/dd/yyyy";

        public static string PR_String;
        public static string DE_String;
        public static string SaleReceipt;

        public static string ReportDetails;
        public static string ReportBatchNumber;

        public static DateTime ReportDate;

        public System.Globalization.RegionInfo objRegInfo = new RegionInfo("en-IN");

        //Apostrophe Save 
        public string ApostropheSave(string ValueField)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(ValueField)))
                ValueField = ValueField.Replace("'", "''");

            return ValueField;
        }

        public void Fill_CommanMaster(ComboBox cmb, string ColumnName)
        {
            objBL.Query = "select ID," + ColumnName + " from CommanMaster where " + ColumnName + " IS NOT NULL OR Trim(" + ColumnName + ")='' and CancelTag=0 order by ID asc";
            objBL.FillComboBox(cmb, ColumnName, "ID");
            cmb.SelectedIndex = -1;
        }

        public int ValidateEmailId(string emailId)
        {
            /*Regular Expressions for email id*/
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (emailId.Length > 0)
                if (!rEMail.IsMatch(emailId))
                    return 0;
                else
                    return 1;
            return 2;
        }

        public DialogResult Delete_Record_Show_Message()
        {
            DialogResult dr;
            return dr = MessageBox.Show("Do you want to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public DialogResult Logout_Record_Show_Message()
        {
            DialogResult dr;
            return dr = MessageBox.Show("Do you want to close your day and logout the application?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public DialogResult Switch_Product()
        {
            DialogResult dr;
            return dr = MessageBox.Show("Do you want to switch current Product as per Production Entry?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public void FillTableNo(ComboBox cmb)
        {
            objBL.Query = "select ID,TableNo from HotelTable where CancelTag=0";
            objBL.FillComboBox(cmb, "TableNo", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_UserFor(ComboBox cmb)
        {
            objBL.Query = "select ID,UserFor from UseForMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "UserFor", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_PlaceMaster(ComboBox cmb)
        {
            objBL.Query = "select ID,PlaceName from PlaceMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "PlaceName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void FillPreformParty(ComboBox cmb)
        {
            objBL.Query = "select ID,PreformParty from PreformPartyMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "PreformParty", "ID");
            cmb.SelectedIndex = -1;
        }

        public void FillEmailMainStatus(ComboBox cmb)
        {
            objBL.Query = "select ID,EmailStatus from ProductStatusMaster where CancelTag=0 and EmailStatus IS NOT NULL";
            objBL.FillComboBox(cmb, "EmailStatus", "ID");
            cmb.SelectedIndex = -1;
        }

        public void FillPreformParty_DGV(DataGridViewComboBoxColumn cmb)
        {
            objBL.Query = "select ID,PreformParty from PreformPartyMaster where CancelTag=0";
            objBL.FillComboBox_DGV(cmb, "PreformParty", "ID");
            //cmb.SelectedIndex = -1;
        }

        public void Fill_Users(ComboBox cmb)
        {
            objBL.Query = "select ID,UserName from Login where CancelTag=0";
            objBL.FillComboBox(cmb, "UserName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Users_CheckBox(CheckedListBox clb)
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,UserName from Login where CancelTag=0";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                clb.DataSource = ds.Tables[0];
                clb.DisplayMember = "UserName";
                clb.ValueMember = "ID";
            }
        }

        public void Fill_Product_Status(CheckedListBox clb)
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,Status from ProductStatusMaster where CancelTag=0 and Status IS NOT NULL";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                clb.DataSource = ds.Tables[0];
                clb.DisplayMember = "Status";
                clb.ValueMember = "ID";
            }
        }
        
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

        public void Fill_Mould(ComboBox cmb)
        {
            objBL.Query = "select ID,SrNo from MouldMaster where CancelTag=0 order by ID";
            objBL.FillComboBox(cmb, "SrNo", "ID");
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
            objBL.Query = "select ID,CustomerName from Customer where CancelTag=0 order by CustomerName asc";
            objBL.FillComboBox(cmb, "CustomerName", "ID");
            cmb.SelectedIndex = -1;
        }
        public void Fill_Employee(ComboBox cmb)
        {
            objBL.Query = "select ID,FullName from Employee where CancelTag=0 order by FullName asc";
            objBL.FillComboBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }

        public string   Get_Employee_Name_By_Id(int Id)
        {
            string FName = string.Empty;

            objBL.Query = "select ID,FullName +'-'+ Designation as FullName from Employee where CancelTag=0 and ID="+Id+"";
            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FullName"])))
                {
                    FName = Convert.ToString(ds.Tables[0].Rows[0]["FullName"].ToString());
                    FName = FName.Replace("\r\n", "");
                }
                else
                    FName = "";
            }
            return FName;
        }

        public void Fill_Employee_By_Designation(ComboBox cmb,string Designation)
        {
            objBL.Query = "select ID,FullName from Employee where CancelTag=0 and Designation='" + Designation + "'";
            objBL.FillComboBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Employee_By_Designation_Operator_Packer(ComboBox cmb)
        {
            objBL.Query = "select ID,FullName from Employee where CancelTag=0 and Designation IN('Operator','Packer') ";
            objBL.FillComboBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }

        public string Fill_Designation_By_EmployeeId(int EmployeeId)
        {
            string Designation = string.Empty;
            DataSet ds = new DataSet();
            objBL.Query = "select ID,FullName,Designation from Employee where CancelTag=0 and ID=" + EmployeeId + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Designation"].ToString())))
                    Designation = Convert.ToString(ds.Tables[0].Rows[0]["Designation"].ToString());
                
            }
            return Designation;
        }

        public void Fill_Designation(ComboBox cmb)
        {
            objBL.Query = "select ID,Designation from DesignationMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "Designation", "ID");
        }

        public void Fill_Designation_Distinct(ComboBox cmb)
        {
            objBL.Query = "select Distinct Designation from Employee where CancelTag=0";
            objBL.FillComboBox(cmb, "Designation", "Designation");
        }

        public void Fill_Designation_Distinct_Operator_Packer(ComboBox cmb)
        {
            objBL.Query = "select Distinct Designation from Employee where CancelTag=0 and Designation IN ('Operator','Packer')";
            objBL.FillComboBox(cmb, "Designation", "Designation");
        }

        public void Get_Machine_Details()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select MachineId,MachineNo,MachineDescription,MachineStatus from MachineMaster where CancelTag=0 and MachineId=" + MachineId + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if(!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MachineDescription"].ToString())))
                    MachineDescription =Convert.ToString(ds.Tables[0].Rows[0]["MachineDescription"].ToString());
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["MachineStatus"].ToString())))
                    MachineStatus = Convert.ToString(ds.Tables[0].Rows[0]["MachineStatus"].ToString());
            }
        }

        public string GetPath_WithoutServer(string KeyName)
        {
            string RPath = "";

            if (!string.IsNullOrEmpty(KeyName))
                RPath = ConfigurationManager.AppSettings[KeyName];

            return RPath;
        }

        private static string shift;
        public string Shift
        {
            get { return shift; }
            set { shift = value; }
        }

        private static string shifthours;
        public string ShiftHours
        {
            get { return shifthours; }
            set { shifthours = value; }
        }

        private static DateTime entrydate;
        public DateTime EntryDate
        {
            get { return entrydate; }
            set { entrydate = value; }
        }

        private static DateTime shiffromdate;
        public DateTime ShifFromDate
        {
            get { return shiffromdate; }
            set { shiffromdate = value; }
        }


        private static DateTime shifttodate;
        public DateTime ShiftToDate
        {
            get { return shifttodate; }
            set { shifttodate = value; }
        }

        private static int machineid;
        public int MachineId
        {
            get { return machineid; }
            set { machineid = value; }
        }

        private static int oeeid;
        public int OEEId
        {
            get { return oeeid; }
            set { oeeid = value; }
        }

        private static int shiftscheduleid;
        public int ShiftScheduleId
        {
            get { return shiftscheduleid; }
            set { shiftscheduleid = value; }
        }

        private static int oeemachineid;
        public int OEEMachineId
        {
            get { return oeemachineid; }
            set { oeemachineid = value; }
        }

        private static string machineno;
        public string MachineNo 
        {
            get { return machineno; }
            set { machineno = value; } 
        }

        private static string machinestatus;
        public string MachineStatus
        {
            get { return machinestatus; }
            set { machinestatus = value; }
        }

        private static string reportperiod;
        public string ReportPeriod
        {
            get { return reportperiod; }
            set { reportperiod = value; }
        }

        

        private static string machinedescription;
        public string MachineDescription
        {
            get { return machinedescription; }
            set { machinedescription = value; }
        }

        public void Fill_Machine(ComboBox cmb)
        {
            objBL.Query = "select MachineId,MachineNo from MachineMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "MachineNo", "MachineId");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Grade(ComboBox cmb)
        {
            objBL.Query = "select GradeId,Grade,Amount from GradeMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "Grade", "GradeId");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Grade_By_GradeDesignation(ComboBox cmb, string GradeDesignation)
        {
            objBL.Query = "select GradeId,Grade,Amount from GradeMaster where CancelTag=0 and GradeDesignation IN('" + GradeDesignation + "','All')";
            objBL.FillComboBox(cmb, "Grade", "GradeId");
            cmb.SelectedIndex = -1;
        }

        public double Get_Grade_Amount(string Grade)
        {
            double GradeAmount = 0;
            DataSet ds = new DataSet();
            objBL.Query = "select GradeId,Grade,Amount from GradeMaster where CancelTag=0 and Grade='" + Grade + "'";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GradeAmount = Check_Null_Double(Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Amount"])));
            }

            return GradeAmount;
        }

        

        public void Fill_Employee_ComboBox_OperatorPacker(ComboBox cmb)
        {
            //objBL.Query = "select ID,FullName +'-'+ Designation as FullName from Employee where CancelTag=0 and Designation IN('Operator','Packer')";
            objBL.Query = "select ID,FullName from Employee where CancelTag=0 and Designation IN('Operator','Packer','NA') order by FullName asc";
            objBL.FillComboBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Employee_CheckedListBox_By_Designation(CheckedListBox cmb, string Designation)
        {
            objBL.Query = "select ID,FullName from Employee where CancelTag=0 and Designation='" + Designation + "'  order by FullName asc";
            objBL.FillCheckListBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Employee_CheckedListBox_By_Designation_Auto(CheckedListBox cmb)
        {
            objBL.Query = "select ID,FullName from Employee where CancelTag=0 and Designation IN('Operator','Packer','NA')  order by FullName asc"; //='" + Designation + "'";
            objBL.FillCheckListBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }   

        public DataSet Fill_CheckListBox_DataSet(string Designation)
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,FullName from Employee where CancelTag=0 and Designation='" + Designation + "'  order by FullName asc";
            ds = objBL.ReturnDataSet();
            return ds;
        }

        public void Fill_Employee_ComboBox_OperatorPacker_Not_In_Id(ComboBox cmb, List<int> Eid)
        {
            string Eid_Set = string.Empty;
            if (Eid.Count > 0)
            {
                for (int i = 0; i < Eid.Count; i++)
                {
                    Eid_Set +=  ""+Eid[i].ToString()+"," ;
                }
                Eid_Set = Eid_Set.Remove(Eid_Set.Length - 1);
            }
            objBL.Query = "select ID,FullName +'-'+ Designation as FullName from Employee where CancelTag=0 and ID NOT IN(" + Eid_Set + ") and Designation IN('Operator','Packer') order by FullName asc";
            objBL.FillComboBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }


        public void Fill_Employee_ComboBox_Supervisor_Not_In_Id(ComboBox cmb, int[] Eid)
        {
            string Eid_Set = string.Empty;
            if (Eid != null)
            {
                if (Eid.Length > 0)
                {
                    for (int i = 0; i < Eid.Length; i++)
                    {
                        Eid_Set += "" + Eid[i].ToString() + ",";
                    }
                    Eid_Set = Eid_Set.Remove(Eid_Set.Length - 1);
                }
                //objBL.Query = "select ID,FullName +'-'+ Designation as FullName from Employee where CancelTag=0 and ID NOT IN(" + Eid_Set + ") and Designation IN('Supervisor')";
                objBL.Query = "select ID,FullName from Employee where CancelTag=0 and ID NOT IN(" + Eid_Set + ") and Designation IN('Supervisor')";
            }
            else
            {
                //objBL.Query = "select ID,FullName +'-'+ Designation as FullName from Employee where CancelTag=0 and Designation IN('Supervisor')";
                objBL.Query = "select ID,FullName from Employee where CancelTag=0 and Designation IN('Supervisor')";
            }
            objBL.FillComboBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Employee_CheckedListBox_By_OperatorPacker(CheckedListBox cmb)
        {
            objBL.Query = "select ID,FullName +'-'+ Designation as FullName from Employee where CancelTag=0 and Designation IN('Operator','Packer')";
            objBL.FillCheckListBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_Employee_CheckedListBox_By_OperatorPacker(CheckedListBox cmb, List<int> Eid)
        {
            string Eid_Set = string.Empty;
            if (Eid.Count > 0)
            {
                for (int i = 0; i < Eid.Count; i++)
                {
                    Eid_Set += "" + Eid[i].ToString() + ",";
                }
                Eid_Set = Eid_Set.Remove(Eid_Set.Length - 1);
            }
           // objBL.Query = "select ID,FullName +'-'+ Designation as FullName from Employee where CancelTag=0 and Designation IN('Operator','Packer')";
            objBL.Query = "select ID,FullName +'-'+ Designation as FullName from Employee where CancelTag=0 and ID NOT IN(" + Eid_Set + ") and Designation IN('Operator','Packer')";
            objBL.FillCheckListBox(cmb, "FullName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_CollectionType(ComboBox cmb)
        {
            objBL.Query = "select ID,CollectionType from CollectionTypeMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "CollectionType", "ID");
            cmb.SelectedIndex = -1;
        }
        public void Fill_Events(ComboBox cmb)
        {
            objBL.Query = "select ID,EventName from EventMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "EventName", "ID");
        }
        public void Fill_Package(ComboBox cmb)
        {
            objBL.Query = "select ID,Package from PackageMaster where CancelTag=0";
            objBL.FillComboBox(cmb, "Package", "ID");
        }
        public void Fill_Item(ComboBox cmb)
        {
            objBL.Query = "select ID,ItemName from Item where CancelTag=0";
            objBL.FillComboBox(cmb, "ItemName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_ProductName(ComboBox cmb)
        {
            objBL.Query = "select ID,ProductName from Product where CancelTag=0 order by ID";
            objBL.FillComboBox(cmb, "ProductName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_PreformName(ComboBox cmb)
        {
            objBL.Query = "select ID,PreformName from Preform where CancelTag=0 order by ID";
            objBL.FillComboBox(cmb, "PreformName", "ID");
            cmb.SelectedIndex = -1;
        }

        public void Fill_WadFitterNames(ComboBox cmb)
        {
            objBL.Query = "select ID,WadFitterName from WadFitter";
            objBL.FillComboBox(cmb, "WadFitterName", "ID");
            cmb.SelectedIndex = -1;
        }

        public string ShiftCode()
        {
            string Shift = string.Empty;
            //bool ShiftFlag = false;

            //TimeSpan StartTimeShift1 = new TimeSpan(07, 0, 0); //10 o'clock
            //TimeSpan EndTimeShift1 = new TimeSpan(15, 0, 0); //12 o'clock

            //TimeSpan StartTimeShift2 = new TimeSpan(15, 0, 0); //10 o'clock
            //TimeSpan EndTimeShift2 = new TimeSpan(23, 0, 0); //12 o'clock

            //TimeSpan StartTimeShift3 = new TimeSpan(23, 0, 0); //10 o'clock
            //TimeSpan EndTimeShift3 = new TimeSpan(7, 0, 0); //12 o'clock

            //TimeSpan StartTimeShift3Other = new TimeSpan(24, 0, 0); //10 o'clock

            //DateTime TodayTime;
            //TimeSpan now = DateTime.Now.TimeOfDay;
            //ShiftFlag = false;

            //if ((now > StartTimeShift1) && (now < EndTimeShift1))
            //    Shift = "I";
            //else if ((now > StartTimeShift2) && (now < EndTimeShift2))
            //    Shift = "II";
            //else 
            ////    if (((now > StartTimeShift3) && (now < EndTimeShift3))
            ////{
            ////    if((now < StartTimeShift3Other))
            ////}|| 
            //    Shift = "III";
            ////else if (now < StartTimeShift3Other )
            //    Shift = "III";
            //else
            //{
            //    //Shift = "III";
            //    //int Checkhours = now.Hours;
            //    //if (Checkhours == 23)
            //    //    ShiftFlag = false;
            //    //else
            //    //    ShiftFlag = true;
            //}

            DateTime currentTime = DateTime.Now;
            //Shift = GetShift(currentTime);
            Shift = GetShiftNew();
            return Shift;
        }

        public string GetShift_By_Date(DateTime dtShift)
        {
            Shift = "";
            ShiftId = 0;
            ShiftHours = "";

            ShifFromDate = DateTime.Now;
            ShiftToDate = DateTime.Now;

            DateTime myDate = dtShift; //DateTime.Now;
            string formattedDate = myDate.ToString("MM/dd/yyyy HH:mm:ss"); // Must use slashes

            //string query = $"SELECT * FROM MyTable WHERE MyDateField = #{formattedDate}#";

            DataSet ds = new DataSet();
            //objBL.Query = "select * from ShiftEntryNew where #" + DateTime.Now + "# between ShifFromDate AND ShiftToDate";
            objBL.Query = "select * from ShiftEntryNew where #" + formattedDate + "# between ShifFromDate AND ShiftToDate";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Shift = Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Shift"]));
                ShiftId = Check_Null_Integer(Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ID"])));
                ShiftHours = Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ShiftHours"]));

                ShifFromDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ShifFromDate"]);
                ShiftToDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ShiftToDate"]);
            }

            return Shift;
        }

        //public string Shift
        //{
        //    get { return shift; }
        //    set { shift = value; }
        //}

        public static int shiftid;
        public int ShiftId
        {
            get { return shiftid; }
            set { shiftid = value; }
        }

        private string GetShiftNew()
        {
            //string Shift = string.Empty;

            //DateTime currentTime = DateTime.Now; // Get current time
            //string shift = GetShift(currentTime);

            DateTime myDate = DateTime.Now;
            string formattedDate = myDate.ToString("MM/dd/yyyy HH:mm:ss"); // Must use slashes

            //string query = $"SELECT * FROM MyTable WHERE MyDateField = #{formattedDate}#";

            DataSet ds = new DataSet();
            //objBL.Query = "select * from ShiftEntryNew where #" + DateTime.Now + "# between ShifFromDate AND ShiftToDate";
            objBL.Query = "select * from ShiftEntryNew where #" + formattedDate + "# between ShifFromDate AND ShiftToDate";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                Shift = Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["Shift"]));
                ShiftId = Check_Null_Integer(Check_Null_String(Convert.ToString(ds.Tables[0].Rows[0]["ID"])));
            }

            return Shift;
        }

        public void GetShift()
        {
             DateTime currentTime = DateTime.Now; // Get current time

        string shift = GetShift(currentTime);
        //Console.WriteLine("Current time: {currentTime}, Shift: {shift}");
        }

        public static string GetShift(DateTime currentTime)
        {
            // Extract the hour from the current time.
            int hour = currentTime.Hour;

            // Determine which shift the hour belongs to
            if (hour >= 7 && hour < 15)
            {
                return "I";
                //return "1st Shift (7:00 AM - 3:00 PM)";
            }
            else if (hour >= 15 && hour < 23)
            {
                return "II";
                //return "2nd Shift (3:00 PM - 11:00 PM)";
            }
            else
            {
                return "III";
                //return "3rd Shift (11:00 PM - 7:00 AM)";
            }
        }

        public string Get_Shift_By_OEEEntryId(int ID)
        {
            string ShiftNo = string.Empty;

               DataSet ds = new DataSet();
               objBL.Query = "select OE.ID,OE.EntryDate as [Date],OE.EntryTime as [Time],OE.Shift,OE.PlantInchargeId,E.FullName as[Plant Incharge],OE.VolumeCheckerId,E1.FullName as [Volume Checker] from ((OEEEntry OE inner join Employee E on E.ID=OE.PlantInchargeId) inner join Employee E1 on E1.ID=OE.VolumeCheckerId) where OE.CancelTag=0 and E.CancelTag=0 and E1.CancelTag=0 and OE.ID=" + ID + "";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //0 ID,
                    //1 EntryDate as [Date],
                    //2 EntryTime as [Time],
                    //3OE.Shift,
                    //4 OE.PlantInchargeId,
                    //5 E.FullName as[Plant Incharge],
                    //6 OE.VolumeCheckerId,
                    //7 E1.FullName as [Volume Checker]

                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Shift"])))
                        ShiftNo = Convert.ToString(ds.Tables[0].Rows[0]["Shift"].ToString());
                }

                return ShiftNo;
        }

        public string Package_RL = "", NoOfPeople_RL = "";
        public double Rate_RL = 0;
        public void GetPackageAmount(int PackageId)
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,Package,NoOfPeople,Rate from PackageMaster where CancelTag=0 and ID=" + PackageId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Package"])))
                    Package_RL = ds.Tables[0].Rows[0]["Package"].ToString();
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["NoOfPeople"])))
                    NoOfPeople_RL = ds.Tables[0].Rows[0]["NoOfPeople"].ToString();
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Rate"])))
                    Rate_RL = Convert.ToDouble(ds.Tables[0].Rows[0]["Rate"].ToString());
            }
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
                    MsgString = "E-mail is not correct.";
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
                case 30:
                    MsgString = "You did not have permission";
                    break;
                case 31:
                    MsgString = "Database Backup Successfully Saved.";
                    break;
                case 32:
                    MsgString = "Do you want to delete this record?";
                    break;
                case 33:
                    MsgString = "Have you added this Sales Order in Tally ?";
                    break;
                case 34:
                    MsgString = "Have you added this Purchase Order in Tally ?";
                    break;
                case 35:
                    MsgString = "This Product Not Found In Production Entry. Please Select Proper Product.";
                    break;
                case 36:
                    MsgString = "SKPI Apps- Part / Equipment due date report.";
                    break;
                case 37:
                    MsgString = "Product is not found in Production Entry";
                    break;
                case 38:
                    MsgString = "Are you sure with completed this task?";
                    break;
                case 39:
                    MsgString = "Are you sure for complete this task?";
                    break;
                case 40:
                    MsgString = "Added in list.";
                    break;
                case 41:
                    MsgString = "Entry already exist.";
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

        public string Return_Time_String_DDMMYYYY(string dt)
        {
            DateTime d;
            d = Convert.ToDateTime(dt);
            return d.ToString("hh:mm tt");
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
            //return 0;
        }

        public int ReturnMaxID_Fix(string TableName,string ColumnName)
        {
            int Maxid = 0;
            objBL.Query = "select Max(" + ColumnName + ") from " + TableName + "";
            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                    Maxid = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            return Maxid;
        }

        public int ReturnMaxID_Fix_Table(string TableName, string ColumnName)
        {
            int Maxid = 0;
            objBL.Query = "select Max(" + ColumnName + ") from " + TableName + "";
            DataSet ds = new DataSet();
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                    Maxid = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            Maxid += 1;
            return Maxid;
        }
        
        public void Fill_Staff(string Desgnation, ComboBox cmb)
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,FullName from Employee where CancelTag=0 and Designation='" + Desgnation + "'";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = ds.Tables[0];
                cmb.DisplayMember = "FullName";
                cmb.ValueMember = "ID";
                cmb.SelectedIndex = -1;
            }
        }

        public string GetDateFormat_DDMMYYY(DateTime dt)
        {
            if (dt != null)
            {
                return dt.ToString("dd-MM-yyyy");
            }
            else
                return "";
        }

        public string GetTimeFormat_HHMMSS(DateTime dt)
        {
            if (dt != null)
            {
                return dt.ToString("HH:mm:ss");
            }
            else
                return "";
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
                    //RL_ExcelFormatPath = GetPath("ExcelFormat") + Form_ExcelFileName;
                    RL_ExcelFormatPath = BusinessLayer.FormatPath + Form_ExcelFileName;
                    FileInfo objFIExcel = new FileInfo(RL_ExcelFormatPath);
                    RL_DestinationPath = GetPath("ReportPath") + Form_DestinationReportFilePath + @"\";
                    DirectoryInfo DI = new DirectoryInfo(RL_DestinationPath);
                    DI.Create();
                    RL_DestinationPath += Form_ReportFileName + ".xlsx";

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

        public void DeleteExcelFile()
        {
            if (!string.IsNullOrEmpty(RL_DestinationPath))
            {
                FileInfo fiDelete = new FileInfo(RL_DestinationPath);
                fiDelete.Delete();
            }
        }

        public string PDFFilePath = "";


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
                objBL.Query = "select UOM from UOMMaster where UOM='" + UOM + "'";
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
                ProfitMarginPer = Convert.ToDouble(Math.Round(Convert.ToDecimal(((Price - Cost) / Price) * 100), 2));
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

        public int Check_Null_Integer(string Value)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Value)))
                return Convert.ToInt32(Convert.ToString(Value));
            else
                return 0;
        }

        public double Check_Null_Double(string Value)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Value)))
                return Convert.ToDouble(Convert.ToString(Value));
            else
                return 0;
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
            objBL.Query = "select ID,CompanyName,Address,ContactNo,EmailId,WebSite,VAT,CST,GST,CGST,SGST,IGST from CompanyInformation where CancelTag=0";
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

                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CGST"])))
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
                BankId_Bill = dsDeliveryChallan.Tables[0].Rows[0]["BankId"].ToString();
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

            AvailbleQuantity_RL = PurchaseQuantity;
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
            if (TableName == "SupplierPendingAmount")
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
                        lblChequeNo.Text = "Trans. No";
                        lblChequeNo.Visible = true;
                        txtChequeNo.Visible = true;
                        break;
                    case "RTGS":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "RTGS Details";
                        lblChequeNo.Text = "Trans. No";
                        lblChequeNo.Visible = true;
                        txtChequeNo.Visible = true;
                        break;
                    case "BANK DEPOSIT":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "Bank Deposite Details";
                        lblChequeNo.Text = "Trans. No";
                        lblChequeNo.Visible = true;
                        txtChequeNo.Visible = true;
                        break;
                    case "IMPS":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "IMPS Details";
                        lblChequeNo.Text = "Trans. No";
                        lblChequeNo.Visible = true;
                        txtChequeNo.Visible = true;
                        break;
                    case "MOBILE BANKING":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "MOBILE BANKING Details";
                        lblChequeNo.Text = "Trans. No";
                        lblChequeNo.Visible = true;
                        txtChequeNo.Visible = true;
                        break;
                    case "AMAZON PAY":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "MOBILE BANKING Details";
                        lblChequeNo.Text = "Trans. No";
                        lblChequeNo.Visible = true;
                        txtChequeNo.Visible = true;
                        break;
                    case "GOOGLE PAY":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "GOOGLE PAY";
                        lblChequeNo.Text = "Trans. No";
                        lblChequeNo.Visible = true;
                        txtChequeNo.Visible = true;
                        break;
                    case "PHONE PAY":
                        gbChequeDetails.Visible = true;
                        gbChequeDetails.Text = "PHONE PAY";
                        lblChequeNo.Text = "Trans. No";
                        lblChequeNo.Visible = true;
                        txtChequeNo.Visible = true;
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
                string FilePath = BusinessLayer.DatabasePath; // GetPath("DBPath");

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
            }
        }

        public DialogResult ReturnDialogResult_Report()
        {
            DialogResult dr;
            dr = MessageBox.Show(MessageString(28), MessageString(3), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return dr;
        }

        public DialogResult ReturnDialogResult_Delete()
        {
            DialogResult dr;
            dr = MessageBox.Show(MessageString(32), MessageString(3), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return dr;
        }

        public DialogResult ReturnDialogResult_Complete()
        {
            DialogResult dr;
            dr = MessageBox.Show(MessageString(38), MessageString(3), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return dr;
        }

        public DialogResult ReturnDialogResult_SalesOrder()
        {
            DialogResult dr;
            dr = MessageBox.Show(MessageString(33), MessageString(3), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return dr;
        }

        public DialogResult ReturnDialogResult_PurchaseOrder()
        {
            DialogResult dr;
            dr = MessageBox.Show(MessageString(34), MessageString(3), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return dr;
        }

        public string EmailId_RL = "", Subject_RL = "", Body_RL = "", FilePath_RL = "";

        public void SendEMail()
        {
            try
            {
                MailStatus = string.Empty;
                EmailValidations();
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                MailAddress fromAddress = new MailAddress(RedundancyLogics.EmailAddress_Static);
                message.From = fromAddress;
                //message.To.Add(EmailId_RL);

                message.To.Add(EmailId_RL);
                message.CC.Add(CCList.ToString());
                //message.CC.Add("nilesh@shreekhodiyarpet.com");
                
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(FilePath_RL);
                message.Attachments.Add(attachment);
                message.Subject = Subject_RL;
                message.IsBodyHtml = true;
                message.Body = Body_RL;

                smtpClient.Port = 465;
                //smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                //smtpClient.Host = "smtp.shreekhodiyarpet.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(EmailAddress_Static, EmailPassword_Static);
                smtpClient.Send(message);
                //ShowMessage(29, 1);
                MailStatus = "Mail sent";
            }
            catch (Exception ex)
            {
                MailStatus = "Mail not sent";
                MessageBox.Show("err: " + ex.Message);
            }
        }

        public string MailStatus = string.Empty;

        //Customer List Box and Display Code

        public void Fill_Customer_ListBox(ListBox lb, string SearchText)
        {
            DataSet ds = new DataSet();
            //objBL.Query = "select ID,CustomerCode,CustomerName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Customer where CancelTag=0 and CustomerName like '%" + SearchText + "%' order by CustomerName desc";

            objBL.Query = "select C.ID,C.CustomerName + ' - ' + CM.City as CustomerNameCity,C.Address,C.CityId,C.MobileNumber as [Mobile Number],C.EmailId,C.AadharCard,C.PANCard,C.DrivingLicence,C.GSTNumber,C.StateCode from Customer C inner join CityMaster CM on C.CityId=CM.ID where C.CancelTag=0 and CM.CancelTag=0 and C.CustomerName like '%" + SearchText + "%' order by C.ID";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "CustomerNameCity";
                lb.ValueMember = "ID";
            }
        }


        private static int supplierid;
        public static string suppliername;
        
        private static int customerid;
        public static string customername;
        public static string address;
        public static string city;
        public static int cityid;
        public static string mobilenumber;
        public static string emailid;
        public static string cclist;
        public static string aadharcard;
        public static string pancard;
        public static string drivinglicence;
        public static string gstnumber;
        public static string statecode;

        public int SupplierId
        {
            get { return supplierid; }
            set { supplierid = value; }
        }

        public string SupplierName
        {
            get { return suppliername; }
            set { suppliername = value; }
        }

        public int CustomerId
        {
            get { return customerid; }
            set { customerid = value; }
        }

        public string CustomerName
        {
            get { return customername; }
            set { customername = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public int CityId
        {
            get { return cityid; }
            set { cityid = value; }
        }
        public string MobileNumber
        {
            get { return mobilenumber; }
            set { mobilenumber = value; }
        }
        public string EmailId
        {
            get { return emailid; }
            set { emailid = value; }
        }
        public string CCList
        {
            get { return cclist; }
            set { cclist = value; }
        }
        public string AadharCard
        {
            get { return aadharcard; }
            set { aadharcard = value; }
        }
        public string PANCard
        {
            get { return pancard; }
            set { pancard = value; }
        }
        public string DrivingLicence
        {
            get { return drivinglicence; }
            set { drivinglicence = value; }
        }
        public string GSTNumber
        {
            get { return gstnumber; }
            set { gstnumber = value; }
        }
        public string StateCode
        {
            get { return statecode; }
            set { statecode = value; }
        }

        private static string reason;
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        private static string reasonindetails;
        public string ReasonInDetails
        {
            get { return reasonindetails; }
            set { reasonindetails = value; }
        }

        public static int CustomerId_Search;

        private void Clear_CustomerRecords()
        {
            CustomerId = 0;
            CustomerName = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            CityId = 0;
            MobileNumber = string.Empty;
            EmailId = string.Empty;
            AadharCard = string.Empty;
            PANCard = string.Empty;
            DrivingLicence = string.Empty;
            GSTNumber = string.Empty;
            StateCode = string.Empty;
        }

        private void Clear_SupplierRecords()
        {
            SupplierId = 0;
            SupplierName = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            CityId = 0;
            MobileNumber = string.Empty;
            EmailId = string.Empty;
            AadharCard = string.Empty;
            PANCard = string.Empty;
            DrivingLicence = string.Empty;
            GSTNumber = string.Empty;
            StateCode = string.Empty;
        }

        public static bool GetRecordFlag;

        //public void Get_Customer_Records_By_Id(int CustomerId_F)
        //{
        //    if (CustomerId_F != 0)
        //    {
        //        DataSet ds = new DataSet();
        //        objBL.Query = "select C.ID,C.CustomerName,C.Address,C.CityId,CM.City,C.MobileNumber,C.EmailId,C.AadharCard,C.PANCard,C.DrivingLicence,C.GSTNumber,C.StateCode from Customer C inner join CityMaster CM on C.CityId=CM.ID where C.CancelTag=0 and CM.CancelTag=0 and C.ID=" + CustomerId_F + " order by C.ID";
        //        ds = objBL.ReturnDataSet();
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            Clear_CustomerRecords();
        //            CustomerId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
        //            CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
        //            Address = ds.Tables[0].Rows[0]["Address"].ToString();
        //            City = ds.Tables[0].Rows[0]["City"].ToString();
        //            CityId = Convert.ToInt32(ds.Tables[0].Rows[0]["CityId"].ToString());
        //            MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
        //            EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
        //            AadharCard = ds.Tables[0].Rows[0]["AadharCard"].ToString();
        //            PANCard = ds.Tables[0].Rows[0]["PANCard"].ToString();
        //            DrivingLicence = ds.Tables[0].Rows[0]["DrivingLicence"].ToString();
        //            GSTNumber = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
        //            StateCode = ds.Tables[0].Rows[0]["StateCode"].ToString();
        //        }
        //    }
        //}

        public void Get_Customer_Records_By_RoomId(int RoomId_F)
        {
            if (RoomId_F != 0)
            {
                DataSet ds = new DataSet();
                //objBL.Query = "select C.ID,C.CustomerName,C.Address,C.CityId,CM.City,C.MobileNumber,C.EmailId,C.AadharCard,C.PANCard,C.DrivingLicence,C.GSTNumber,C.StateCode from Customer C inner join CityMaster CM on C.CityId=CM.ID where C.CancelTag=0 and CM.CancelTag=0 and C.ID=" + CustomerId_F + " order by C.ID";

                objBL.Query = "select CI.ID,CI.EntryDate,CI.CustomerId,C.CustomerName,C.Address,C.CityId,Cit.City,C.MobileNumber,C.EmailId,C.AadharCard,C.PANCard,C.DrivingLicence,C.GSTNumber,C.StateCode,CI.RoomId,R.RoomNo,CI.VehicleNumber,CI.CheckInDate,CI.CheckInTime,CI.CheckOutDate,CI.CheckOutTime,CI.NoOfDays,CI.NoOfPersonLiving,CI.ChargesPerDay,CI.Amount,CI.ExtraCharges,CI.HotelCharges,CI.TotalAmount,CI.GSTPer,CI.GSTAmount,CI.NetAmount,CI.PaymentMode,CI.BankId,CI.BankName,CI.AccountNumber,CI.TransactionDate,CI.ChequeNumber,CI.PartyBank,CI.PartyBankAccountNumber,CI.Status,CI.CheckInBy,CI.ReferenceBy,CI.EmailStatus,CI.PrintFlag,CI.UserId from (((CheckIn CI inner join Customer C on C.ID=CI.CustomerId) inner join CityMaster Cit on Cit.ID=C.CityId) inner join RoomMaster R on R.ID=CI.RoomId) where CI.CancelTag=0 and R.CancelTag=0 and C.CancelTag=0 and Cit.CancelTag=0 and CI.RoomId=" + RoomId_F + " and CI.Status='Check In' order by CI.EntryDate desc";

                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Clear_CustomerRecords();
                    CustomerId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    City = ds.Tables[0].Rows[0]["City"].ToString();
                    CityId = Convert.ToInt32(ds.Tables[0].Rows[0]["CityId"].ToString());
                    MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    AadharCard = ds.Tables[0].Rows[0]["AadharCard"].ToString();
                    PANCard = ds.Tables[0].Rows[0]["PANCard"].ToString();
                    DrivingLicence = ds.Tables[0].Rows[0]["DrivingLicence"].ToString();
                    GSTNumber = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
                    StateCode = ds.Tables[0].Rows[0]["StateCode"].ToString();
                }
            }
        }
        public void FillCustomerDetailsRichTextBox(RichTextBox rtb, int CustomerId_F)
        {
            if (CustomerId_F != 0)
            {
                Get_Customer_Records_By_Id(CustomerId_F);
                Customer_Details_RichTextBox();
                rtb.Text = CustomerDetails_RTB.ToString();
            }
        }

        public string CustomerDetails_RTB = string.Empty;
        private void Customer_Details_RichTextBox()
        {
            CustomerDetails_RTB = string.Empty;

            CustomerDetails_RTB = "Customer No:\t\t" + CustomerId + "\n" +
                                    "Customer Name:\t" + CustomerName + "\n" +
                                    "Address:\t\t" + Address + "\n" +
                                    "City:\t\t\t" + City + "\n" +
                                    "Mobile No.:\t\t" + MobileNumber + "\n" +
                                    "Email Id:\t\t" + EmailId + "\n" +
                                    "Aadhar Card:\t\t" + AadharCard + "\n";
        }


        //Room Details

        private static string roomno;
        private static string roomdescription;
        private static string roomcharges;

        public string RoomNo
        {
            get { return roomno; }
            set { roomno = value; }
        }
        public string RoomDescription
        {
            get { return gstnumber; }
            set { gstnumber = value; }
        }
        public string RoomCharges
        {
            get { return roomcharges; }
            set { roomcharges = value; }
        }

        public void Get_Room_Records_By_Id(int RoomId)
        {
            if (RoomId != 0)
            {
                DataSet ds = new DataSet();
                objBL.Query = "Select ID,RoomNo,RoomDescription,RoomCharges from RoomMaster where CancelTag=0 and ID=" + RoomId + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Clear_CustomerRecords();
                    RoomNo = ds.Tables[0].Rows[0]["ID"].ToString();
                    RoomDescription = ds.Tables[0].Rows[0]["RoomDescription"].ToString();
                    RoomCharges = ds.Tables[0].Rows[0]["RoomCharges"].ToString();
                }
            }
        }

        //Room
        public void FillRoom(ComboBox cmbRoomNo)
        {
            objBL.Query = "Select ID,RoomNo from RoomMaster where CancelTag=0";
            objBL.FillComboBox(cmbRoomNo, "RoomNo", "ID");
        }
        public void FillItemDetailsRichTextBox(RichTextBox rtb, int ItemId_F)
        {
            if (ItemId_F != 0)
            {
                Get_Item_Records_By_Id(ItemId_F);
                Item_Details_RichTextBox();
                rtb.Text = ItemDetails_RTB.ToString();
            }
        }

        public string ItemDetails_RTB = string.Empty;
        private void Item_Details_RichTextBox()
        {
            ItemDetails_RTB = string.Empty;

            ItemDetails_RTB = "Item No:\t" + ItemId + "\t\tUOM:\t" + UOM + "\n" +
                                    "Item Name:\t" + ItemName + "\n" +
                                    "Category:\t" + ManufracturerName + "\n" +
                                    "Cost:\t\t" + Cost + "\t\tPrice-" + Price;
        }



        private static int itemid;
        private static int manufracturerid;
        private static string manufracturername;
        private static string itemname;
        private static string batchnumber;
        private static string hsncode;
        private static string uom;
        private static double cost;
        private static double price;
        private static double profitmarginper;
        private static double profitmarginamount;
        private static double cgst;
        private static double sgst;
        private static double igst;
        private static double mrp;

        public int ItemId
        {
            get { return itemid; }
            set { itemid = value; }
        }
        public int ManufracturerId
        {
            get { return manufracturerid; }
            set { manufracturerid = value; }
        }
        public string ManufracturerName
        {
            get { return manufracturername; }
            set { manufracturername = value; }
        }
        public string ItemName
        {
            get { return itemname; }
            set { itemname = value; }
        }
        public string BatchNumber
        {
            get { return batchnumber; }
            set { batchnumber = value; }
        }
        public string HSNCode
        {
            get { return hsncode; }
            set { hsncode = value; }
        }
        public string UOM
        {
            get { return uom; }
            set { uom = value; }
        }

        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public double MRP
        {
            get { return mrp; }
            set { mrp = value; }
        }
        public double ProfitMarginPer
        {
            get { return profitmarginper; }
            set { profitmarginper = value; }
        }
        public double ProfitMarginAmount
        {
            get { return profitmarginamount; }
            set { profitmarginamount = value; }
        }
        public double CGST
        {
            get { return cgst; }
            set { cgst = value; }
        }
        public double SGST
        {
            get { return sgst; }
            set { sgst = value; }
        }
        public double IGST
        {
            get { return igst; }
            set { igst = value; }
        }

        private static int departmentid;
        public int DepartmentId
        {
            get { return departmentid; }
            set { departmentid = value; }
        }

        private void Clear_ItemRecords()
        {
            ProductType = string.Empty;
            ProductName = string.Empty;
            ProductNickName = string.Empty;
            PreformName = string.Empty;

            MouldId = 0;
            MouldNo = string.Empty;

            PreformNeckSize = string.Empty;
            PreformWeight = string.Empty;
            PreformNeckID = string.Empty;
            PreformNeckOD = string.Empty;
            PreformNeckCollarGap = string.Empty;
            PreformNeckHeight = string.Empty;

            ProductNeckSize = string.Empty;
            ProductNeckSizeRatio = string.Empty;
            ProductNeckSizeMinValue = string.Empty;
            ProductNeckSizeMaxValue = string.Empty;
            ProductWeight = string.Empty;
            ProductWeightRatio = string.Empty;
            ProductWeightMinValue = string.Empty;
            ProductWeightMaxValue = string.Empty;
            ProductNeckID = string.Empty;
            ProductNeckIDRatio = string.Empty;
            ProductNeckIDMinValue = string.Empty;
            ProductNeckIDMaxValue = string.Empty;
            ProductNeckOD = string.Empty;
            ProductNeckODRatio = string.Empty;
            ProductNeckODMinValue = string.Empty;
            ProductNeckODMaxValue = string.Empty;
            ProductNeckCollarGap = string.Empty;
            ProductNeckCollarGapRatio = string.Empty;
            ProductNeckCollarGapMinValue = string.Empty;
            ProductNeckCollarGapMaxValue = string.Empty;
            ProductNeckHeight = string.Empty;
            ProductNeckHeightRatio = string.Empty;
            ProductNeckHeightMinValue = string.Empty;
            ProductNeckHeightMaxValue = string.Empty;
            ProductHeight = string.Empty;
            ProductHeightRatio = string.Empty;
            ProductHeightMinValue = string.Empty;
            ProductHeightMaxValue = string.Empty;
            ProductVolume = string.Empty;
            ProductVolumeRatio = string.Empty;
            ProductVolumeMinValue = string.Empty;
            ProductVolumeMaxValue = string.Empty;
            Status = string.Empty;
            NoteR = string.Empty;
            Standard = string.Empty;
            Qty = string.Empty;

            Status = string.Empty;
            NoteR = string.Empty;
            Qty = string.Empty;
            Semi = 0;
            Auto1 = 0;
            Auto2 = 0;
            Servo = 0;
        }
        public void Get_Item_Records_By_Id(int ItemId_F)
        {
            if (ItemId_F != 0)
            {
                Clear_ItemRecords();
                DataSet ds = new DataSet();
                //objBL.Query = "select C.ID,C.CustomerName,C.Address,C.CityId,CM.City,C.MobileNumber,C.EmailId,C.AadharCard,C.PANCard,C.DrivingLicence,C.GSTNumber,C.StateCode from Customer C inner join CityMaster CM on C.CityId=CM.ID where C.CancelTag=0 and CM.CancelTag=0 and C.ID=" + CustomerId + " order by C.ID";
                objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Cost,I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ID=" + ItemId_F + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ItemId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    ProductId= Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    ManufracturerId = Convert.ToInt32(ds.Tables[0].Rows[0]["ManufracturerId"].ToString());
                    ManufracturerName = ds.Tables[0].Rows[0]["ManufracturerName"].ToString();
                    ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["BatchNumber"].ToString())))
                        BatchNumber = ds.Tables[0].Rows[0]["BatchNumber"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["HSNCode"].ToString())))
                        HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["UOM"].ToString())))
                        UOM = ds.Tables[0].Rows[0]["UOM"].ToString();

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Cost"].ToString())))
                        Cost = Convert.ToDouble(ds.Tables[0].Rows[0]["Cost"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Price"].ToString())))
                        Price = Convert.ToDouble(ds.Tables[0].Rows[0]["Price"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["MRP"].ToString())))
                        MRP = Convert.ToDouble(ds.Tables[0].Rows[0]["MRP"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProfitMarginPer"].ToString())))
                        ProfitMarginPer = Convert.ToDouble(ds.Tables[0].Rows[0]["ProfitMarginPer"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProfitMarginAmount"].ToString())))
                        ProfitMarginAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["ProfitMarginAmount"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["CGST"].ToString())))
                        CGST = Convert.ToDouble(ds.Tables[0].Rows[0]["CGST"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["SGST"].ToString())))
                        SGST = Convert.ToDouble(ds.Tables[0].Rows[0]["SGST"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["IGST"].ToString())))
                        IGST = Convert.ToDouble(ds.Tables[0].Rows[0]["IGST"].ToString());
                }
            }
        }

        //Suppliers
        public void Fill_Supplier_ListBox(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,SupplierName,Address,CityId,MobileNumber,EmailId,AadharCard,PANCard,DrivingLicence,GSTNumber,StateCode,DOB from Supplier where CancelTag=0 and SupplierName like '%" + SearchText + "%'";
            else
                objBL.Query = "select ID,SupplierName,Address,CityId,MobileNumber,EmailId,AadharCard,PANCard,DrivingLicence,GSTNumber,StateCode,DOB from Supplier where CancelTag=0 order by ID";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "SupplierName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = 0;
            }
        }

        public void Fill_Client_ListBox(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,CustomerName,Address,CityId,MobileNumber,EmailId,AadharCard,PANCard,DrivingLicence,GSTNumber,StateCode,DOB from Customer where CancelTag=0 and CustomerName like '%" + SearchText + "%'";
            else
                objBL.Query = "select ID,CustomerName,Address,CityId,MobileNumber,EmailId,AadharCard,PANCard,DrivingLicence,GSTNumber,StateCode,DOB from Customer where CancelTag=0 order by ID";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "CustomerName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = 0;
            }
        }

        public void Fill_Preform_ListBox(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,PreformType,Standard,PreformName,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight from Preform where CancelTag=0 and PreformName like '%" + SearchText + "%'";
            else
                objBL.Query = "select ID,PreformType,Standard,PreformName,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight from Preform where CancelTag=0 order by ID";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "PreformName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = 0;
            }
        }

        public void Fill_Item_ListBox(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if(SearchType == "Text")
                objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard from Product where CancelTag=0 and ProductName like '%" + SearchText + "%' order by ProductName asc";
            else
                objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard from Product where CancelTag=0 order by ProductName asc";
          
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.Enabled = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "ProductName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = -1;
            }
        }

        public void Fill_Item_ListBox_OEE(ListBox lb, string SearchText, string SearchType)
       {
            string QueryWhere=string.Empty;

            if (MachineStatus == "Semi")
                //QueryWhere = " and Val(Semi) > 0 ";
                QueryWhere = " and Semi <> null ";
            else if (MachineStatus == "Auto-1")
                //QueryWhere = " and Val(Auto1) > 0 ";
                QueryWhere = " and Auto1 <> null ";
            else if (MachineStatus == "Auto-2")
                //QueryWhere = " and Val(Auto2) > 0 ";
                QueryWhere = " and Auto2 <> null ";
            else if (MachineStatus == "Sarvo")
                QueryWhere = " and Servo <> null ";
            else
                QueryWhere = "";

            //if(!string.IsNullOrEmpty(MachineStatus))
            //    QueryWhere = " and Val(" + MachineStatus + ") > 0 ";

            

            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard from Product where CancelTag=0 and ProductName like '%" + SearchText + "%' "+QueryWhere+" order by ProductName asc";
            else
                objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard " + QueryWhere + " from Product where CancelTag=0 order by ProductName asc";

            //objBL.Query = 
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.Enabled = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "ProductName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = -1;
            }
        }

        public void Fill_Item_ListBox_In_QCRecord(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard from Product where CancelTag=0 and ID IN(select distinct ProductId from QCEntryMachineValues where CancelTag=0) and ProductName like '%" + SearchText + "%' order by ProductName asc";
            else
                objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard from Product where CancelTag=0 and ID IN(select distinct ProductId from QCEntryMachineValues where CancelTag=0) order by ProductName asc";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.Enabled = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "ProductName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = -1;
            }
        }

        public void Fill_Cap_ListBox(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,CapName from CapMaster where CancelTag=0 and CapName like '%" + SearchText + "%'";
            else
                objBL.Query = "select ID,CapName from CapMaster where CancelTag=0 order by ID";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "CapName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = 0;
            }
        }

        //Fill Part Master
        public void Fill_Part_ListBox(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select PM.ID,PM.DepartmentId,L.UserName as [Department],PM.PartName as [Part Name],PM.Description,PM.UomId,U.UnitOfMessurement as [Unit],PM.SupplierId,S.SupplierName as [Supplier Name],PM.HSNCode as [HSN Code],PM.UsedForId,UFM.UserFor as [Used For],PM.PlaceId,PLM.PlaceName as [Place],PM.OpeningStock,PM.Status from (((((PartMaster PM inner join Login L on L.ID=PM.DepartmentId) inner join UOM U on U.ID=PM.UomId) inner join Supplier S on S.ID=PM.SupplierId) inner join UseForMaster UFM on UFM.ID=PM.UsedForId) inner join PlaceMaster PLM on PLM.ID=PM.PlaceId) where L.CancelTag=0 and PM.CancelTag=0 and U.CancelTag=0 and S.CancelTag=0 and UFM.CancelTag=0 and PLM.CancelTag=0 and PM.DepartmentId=" + DepartmentId + " and PM.PartName like '%" + SearchText + "%' order by PM.PartName asc";
            else
                objBL.Query = "select PM.ID,PM.DepartmentId,L.UserName as [Department],PM.PartName as [Part Name],PM.Description,PM.UomId,U.UnitOfMessurement as [Unit],PM.SupplierId,S.SupplierName as [Supplier Name],PM.HSNCode as [HSN Code],PM.UsedForId,UFM.UserFor as [Used For],PM.PlaceId,PLM.PlaceName as [Place],PM.OpeningStock,PM.Status from (((((PartMaster PM inner join Login L on L.ID=PM.DepartmentId) inner join UOM U on U.ID=PM.UomId) inner join Supplier S on S.ID=PM.SupplierId) inner join UseForMaster UFM on UFM.ID=PM.UsedForId) inner join PlaceMaster PLM on PLM.ID=PM.PlaceId) where L.CancelTag=0 and PM.CancelTag=0 and U.CancelTag=0 and S.CancelTag=0 and UFM.CancelTag=0 and PLM.CancelTag=0 and PM.DepartmentId="+DepartmentId+" order by PM.PartName asc";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.Enabled = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "Part Name";
                lb.ValueMember = "ID";
                lb.SelectedIndex = -1;
            }
        }

        public void Fill_Employee_ListBox(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,FullName from Employee where CancelTag=0 and FullName like '%" + SearchText + "%'";
            else
                objBL.Query = "select ID,FullName from Employee where CancelTag=0 order by FullName asc";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "FullName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = -1;
            }
        }

        public void Fill_Wad_ListBox(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,WadName from WadMaster where CancelTag=0 and WadName like '%" + SearchText + "%'";
            else
                objBL.Query = "select ID,WadName from WadMaster where CancelTag=0 order by WadName asc";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "WadName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = 0;
            }
        }

        public void Fill_OtherMaterial_ListBox(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,MaterialName from OtherMaterial where CancelTag=0 and MaterialName like '%" + SearchText + "%'";
            else
                objBL.Query = "select ID,MaterialName from OtherMaterial where CancelTag=0 order by MaterialName asc";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "MaterialName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = 0;
            }
        }

        public void Fill_OtherMaterial_ListBox_FG(ListBox lb, string SearchText, string SearchType)
        {
            lb.DataSource = null;
            DataSet ds = new DataSet();
            if (SearchType == "Text")
                objBL.Query = "select ID,MaterialName from OtherMaterial where CancelTag=0 and Description='FG' and MaterialName like '%" + SearchText + "%'";
            else
                objBL.Query = "select ID,MaterialName from OtherMaterial where CancelTag=0 and Description='FG' order by MaterialName asc";

            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "MaterialName";
                lb.ValueMember = "ID";
                lb.SelectedIndex = 0;
            }
        }

        public void Fill_Item_ListBox_Menu(ListBox lb, string SearchText, string SearchType)
        {
            DataSet ds = new DataSet();
            //objBL.Query = "select ID,CustomerCode,CustomerName,Address,MobileNumber,EmailId,GSTNumber,StateCode from Customer where CancelTag=0 and CustomerName like '%" + SearchText + "%' order by CustomerName desc";
            objBL.Query = "select I.ID,I.ManufracturerId,M.ManufracturerName,I.ItemName,I.BatchNumber,I.HSNCode,I.UOM,I.Cost,I.Price,I.MRP,I.ProfitMarginPer,I.ProfitMarginAmount,I.CGST,I.SGST,I.IGST from Item I inner join Manufracturer M on I.ManufracturerId=M.ID where I.CancelTag=0 and I.ItemName like '%" + SearchText + "%' and M.ManufracturerName IN('Menu')";
            //objBL.Query = "select C.ID,C.CustomerName + ' - ' + CM.City as CustomerNameCity,C.Address,C.CityId,C.MobileNumber as [Mobile Number],C.EmailId,C.AadharCard,C.PANCard,C.DrivingLicence,C.GSTNumber,C.StateCode from Customer C inner join CityMaster CM on C.CityId=CM.ID where C.CancelTag=0 and CM.CancelTag=0 and C.CustomerName like '%" + SearchText + "%' order by C.ID";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb.Visible = true;
                lb.DataSource = ds.Tables[0];
                lb.DisplayMember = "ItemName";
                lb.ValueMember = "ID";
            }
        }
        public string StringFormatSet(string Value1, string Value2)
        {
            return String.Format("{0}\t{1}", Value1, Value2);
        }

        public bool CheckNull_String(string chkString)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(chkString)))
                return true;
            else
                return false;

        }

        public int GetMonthNumber(string Month)
        {
            //January
            //February
            //March
            //April
            //May
            //June
            //July
            //August
            //September
            //October
            //November
            //December
            int MonthNumber = 0;
            switch (Month)
            {
                case "January":
                    MonthNumber = 01;
                    break;
                case "February":
                    MonthNumber = 02;
                    break;
                case "March":
                    MonthNumber = 03;
                    break;
                case "April":
                    MonthNumber = 04;
                    break;
                case "May":
                    MonthNumber = 05;
                    break;
                case "June":
                    MonthNumber = 06;
                    break;
                case "July":
                    MonthNumber = 07;
                    break;
                case "August":
                    MonthNumber = 08;
                    break;
                case "September":
                    MonthNumber = 09;
                    break;
                case "October":
                    MonthNumber = 10;
                    break;
                case "November":
                    MonthNumber = 11;
                    break;
                case "December":
                    MonthNumber = 12;
                    break;
            }

            return MonthNumber;
        }

        public string GetMonthName(int Month)
        {
            //January
            //February
            //March
            //April
            //May
            //June
            //July
            //August
            //September
            //October
            //November
            //December
            string MonthName =string.Empty;
            switch (Month)
            {
                case 1:
                    MonthName = "January";
                    break;
                case 2:
                    MonthName = "February";
                    break;
                case 3:
                    MonthName = "March";
                    break;
                case 4:
                    MonthName = "April";
                    break;
                case 5:
                    MonthName = "May";
                    break;
                case 6:
                    MonthName = "June";
                    break;
                case 7:
                    MonthName = "July";
                    break;
                case 8:
                    MonthName = "August";
                    break;
                case 9:
                    MonthName = "September";
                    break;
                case 10:
                    MonthName = "October";
                    break;
                case 11:
                    MonthName = "November";
                    break;
                case 12:
                    MonthName = "December";
                    break;
            }
            return MonthName;
        }


        private static int mouldid;
        private static string srnomould;
        private static string neck;
        private static string tillcolarfreshblow;
        private static string ofcfreshblow;
        private static string tillcolarfinal;
        private static string ofcfinal;
        private static string drawingno;
        private static string autosemi;
        private static string cavity;
        private static string height;
        private static string lebalspace;
        private static string lebalod;
        private static string nickname;
        private static string party;
        private static string tallyname;
        private static string repairing;
        private static string extrabrushes;
        private static string extraaccessories;
        private static string currentstatus;
        private static string material;

        public int MouldId
        {
            get { return mouldid; }
            set { mouldid = value; }
        }

        public string SrNoMould
        {
            get { return srnomould; }
            set { srnomould = value; }
        }
        public string Neck
        {
            get { return neck; }
            set { neck = value; }
        }
        public string TillColarFreshBlow
        {
            get { return tillcolarfreshblow; }
            set { tillcolarfreshblow = value; }
        }
        public string OfcFreshBlow
        {
            get { return ofcfreshblow; }
            set { ofcfreshblow = value; }
        }
        public string TillColarFinal
        {
            get { return tillcolarfinal; }
            set { tillcolarfinal = value; }
        }
        public string OfcFinal
        {
            get { return ofcfinal; }
            set { ofcfinal = value; }
        }
        public string DrawingNo
        {
            get { return drawingno; }
            set { drawingno = value; }
        }
        public string AutoSemi
        {
            get { return autosemi; }
            set { autosemi = value; }
        }
        public string Material
        {
            get { return material; }
            set { material = value; }
        }
        public string Cavity
        {
            get { return cavity; }
            set { cavity = value; }
        }
        public string Height
        {
            get { return height; }
            set { height = value; }
        }
        public string LebalSpace
        {
            get { return lebalspace; }
            set { lebalspace = value; }
        }

        public string LebalOD
        {
            get { return lebalod; }
            set { lebalod = value; }
        }

        public string NickName
        {
            get { return nickname; }
            set { nickname = value; }
        }

        public string Party
        {
            get { return party; }
            set { party = value; }
        }
        public string TallyName
        {
            get { return tallyname; }
            set { tallyname = value; }
        }
        public string Repairing
        {
            get { return repairing; }
            set { repairing = value; }
        }
        public string ExtraBrushes
        {
            get { return extrabrushes; }
            set { extrabrushes = value; }
        }
        public string ExtraAccessories
        {
            get { return extraaccessories; }
            set { extraaccessories = value; }
        }
        public string CurrentStatus
        {
            get { return currentstatus; }
            set { currentstatus = value; }
        }

        //ID,
        //SrNo,
        //Neck,
        //TillColarFreshBlow,
        //OfcFreshBlow,
        //TillColarFinal,
        //OfcFinal,
        //DrawingNo,
        //AutoSemi,
        //Cavity,
        //Height,
        //LebalSpace,
        //LebalOD,
        //NickName,
        //Party,
        //TallyName,
        //Repairing,
        //ExtraBrushes,
        //ExtraAccessories,
        //CurrentStatus,


        private void Clear_MouldRecord()
        {
            MouldId = 0;
            SrNoMould = string.Empty;
            Neck = string.Empty;
            TillColarFreshBlow = string.Empty;
            OfcFreshBlow = string.Empty;
            TillColarFinal = string.Empty;
            DrawingNo = string.Empty;
            AutoSemi = string.Empty;
            Material = string.Empty;
            Cavity = string.Empty;
            Height = string.Empty;
            LebalSpace = string.Empty;
            LebalOD = string.Empty;
            NickName = string.Empty;
            Party = string.Empty;
            TallyName = string.Empty;
            Repairing = string.Empty;
            ExtraBrushes = string.Empty;
            ExtraAccessories = string.Empty;
            CurrentStatus = string.Empty;
        }
        public void Get_Mould_Records_By_Id(int MouldId_F)
        {
            if (MouldId_F != 0)
            {
                Clear_MouldRecord();
                DataSet ds = new DataSet();
                objBL.Query = "select ID,SrNo,Neck,TillColarFreshBlow,OfcFreshBlow,TillColarFinal,OfcFinal,DrawingNo,AutoSemi,Material,Cavity,Height,LebalSpace,LebalOD,NickName,Party,TallyName,Repairing,ExtraBrushes,ExtraAccessories,CurrentStatus,UserId from MouldMaster where CancelTag=0 and ID=" + MouldId_F + " order by ID";
                ds = objBL.ReturnDataSet();
                //ID,
                //SrNo,
                //Neck,
                //TillColarFreshBlow,
                //OfcFreshBlow,
                //TillColarFinal,
                //OfcFinal,
                //DrawingNo,
                //AutoSemi,
                //Material
                //Cavity,
                //Height,
                //LebalSpace,
                //LebalOD,
                //NickName,
                //Party,
                //TallyName,
                //Repairing,
                //ExtraBrushes,
                //ExtraAccessories,
                //CurrentStatus,

                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    MouldId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    SrNoMould = ds.Tables[0].Rows[0]["SrNo"].ToString();

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Neck"].ToString())))
                        Neck = ds.Tables[0].Rows[0]["Neck"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["TillColarFreshBlow"].ToString())))
                        TillColarFreshBlow = ds.Tables[0].Rows[0]["TillColarFreshBlow"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["OfcFreshBlow"].ToString())))
                        OfcFreshBlow = ds.Tables[0].Rows[0]["OfcFreshBlow"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["TillColarFinal"].ToString())))
                        TillColarFinal = Convert.ToString(ds.Tables[0].Rows[0]["TillColarFinal"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["DrawingNo"].ToString())))
                        DrawingNo = Convert.ToString(ds.Tables[0].Rows[0]["DrawingNo"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["AutoSemi"].ToString())))
                        AutoSemi = Convert.ToString(ds.Tables[0].Rows[0]["AutoSemi"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Material"].ToString())))
                        Material = Convert.ToString(ds.Tables[0].Rows[0]["Material"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Cavity"].ToString())))
                        Cavity = Convert.ToString(ds.Tables[0].Rows[0]["Cavity"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Height"].ToString())))
                        Height = Convert.ToString(ds.Tables[0].Rows[0]["Height"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["LebalSpace"].ToString())))
                        LebalSpace = Convert.ToString(ds.Tables[0].Rows[0]["LebalSpace"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["LebalOD"].ToString())))
                        LebalOD = Convert.ToString(ds.Tables[0].Rows[0]["LebalOD"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["NickName"].ToString())))
                        NickName = Convert.ToString(ds.Tables[0].Rows[0]["NickName"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Party"].ToString())))
                        Party = Convert.ToString(ds.Tables[0].Rows[0]["Party"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["TallyName"].ToString())))
                        TallyName = Convert.ToString(ds.Tables[0].Rows[0]["TallyName"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Repairing"].ToString())))
                        Repairing = Convert.ToString(ds.Tables[0].Rows[0]["Repairing"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ExtraBrushes"].ToString())))
                        ExtraBrushes = Convert.ToString(ds.Tables[0].Rows[0]["ExtraBrushes"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ExtraAccessories"].ToString())))
                        ExtraAccessories = Convert.ToString(ds.Tables[0].Rows[0]["ExtraAccessories"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["CurrentStatus"].ToString())))
                        CurrentStatus = Convert.ToString(ds.Tables[0].Rows[0]["CurrentStatus"].ToString());
                }
            }
        }
        private static int productid;
        private static string mouldno;
        private static string producttype;

        private static string preformtype;

        private static string preformname;
        private static string preformnecksize;
        private static string preformweight;
        private static string preformneckid;
        private static string preformneckod;
        private static string preformneckcollargap;
        private static string preformneckheight;

        private static string productname;
        private static string productnickname;

        private static string productnecksize;
        private static string productnecksizeratio;
        private static string productnecksizeminvalue;
        private static string productnecksizemaxvalue;

        private static string productweight;
        private static string productweightratio;
        private static string productweightminvalue;
        private static string productweightmaxvalue;

        private static string productneckid;
        private static string productneckidratio;
        private static string productneckidminvalue;
        private static string productneckidmaxvalue;

        private static string productneckod;
        private static string productneckodratio;
        private static string productneckodminvalue;
        private static string productneckodmaxvalue;

        private static string productneckcollargap;
        private static string productneckcollargapratio;
        private static string productneckcollargapminvalue;
        private static string productneckcollargapmaxvalue;

        private static string productneckheight;
        private static string productneckheightratio;
        private static string productneckheightminvalue;
        private static string productneckheightmaxvalue;

        private static string productheight;
        private static string productheightratio;
        private static string productheightminvalue;
        private static string productheightmaxvalue;

        private static string productvolume;
        private static string productvolumeratio;
        private static string productvolumeminvalue;
        private static string productvolumemaxvalue;

        private static string status;
        private static string noter;
        public int ProductId
        {
            get { return productid; }
            set { productid = value; }
        }
        public string MouldNo
        {
            get { return mouldno; }
            set { mouldno = value; }
        }
        public string ProductType
        {
            get { return producttype; }
            set { producttype = value; }
        }
        public string PreformType
        {
            get { return preformtype; }
            set { preformtype = value; }
        }

       
        public string PreformName
        {
            get { return preformname; }
            set { preformname = value; }
        }

        public string PreformNeckSize
        {
            get { return preformnecksize; }
            set { preformnecksize = value; }
        }
        public string PreformWeight
        {
            get { return preformweight; }
            set { preformweight = value; }
        }
        public string PreformNeckID
        {
            get { return preformneckid; }
            set { preformneckid = value; }
        }
        public string PreformNeckOD
        {
            get { return preformneckod; }
            set { preformneckod = value; }
        }
        public string PreformNeckCollarGap
        {
            get { return preformneckcollargap; }
            set { preformneckcollargap = value; }
        }
        public string PreformNeckHeight
        {
            get { return preformneckheight; }
            set { preformneckheight = value; }
        }
        public string ProductName
        {
            get { return productname; }
            set { productname = value; }
        }
        public string ProductNickName
        {
            get { return productnickname; }
            set { productnickname = value; }
        }
        public string ProductNeckSize
        {
            get { return productnecksize; }
            set { productnecksize = value; }
        }
        public string ProductNeckSizeRatio
        {
            get { return productnecksizeratio; }
            set { productnecksizeratio = value; }
        }
        public string ProductNeckSizeMinValue
        {
            get { return productnecksizeminvalue; }
            set { productnecksizeminvalue = value; }
        }
        public string ProductNeckSizeMaxValue
        {
            get { return productnecksizemaxvalue; }
            set { productnecksizemaxvalue = value; }
        }
        public string ProductWeight
        {
            get { return productweight; }
            set { productweight = value; }
        }
        public string ProductWeightRatio
        {
            get { return productweightratio; }
            set { productweightratio = value; }
        }
        public string ProductWeightMinValue
        {
            get { return productweightminvalue; }
            set { productweightminvalue = value; }
        }
        public string ProductWeightMaxValue
        {
            get { return productweightmaxvalue; }
            set { productweightmaxvalue = value; }
        }
        public string ProductNeckID
        {
            get { return productneckid; }
            set { productneckid = value; }
        }
        public string ProductNeckIDRatio
        {
            get { return productneckidratio; }
            set { productneckidratio = value; }
        }
        public string ProductNeckIDMinValue
        {
            get { return productneckidminvalue; }
            set { productneckidminvalue = value; }
        }
        public string ProductNeckIDMaxValue
        {
            get { return productneckidmaxvalue; }
            set { productneckidmaxvalue = value; }
        }
        public string ProductNeckOD
        {
            get { return productneckod; }
            set { productneckod = value; }
        }
        public string ProductNeckODRatio
        {
            get { return productneckodratio; }
            set { productneckodratio = value; }
        }
        public string ProductNeckODMinValue
        {
            get { return productneckodminvalue; }
            set { productneckodminvalue = value; }
        }
        public string ProductNeckODMaxValue
        {
            get { return productneckodmaxvalue; }
            set { productneckodmaxvalue = value; }
        }
        public string ProductNeckCollarGap
        {
            get { return productneckcollargap; }
            set { productneckcollargap = value; }
        }
        public string ProductNeckCollarGapRatio
        {
            get { return productneckcollargapratio; }
            set { productneckcollargapratio = value; }
        }
        public string ProductNeckCollarGapMinValue
        {
            get { return productneckcollargapminvalue; }
            set { productneckcollargapminvalue = value; }
        }
        public string ProductNeckCollarGapMaxValue
        {
            get { return productneckcollargapmaxvalue; }
            set { productneckcollargapmaxvalue = value; }
        }
        public string ProductNeckHeight
        {
            get { return productneckheight; }
            set { productneckheight = value; }
        }
        public string ProductNeckHeightRatio
        {
            get { return productneckheightratio; }
            set { productneckheightratio = value; }
        }
        public string ProductNeckHeightMinValue
        {
            get { return productneckheightminvalue; }
            set { productneckheightminvalue = value; }
        }
        public string ProductNeckHeightMaxValue
        {
            get { return productneckheightmaxvalue; }
            set { productneckheightmaxvalue = value; }
        }
        public string ProductHeight
        {
            get { return productheight; }
            set { productheight = value; }
        }
        public string ProductHeightRatio
        {
            get { return productheightratio; }
            set { productheightratio = value; }
        }
        public string ProductHeightMinValue
        {
            get { return productheightminvalue; }
            set { productheightminvalue = value; }
        }
        public string ProductHeightMaxValue
        {
            get { return productheightmaxvalue; }
            set { productheightmaxvalue = value; }
        }
        public string ProductVolume
        {
            get { return productvolume; }
            set { productvolume = value; }
        }
        public string ProductVolumeRatio
        {
            get { return productvolumeratio; }
            set { productvolumeratio = value; }
        }
        public string ProductVolumeMinValue
        {
            get { return productvolumeminvalue; }
            set { productvolumeminvalue = value; }
        }
        public string ProductVolumeMaxValue
        {
            get { return productvolumemaxvalue; }
            set { productvolumemaxvalue = value; }
        }
     
        private static string productmajoraxis;
        public string ProductMajorAxis
        {
            get { return productmajoraxis; }
            set { productmajoraxis = value; }
        }
        private static string productmajoraxisratio;
        public string ProductMajorAxisRatio
        {
            get { return productmajoraxisratio; }
            set { productmajoraxisratio = value; }
        }
        private static string productmajoraxisminvalue;
        public string ProductMajorAxisMinValue
        {
            get { return productmajoraxisminvalue; }
            set { productmajoraxisminvalue = value; }
        }
        private static string productmajoraxismaxvalue;
        public string ProductMajorAxisMaxValue
        {
            get { return productmajoraxismaxvalue; }
            set { productmajoraxismaxvalue = value; }
        }
        private static string productminoraxis;
        public string ProductMinorAxis
        {
            get { return productminoraxis; }
            set { productminoraxis = value; }
        }
        private static string productminoraxisratio;
        public string ProductMinorAxisRatio
        {
            get { return productminoraxisratio; }
            set { productminoraxisratio = value; }
        }
        private static string productminoraxisminvalue;
        public string ProductMinorAxisMinValue
        {
            get { return productminoraxisminvalue; }
            set { productminoraxisminvalue = value; }
        }
        private static string productminoraxismaxvalue;
        public string ProductMinorAxisMaxValue
        {
            get { return productminoraxismaxvalue; }
            set { productminoraxismaxvalue = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string NoteR
        {
            get { return noter; }
            set { noter = value; }
        }
        private static string standard;
        public string Standard
        {
            get { return standard; }
            set { standard = value; }
        }
        private static string qty;
        public string Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        private static int preformid;
        public int PreformId
        {
            get { return preformid; }
            set { preformid = value; }
        }

        private static int capid;
        public int CapId
        {
            get { return capid; }
            set { capid = value; }
        }
        private static string capname;
        public string CapName
        {
            get { return capname; }
            set { capname = value; }
        }
        private static string wad;
        public string Wad
        {
            get { return wad; }
            set { wad = value; }
        }

        private static int wadid;
        public int WadId
        {
            get { return wadid; }
            set { wadid = value; }
        }

        private static string wadname;
        public string WadName
        {
            get { return wadname; }
            set { wadname = value; }
        }

        private static int  materialid;
        public int MaterialID
        {
            get { return materialid; }
            set { materialid = value; }
        }

        private static string materialname;
        public string MaterialName
        {
            get { return materialname; }
            set { materialname = value; }
        }

        //Cap Details

        private static string outerdiastandard;
        public string OuterDiaStandard
        {
            get { return outerdiastandard; }
            set { outerdiastandard = value; }
        }

        private static string outerdiatolerance;
        public string OuterDiaTolerance
        {
            get { return outerdiatolerance; }
            set { outerdiatolerance = value; }
        }

        private static string outerdiaminvalue;
        public string OuterDiaMinValue
        {
            get { return outerdiaminvalue; }
            set { outerdiaminvalue = value; }
        }

        private static string outerdiamaxvalue;
        public string OuterDiaMaxValue
        {
            get { return outerdiamaxvalue; }
            set { outerdiamaxvalue = value; }
        }

        private static string innerdiaWiththreadstandard;
        public string InnerDiaWithThreadStandard
        {
            get { return innerdiaWiththreadstandard; }
            set { innerdiaWiththreadstandard = value; }
        }

        private static string innerdiawiththreadtolerance;
        public string InnerDiaWithThreadTolerance
        {
            get { return innerdiawiththreadtolerance; }
            set { innerdiawiththreadtolerance = value; }
        }

        private static string innerdiawiththreadminvalue;
        public string InnerDiaWithThreadMinValue
        {
            get { return innerdiawiththreadminvalue; }
            set { innerdiawiththreadminvalue = value; }
        }

        private static string innerdiawiththreadmaxvalue;
        public string InnerDiaWithThreadMaxValue
        {
            get { return innerdiawiththreadmaxvalue; }
            set { innerdiawiththreadmaxvalue = value; }
        }

        private static string innerdiawothreadstandard;
        public string InnerDiaWOThreadStandard
        {
            get { return innerdiawothreadstandard; }
            set { innerdiawothreadstandard = value; }
        }

        private static string innerdiawothreadtolerance;
        public string InnerDiaWOThreadTolerance
        {
            get { return innerdiawothreadtolerance; }
            set { innerdiawothreadtolerance = value; }
        }

        private static string innerdiawothreadminvalue;
        public string InnerDiaWOThreadMinValue
        {
            get { return innerdiawothreadminvalue; }
            set { innerdiawothreadminvalue = value; }
        }

        private static string innerdiawothreadmaxvalue;
        public string InnerDiaWOThreadMaxValue
        {
            get { return innerdiawothreadmaxvalue; }
            set { innerdiawothreadmaxvalue = value; }
        }

        //private static string innerdiawothreadmaxvalue;
        //public string InnerDiaWOThreadMaxValue
        //{
        //    get { return innerdiawothreadmaxvalue; }
        //    set { innerdiawothreadmaxvalue = value; }
        //}
        private void PreformRecords_ClearAll()
        {
            PreformType = string.Empty;
            PreformName = string.Empty;
            Standard = string.Empty;
            PreformNeckSize = string.Empty;
            PreformWeight = string.Empty;
            PreformNeckID = string.Empty;
            PreformNeckOD = string.Empty;
            PreformNeckCollarGap = string.Empty;
            PreformNeckHeight = string.Empty;
        }

        public void Get_Preform_Records_By_Id(int PreformId_F)
        {
            if (PreformId_F != 0)
            {
                PreformRecords_ClearAll();
                DataSet ds = new DataSet();
                //objBL.Query = "select ID,ProductType,ProductName,ProductNickName,PreformName,MouldId,MouldNo,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,Status,NoteR,Standard,Qty from Product where CancelTag=0 and ID=" + ProductId_F + "";
                objBL.Query = "select ID,PreformType,PreformName,Standard,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight from Preform where CancelTag=0 and ID="+ PreformId_F + "";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    PreformId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformType"].ToString())))
                        PreformType = ds.Tables[0].Rows[0]["PreformType"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformName"].ToString())))
                        PreformName = ds.Tables[0].Rows[0]["PreformName"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Standard"].ToString())))
                        Standard = ds.Tables[0].Rows[0]["Standard"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckSize"].ToString())))
                        PreformNeckSize = ds.Tables[0].Rows[0]["PreformNeckSize"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformWeight"].ToString())))
                        PreformWeight = ds.Tables[0].Rows[0]["PreformWeight"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckID"].ToString())))
                        PreformNeckID = ds.Tables[0].Rows[0]["PreformNeckID"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckOD"].ToString())))
                        PreformNeckOD = ds.Tables[0].Rows[0]["PreformNeckOD"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckCollarGap"].ToString())))
                        PreformNeckCollarGap = ds.Tables[0].Rows[0]["PreformNeckCollarGap"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckHeight"].ToString())))
                        PreformNeckHeight = ds.Tables[0].Rows[0]["PreformNeckHeight"].ToString();
                }
            }
        }

        public void Get_Product_ID_By_Name(string ProductName_F)
        {
            if (ProductName_F != "")
            {
                ProductId = 0;
                DataSet ds = new DataSet();
                objBL.Query = "select ID,PreformId,PreformName,ProductType,ProductName,ProductNickName,MouldId,MouldNo,Standard,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,ProductMajorAxis,ProductMajorAxisRatio,ProductMajorAxisMinValue,ProductMajorAxisMaxValue,ProductMinorAxis,ProductMinorAxis,ProductMinorAxisRatio,ProductMinorAxisMinValue,ProductMinorAxisMaxValue,Status,NoteR,PackingQty,Semi,Auto1,Auto2,Servo from Product where CancelTag=0 and ProductName='" + ProductName_F + "'";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ProductId = Check_Null_Integer(Convert.ToString(ds.Tables[0].Rows[0]["ID"].ToString()));
                }
            }
        }

        public void Get_Product_Records_By_Id(int ProductId_F)
        {
            if (ProductId_F != 0)
            {
                Clear_ItemRecords();
                DataSet ds = new DataSet();
                objBL.Query = "select ID,PreformId,PreformName,ProductType,ProductName,ProductNickName,MouldId,MouldNo,Standard,PreformNeckSize,PreformWeight,PreformNeckID,PreformNeckOD,PreformNeckCollarGap,PreformNeckHeight,ProductNeckSize,ProductNeckSizeRatio,ProductNeckSizeMinValue,ProductNeckSizeMaxValue,ProductWeight,ProductWeightRatio,ProductWeightMinValue,ProductWeightMaxValue,ProductNeckID,ProductNeckIDRatio,ProductNeckIDMinValue,ProductNeckIDMaxValue,ProductNeckOD,ProductNeckODRatio,ProductNeckODMinValue,ProductNeckODMaxValue,ProductNeckCollarGap,ProductNeckCollarGapRatio,ProductNeckCollarGapMinValue,ProductNeckCollarGapMaxValue,ProductNeckHeight,ProductNeckHeightRatio,ProductNeckHeightMinValue,ProductNeckHeightMaxValue,ProductHeight,ProductHeightRatio,ProductHeightMinValue,ProductHeightMaxValue,ProductVolume,ProductVolumeRatio,ProductVolumeMinValue,ProductVolumeMaxValue,ProductMajorAxis,ProductMajorAxisRatio,ProductMajorAxisMinValue,ProductMajorAxisMaxValue,ProductMinorAxis,ProductMinorAxis,ProductMinorAxisRatio,ProductMinorAxisMinValue,ProductMinorAxisMaxValue,Status,NoteR,PackingQty,Semi,Auto1,Auto2,Servo from Product where CancelTag=0 and ID=" + ProductId_F + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ProductId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformId"])))
                        PreformId = Convert.ToInt32(ds.Tables[0].Rows[0]["PreformId"].ToString());
                    else
                        PreformId = 0;
                   
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformName"])))
                        PreformName = ds.Tables[0].Rows[0]["PreformName"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductType"])))
                        ProductType = ds.Tables[0].Rows[0]["ProductType"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductName"])))
                        ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNickName"])))
                        ProductNickName = ds.Tables[0].Rows[0]["ProductNickName"].ToString();

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["MouldId"])))
                    {
                        MouldId = Convert.ToInt32(ds.Tables[0].Rows[0]["MouldId"].ToString());
                        Get_Mould_Records_By_Id(MouldId);

                        if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["MouldNo"])))
                            MouldNo = ds.Tables[0].Rows[0]["MouldNo"].ToString();
                    }
                        
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Standard"])))
                        Standard = ds.Tables[0].Rows[0]["Standard"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckSize"])))
                        PreformNeckSize = ds.Tables[0].Rows[0]["PreformNeckSize"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformWeight"])))
                        PreformWeight = ds.Tables[0].Rows[0]["PreformWeight"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckID"])))
                        PreformNeckID = ds.Tables[0].Rows[0]["PreformNeckID"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckOD"])))
                        PreformNeckOD = ds.Tables[0].Rows[0]["PreformNeckOD"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckCollarGap"])))
                        PreformNeckCollarGap = ds.Tables[0].Rows[0]["PreformNeckCollarGap"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PreformNeckHeight"])))
                        PreformNeckHeight = ds.Tables[0].Rows[0]["PreformNeckHeight"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckSize"])))
                        ProductNeckSize = ds.Tables[0].Rows[0]["ProductNeckSize"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckSizeRatio"])))
                        ProductNeckSizeRatio = ds.Tables[0].Rows[0]["ProductNeckSizeRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckSizeMinValue"])))
                        ProductNeckSizeMinValue = ds.Tables[0].Rows[0]["ProductNeckSizeMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckSizeMaxValue"])))
                        ProductNeckSizeMaxValue = ds.Tables[0].Rows[0]["ProductNeckSizeMaxValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductWeight"])))
                        ProductWeight = ds.Tables[0].Rows[0]["ProductWeight"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductWeightRatio"])))
                        ProductWeightRatio = ds.Tables[0].Rows[0]["ProductWeightRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductWeightMinValue"])))
                        ProductWeightMinValue = ds.Tables[0].Rows[0]["ProductWeightMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductWeightMaxValue"])))
                        ProductWeightMaxValue = ds.Tables[0].Rows[0]["ProductWeightMaxValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckID"])))
                        ProductNeckID = ds.Tables[0].Rows[0]["ProductNeckID"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckIDRatio"])))
                        ProductNeckIDRatio = ds.Tables[0].Rows[0]["ProductNeckIDRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckIDMinValue"])))
                        ProductNeckIDMinValue = ds.Tables[0].Rows[0]["ProductNeckIDMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckIDMaxValue"])))
                        ProductNeckIDMaxValue = ds.Tables[0].Rows[0]["ProductNeckIDMaxValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckOD"])))
                        ProductNeckOD = ds.Tables[0].Rows[0]["ProductNeckOD"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckODRatio"])))
                        ProductNeckODRatio = ds.Tables[0].Rows[0]["ProductNeckODRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckODMinValue"])))
                        ProductNeckODMinValue = ds.Tables[0].Rows[0]["ProductNeckODMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckODMaxValue"])))
                        ProductNeckODMaxValue = ds.Tables[0].Rows[0]["ProductNeckODMaxValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckCollarGap"])))
                        ProductNeckCollarGap = ds.Tables[0].Rows[0]["ProductNeckCollarGap"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckCollarGapRatio"])))
                        ProductNeckCollarGapRatio = ds.Tables[0].Rows[0]["ProductNeckCollarGapRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckCollarGapMinValue"])))
                        ProductNeckCollarGapMinValue = ds.Tables[0].Rows[0]["ProductNeckCollarGapMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckCollarGapMaxValue"])))
                        ProductNeckCollarGapMaxValue = ds.Tables[0].Rows[0]["ProductNeckCollarGapMaxValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckHeight"])))
                        ProductNeckHeight = ds.Tables[0].Rows[0]["ProductNeckHeight"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckHeightRatio"])))
                        ProductNeckHeightRatio = ds.Tables[0].Rows[0]["ProductNeckHeightRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckHeightMinValue"])))
                        ProductNeckHeightMinValue = ds.Tables[0].Rows[0]["ProductNeckHeightMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductNeckHeightMaxValue"])))
                        ProductNeckHeightMaxValue = ds.Tables[0].Rows[0]["ProductNeckHeightMaxValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductHeight"])))
                        ProductHeight = ds.Tables[0].Rows[0]["ProductHeight"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductHeightRatio"])))
                        ProductHeightRatio = ds.Tables[0].Rows[0]["ProductHeightRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductHeightMinValue"])))
                        ProductHeightMinValue = ds.Tables[0].Rows[0]["ProductHeightMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductHeightMaxValue"])))
                        ProductHeightMaxValue = ds.Tables[0].Rows[0]["ProductHeightMaxValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductVolume"])))
                        ProductVolume = ds.Tables[0].Rows[0]["ProductVolume"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductVolumeRatio"])))
                        ProductVolumeRatio = ds.Tables[0].Rows[0]["ProductVolumeRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductVolumeMinValue"])))
                        ProductVolumeMinValue = ds.Tables[0].Rows[0]["ProductVolumeMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductVolumeMaxValue"])))
                        ProductVolumeMaxValue = ds.Tables[0].Rows[0]["ProductVolumeMaxValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductMajorAxis"])))
                        ProductMajorAxis = ds.Tables[0].Rows[0]["ProductMajorAxis"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductMajorAxisRatio"])))
                        ProductMajorAxisRatio = ds.Tables[0].Rows[0]["ProductMajorAxisRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductMajorAxisMinValue"])))
                        ProductMajorAxisMinValue = ds.Tables[0].Rows[0]["ProductMajorAxisMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductMajorAxisMaxValue"])))
                        ProductMajorAxisMaxValue = ds.Tables[0].Rows[0]["ProductMajorAxisMaxValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductMinorAxis"])))
                        ProductMinorAxis = ds.Tables[0].Rows[0]["ProductMinorAxis"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductMinorAxisRatio"])))
                        ProductMinorAxisRatio = ds.Tables[0].Rows[0]["ProductMinorAxisRatio"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductMinorAxisMinValue"])))
                        ProductMinorAxisMinValue = ds.Tables[0].Rows[0]["ProductMinorAxisMinValue"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ProductMinorAxisMaxValue"])))
                        ProductMinorAxisMaxValue = ds.Tables[0].Rows[0]["ProductMinorAxisMaxValue"].ToString();

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Status"])))
                        Status = ds.Tables[0].Rows[0]["Status"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["NoteR"])))
                        NoteR = ds.Tables[0].Rows[0]["NoteR"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PackingQty"])))
                        Qty = ds.Tables[0].Rows[0]["PackingQty"].ToString();

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Semi"])))
                        Semi = Convert.ToDouble(ds.Tables[0].Rows[0]["Semi"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Auto1"])))
                        Auto1 = Convert.ToDouble(ds.Tables[0].Rows[0]["Auto1"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Auto2"])))
                        Auto2 = Convert.ToDouble(ds.Tables[0].Rows[0]["Auto2"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Servo"])))
                        Servo = Convert.ToDouble(ds.Tables[0].Rows[0]["Servo"].ToString());
                }
            }
        }

        private static double semi;
        public double Semi
        {
            get { return semi; }
            set { semi = value; }
        }

        private static double auto1;
        public double Auto1
        {
            get { return auto1; }
            set { auto1 = value; }
        }

        private static double auto2;
        public double Auto2
        {
            get { return auto2; }
            set { auto2 = value; }
        }

        private static double servo;
        public double Servo
        {
            get { return servo; }
            set { servo = value; }
        }

        private static int partid;
        public int PartId
        {
            get { return partid; }
            set { partid = value; }
        }

        private static string department;
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
                private static string partname;
        public string PartName
        {
            get { return partname; }
            set { partname = value; }
        }

                   private static string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

          private static string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }  
        
        private static string usedfor;
        public string UsedFor
        {
            get { return usedfor; }
            set { usedfor = value; }
        }

          private static string placename;
        public string PlaceName
        {
            get { return placename; }
            set { placename = value; }
        }

        private static string openingstock;
        public string OpeningStock
        {
            get { return openingstock; }
            set { openingstock = value; }
        }

        //  private static string status;
        //public string Status
        //{
        //    get { return status; }
        //    set { status = value; }
        //}
    
        public void Get_Part_Records_By_Id(int PartId_F)
        {
            if (PartId_F != 0)
            {
                Clear_ItemRecords();
                DataSet ds = new DataSet();
                objBL.Query = "select PM.ID,PM.DepartmentId,L.UserName as [Department],PM.PartName as [Part Name],PM.Description,PM.UomId,U.UnitOfMessurement as [Unit],PM.SupplierId,S.SupplierName as [Supplier Name],PM.HSNCode as [HSN Code],PM.UsedForId,UFM.UserFor as [Used For],PM.PlaceId,PLM.PlaceName as [Place],PM.OpeningStock,PM.Status from (((((PartMaster PM inner join Login L on L.ID=PM.DepartmentId) inner join UOM U on U.ID=PM.UomId) inner join Supplier S on S.ID=PM.SupplierId) inner join UseForMaster UFM on UFM.ID=PM.UsedForId) inner join PlaceMaster PLM on PLM.ID=PM.PlaceId) where L.CancelTag=0 and PM.CancelTag=0 and U.CancelTag=0 and S.CancelTag=0 and UFM.CancelTag=0 and PLM.CancelTag=0 and PM.ID=" + PartId_F + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    PartId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["DepartmentId"])))
                        DepartmentId = Convert.ToInt32(ds.Tables[0].Rows[0]["DepartmentId"].ToString());
                    else
                        DepartmentId = 0;

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Department"])))
                        Department = ds.Tables[0].Rows[0]["Department"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Part Name"])))
                        PartName = ds.Tables[0].Rows[0]["Part Name"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Description"])))
                        Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Unit"])))
                        Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Supplier Name"])))
                        SupplierName = ds.Tables[0].Rows[0]["Supplier Name"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["HSN Code"])))
                        HSNCode = ds.Tables[0].Rows[0]["HSN Code"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Used For"])))
                        UsedFor = ds.Tables[0].Rows[0]["Used For"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Place"])))
                        PlaceName = ds.Tables[0].Rows[0]["Place"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["OpeningStock"])))
                        OpeningStock = ds.Tables[0].Rows[0]["OpeningStock"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Status"])))
                        Status = ds.Tables[0].Rows[0]["Status"].ToString();

                    Part_Details_RichTextBox();
                }
            }
        }

        public string PartDetails_RTB = string.Empty;
        private void Part_Details_RichTextBox()
        {
            PartDetails_RTB = string.Empty;
            PartDetails_RTB = "Part No:\t\t" + PartId + "\n" +
                                    "Part Name:\t" + PartName + "\n" +
                                    "Description:\t" + Description + "\n" +
                                    "Department:\t" + Department + "\n" +
                                    "Supplier Name:\t" + SupplierName + "\n" +
                                    "HSN Code:\t" + HSNCode + "\n" +
                                    "Used For:\t" + UsedFor + "\n" +
                                    "Place Name:\t" + PlaceName + "\n" +
                                    "Status:\t\t" + Status + "\n";
        }

        //Supplier
        public void Get_Supplier_Records_By_Id(int SupplierId_F)
        {
            if (SupplierId_F != 0)
            {
                Clear_SupplierRecords();
                DataSet ds = new DataSet();
                objBL.Query = "select S.ID,S.SupplierName,S.Address,S.CityId,C.City,S.MobileNumber,S.EmailId,S.AadharCard,S.PANCard,S.DrivingLicence,S.GSTNumber,S.StateCode from Supplier S inner join CityMaster C on C.ID=S.CityId where C.CancelTag=0 and S.CancelTag=0 and S.ID=" + SupplierId_F + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SupplierId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["SupplierName"])))
                        SupplierName = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Address"])))
                        Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["City"])))
                        City = ds.Tables[0].Rows[0]["City"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"])))
                        MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["EmailId"])))
                        EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["AadharCard"])))
                        AadharCard = ds.Tables[0].Rows[0]["AadharCard"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PANCard"])))
                        PANCard = ds.Tables[0].Rows[0]["PANCard"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["DrivingLicence"])))
                        DrivingLicence = ds.Tables[0].Rows[0]["DrivingLicence"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["GSTNumber"])))
                        GSTNumber = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["StateCode"])))
                        StateCode = ds.Tables[0].Rows[0]["StateCode"].ToString();
                }
            }
        }
 
        public void Get_Customer_Records_By_Id(int CustomerId_F)
        {
            if (CustomerId_F != 0)
            {
                Clear_CustomerRecords();
                DataSet ds = new DataSet();
                objBL.Query = "select S.ID,S.CustomerName,S.Address,S.CityId,C.City,S.MobileNumber,S.EmailId,S.AadharCard,S.PANCard,S.DrivingLicence,S.GSTNumber,S.StateCode,S.CCList from Customer S inner join CityMaster C on C.ID=S.CityId where C.CancelTag=0 and S.CancelTag=0 and S.ID=" + CustomerId_F + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CustomerId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["CustomerName"])))
                        CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Address"])))
                        Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["City"])))
                        City = ds.Tables[0].Rows[0]["City"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["MobileNumber"])))
                        MobileNumber = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["EmailId"])))
                        EmailId = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["AadharCard"])))
                        AadharCard = ds.Tables[0].Rows[0]["AadharCard"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["PANCard"])))
                        PANCard = ds.Tables[0].Rows[0]["PANCard"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["DrivingLicence"])))
                        DrivingLicence = ds.Tables[0].Rows[0]["DrivingLicence"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["GSTNumber"])))
                        GSTNumber = ds.Tables[0].Rows[0]["GSTNumber"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["StateCode"])))
                        StateCode = ds.Tables[0].Rows[0]["StateCode"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["CCList"])))
                        CCList = ds.Tables[0].Rows[0]["CCList"].ToString();
                }
            }
        }

        private void Clear_CapRecords()
        {
            CapId = 0;
            CapName = string.Empty;
            Wad = string.Empty;
        }


        public void Get_Cap_Records_By_Id(int CapId_F)
        {
            if (CapId_F != 0)
            {
                Clear_CapRecords();
                DataSet ds = new DataSet();
                objBL.Query = "select ID,CapName,Wad from CapMaster where CancelTag=0 and ID=" + CapId_F + "";
           
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                        CapId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    else
                        CapId = 0;

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["CapName"])))
                        CapName = ds.Tables[0].Rows[0]["CapName"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Wad"])))
                        Wad = ds.Tables[0].Rows[0]["Wad"].ToString();
                }
            }
        }

        private void Clear_WadRecords()
        {
            WadId = 0;
            WadName = string.Empty;
        }

        public void Get_Wad_Records_By_Id(int WadId_F)
        {
            if (WadId_F != 0)
            {
                Clear_WadRecords();
                DataSet ds = new DataSet();
                objBL.Query = "select ID,WadName from WadMaster where CancelTag=0 and ID=" + WadId_F + "";

                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                        WadId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    else
                        WadId = 0;

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["WadName"])))
                        WadName = ds.Tables[0].Rows[0]["WadName"].ToString();
                }
            }
        }

        public void Get_Material_Records_By_Id(int MaterialId_F)
        {
            if (MaterialId_F != 0)
            {
                Clear_WadRecords();
                DataSet ds = new DataSet();
                objBL.Query = "select ID,MaterialName from OtherMaterial where CancelTag=0 and ID=" + MaterialId_F + "";

                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                        MaterialID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    else
                        MaterialID = 0;

                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["MaterialName"])))
                        MaterialName = ds.Tables[0].Rows[0]["MaterialName"].ToString();
                }
            }
        }

        public void SetReportMailId(CheckBox cb)
        {
            EmailId = Convert.ToString(ConfigurationManager.AppSettings["ReportMail"]);
            cb.Text = EmailId;
        }


        //Employee

        private void Clear_Employee_Records()
        {
            EmployeeId = 0;
            Designation = string.Empty;
            FullName = string.Empty;
        }

        private static int employeeid;
        public int EmployeeId
        {
            get { return employeeid; }
            set { employeeid = value; }
        }

        private static string designation;
        public string Designation
        {
            get { return designation; }
            set { designation = value; }
        }

        private static string fullname;
        public string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        } 
        
        public void Get_Employee_Records_By_Id(int EmployeeId_F)
        {
            if (EmployeeId_F != 0)
            {
                Clear_Employee_Records();
                DataSet ds = new DataSet();
                objBL.Query = "select ID,FullName,Gender,DOB,Age,BloodGroup,CurrentAddress,AsAbove,PermenentAddress,MobileNo1,MobileNo2,EmailId,DateOfJoining,Department,Designation,Qualification,Experience,SalaryPerMonth,SalaryPerDay from Employee where CancelTag=0 and ID=" + EmployeeId_F + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["ID"])))
                        EmployeeId = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString());
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["FullName"])))
                        FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                    if (CheckNull_String(Convert.ToString(ds.Tables[0].Rows[0]["Designation"])))
                        Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                }
            }
        }

        //public void SetReportMailId(CheckBox cb)
        //{
        //    EmailId = Convert.ToString(ConfigurationManager.AppSettings["ReportMail"]);
        //    cb.Text = EmailId;
        //}

        private static string whatsappsno;
        public string WhatsAppsNo
        {
            get { return whatsappsno; }
            set { whatsappsno = value; }
        }

        public void SetWhatsAppsNoReport(CheckBox cb)
        {
            WhatsAppsNo = Convert.ToString(ConfigurationManager.AppSettings["WhatsAppsNo"]);
            cb.Text = WhatsAppsNo;
        }

        private static string emailbody;
        public string EmailBody
        {
            get { return emailbody; }
            set { emailbody = value; }
        }

        public bool FlagEmail = false;
        public void SendEmail_Details(string ToEmail, int MsgNo, string AttachmentPath)
        {
            FlagEmail = false;
            if (!string.IsNullOrEmpty(ToEmail))
            {
                FillCompanyData();
                EmailId_RL = ToEmail;
                Subject_RL = MessageString(MsgNo);
                string body = "<div><p>Dear Sir,<p/><p>Please find attachment of pdf file.</p><p>" + EmailBody + " </p><p>" + CI_CompanyName + "</p></div>";
                Body_RL = body;
                FilePath_RL = AttachmentPath;
                SendEMail();
                FlagEmail = true;
            }
        }

        public void SendEmail_BirthdayMail(string ToEmail)
        {
            if (!string.IsNullOrEmpty(ToEmail))
            {
                FillCompanyData();
                EmailId_RL = ToEmail;
                Subject_RL = MessageString(37);
                string body = "<div><p>Dear Sir,<p/><p>" + EmailBody + " </p><p>Thanks,</p><p>" + CI_CompanyName + "</p></div>";
                Body_RL = body;
                //FilePath_RL = AttachmentPath;
                SendEMailWithoutFile();
            }
        }

        public void SendEMailWithoutFile()
        {
            try
            {
                EmailValidations();
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                MailAddress fromAddress = new MailAddress(RedundancyLogics.EmailAddress_Static);
                message.From = fromAddress;
                message.To.Add(EmailId_RL);
                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(FilePath_RL);
                //message.Attachments.Add(attachment);
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

        public void WhatsAppsReportOpen(string WhatsAppNo, string ReportPath)
        {
            string WhatsAppNoLink = string.Empty;
            string currentDirectory = Path.GetDirectoryName(ReportPath);
            string fullPathOnly = Path.GetFullPath(currentDirectory);

            Process.Start(fullPathOnly);
            //"https://api.whatsapp.com/send?phone=+917058620423"
            WhatsAppNoLink = @"https://api.whatsapp.com/send?phone=" + WhatsAppNo + "";
            //WhatsAppNoLink = @"https://web.whatsapp.com/send?phone=+91" + WhatsAppNo + "";
            Process.Start(WhatsAppNoLink);
        }
    }
}
