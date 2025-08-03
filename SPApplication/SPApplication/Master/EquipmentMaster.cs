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
    public partial class EquipmentMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0, Result = 0, DepartmentId = 0;
        bool SearchTag = false;

        public EquipmentMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PARTEQUIPMENT);
            Set_Department();
        }
        private void Set_Department()
        {
            if (BusinessLayer.UserName_Static == BusinessResources.USER_ADMIN || BusinessLayer.UserName_Static == BusinessResources.USER_ACCOUNTS || BusinessLayer.UserName_Static == BusinessResources.USER_AJAY)
            {
                //cmbDepartmentSearch.Text = BusinessLayer.UserName_Static.ToString();
                cmbDepartment.Enabled = true;
                 
            }
            else
            {
                cmbDepartment.Enabled = false;
                cmbDepartment.Text = BusinessLayer.UserName_Static.ToString();
            }
        }

        private void EquipmentMaster_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            cmbDepartment.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
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
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update EquipmentMaster set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update EquipmentMaster set DepartmentId=" + DepartmentId + ",EquipmentName='" + txtEquipmentName.Text + "',Description='" + txtDescription.Text + "',ModifiedId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into EquipmentMaster(DepartmentId,EquipmentName,Description,UserId) values(" + DepartmentId + ",'" + txtEquipmentName.Text + "','" + txtDescription.Text + "'," + BusinessLayer.UserId_Static + ")";

                    Result = objBL.Function_ExecuteNonQuery();
                    if (Result > 0)
                    {
                        FillGrid();
                        ClearAll();
                        if (!FlagDelete)
                            objRL.ShowMessage(7, 1);
                        else
                            objRL.ShowMessage(9, 1);
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

        private void ClearAll()
        {
            Result = 0;
            objEP.Clear();
            FlagDelete = false;
            TableID = 0;
            cmbDepartment.SelectedIndex = -1;
            txtEquipmentName.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
            txtEquipmentName.Focus();
        }

        protected bool Validation()
        {
            objEP.Clear();
            if (cmbDepartment.SelectedIndex == -1)
            {
                cmbDepartment.Focus();
                objEP.SetError(cmbDepartment, "Enter Department Name");
                return true;
            }
            else if (txtEquipmentName.Text == "")
            {
                txtEquipmentName.Focus();
                objEP.SetError(txtEquipmentName, "Enter Equipment Name");
                return true;
            }
            else if (txtDescription.Text == "")
            {
                txtDescription.Focus();
                objEP.SetError(txtDescription, "Enter Description");
                return true;
            }
            else
                return false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dataGridView1.Rows.Count;
                CurrentRowIndex = dataGridView1.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    DepartmentId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    cmbDepartment.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtEquipmentName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (!SearchTag)
                objBL.Query = "select EM.ID,EM.DepartmentId,L.UserName as [Department],EM.EquipmentName as [Equipment Name],EM.Description from EquipmentMaster EM inner join Login L on L.ID=EM.DepartmentId where EM.CancelTag=0 and L.CancelTag=0 order by EM.EquipmentName asc";
            else
                objBL.Query = "select EM.ID,EM.DepartmentId,L.UserName as [Department],EM.EquipmentName as [Equipment Name],EM.Description from EquipmentMaster EM inner join Login L on L.ID=EM.DepartmentId where EM.CancelTag=0 and L.CancelTag=0 and EM.EquipmentName like '%" + txtSearch.Text + "%' order by EM.EquipmentName asc"; 

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 EM.ID,
                //1 EM.DepartmentId,
                //2 L.UserName as [Department],
                //3 EM.EquipmentName as [Equipment Name],
                //4 EM.Description
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 200;
                dataGridView1.Columns[4].Width = 150;
               
            }
        }
        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from EquipmentMaster where DepartmentId=" + DepartmentId + " and CancelTag=0 and EquipmentName='" + txtEquipmentName.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}
