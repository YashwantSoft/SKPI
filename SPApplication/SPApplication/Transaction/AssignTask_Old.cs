using BusinessLayerUtility;
using SPApplication.Master;
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
    public partial class AssignTask_Old : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0, EmployeeId = 0;

        public AssignTask_Old()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_ASSIGNTASK);
            objRL.Fill_Users(cmbDepartment);
            objRL.Fill_Users(cmbSearchList);
            btnAll.BackColor = objDL.GetBackgroundColor();
            btnAll.ForeColor = objDL.GetForeColor();
            cmbStatusSearch.Text = "Pending";
            cbAllSearch.Checked = false;    
        }

        private void ClearAll()
        {
            btnSave.Visible = true;
            objEP.Clear();
            TableID = 0;
            FlagDelete = false;
            dtpDate.Value = DateTime.Now.Date;
            dtpTime.Value = DateTime.Now;
            txtID.Text = "";
            // cmbDepartment.SelectedIndex = -1;
            rtbTask.Text = "";
            rtbNotes.Text = "";
            cmbStatus.SelectedIndex = -1;
            GetID();
            IDFlag = false;
            SearchTag = false;
            SearchByTask = false;
            DateFlag = false;
            cbAllSearch.Checked = false;
            cmbStatusSearch.Text = "Pending";
            cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
            cmbSearchList.Text = BusinessLayer.UserName_Static.ToString();
            cmbPriority.SelectedIndex = -1;
            dtpDueDate.Value = DateTime.Now.Date;
            dtpCompleteDate.Value = DateTime.Now.Date;
            txtTaskDays.Text = "0";
            txtCompleteDays.Text = "0";
            ClearAllSearch();
            rtbTask.Focus();
        }

        private void ClearAllSearch()
        {
            IDFlag = false;
            SearchTag = false;
            SearchByTask = false;
            cmbSearchList.SelectedIndex = -1;
            cmbStatusSearch.SelectedIndex = -1;
            txtSearchID.Text = "";
            cbAllSearch.Checked = false;
            cmbStatusSearch.Text = "Pending";
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_AJAY)
                cmbDepartment.Enabled = true;
            else
            {
                SearchTag = true;
                cmbDepartment.Enabled = false;
                cmbSearchList.Enabled = false;
                cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
                cmbSearchList.Text = BusinessLayer.UserName_Static.ToString();
            }
        }

        private void GetID()
        {
            int IDNo = 0;
            IDNo = Convert.ToInt32(objRL.ReturnMaxID("AssignTask"));
            txtID.Text = IDNo.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AssignTask_Load(object sender, EventArgs e)
        {
            ClearAll();

           
            FillGrid();
            cmbPriority.Text = "High";
            cmbPriority.Text = "High";
            rtbTask.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
            cbToday.Checked = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = objRL.Delete_Record_Show_Message();
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                FlagDelete = true;
                SaveDB();
            }
            else
                return;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
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
            else if (cmbStatus.SelectedIndex == -1)
            {
                objEP.SetError(cmbStatus, "Select cmbStatus");
                cmbStatus.Focus();
                return true;
            }
            else
                return false;
        }

        int ComDays = 0, TasDays = 0;
        protected void SaveDB()
        {
            if (!Validation())
            {
                ComDays = 0; TasDays = 0;

                if (txtTaskDays.Text != "")
                    TasDays = Convert.ToInt32(txtTaskDays.Text);
                if (txtCompleteDays.Text != "")
                    ComDays = Convert.ToInt32(txtCompleteDays.Text);

                if (TableID != 0)
                    if (FlagDelete)
                        objBL.Query = "update AssignTask set CancelTag=1 where ID=" + TableID + "";
                    else
                        objBL.Query = "update AssignTask set EntryDate='" + dtpDate.Value.ToShortDateString() + "',EntryTime='" + dtpTime.Value.ToShortTimeString() + "',LoginId=" + cmbDepartment.SelectedValue + ",Task='" + rtbTask.Text + "',Priority='" + cmbPriority.Text + "',Status='" + cmbStatus.Text + "',DueDate='" + dtpDate.Value.ToShortDateString() + "',TaskDays='" + TasDays + "',CompleteDate='" + dtpCompleteDate.Value.ToShortDateString() + "',CompleteDays='" + ComDays + "',Notes='" + rtbNotes.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                else
                    objBL.Query = "insert into AssignTask(EntryDate,EntryTime,LoginId,Task,Priority,Status,DueDate,TaskDays,CompleteDate,CompleteDays,Notes,UserId) values('" + dtpDate.Value.ToShortDateString() + "','" + dtpTime.Value.ToShortTimeString() + "'," + cmbDepartment.SelectedValue + ",'" + rtbTask.Text + "','" + cmbPriority.Text + "','" + cmbStatus.Text + "','" + dtpDate.Value.ToShortDateString() + "'," + TasDays + ",'" + dtpCompleteDate.Value.ToShortDateString() + "'," + ComDays + ",'" + rtbNotes.Text + "'," + BusinessLayer.UserId_Static + ")";

                if (objBL.Function_ExecuteNonQuery() > 0)
                {
                    if (FlagDelete)
                        objRL.ShowMessage(9, 1);
                    else
                        objRL.ShowMessage(7, 1);

                    ClearAll();
                    FillGrid();
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        string MainQuery = string.Empty;
        string WhereClause = string.Empty;
        string OrderByClause = string.Empty;
        string UserClause = string.Empty;
        bool DateFlag = false;
        bool SearchTag = false;
        bool IDFlag = false;
        string Status = string.Empty;

        protected void FillGrid()
        {
            MainQuery = string.Empty;
            WhereClause = string.Empty;
            OrderByClause = string.Empty;
            UserClause = string.Empty;

            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            MainQuery = "select T.ID as [Select],T.ID,T.EntryDate as [Date],T.EntryTime as [Time],L.UserName as [Department],T.Task,T.FollowUp,T.Status,T.CompleteDate  as [Complete Date],T.CompleteDays as [Complete Days] from AssignTask T inner join Login L on L.ID=T.LoginId where T.CancelTag=0 and L.CancelTag=0";
            OrderByClause = "  order by T.EntryDate desc";

            if (cbAllSearch.Checked)
                WhereClause = string.Empty;
            else
                WhereClause = " and T.Status='" + cmbStatusSearch.Text + "'";
            
            if (DateFlag)
                WhereClause += " and T.EntryDate between #" + dtpFromDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "# and #" + dtpToDate.Value.ToString(RedundancyLogics.DateFormatMMDDYYYY) + "#";
            if (SearchTag)
                WhereClause += " and T.LoginId=" + cmbSearchList.SelectedValue + "";
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
                //0 T.ID as [Select],
                //1 ID,
                //2 EntryDate as [Date],
                //3 EntryTime as [Time],
                //4 L.UserName as [Department],,
                //5 AT.Task
                //6 T.FollowUp,
                //7 T.Status,
                //8 T.CompleteDate  as [Complete Date],
                //9 T.CompleteDays as [Complete Days],
 

                dataGridView1.DataSource = ds.Tables[0];
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                //dataGridView1.Columns[0].Visible = false;
               // dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[0].Width =30;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].Width = 400;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 100;
                dataGridView1.Columns[7].Width = 120;
                dataGridView1.Columns[8].Width = 100;
                dataGridView1.Columns[9].Width = 120;
                //dataGridView1.Columns[10].Width = 120;
                //dataGridView1.Columns[11].Width = 200;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        Status = row.Cells[6].Value.ToString();
                        if (Status.ToString() == "Pending")
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                        else if (Status.ToString() == "Complete")
                            row.DefaultCellStyle.BackColor = Color.Lime;
                        else
                            row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        int LoginId = 0;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //0 ID,
            //1 EntryDate as [Date],
            //2 EntryTime as [Time],
            //3 L.UserName as [Department],,
            //4 AT.Task
            //5 AT.Priority
            //6 T.Status,
            //7 T.DueDate as[Due Date],
            //8 T.TaskDays as [Task Days],
            //9 T.CompleteDate  as [Complete Date],
            //10 T.CompleteDays as [Complete Days],
            //11 T.Notes

            try
            {
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtID.Text = TableID.ToString();
                    dtpDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    dtpTime.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    cmbDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    rtbTask.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    cmbPriority.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    cmbStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    if (cmbStatus.Text == "Complete")
                        btnSave.Visible = false;
                    dtpDueDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                    txtTaskDays.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    dtpCompleteDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                    txtCompleteDays.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    rtbNotes.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    LoginId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
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

        private void txtSearchID_TextChanged(object sender, EventArgs e)
        {
            SearchTag = false;
            PendingFlag = false;
            if (txtSearchID.Text != "")
                IDFlag = true;
            else
                IDFlag = false;

            FillGrid();
        }
        bool PendingFlag = false;
        private void cbAllSearch_CheckedChanged(object sender, EventArgs e)
        {
            //SearchTag = false;
            IDFlag = false;
            if (cbAllSearch.Checked)
            {
                cmbStatusSearch.SelectedIndex = -1;
                cmbStatusSearch.Enabled = false;
                PendingFlag = true;
            }
            else
            {
                cmbStatusSearch.SelectedIndex = -1;
                cmbStatusSearch.Enabled = true;
                PendingFlag = true;
            }
              
            FillGrid();
        }

        private void cmbSearchList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //PendingFlag = false;
            IDFlag = false;
            if (cmbSearchList.SelectedIndex > -1)
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            ClearAllSearch();
            FillGrid();
        }

        private void cbToday_CheckedChanged(object sender, EventArgs e)
        {
            //DateFlag = true;
            //IDFlag = false;
            //SearchTag = false;
            //if (cbToday.Checked)
            //{
            //    dtpFromDate.Enabled = false;
            //    dtpToDate.Enabled = false;
            //}
            //else
            //{
            //    dtpFromDate.Enabled = true;
            //    dtpToDate.Enabled = true;
            //}
            //FillGrid();
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpToDate.Value > dtpFromDate.Value)
            //{
            //    DateFlag = true;
            //    IDFlag = false;
            //    SearchTag = false;
            //}
            //else
            //    dtpToDate.Value = DateTime.Now.Date;

            //FillGrid();
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpFromDate.Value < dtpToDate.Value)
            //{
            //    DateFlag = true;
            //    IDFlag = false;
            //    SearchTag = false;
            //}
            //else
            //    dtpFromDate.Value = DateTime.Now.Date;

            //FillGrid();
        }

        private void cmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rtbTask.Focus();
        }

        private void rtbTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbPriority.Focus();
        }

        private void cmbPriority_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpDueDate.Focus();
        }

        private void dtpDueDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbStatus.Focus();
        }
        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rtbNotes.Focus();
        }
        private void rtbNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
        private void btnReport_Click(object sender, EventArgs e)
        {

        }

        private void cmbStatusSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbStatusSearch.SelectedIndex > -1)
            {
                FillGrid();
            }
        }

       // TimeSpan TaskDaysTS, CompleteDaysTS;
        int TaskDaysTS, CompleteDaysTS; 
        private void dtpDueDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDueDate.Value >= dtpDate.Value)
            {
                TaskDaysTS = Convert.ToInt32((dtpDueDate.Value - dtpDate.Value).TotalDays);

                //  TaskDaysTS = dtpDueDate.Value.Subtract(dtpDate.Value);
                txtTaskDays.Text = TaskDaysTS.ToString();
            }
            else
            {
                dtpDueDate.Value = DateTime.Now.Date;
                txtTaskDays.Text = "0";
            }
        }

        private void dtpCompleteDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpCompleteDate.Value >= dtpDate.Value)
            {
                CompleteDaysTS = Convert.ToInt32((dtpCompleteDate.Value - dtpDate.Value).TotalDays);

                //   CompleteDaysTS = dtpDueDate.Value.Subtract(dtpDate.Value);
                txtCompleteDays.Text = CompleteDaysTS.ToString();
            }
            else
            {
                dtpCompleteDate.Value = DateTime.Now.Date;
                txtCompleteDays.Text = "0";
            }
        }

        bool SearchByTask = false;
        private void txtSearchTask_TextChanged(object sender, EventArgs e)
        {
            IDFlag = false;
            
            if (txtSearchTask.Text != "")
                SearchByTask = true;
            else
                SearchByTask = false;

            FillGrid();
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            TaskMaster objForm = new TaskMaster();
            objForm.ShowDialog(this);
        }
    }
}
