using BusinessLayerUtility;
using SPApplication.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Transaction
{
    public partial class Planner : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        //Fill Grid Search Parameter
        string MainQuery = string.Empty, WhereClause = string.Empty, OrderByClause = string.Empty, UserClause = string.Empty, Status = string.Empty;

        bool SearchByTask = false, IDFlag = false, DepartmentFlag = false, FlagTask = false, StatusFlag = false;
        string MString = string.Empty, ConcatDate = string.Empty, DString = string.Empty, FollowUp = string.Empty, CompleteDays = string.Empty;
        int MonthNo = 0, SrNo = 1, BookingMonth = 0, TMId = 0;
        static int RowIndexStatic;
        DateTime CompleteDate, EntryDate, ButtonDate;
        int day_C = 0, month_C = 0, year_C = 0;

        public Planner()
        {
            InitializeComponent();
            Clear_Values();
            objDL.SetLabelDesign(lblHeader, BusinessResources.LBL_HEADER_PLANNER);
            objDL.SetButtonDesign(btnExit, BusinessResources.BTN_EXIT);
            objDL.SetButtonDesign(btnClear, BusinessResources.BTN_CLEAR);
            btnAddYourTask.BackColor = objDL.GetBackgroundColor();
            btnAddYourTask.ForeColor = objDL.GetForeColor();

            lblTodayDate.BackColor = objDL.GetBackgroundColor();
            lblTodayDate.ForeColor = objDL.GetForeColor();
            objRL.Fill_Users(cmbDepartment);
            Set_Department();
            //cbDepartmentAll.Checked = true;
        }

        private void Set_Department()
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_AJAY)
            {
                //cmbDepartmentSearch.Text = BusinessLayer.UserName_Static.ToString();
                cmbDepartment.Enabled = true;
                cbDepartmentAll.Enabled = true;
               cbDepartmentAll.Checked = true;
            }
            else
            {
                cmbDepartment.Enabled = false;
                cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
                cbDepartmentAll.Enabled = false;
            }
        }

        private void Planner_Load(object sender, EventArgs e)
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
            //ClearAll();
            //SetButtons();
            //cbDepartmentAll.Checked = true;
            Set_Today_Date();
          //  FillGrid_AssignTask_Monthly();
            cmbMonth.Focus();
        }
       
        private void Set_Today_Date()
        {
            day_C = 0; month_C = 0; year_C = 0;

            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;
            year_C= dateAndTime.Year;
            month_C = dateAndTime.Month;
            day_C = dateAndTime.Day;
            
            int Dt = Convert.ToInt32(day_C);
            DString = Dt.ToString();

            //if (Dt < 10)
            //{
            //    DString = Dt.ToString();
            //    DString = DString.Remove(0);
            //}
            //else
            //{
            //    DString = Dt.ToString();
            //}

            HighLight_Button();

           // cmbMonth.Text = objRL.GetMonthName(month_C);
           // Add_Task_In_AssignTask();
            ButtonDate = DateTime.Now.Date;
           // FillGrid_AssignTask_Monthly();
        }

        private void HighLight_Button()
        {
            foreach (var button in this.gbCalendar.Controls.OfType<System.Windows.Forms.Button>())
            //this.gbMachineNo.Controls.OfType<System.Windows.Forms.Button>()
            {
                if (DString == button.Text)
                {
                    button.BackColor = objDL.GetBackgroundColor();
                    button.ForeColor = objDL.GetForeColor();
                }
                else
                {
                    button.BackColor = Color.LightSkyBlue;
                    button.ForeColor = Color.Black;
                }
            }
        }

        private void ButtonClick_Event(object sender)
        {
            if (cmbMonth.SelectedIndex > -1)
            {
                Clear_Values();
                //foreach (var button in this.Controls.OfType<System.Windows.Forms.Button>())
                foreach (var button in this.gbCalendar.Controls.OfType<System.Windows.Forms.Button>())
                //this.gbMachineNo.Controls.OfType<System.Windows.Forms.Button>()
                {
                    //button.BackColor = objDL.GetBackgroundColor();
                    //button.ForeColor = objDL.GetForeColor();

                    button.BackColor = Color.LightSkyBlue;
                    button.ForeColor = Color.Black;
                }
                Control ctrl = ((Control)sender);
                //ctrl.BackColor = objDL.GetBackgroundColor();
             

                int Dt = Convert.ToInt32(ctrl.Text);
                year_C = DateTime.Now.Date.Year;
                if (Dt < 10)
                    DString = "0" + Dt.ToString();
                else
                    DString = Dt.ToString();

                if (cmbMonth.SelectedIndex > -1)
                {
                    MonthNo = objRL.GetMonthNumber(cmbMonth.Text);

                    if (MonthNo < 10)
                        MString = "0" + MonthNo.ToString();
                    else
                        MString = MonthNo.ToString();

                    //MString = MonthNo.ToString();
                }

                ConcatDate = DString + "/" + MString + "/" + year_C;
                ButtonDate = Convert.ToDateTime(ConcatDate);
                FillGrid();
                FillGrid_AssignTask_Monthly();
                ctrl.BackColor = Color.Pink;
            }
            else
            {
                objEP.SetError(cmbMonth, "Select Month");
                cmbMonth.Focus();
            }
        }

        private void Clear_Values()
        {
            dataGridView1.Rows.Clear();
            gbDetails.Visible = false;
            BookingMonth = 0;
            gbDetails.Text = "";
            ButtonDate = DateTime.Now.Date;
            MString = string.Empty;
            ConcatDate = string.Empty;
            DString = string.Empty;
            MonthNo = 0;
            MainQuery = string.Empty; WhereClause = string.Empty; OrderByClause = string.Empty; UserClause = string.Empty; Status = string.Empty;
            SearchByTask = false; IDFlag = false; DepartmentFlag = false; FlagTask = false; StatusFlag = false;
            MString = string.Empty; ConcatDate = string.Empty; DString = string.Empty; FollowUp = string.Empty; CompleteDays = string.Empty;
            MonthNo = 0; SrNo = 1; BookingMonth = 0; TMId = 0;
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Values();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void cbDepartmentAll_CheckedChanged(object sender, EventArgs e)
        {
            SearchByTask = false;
            IDFlag = false;
            DepartmentFlag = false;
            if (cbDepartmentAll.Checked)
            {
                cmbDepartment.SelectedIndex = -1;
                cmbDepartment.Enabled = false;
                DepartmentFlag = false;
            }
            else
            {
                cmbDepartment.SelectedIndex = -1;
                cmbDepartment.Enabled = true;
                DepartmentFlag = true;
            }
        }

        private void ButtonClickMain(object sender, EventArgs e)
        {
            ButtonClick_Event(sender);
        }

        private void cmbMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            objEP.Clear();
            if (cmbMonth.SelectedIndex > -1)
            {
                Clear_Values();
                GetMonth();
                Add_Task_In_AssignTask();
                FillGrid_AssignTask_Monthly();
            }
            else
            {
                objEP.SetError(cmbMonth, "Select Month");
                cmbMonth.Focus();
            }
        }

      //  int TMId = 0;

        string[] MonthArray = {
                                                    "January",
                                                    "February",
                                                    "March",
                                                    "April",
                                                    "May",
                                                    "June",
                                                    "July",
                                                    "August",
                                                    "September",
                                                    "October",
                                                    "November",
                                                    "December",
                                                };

        bool FlagFill = false; int ButtonDay = 0;
        //DateTime ReturnDate;

        bool DateFlag = false;
        private DateTime Get_DateTime(string MonthS,DateTime ScheduleDate_Main)
        {
            DateFlag = false;
            DateTime ReturnDate = DateTime.MinValue;
            
            int Dt = 0;

            if (!FlagFill)
                Dt = Convert.ToInt32(ScheduleDate_Main.Day);
            else
                Dt = ButtonDay;

            year_C = Convert.ToInt32(ScheduleDate_Main.Year);

            if (Dt < 10)
                DString = "0" + Dt.ToString();
            else
                DString = Dt.ToString();

            if (!string.IsNullOrEmpty(Convert.ToString(MonthS)))
            {
                MonthNo = objRL.GetMonthNumber(MonthS);

                if (MonthNo < 10)
                    MString = "0" + MonthNo.ToString();
                else
                    MString = MonthNo.ToString();

                //MString = MonthNo.ToString();
            }
            
            int MDays = 0;
            MDays = System.DateTime.DaysInMonth(year_C, MonthNo);

            if (Dt <= MDays)
            {
                //ReturnDate = DateTime.Now.Date;
                ConcatDate = DString + "/" + MString + "/" + year_C;

                ReturnDate = DateTime.ParseExact(ConcatDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateFlag = true;
                //// ReturnDate = DateTime.TryParse(ConcatDate out Retu, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
                DateFlag = false;
            //else
            //    RFlag = false;
            

            //ReturnDate = Convert.ToDateTime(ConcatDate);
            return ReturnDate;
        }

        string ScheduleOn = string.Empty;
        private void Add_Task_In_AssignTask()
        {
            TMId = 0; ScheduleOn = string.Empty;
            if (cmbMonth.SelectedIndex > -1)
            {
                BookingMonth = objRL.GetMonthNumber(cmbMonth.Text);
                DataSet ds = new DataSet();
                objBL.Query = "select ID,DepartmentId,Task,ScheduleOn,ScheduleDay,ScheduleDate from TaskMaster where CancelTag=0 and ScheduleOn NOT IN('Daily','Weekly')";
                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        FlagFill = false;
                        TMId = 0; LoginId = 0;
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ID"])))
                        {
                            TMId = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());

                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ScheduleDate"])))
                                ScheduleDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ScheduleDate"].ToString());
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ScheduleOn"])))
                                ScheduleOn = Convert.ToString(ds.Tables[0].Rows[i]["ScheduleOn"].ToString());

                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["DepartmentId"])))
                                LoginId = Convert.ToInt32(ds.Tables[0].Rows[i]["DepartmentId"].ToString());
                            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Task"])))
                                Task = Convert.ToString(ds.Tables[0].Rows[i]["Task"].ToString());
                            CompleteDays = "0";

                            //Weekly
                            //Fortnightly
                            //Monthly
                            //Quarterly
                            //Half Yearly
                            //Yearly

                            if (ScheduleOn == "Monthly")
                            {
                                for (int j = 0; j < MonthArray.Length; j++)
                                {
                                    EntryDate = Convert.ToDateTime(Get_DateTime(MonthArray[j].ToString(), ScheduleDate));
                                    if (DateFlag)
                                    {
                                        //TaskMonth = MonthArray[j].ToString();
                                        SaveDB();
                                    }
                                }
                            }
                            else if (ScheduleOn == "Quarterly")
                            {
                                for (int j = 1; j < 12; j=j+3)
                                {
                                    EntryDate = ScheduleDate.AddMonths(3); // Convert.ToDateTime(Get_DateTime(MonthArray[j].ToString(), ScheduleDate));
                                    ScheduleDate = EntryDate;
                                    SaveDB();
                                }
                            }
                            else if (ScheduleOn == "Half Yearly")
                            {
                                for (int j = 1; j < 12; j = j + 6)
                                {
                                    EntryDate = ScheduleDate.AddMonths(6); // Convert.ToDateTime(Get_DateTime(MonthArray[j].ToString(), ScheduleDate));
                                    ScheduleDate = EntryDate;
                                    SaveDB();
                                }
                            }
                            else if (ScheduleOn == "Yearly")
                            {
                                    EntryDate = ScheduleDate.AddMonths(12); // Convert.ToDateTime(Get_DateTime(MonthArray[j].ToString(), ScheduleDate));
                                    //ScheduleDate = EntryDate;
                                    SaveDB();
                                    ScheduleDate = DateTime.Now.Date;
                            }
                        }
                    }
                }
                //FillGrid_AssignTask_Monthly();
            }
        }

        private void SaveDB()
        {
            if (!CheckExist_AssignTast())
            {
                objBL.Query = "insert into AssignTask(EntryDate,EntryTime,LoginId,Task,FollowUp,Status,CompleteDate,CompleteDays,TMId,TaskMonth,UserId) values('" + EntryDate.ToShortDateString() + "','" + EntryTime.ToShortTimeString() + "'," + LoginId + ",'" + Task + "','" + FollowUp + "','Pending','" + CompleteDate.ToShortDateString() + "'," + CompleteDays + "," + TMId + ",'" + TaskMonth + "'," + BusinessLayer.UserId_Static + ")";
                objBL.Function_ExecuteNonQuery();
            }
        }

        DateTime EntryTime, ScheduleDate;
        int LoginId = 0 ;
        string Task = "",  TaskMonth = "";

        private bool CheckExist_AssignTast()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID,EntryDate,EntryTime,LoginId,Task,FollowUp,Status,CompleteDate,CompleteDays,TMId,TaskMonth from AssignTask where TMId=" + TMId + " and CancelTag=0 and EntryDate=#" + EntryDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";
            //objBL.Query = "select ID,EntryDate,EntryTime,LoginId,Task,FollowUp,Status,CompleteDate,CompleteDays,TMId,TaskMonth from AssignTask where TMId=" + TMId + " and CancelTag=0 and TaskMonth='" + TaskMonth + "' and EntryDate=#" + EntryDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void FillGrid_AssignTask_Monthly()
        {
            int FlagSet = 0;
            int days = DateTime.DaysInMonth(Convert.ToInt32(DateTime.Now.Date.Year), objRL.GetMonthNumber(cmbMonth.Text));

            //foreach (var button in this.gbCalendar.Controls.OfType<System.Windows.Forms.Button>())
            for(int j=1;j<=days;j++)
            {
                    FlagFill = true;
                    ButtonDay = Convert.ToInt32(j);
                    TaskMonth = cmbMonth.Text;
                    ScheduleDate = DateTime.Now.Date;
                    EntryDate = Convert.ToDateTime(Get_DateTime(TaskMonth.ToString(), ScheduleDate));
                    //EntryDate = Convert.ToDateTime(EntryDate.ToString(BusinessResources.DATEFORMATMMDDYYYY));

                    if (DateFlag)
                    {
                        DataSet ds = new DataSet();
                        //objBL.Query = "select ID,EntryDate,EntryTime,LoginId,Task,FollowUp,Status,CompleteDate,CompleteDays,TMId,TaskMonth from AssignTask where CancelTag=0 and TaskMonth='" + TaskMonth + "' and EntryDate=#" + EntryDate + "# ";// and Status='Pending' ";
                        objBL.Query = "select T.ID,T.EntryDate as [Date],T.EntryTime as [Time],L.UserName as [Department],T.Task,T.FollowUp,T.Status,T.CompleteDate  as [Complete Date],T.CompleteDays as [Complete Days],T.LoginId from AssignTask T inner join Login L on L.ID=T.LoginId where T.CancelTag=0 and L.CancelTag=0 and T.EntryDate=#" + EntryDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# ";// and Status='Pending' ";
                        ds = objBL.ReturnDataSet();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //Button button = new Button();
                            //button.Name = "btn" + j.ToString();

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Status"])))
                                {
                                    if (ds.Tables[0].Rows[i]["Status"].ToString() == "Pending")
                                    {
                                        FlagSet = 1;
                                        FlagSet++;
                                        break;
                                    }
                                    else
                                        FlagSet = 0;
                                }
                            }

                            foreach (var button in this.gbCalendar.Controls.OfType<System.Windows.Forms.Button>())
                            {
                                if (button.Text == j.ToString())
                                {
                                    if (FlagSet > 0)
                                    {
                                        button.ForeColor = Color.Black;
                                        button.BackColor = Color.Yellow;
                                    }
                                    else
                                    {
                                        button.ForeColor = Color.Black;
                                        button.BackColor = Color.Lime;
                                    }
                                }
                            }
                        }
                    }
            }
        }

        private void GetMonth()
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
            if (cmbMonth.SelectedIndex > -1)
            {
                BookingMonth = objRL.GetMonthNumber(cmbMonth.Text);
                if (BookingMonth == Convert.ToInt32(DateTime.Now.Date.Month))
                    Set_Today_Date();
                else
                {
                    DString = "";
                    HighLight_Button();
                }
                SetButtons();
            }
        }

        private void SetButtons()
        {
            if (year_C > 0)
            {
                btn29.Visible = true;
                btn30.Visible = true;
                btn31.Visible = true;

                if (BookingMonth == 2)
                {
                    if (year_C % 4 == 0)
                    {
                        btn30.Visible = false;
                        btn31.Visible = false;
                    }
                    else
                    {
                        btn29.Visible = false;
                        btn30.Visible = false;
                        btn31.Visible = false;
                    }
                }
                else if (BookingMonth == 4 || BookingMonth == 6 || BookingMonth == 9 || BookingMonth == 11)
                    btn31.Visible = false;
                else
                    btn31.Visible = true;
            }
        }


        protected void FillGrid()
        {
            dataGridView1.Rows.Clear();
            RowIndexStatic = 0;
            SrNo = 1;
            DataSet ds = new DataSet();
            objBL.Query = "select T.ID,T.EntryDate as [Date],T.EntryTime as [Time],L.UserName as [Department],T.Task,T.FollowUp,T.Status,T.CompleteDate  as [Complete Date],T.CompleteDays as [Complete Days],T.LoginId from AssignTask T inner join Login L on L.ID=T.LoginId where T.CancelTag=0 and L.CancelTag=0 and T.EntryDate=#" + ButtonDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.Visible = true; gbDetails.Visible = true;
                gbDetails.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[RowIndexStatic].Cells["clmId"].Value = ds.Tables[0].Rows[i]["ID"].ToString();
                    TMId = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                    dataGridView1.Rows[RowIndexStatic].Cells["clmSrNo"].Value = SrNo.ToString();
                    EntryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]);
                    dataGridView1.Rows[RowIndexStatic].Cells["clmEntryDate"].Value = Convert.ToString(EntryDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Department"])))
                        dataGridView1.Rows[RowIndexStatic].Cells["clmDepartment"].Value = ds.Tables[0].Rows[i]["Department"].ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Task"])))
                        dataGridView1.Rows[RowIndexStatic].Cells["clmTask"].Value = ds.Tables[0].Rows[i]["Task"].ToString();
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Status"])))
                        dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value = ds.Tables[0].Rows[i]["Status"].ToString();

                    dataGridView1.Rows[RowIndexStatic].Cells["clmComplete"].Value = "Complete";
                    RowIndexStatic++;
                    SrNo++;

                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Status = row.Cells["clmStatus"].Value.ToString();
                if (Status.ToString() == "Pending")
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                else if (Status.ToString() == "Complete")
                    row.DefaultCellStyle.BackColor = Color.Lime;
                else
                    row.DefaultCellStyle.BackColor = Color.Red;
            }
        }

        protected void FillGrid_All()
        {
            dataGridView1.Rows.Clear();
            RowIndexStatic = 0;
            SrNo = 1;
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;
            DataSet ds = new DataSet();

            MainQuery = "Select TM.ID,TM.DepartmentId,L.UserName as [Department],TM.Task,TM.ScheduleOn as [Schedule On],TM.ScheduleDay as [Schedule Day],TM.ScheduleDate  as [Schedule Date],TM.CreatedDate as [Date] from TaskMaster TM inner join Login L on L.ID=TM.DepartmentId where TM.CancelTag=0 and L.CancelTag=0 and TM.ScheduleOn='Monthly'  and TM.ScheduleDate=#" + ButtonDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";
            OrderByClause = "  order by TM.CreatedDate asc";

            if (DepartmentFlag)
            {
                if (cmbDepartment.SelectedIndex < 0)
                    cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
                if (cmbDepartment.SelectedIndex > -1)
                    WhereClause += " and TM.LoginId=" + cmbDepartment.SelectedValue + "";
            }

            if (string.IsNullOrEmpty(WhereClause))
                WhereClause = string.Empty;

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gbDetails.Visible = true;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TMId = 0; FlagTask = false;
                    //0 ID,
                    //1 DepartmentId,
                    //2 L.UserName
                    //3 Task,
                    //4 ScheduleOn,
                    //5 ScheduleDay,
                    //6 ScheduleDate
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[RowIndexStatic].Cells["clmId"].Value = ds.Tables[0].Rows[i]["ID"].ToString();
                    TMId = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                    dataGridView1.Rows[RowIndexStatic].Cells["clmSrNo"].Value = SrNo.ToString();
                    EntryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]);
                    dataGridView1.Rows[RowIndexStatic].Cells["clmEntryDate"].Value = Convert.ToString(EntryDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
                    //dataGridView1.Rows[RowIndexStatic].Cells["clmEntryDate"].Value = Convert.ToString(EntryDate.ToString(BusinessResources.T));
                    dataGridView1.Rows[RowIndexStatic].Cells["clmDepartment"].Value = ds.Tables[0].Rows[i]["Department"].ToString();
                    dataGridView1.Rows[RowIndexStatic].Cells["clmTask"].Value = ds.Tables[0].Rows[i]["Task"].ToString();
                    //dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value = "Pending";
                    Get_StatusInformation();

                    if (FlagTask)
                    {
                        dataGridView1.Rows[RowIndexStatic].Cells["clmFollowUp"].Value = FollowUp;
                        dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value = Status;
                        dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDate"].Value = Convert.ToString(CompleteDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
                        dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDays"].Value = CompleteDays;
                    }
                    else
                    {
                        dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value = "Pending";
                        dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDate"].Value = "";
                        dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDays"].Value = "";
                        dataGridView1.Rows[RowIndexStatic].Cells["clmFollowUp"].Value = "";
                    }
                    //
                    //dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value = ds.Tables[0].Rows[i]["Status"].ToString();
                    //Status = dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value.ToString();
                    //CompleteDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Complete Date"]);
                    //dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDate"].Value = Convert.ToString(CompleteDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
                    //dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDays"].Value = ds.Tables[0].Rows[i]["Complete Days"].ToString();

                    //if (Status.ToString() == "Pending")
                    //    dataGridView1.DefaultCellStyle.BackColor = Color.Yellow;
                    //else if (Status.ToString() == "Complete")
                    //    dataGridView1.DefaultCellStyle.BackColor = Color.Lime;
                    //else
                    //    dataGridView1.DefaultCellStyle.BackColor = Color.Red;
                    SrNo++;
                    RowIndexStatic++;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Status = row.Cells["clmStatus"].Value.ToString();
                    if (Status.ToString() == "Pending")
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    else if (Status.ToString() == "Complete")
                        row.DefaultCellStyle.BackColor = Color.Lime;
                    else
                        row.DefaultCellStyle.BackColor = Color.Red;
                }

                gbDetails.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        private void Get_StatusInformation()
        {
            FollowUp = string.Empty;
            Status = string.Empty;
            CompleteDate = DateTime.Now.Date;
            CompleteDays = string.Empty;

            DataSet ds = new DataSet();
            objBL.Query = "select T.ID,T.EntryDate as [Date],T.EntryTime as [Time],L.UserName as [Department],T.Task,T.FollowUp,T.Status,T.CompleteDate  as [Complete Date],T.CompleteDays as [Complete Days],T.LoginId from AssignTask T inner join Login L on L.ID=T.LoginId where T.CancelTag=0 and L.CancelTag=0 and T.TMId=" + TMId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
            {
                FlagTask = true;
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["FollowUp"].ToString())))
                    FollowUp = ds.Tables[0].Rows[0]["FollowUp"].ToString();
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Status"].ToString())))
                    Status = ds.Tables[0].Rows[0]["Status"].ToString();
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Complete Days"].ToString())))
                    CompleteDays = ds.Tables[0].Rows[0]["Complete Days"].ToString();
                if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["Complete Days"].ToString())))
                    CompleteDays = Convert.ToString(ds.Tables[0].Rows[0]["Complete Days"].ToString());
            }
            else
                FlagTask = false;
        }

        private void btnAddYourTask_Click(object sender, EventArgs e)
        {
            TaskMaster objForm = new TaskMaster();
            objForm.ShowDialog(this);
            Set_Today_Date();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DeleteValueDataGridView(dataGridView1, e.RowIndex);
        }

        private void DeleteValueDataGridView(DataGridView dgv, int IndexGrid)
        {
            if (dgv.CurrentCell.ColumnIndex == 7)
            {
                DialogResult dr;
                dr = objRL.ReturnDialogResult_Complete();
                if (dr == DialogResult.Yes)
                {
                    int ID_Update = Convert.ToInt32(dgv.Rows[IndexGrid].Cells["clmId"].Value.ToString());
                    objBL.Query = "update AssignTask set  Status='Complete',CompleteDate='" + DateTime.Now.Date.ToShortDateString() + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + ID_Update + "";
                    objBL.Function_ExecuteNonQuery();
                    FillGrid();
                    FillGrid_AssignTask_Monthly();
                    //dgv.Rows.RemoveAt(IndexGrid);
                    //FillSRNO(dgv);
                }
            }
        }

        //protected void FillGrid()
        //{
        //    dataGridView1.Rows.Clear();
        //    RowIndexStatic = 0;
        //    SrNo = 1;
        //    MainQuery = string.Empty;
        //    WhereClause = string.Empty;
        //    OrderByClause = string.Empty;
        //    UserClause = string.Empty;

        //    DataSet ds = new DataSet();

        //    MainQuery = "select T.ID,T.EntryDate as [Date],T.EntryTime as [Time],L.UserName as [Department],T.Task,T.FollowUp,T.Status,T.CompleteDate  as [Complete Date],T.CompleteDays as [Complete Days],T.LoginId from AssignTask T inner join Login L on L.ID=T.LoginId where T.CancelTag=0 and L.CancelTag=0";
        //    OrderByClause = "  order by T.EntryDate desc";

        //    if (StatusFlag)
        //    {
        //        if (cmbDepartment.SelectedIndex < 0)
        //            cmbStatusSearch.Text = "Pending";
        //        if (cmbStatusSearch.SelectedIndex > -1)
        //            WhereClause += " and T.Status='" + cmbStatusSearch.Text + "'";
        //    }

        //    if (DepartmentFlag)
        //    {
        //        if (cmbDepartment.SelectedIndex < 0)
        //            cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
        //        if (cmbDepartment.SelectedIndex > -1)
        //            WhereClause += " and T.LoginId=" + cmbDepartment.SelectedValue + "";
        //    }

        //    if (IDFlag)
        //        WhereClause += " and T.ID=" + txtSearchID.Text + "";
        //    if (SearchByTask)
        //        WhereClause += " and T.Task like '%" + txtSearchTask.Text + "%'";

        //    if (string.IsNullOrEmpty(WhereClause))
        //        WhereClause = string.Empty;

        //    objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
        //    ds = objBL.ReturnDataSet();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            //0 ID,
        //            //1 EntryDate as [Date],
        //            //2 EntryTime as [Time],
        //            //3 L.UserName as [Department],,
        //            //4 AT.Task
        //            //5 T.FollowUp,
        //            //6 T.Status,
        //            //7 T.CompleteDate  as [Complete Date],
        //            //8 T.CompleteDays as [Complete Days],
        //            //9T.LoginId
        //            dataGridView1.Rows.Add();

        //            if (cmbStatusSearch.Text == "Pending")
        //                dataGridView1.Columns["clmCheckBox"].Visible = true;
        //            else
        //                dataGridView1.Columns["clmCheckBox"].Visible = false;

        //            dataGridView1.Rows[RowIndexStatic].Cells["clmId"].Value = ds.Tables[0].Rows[i]["ID"].ToString();
        //            dataGridView1.Rows[RowIndexStatic].Cells["clmSrNo"].Value = SrNo.ToString();
        //            EntryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]);
        //            dataGridView1.Rows[RowIndexStatic].Cells["clmEntryDate"].Value = Convert.ToString(EntryDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
        //            //dataGridView1.Rows[RowIndexStatic].Cells["clmEntryDate"].Value = Convert.ToString(EntryDate.ToString(BusinessResources.T));
        //            dataGridView1.Rows[RowIndexStatic].Cells["clmDepartment"].Value = ds.Tables[0].Rows[i]["Department"].ToString();
        //            dataGridView1.Rows[RowIndexStatic].Cells["clmTask"].Value = ds.Tables[0].Rows[i]["Task"].ToString();
        //            dataGridView1.Rows[RowIndexStatic].Cells["clmFollowUp"].Value = ds.Tables[0].Rows[i]["FollowUp"].ToString();
        //            dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value = ds.Tables[0].Rows[i]["Status"].ToString();
        //            Status = dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value.ToString();

        //            //if (Status.ToString() == "Pending")
        //            //    dataGridView1.DefaultCellStyle.BackColor = Color.Yellow;
        //            //else if (Status.ToString() == "Complete")
        //            //    dataGridView1.DefaultCellStyle.BackColor = Color.Lime;
        //            //else
        //            //    dataGridView1.DefaultCellStyle.BackColor = Color.Red;

        //            CompleteDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Complete Date"]);
        //            dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDate"].Value = Convert.ToString(CompleteDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
        //            dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDays"].Value = ds.Tables[0].Rows[i]["Complete Days"].ToString();
        //            SrNo++;
        //            RowIndexStatic++;
        //        }

        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            Status = row.Cells["clmStatus"].Value.ToString();
        //            if (Status.ToString() == "Pending")
        //                row.DefaultCellStyle.BackColor = Color.Yellow;
        //            else if (Status.ToString() == "Complete")
        //                row.DefaultCellStyle.BackColor = Color.Lime;
        //            else
        //                row.DefaultCellStyle.BackColor = Color.Red;
        //        }
        //    }
        //}
    }
}
