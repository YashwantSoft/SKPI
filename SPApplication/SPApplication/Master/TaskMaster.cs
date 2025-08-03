using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApplication.Master
{
    public partial class TaskMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        public TaskMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_TASKMASTER);
            objRL.Fill_Users(cmbDepartment);
            //Fortnightly
            //Monthly
            //Quarterly
            //Half Yearly
            //Yearly
            //Set_Department();
        }
        
        private void TaskMaster_Load(object sender, EventArgs e)
        {
            ClearAll();
            Set_Department();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll()
        {
            FlagDelete = false;
            objEP.Clear();
            TableID = 0;
            cmbDepartment.SelectedIndex = -1;
            cmbScheduleOn.SelectedIndex = -1;
            cmbDay.SelectedIndex = -1;
            dtpScheduleDate.Value = DateTime.Now.Date;
            txtTask.Text = "";
            lblDay.Visible = false;
            cmbDay.Visible = false;
            lblDate.Visible = false;
            dtpScheduleDate.Visible = false;
            Set_Department();
            cmbDepartment.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        protected bool Validation()
        {
            bool ReturnFlag = false;
            objEP.Clear();
            if (cmbDepartment.SelectedIndex == -1)
            {
                cmbDepartment.Focus();
                objEP.SetError(cmbDepartment, "Select Department Name");
                ReturnFlag = true;
            }
            else if (cmbScheduleOn.SelectedIndex == -1)
            {
                cmbScheduleOn.Focus();
                objEP.SetError(cmbScheduleOn, "Select Department Name");
                ReturnFlag = true;
            }
            else if (txtTask.Text == "")
            {
                txtTask.Focus();
                objEP.SetError(txtTask, "Enter Task");
                ReturnFlag = true;
            }
            else
                ReturnFlag = false;

            if (!ReturnFlag)
            {
                if (cmbScheduleOn.Text == "Weekly")
                {
                    if (cmbDay.SelectedIndex == -1)
                    {
                        cmbDay.Focus();
                        objEP.SetError(cmbDay, "Select Day");
                        ReturnFlag = true;
                    }
                }
                else
                    ReturnFlag = false;
            }
                return false;
        }
        
        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from TaskMaster where CancelTag=0 and DepartmentId=" + cmbDepartment.SelectedValue + " and ScheduleOn='" + cmbScheduleOn.Text + "' and ScheduleDay='" + cmbDay.Text + "' and ScheduleDate=#" + dtpScheduleDate.Value.ToString(BusinessResources.DATEFORMATMMDDYYYY) + "# and Task='" + txtTask.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        string ScheduleDay = string.Empty;

        protected void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    if (cmbScheduleOn.Text == "Monthly")
                        ScheduleDay = dtpScheduleDate.Value.DayOfWeek.ToString();
                    else if (cmbScheduleOn.Text == "Weekly")
                        ScheduleDay = cmbDay.Text.ToString();
                    else
                        ScheduleDay = "";

                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update TaskMaster set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update TaskMaster set DepartmentId=" + cmbDepartment.SelectedValue + ",Task='" + txtTask.Text + "',ScheduleOn='" + cmbScheduleOn.Text + "',ScheduleDay='" + ScheduleDay + "',ScheduleDate='" + dtpScheduleDate.Value.ToShortDateString() + "', UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into TaskMaster(DepartmentId,Task,ScheduleOn,ScheduleDay,ScheduleDate,UserId) values(" + cmbDepartment.SelectedValue + ",'" + txtTask.Text + "','" + cmbScheduleOn.Text + "','" + ScheduleDay + "','" + dtpScheduleDate.Value.ToShortDateString() + "'," + BusinessLayer.UserId_Static + ")";

                    Result = objBL.Function_ExecuteNonQuery();

                    if (Result > 0)
                    {
                        FillGrid();
                        ClearAll();
                        if(FlagDelete)
                            objRL.ShowMessage(9, 1);
                        else
                            objRL.ShowMessage(7, 1);
                    }
                }
                else
                {
                    objRL.ShowMessage(12, 9);
                    return;
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        int Result = 0;
        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        private void Set_Department()
        {
            cmbDepartment.Enabled = false;
            
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_VIJAY)
                cmbDepartment.Enabled = true;

            cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
            FillGrid();
        }

        protected void FillGrid()
        {
            if (cmbDepartment.SelectedIndex > -1)
            {
                dataGridView1.DataSource = null;
                DataSet ds = new DataSet();

                if (!SearchTag)
                    objBL.Query = "select TM.ID,TM.DepartmentId,L.UserName as [Department],TM.Task,TM.ScheduleOn as [Schedule On],TM.ScheduleDay as [Schedule Day],TM.ScheduleDate  as [Schedule Date] from TaskMaster TM inner join Login L on L.ID=TM.DepartmentId where TM.CancelTag=0 and L.CancelTag=0 and TM.DepartmentId=" + cmbDepartment.SelectedValue + "";
                else
                    objBL.Query = "select TM.ID,TM.DepartmentId,L.UserName as [Department],TM.Task,TM.ScheduleOn as [Schedule On],TM.ScheduleDay as [Schedule Day],TM.ScheduleDate  as [Schedule Date] from TaskMaster TM inner join Login L on L.ID=TM.DepartmentId where TM.CancelTag=0 and L.CancelTag=0 and TM.DepartmentId=" + cmbDepartment.SelectedValue + " and TM.Task like '%" + txtSearch.Text + "%'";

                ds = objBL.ReturnDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //0 ID,
                    //1 DepartmentId,
                    //2 L.UserName
                    //3 Task,
                    //4 ScheduleOn,
                    //5 ScheduleDay,
                    //6 ScheduleDate

                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Width = 120;
                    dataGridView1.Columns[3].Width = 550;
                    dataGridView1.Columns[4].Width = 120;
                    dataGridView1.Columns[5].Width = 120;
                    dataGridView1.Columns[6].Width = 120;
                    lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             
                DialogResult dr;
                dr = objRL.Delete_Record_Show_Message(); // MessageBox.Show("Do yo want to delete this record?", "Delete Record", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    FlagDelete = true;
                    SaveDB();
                    
                 
                }
                else
                    ClearAll();
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        private void cmbScheduleOn_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ScheduleOn_Selected();
        }

        private void ScheduleOn_Selected()
        {
            if (cmbScheduleOn.SelectedIndex > -1)
            {
                lblDay.Visible = false;
                cmbDay.Visible = false;
                lblDate.Visible = false;
                dtpScheduleDate.Visible = false;
                if (cmbScheduleOn.Text == "Weekly")
                {
                    lblDay.Visible = true;
                    cmbDay.Visible = true;
                    lblDate.Visible = false;
                    dtpScheduleDate.Visible = false;
                }
                if (cmbScheduleOn.Text == "Monthly")
                {
                    lblDate.Visible = true;
                    dtpScheduleDate.Visible = true;
                    lblDay.Visible = false;
                    cmbDay.Visible = false;
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    //0 ID,
                    //1 DepartmentId,
                    //2 L.UserName
                    //3 Task,
                    //4 ScheduleOn,
                    //5 ScheduleDay,
                    //6 ScheduleDate

                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    cmbDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtTask.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cmbScheduleOn.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    cmbDay.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    dtpScheduleDate.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
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

        private void cmbDepartment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbDepartment.SelectedIndex > -1)
                FillGrid();
        }

        private void dtpScheduleDate_ValueChanged(object sender, EventArgs e)
        {
            string DayDate = dtpScheduleDate.Value.DayOfWeek.ToString();
            if (DayDate == "Tuesday")
            {
                dtpScheduleDate.Value = DateTime.Now.Date;
                return;
            }
        }
    }
}
