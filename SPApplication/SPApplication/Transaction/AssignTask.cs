using BusinessLayerUtility;
using SPApplication.Master;
using SPApplication.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Transaction
{
    public partial class AssignTask : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0, EmployeeId = 0;

        string Status = string.Empty;
        int CompleteDaysTS = 0;
        DateTime CurrentDate = DateTime.Now.Date;

        public AssignTask()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnReport, btnExit, BusinessResources.LBL_HEADER_ASSIGNTASK);
            objDL.SetButtonDesign(btnReport, BusinessResources.BTN_REPORT);
            objDL.SetPlusButtonDesign(btnAddTask);
            objRL.Fill_Users(cmbDepartment);
            objRL.Fill_Users(cmbDepartmentSearch);
            btnAll.BackColor = objDL.GetBackgroundColor();
            btnAll.ForeColor = objDL.GetForeColor();
            cmbStatusSearch.Text = "Pending";
            cbDepartmentAll.Checked = false;
            cmbStatusSearch.Text = "Pending";
            Set_Department();
        }

        private void Set_Department()
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_AJAY)
            {
                //cmbDepartmentSearch.Text = BusinessLayer.UserName_Static.ToString();
                cmbDepartment.Enabled = true;
                cbDepartmentAll.Enabled = true;
            }
            else
            {
                cmbDepartment.Enabled = false;
                cmbDepartmentSearch.Enabled = false;
                cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
                cmbDepartmentSearch.Text = BusinessLayer.UserName_Static.ToString();
                cbDepartmentAll.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        private void Get_Complete_Days()
        {
            if (CurrentDate >= dtpDate.Value)
                CompleteDaysTS = Convert.ToInt32((CurrentDate - dtpDate.Value).TotalDays);
        }

        private bool Check_CheckBox_Datagridview()
        {
            bool Return = false;

            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["clmCheckBox"].Value) == true)
                    {
                        Return = true;
                        break;
                    }
                    else
                        Return = false;
                }
            }
            return Return;
        }

        string FollowUp = string.Empty;

        protected void SaveDB()
        {
            if (Check_CheckBox_Datagridview())
            {
                if (cmbStatus.SelectedIndex > -1)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["clmCheckBox"].Value))
                        {
                            EntryDate = Convert.ToDateTime(dataGridView1.Rows[i].Cells["clmEntryDate"].Value.ToString());
                            dtpDate.Value = EntryDate;
                            Get_Complete_Days();

                            if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["clmFollowUp"].Value)))
                                FollowUp = Convert.ToString(dataGridView1.Rows[i].Cells["clmFollowUp"].Value);
                            else
                                FollowUp = "";

                            if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells["clmId"].Value)))
                                TableID = Convert.ToInt32(dataGridView1.Rows[i].Cells["clmId"].Value.ToString());

                            if (TableID != 0)
                            {
                                objBL.Query = "update AssignTask set FollowUp='" + FollowUp + "',Status='" + cmbStatus.Text + "',CompleteDate='" + CurrentDate.ToShortDateString() + "',CompleteDays='" + CompleteDaysTS + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                                objBL.Function_ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            else
            {
                if (!Validation())
                {
                    Get_Complete_Days();
                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update  AssignTask set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update AssignTask set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',LoginId=" + cmbDepartment.SelectedValue + ",Task='" + rtbTask.Text + "',FollowUp='" + rtbFollowUp.Text + "',Status='" + cmbStatus.Text + "',CompleteDate='" + CurrentDate.ToShortDateString() + "',CompleteDays='" + CompleteDaysTS + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into AssignTask(EntryDate,EntryTime,LoginId,Task,FollowUp,Status,CompleteDate,CompleteDays,TMId,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + cmbDepartment.SelectedValue + ",'" + rtbTask.Text + "','" + rtbFollowUp.Text + "','" + cmbStatus.Text + "','" + CurrentDate.ToShortDateString() + "'," + CompleteDaysTS + "," + TMId + "," + BusinessLayer.UserId_Static + ")";

                    if (objBL.Function_ExecuteNonQuery() > 0)
                    {
                        rtbTask.Focus();

                        //if (FlagDelete)
                        //    objRL.ShowMessage(9, 1);
                        //else
                        //{
                        //    objRL.ShowMessage(7, 1);
                        //    rtbTask.Focus();
                        //}
                     
                    }
                }
                else
                {
                    objRL.ShowMessage(17, 4);
                    return;
                }
            }
            FillGrid();
            ClearAll();
        }

        int TMId = 0;
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            cmbDepartment.SelectedIndex = -1;
            cmbDepartment.Focus();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtID.Text == "")
            {
                objEP.SetError(txtID, "Enter ID");
                txtID.Focus();
                return true;
            }
            else if (cmbDepartment.SelectedIndex == -1)
            {
                objEP.SetError(cmbDepartment, "Select Department");
                cmbDepartment.Focus();
                return true;
            }
            else if (rtbTask.Text == "")
            {
                objEP.SetError(rtbTask, "Enter Task");
                rtbTask.Focus();
                return true;
            }
            else
                return false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            //objDB1.GetSendToBackTrue();
        }

        Dashboard1 objDB1 = new Dashboard1();

        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void AT_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
            Fill_TaskSchedule();
            rtbTask.Focus();
        }

        private bool TaskFlag()
        {
            DataSet ds = new DataSet();
            objBL.Query = "Select ID,EntryDate,LoginId,Flag from TaskFlag where CancelTag=0 and EntryDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and LoginId=" + BusinessLayer.UserId_Static + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

         
        //DateTime ScheduleDate;
        //bool FlagSchedule = false;
        //private bool CheckDueDateTask()
        //{
        //    FlagSchedule = false;
        //    DataSet ds = new DataSet();
        //    objBL.Query = "Select TS.ID,TS.EntryDate,TS.LoginId,TS.TaskId,TM.Task,TS.StartDate,TS.DueDate,TS.Flag from TaskSchedule TS inner join TaskMaster TM on TM.ID=TS.TaskId where TS.CancelTag=0 and TM.CancelTag=0 and LogintId=" + BusinessLayer.UserId_Static + " and DueDate=#" + DateTime.Now.Date.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";
        //    ds = objBL.ReturnDataSet();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        FlagSchedule = true;
        //        return true;
        //    }
        //    else
        //    {
        //        DataSet dsSchedule = new DataSet();
        //        objBL.Query = "Select TS.ID,TS.EntryDate,TS.LoginId,TS.TaskId,TM.Task,TS.StartDate,TS.DueDate,TS.Flag from TaskSchedule TS inner join TaskMaster TM on TM.ID=TS.TaskId where TS.CancelTag=0 and TM.CancelTag=0 and LogintId=" + BusinessLayer.UserId_Static + "";
        //        dsSchedule = objBL.ReturnDataSet();
        //        if (dsSchedule.Tables[0].Rows.Count > 0)
        //        {
        //            FlagSchedule = false;
        //            return false;
        //        }
        //        else
        //        {
        //            FlagSchedule = true;
        //            return true;
        //        }
        //    }
        //}

        string TaskInsert = string.Empty, Schedule = string.Empty, ScheduleDay=string.Empty;
        string CurrentDay = string.Empty;
        bool InsertFlag = false,InsertTaskFlag = false;

        private void Fill_TaskSchedule()
        {
            if (!TaskFlag())
            {
                DataSet ds = new DataSet();
                objBL.Query = "select ID,DepartmentId,Task,ScheduleOn,ScheduleDay,ScheduleDate from TaskMaster where CancelTag=0 and DepartmentId=" + BusinessLayer.UserId_Static + "";
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        InsertFlag = false;
                        CurrentDay = Convert.ToString(DateTime.Now.DayOfWeek);

                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ID"].ToString())))
                            TMId = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["Task"].ToString())))
                            TaskInsert = Convert.ToString(ds.Tables[0].Rows[i]["Task"].ToString());
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ScheduleDay"].ToString())))
                            ScheduleDay = Convert.ToString(ds.Tables[0].Rows[i]["ScheduleDay"].ToString());
                        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[i]["ScheduleOn"].ToString())))
                            Schedule = Convert.ToString(ds.Tables[0].Rows[i]["ScheduleOn"].ToString());

                        if (CurrentDay != "Tuesday")
                        {
                            if (Schedule == "Daily")
                            {
                                InsertFlag = true;
                            }
                            else
                            {
                                if (CurrentDay == ScheduleDay)
                                    InsertFlag = true;
                            }

                            if (InsertFlag)
                            {
                                rtbTask.Text = TaskInsert.ToString();
                                cmbStatus.Text = "Pending";
                                FlagDelete = false;
                                SaveDB();
                                InsertTaskFlag = true;
                            }
                        }
                    }
                    if (InsertTaskFlag)
                    {
                        objBL.Query = "insert into TaskFlag(EntryDate,LoginId) values('" + DateTime.Now.Date.ToShortDateString() + "'," + BusinessLayer.UserId_Static + ")";
                        objBL.Function_ExecuteNonQuery();
                    }
                }
            }
        }
        
        private void ClearAll()
        {
            RowIndexStatic = 0;
            SrNo = 1;
            btnSave.Visible = true;
            objEP.Clear();
            TableID = 0;
            FlagDelete = false;
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;
            txtID.Text = "";
            rtbTask.Text = "";
            rtbFollowUp.Text = "";
            cmbStatus.SelectedIndex = -1;
            GetID();
            cmbStatus.Text = "Pending";

            IDFlag = false;
            SearchByTask = false;
            StatusFlag = true;
            DepartmentFlag = true;
            cmbStatusSearch.Text = "Pending";
            cmbDepartmentSearch.Text = BusinessLayer.UserName_Static.ToString();
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("AssignTask"));
            txtID.Text = IDNo.ToString();
        }

        private void cmbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (cbSelectAll.Checked)
                        dataGridView1.Rows[i].Cells["clmCheckBox"].Value = true;
                    else
                        dataGridView1.Rows[i].Cells["clmCheckBox"].Value = false;
                }
            }
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool IDFlag = false;
        bool SearchByTask = false;
        bool DepartmentFlag = false;
        bool StatusFlag = false;

        static int RowIndexStatic;
        DateTime CompleteDate, EntryDate;

        protected void FillGrid()
        {
            dataGridView1.Rows.Clear();
            RowIndexStatic = 0;
            SrNo = 1;
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;

            DataSet ds = new DataSet();
            
            MainQuery = "select T.ID,T.EntryDate as [Date],T.EntryTime as [Time],L.UserName as [Department],T.Task,T.FollowUp,T.Status,T.CompleteDate  as [Complete Date],T.CompleteDays as [Complete Days],T.LoginId from AssignTask T inner join Login L on L.ID=T.LoginId where T.CancelTag=0 and L.CancelTag=0";
            OrderByClause = "  order by T.EntryDate desc";

            DateTime dtCheckDate;
            dtCheckDate = DateTime.Now.Date;
            dtCheckDate = dtCheckDate.AddMonths(1);
            
            WhereClause = " and T.EntryDate < #" + dtCheckDate.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "#";

            if (StatusFlag)
            {
                if (cmbDepartmentSearch.SelectedIndex < 0)
                    cmbStatusSearch.Text = "Pending";
                if (cmbStatusSearch.SelectedIndex > -1)
                    WhereClause += " and T.Status='" + cmbStatusSearch.Text + "'";
            }

            if (DepartmentFlag)
            {
                if(cmbDepartmentSearch.SelectedIndex < 0)
                    cmbDepartmentSearch.Text = BusinessLayer.UserName_Static.ToString();
                if (cmbDepartmentSearch.SelectedIndex > -1)
                     WhereClause += " and T.LoginId=" + cmbDepartmentSearch.SelectedValue + "";
            }

            if (IDFlag)
                WhereClause += " and T.ID=" + txtSearchID.Text + "";
            if (SearchByTask)
                WhereClause += " and T.Task like '%" + txtSearchTask.Text + "%'";

            if (string.IsNullOrEmpty(WhereClause))
                WhereClause = string.Empty;

            objBL.Query = MainQuery + UserClause + WhereClause + OrderByClause;
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //0 ID,
                    //1 EntryDate as [Date],
                    //2 EntryTime as [Time],
                    //3 L.UserName as [Department],,
                    //4 AT.Task
                    //5 T.FollowUp,
                    //6 T.Status,
                    //7 T.CompleteDate  as [Complete Date],
                    //8 T.CompleteDays as [Complete Days],
                    //9T.LoginId

                    dataGridView1.Rows.Add();

                  
                    if (cmbStatusSearch.Text == "Pending")
                        dataGridView1.Columns["clmCheckBox"].Visible = true;
                    else
                        dataGridView1.Columns["clmCheckBox"].Visible = false;

                    dataGridView1.Rows[RowIndexStatic].Cells["clmId"].Value = ds.Tables[0].Rows[i]["ID"].ToString();
                    dataGridView1.Rows[RowIndexStatic].Cells["clmSrNo"].Value = SrNo.ToString();
                    EntryDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]);
                    dataGridView1.Rows[RowIndexStatic].Cells["clmEntryDate"].Value = Convert.ToString(EntryDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
                    //dataGridView1.Rows[RowIndexStatic].Cells["clmEntryDate"].Value = Convert.ToString(EntryDate.ToString(BusinessResources.T));
                    dataGridView1.Rows[RowIndexStatic].Cells["clmDepartment"].Value = ds.Tables[0].Rows[i]["Department"].ToString();
                    dataGridView1.Rows[RowIndexStatic].Cells["clmTask"].Value = ds.Tables[0].Rows[i]["Task"].ToString();
                    dataGridView1.Rows[RowIndexStatic].Cells["clmFollowUp"].Value = ds.Tables[0].Rows[i]["FollowUp"].ToString();
                    dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value = ds.Tables[0].Rows[i]["Status"].ToString();
                    Status = dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value.ToString();

                    //if (Status.ToString() == "Pending")
                    //    dataGridView1.DefaultCellStyle.BackColor = Color.Yellow;
                    //else if (Status.ToString() == "Complete")
                    //    dataGridView1.DefaultCellStyle.BackColor = Color.Lime;
                    //else
                    //    dataGridView1.DefaultCellStyle.BackColor = Color.Red;

                    CompleteDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Complete Date"]);
                    dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDate"].Value = Convert.ToString(CompleteDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
                    dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDays"].Value = ds.Tables[0].Rows[i]["Complete Days"].ToString();

                

                    SrNo++;
                    RowIndexStatic++;
                }

                dataGridView1.Columns["clmCompleteDate"].Visible = false;
                dataGridView1.Columns["clmCompleteDays"].Visible = false;

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
        }

        int SrNo = 1;

        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            SearchByTask = false;
            StatusFlag = false;
            DepartmentFlag = true;
            if (txtSearchID.Text != "")
                IDFlag = true;
            else
                IDFlag = false;

            FillGrid();
        }

        private void txtSearchTask_TextChanged(object sender, EventArgs e)
        {
            IDFlag = false;
            StatusFlag = false;
            DepartmentFlag = true;

            if (txtSearchTask.Text != "")
                SearchByTask = true;
            else
                SearchByTask = false;

            FillGrid();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            IDFlag = false;
            SearchByTask = false;
            cbDepartmentAll.Checked = true;
            cbStatusAll.Checked = true;
            Set_Department();
            //DepartmentFlag = true;
            FillGrid();
        }

        private void cmbDepartmentSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            IDFlag = false;
            SearchByTask = false;
            if (cmbDepartmentSearch.SelectedIndex > -1)
                    FillGrid();
        }

        private void cmbStatusSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbStatusSearch.SelectedIndex > -1)
            {
                FillGrid();
            }
        }

        private void cbDepartmentAll_CheckedChanged(object sender, EventArgs e)
        {
            SearchByTask = false;
            IDFlag = false;

            if (cbDepartmentAll.Checked)
            {
                cmbDepartmentSearch.SelectedIndex = -1;
                cmbDepartmentSearch.Enabled = false;
                DepartmentFlag = false;
            }
            else
            {
                cmbDepartmentSearch.SelectedIndex = -1;
                cmbDepartmentSearch.Enabled = true;
                DepartmentFlag = true;
            }
            FillGrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //0 ID,
                //1 ID,
                //2 EntryDate as [Date],
                //3 EntryTime as [Time],
                //4 L.UserName as [Department],,
                //5 AT.Task
                //6 T.FollowUp,
                //7 T.Status,
                //8 T.CompleteDate  as [Complete Date],
                //9 T.CompleteDays as [Complete Days],
                //10 T.LoginId

               //1  dataGridView1.Rows[RowIndexStatic].Cells["clmId"].Value = ds.Tables[0].Rows[i]["ID"].ToString();
               //2 dataGridView1.Rows[RowIndexStatic].Cells["clmSrNo"].Value = SrNo.ToString();
               //3 dataGridView1.Rows[RowIndexStatic].Cells["clmEntryDate"].Value = Convert.ToString(EntryDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
               //4 dataGridView1.Rows[RowIndexStatic].Cells["clmDepartment"].Value = ds.Tables[0].Rows[i]["Department"].ToString();
               //5  dataGridView1.Rows[RowIndexStatic].Cells["clmTask"].Value = ds.Tables[0].Rows[i]["Task"].ToString();
               //6 dataGridView1.Rows[RowIndexStatic].Cells["clmFollowUp"].Value = ds.Tables[0].Rows[i]["FollowUp"].ToString();
               //7  dataGridView1.Rows[RowIndexStatic].Cells["clmStatus"].Value = ds.Tables[0].Rows[i]["Status"].ToString();
               //8  dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDate"].Value = Convert.ToString(CompleteDate.ToString(BusinessResources.DATEFORMATDDMMYYYY));
               //9 dataGridView1.Rows[RowIndexStatic].Cells["clmCompleteDays"].Value = ds.Tables[0].Rows[i]["Complete Days"].ToString();

                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll();
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    txtID.Text = TableID.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    //dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    cmbDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    rtbTask.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    rtbFollowUp.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    cmbStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                    if (cmbStatus.Text == "Complete")
                        btnSave.Visible = false;
                    else
                        btnSave.Visible = true;

                    CompleteDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                    CompleteDaysTS = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());
                }
            }
            catch (Exception ex1)
            {
                objRL.ErrorMessge(ex1.ToString());
                return;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void cbStatusAll_CheckedChanged(object sender, EventArgs e)
        {
            SearchByTask = false;
            IDFlag = false;
            if (cbStatusAll.Checked)
            {
                cmbStatusSearch.SelectedIndex = -1;
                cmbStatusSearch.Enabled = false;
                StatusFlag = false;
            }
            else
            {
                cmbStatusSearch.SelectedIndex = -1;
                cmbStatusSearch.Enabled = true;
                cmbStatusSearch.Text = "Pending";
                StatusFlag = true;
            }
            FillGrid();
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            TaskMaster objForm = new TaskMaster();
            objForm.ShowDialog(this);
        }

        private void AssignTask_MinimumSizeChanged(object sender, EventArgs e)
        {

        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    //if (keyData == (Keys.LWin))// | Keys.D))
        //    //{
        //    //    this.WindowState = FormWindowState.Minimized;

        //    //    //SaveDB();
        //    //    //return true;
        //    }


        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
    }
}
