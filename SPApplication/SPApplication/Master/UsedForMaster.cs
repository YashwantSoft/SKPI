using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication.Master
{
    public partial class UsedForMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        public UsedForMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_USEDFOR);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
       
        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (!SearchTag)
                objBL.Query = "select ID,UserFor from UseForMaster where CancelTag=0";
            else
                objBL.Query = "select ID,UserFor from UseForMaster where CancelTag=0 and UserFor like '%" + txtSearch.Text + "%'";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 500;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }
        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from UseForMaster where CancelTag=0 and UserFor='" + txtManufractureName.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    SaveDB();
                    FillGrid();
                    ClearAll();
                    objRL.ShowMessage(7, 1);
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        protected void SaveDB()
        {
            if (TableID != 0)
                if (FlagDelete)
                    objBL.Query = "update UseForMaster set CancelTag=1 where ID=" + TableID + "";
                else
                    objBL.Query = "update UseForMaster set UserFor='" + txtManufractureName.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
            else
                objBL.Query = "insert into UseForMaster(UserFor,UserId) values('" + txtManufractureName.Text + "'," + BusinessLayer.UserId_Static + ")";

            objBL.Function_ExecuteNonQuery();
        }

        protected bool Validation()
        {
            objEP.Clear();
            if (txtManufractureName.Text == "")
            {
                txtManufractureName.Focus();
                objEP.SetError(txtManufractureName, "Enter UserForMaster Name");
                return true;
            }
            else
                return false;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                DialogResult dr;
                dr = MessageBox.Show("Do yo want to delete this record?", "Delete Record", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    FlagDelete = true;
                    SaveDB();
                    ClearAll();
                    FillGrid();
                    objRL.ShowMessage(9, 1);
                }
                else
                    ClearAll();
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }
        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            txtManufractureName.Text = "";
            SearchTag = false;
            txtManufractureName.Focus();
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
                    txtManufractureName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
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
        private void Manufracture_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            txtManufractureName.Focus();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }
    }
}
