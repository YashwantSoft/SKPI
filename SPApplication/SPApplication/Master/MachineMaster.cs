using BusinessLayerUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPApplication.Master
{
    public partial class MachineMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;
        string WhereClause = string.Empty;
        string MainQuery = string.Empty;

        public MachineMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_MACHINEMASTER);
        }

        private void MachineMaster_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            txtMachineNo.Focus();
        }

        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();
            WhereClause = string.Empty;
            MainQuery = string.Empty;

            MainQuery = "select MachineId,MachineNo as [Machine No],MachineDescription as [Machine Description],MachineStatus as [Machine Status] from MachineMaster where CancelTag=0 ";
            
            if (SearchTag)
                WhereClause = " and MachineNo like '%" + txtSearch.Text + "%'";
            
            objBL.Query = MainQuery + WhereClause; 
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //0 MachineId,
                //1 MachineNo as [Machine No],
                //2 MachineDescription as [Machine Description],
                //3 MachineStatus as [Machine Status] 

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 200;
                dataGridView1.Columns[2].Width = 300;
                dataGridView1.Columns[3].Width = 120;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select MachineId from MachineMaster where CancelTag=0 and MachineNo=" + txtMachineNo.Text + " and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
            dr = MessageBox.Show("Do yo want to delete this record", "Delete Record", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                FlagDelete = true;
                SaveDB();
            }
            else
                ClearAll();
        }

        protected bool Validation()
        {
            objEP.Clear();

            if (txtMachineNo.Text == "")
            {
                txtMachineNo.Focus();
                objEP.SetError(txtMachineNo, "Enter Machine No");
                return true;
            }
            else if (txtMachineDescription.Text == "")
            {
                txtMachineDescription.Focus();
                objEP.SetError(txtMachineDescription, "Enter Description");
                return true;
            }
            else
                return false;
        }

        protected void ClearAll()
        {
            TableID = 0;
            objEP.Clear();
            FlagDelete = false;
            btnDelete.Enabled = false;
            txtMachineNo.Text = "";
            txtMachineDescription.Text = "";
            cmbMachineStatus.SelectedIndex = -1;
            txtMachineNo.Focus();
        }

        private void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update MachineMaster set CancelTag=1 where MachineId=" + TableID + "";
                        else
                            objBL.Query = "update MachineMaster set MachineNo='" + txtMachineNo.Text + "',MachineDescription='" + objRL.ApostropheSave(txtMachineDescription.Text) + "',MachineStatus='"+cmbMachineStatus.Text+"',ModifiedId=" + BusinessLayer.UserId_Static + " where MachineId=" + TableID + "";
                    else
                        objBL.Query = "insert into MachineMaster(MachineNo,PartName,MachineDescription,MachineStatus,UserId) values('" + txtMachineNo.Text + "','" + objRL.ApostropheSave(txtMachineDescription.Text) + "','" + cmbMachineStatus.Text + "'," + BusinessLayer.UserId_Static + ")";

                    if (objBL.Function_ExecuteNonQuery() > 0)
                    {
                        if (!FlagDelete)
                            objRL.ShowMessage(7, 1);
                        else
                            objRL.ShowMessage(9, 1);

                        FillGrid();
                        ClearAll();
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
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
                    //1 MachineNo as [Machine No]
                    //2 Description
                    ClearAll();
                    btnDelete.Enabled = true;
                    TableID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    txtMachineNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtMachineDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cmbMachineStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
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

        private void txtMachineNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            objRL.NumericValue(sender, e, txtMachineNo);
        }
    }
}
