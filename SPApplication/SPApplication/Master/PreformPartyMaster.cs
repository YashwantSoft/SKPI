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
    public partial class PreformPartyMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        public PreformPartyMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PREFORMPARTY);
        }

        private void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update PreformPartyMaster set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update PreformPartyMaster set City='" + txtPreformParty.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into PreformPartyMaster(PreformParty,UserId) values('" + txtPreformParty.Text + "'," + BusinessLayer.UserId_Static + ")";

                    if (objBL.Function_ExecuteNonQuery() > 0)
                    {
                        if(!FlagDelete)
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

        private void PreformPartyMaster_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            txtPreformParty.Focus();
        }

        private void ClearAll()
        {
            TableID = 0;
            txtPreformParty.Text = "";
            txtSearch.Text = "";
            txtPreformParty.Focus();
        }
 
        protected bool Validation()
        {
            objEP.Clear();
            if (txtPreformParty.Text == "")
            {
                txtPreformParty.Focus();
                objEP.SetError(txtPreformParty, "Enter Preform Party Name");
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
                    txtPreformParty.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
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
                objBL.Query = "select ID,PreformParty from PreformPartyMaster where CancelTag=0";
            else
                objBL.Query = "select ID,PreformParty from PreformPartyMaster where CancelTag=0 and PreformParty like '%" + txtSearch.Text + "%'";

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
            objBL.Query = "select ID from PreformPartyMaster where CancelTag=0 and PreformParty='" + txtPreformParty.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
       
    }
}
