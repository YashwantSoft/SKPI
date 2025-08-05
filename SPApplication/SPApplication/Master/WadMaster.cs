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
    public partial class WadMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();      //Own Developed Class
        ErrorProvider objEP = new ErrorProvider();      //System Class
        RedundancyLogics objRL = new RedundancyLogics();  //Own Developed Class
        DesignLayer objDL = new DesignLayer(); //Own Developed Class

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        public WadMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_WADMASTER);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FlagDelete = false;
            SaveDB();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void WadMaster_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillGrid();
            txtWadName.Focus();
        }

        private void ClearAll()
        {
            btnDelete.Enabled = false;
            Result = 0;
            FlagDelete = false;
            objEP.Clear();
            TableID = 0;
            txtWadName.Text = "";
            txtWadName.Text = "";
            txtWadName.Text = "";
            txtWadName.Text = "";
            txtWadName.Text = "";
            txtWadName.Text = "";
            txtNote.Text = "";
            txtWadName.Focus();
        }

        int Result = 0;
        string AposValue = string.Empty;

        protected void SaveDB()
        {
            Result = 0; AposValue = string.Empty;
            if (!Validation())
            {
                if (!CheckExist())
                {
                    AposValue = txtWadName.Text;
                    //'" + ItemName.Replace("'", "''") + "',

                    if (TableID != 0)
                        if (FlagDelete)
                            objBL.Query = "update WadMaster set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update WadMaster set WadName='" + AposValue.Replace("'", "''") + "',Wad='" + txtNote.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableID + "";
                    else
                        objBL.Query = "insert into WadMaster(WadName,Note,UserId) values('" + AposValue.Replace("'", "''") + "','" + txtNote.Text + "'," + BusinessLayer.UserId_Static + ")";

                    Result = objBL.Function_ExecuteNonQuery();
                    if (Result > 0)
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

        protected bool Validation()
        {
            objEP.Clear();
            if (txtWadName.Text == "")
            {
                txtWadName.Focus();
                objEP.SetError(txtWadName, "Enter Wad Name");
                return true;
            }
            else
                return false;
        }

        protected void FillGrid()
        {
            dataGridView1.DataSource = null;
            DataSet ds = new DataSet();

            if (!SearchTag)
                objBL.Query = "select ID,WadName,Note from WadMaster where CancelTag=0";
            else
                objBL.Query = "select ID,WadName,Note from WadMaster where CancelTag=0 and WadName like '%" + txtSearch.Text + "%'";

            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 400;
                dataGridView1.Columns[2].Width = 150;
                lblTotalCount.Text = "Total Count: " + ds.Tables[0].Rows.Count;
            }
        }

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from WadMaster where CancelTag=0 and WadName='" + txtWadName.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
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
                    txtWadName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtNote.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
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
    }
}
