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
    public partial class PlaceMaster : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        DesignLayer objDL = new DesignLayer();

        bool FlagDelete = false;
        int RowCount_Grid = 0, CurrentRowIndex = 0, TableID = 0;
        bool SearchTag = false;

        public PlaceMaster()
        {
            InitializeComponent();
            objDL.SetDesignMaster(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_PLACEMASTER);
        }

        private void Rack_Load(object sender, EventArgs e)
        {
            objRL.Add_Tool_Tip(btnSave, btnClear, btnDelete, btnExit);
            ClearAll();
            FillGrid();

            txtPlaceName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                //if (!AlreadyExist())
                //{
                    SaveDB();
                    FillGrid();
                    ClearAll();
                    objRL.ShowMessage(7, 1);
                //}
                //else
                //{
                //    objRL.ShowMessage(12, 9);
                //    return;
                //}
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                DialogResult dr;
                dr = objRL.Delete_Record_Show_Message();

                if (dr == DialogResult.Yes)
                {
                    FlagDelete = true;
                    SaveDB();
                }
            }
            else
            {
                objRL.ShowMessage(17, 4);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll()
        {
            objEP.Clear();
            TableID = 0;
            txtPlaceName.Text = "";
            txtPlaceName.Focus();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtPlaceName.Text == "")
            {
                objEP.SetError(txtPlaceName , "Enter Rack Number");
                txtPlaceName.Focus();
                return true;
            }
            else
                return false;
        }

        protected bool CheckExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from PlaceMaster where CancelTag=0 and PlaceName='" + txtPlaceName.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();

            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        protected void SaveDB()
        {
            if (!Validation())
            {
                if (!CheckExist())
                {
                    if (TableID != 0)
                    {
                        if (FlagDelete)
                            objBL.Query = "update PlaceMaster set CancelTag=1 where ID=" + TableID + "";
                        else
                            objBL.Query = "update PlaceMaster set PlaceName='" + txtPlaceName.Text + "',UserId= " + BusinessLayer.UserId_Static + " where CancelTag=0 and ID=" + TableID + "";
                    }
                    else
                        objBL.Query = "insert into PlaceMaster(PlaceName,UserId) values('" + txtPlaceName.Text + "', " + BusinessLayer.UserId_Static + ")";

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

        protected bool AlreadyExist()
        {
            DataSet ds = new DataSet();
            objBL.Query = "select ID from PlaceMaster where CancelTag=0 and Trim(PlaceName)='" + txtPlaceName.Text + "' and ID <> " + TableID + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        protected void FillGrid()
        {
            try
            {
                if (!SearchTag)
                    objBL.Query = "select  ID,PlaceName as [Place Name] from PlaceMaster RM  where RM.CancelTag=0 order by PlaceName";
                else
                    objBL.Query = "select  ID,PlaceName as [Place Name] from PlaceMaster RM  where RM.CancelTag=0 and RM.PlaceName like '%" + txtSearch.Text + "%'  order by PlaceName";

                DataSet ds = new DataSet();
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblTotalCount.Text = "Total Count-" + ds.Tables[0].Rows.Count;
                    dgvRack.DataSource = ds.Tables[0];
                    dgvRack.Columns[0].Visible = false;
                    dgvRack.Columns[1].Width = 500;
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        int SrNo = 1;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
                SearchTag = true;
            else
                SearchTag = false;

            FillGrid();
        }

        private void dgvRack_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                RowCount_Grid = dgvRack.Rows.Count;
                CurrentRowIndex = dgvRack.CurrentRow.Index;

                if (RowCount_Grid >= 0 && CurrentRowIndex >= 0)
                {
                    ClearAll();
                    TableID = Convert.ToInt32(dgvRack.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtPlaceName.Text = dgvRack.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void txtPlaceName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }
    }
}
