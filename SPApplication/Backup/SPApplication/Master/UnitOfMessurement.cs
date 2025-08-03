using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayerUtility;

namespace SPApplication
{
    public partial class UnitOfMessurement : Form
    {
        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        int TableId = 0;
        bool DeleteFlag = false;
        string ExecuteType = "";
        bool SearchFlag = false;

        public UnitOfMessurement()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.LBL_HEADER_UOM);
        }

        private void UnitOfMessurement_Load(object sender, EventArgs e)
        {
            try
            {
                
                FillGrid();
                ClearAll();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save_Update_Delete();
            }
            catch (Exception ex1) { MessageBox.Show(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteFlag = true;
                Save_Update_Delete();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClearAll()
        {
            TableId = 0;
            objEP.Clear();
            DeleteFlag = false;
            SearchFlag = false;
            ExecuteType = "";
            txtUnitOfMessurement.Text = "";
            txtUnitOfMessurement.Focus();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtUnitOfMessurement.Text == "")
            {
                objEP.SetError(txtUnitOfMessurement, "Enter unit of messurement");
                txtUnitOfMessurement.Focus();
                return true;
            }
            else
                return false;
        }

        private bool AlreadyExist()
        {
            string ExistValue = txtUnitOfMessurement.Text.Replace(" ","");
            DataSet ds = new DataSet();
            objBL.Query = "select ID from UOM where CancelTag=0 and Trim(UnitOfMessurement)='" + ExistValue + "' and ID <> " + TableId + "";
            ds = objBL.ReturnDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void Save_Update_Delete()
        {
            try
            {
                if (!Validation())
                {
                    if (DeleteFlag == false && TableId != 0 || TableId == 0)
                    {
                        if (AlreadyExist())
                        {
                            objRL.ShowMessage(12, 5);
                            return;
                        }
                    }

                    if (TableId != 0)
                    {
                        if (DeleteFlag == true)
                        {
                            objBL.Query = "update UOM set CancelTag=1 where ID=" + TableId + "";
                            ExecuteType = "Delete";
                        }
                        else
                        {
                            if (!AlreadyExist())
                            {
                                objBL.Query = "update UOM set UnitOfMessurement='" + txtUnitOfMessurement.Text + "',UserId="+BusinessLayer.UserId_Static+" where ID=" + TableId + "";
                                ExecuteType = "Update";
                            }
                        }
                    }
                    else
                    {
                        objBL.Query = "insert into UOM(UnitOfMessurement,UserId) values('" + txtUnitOfMessurement.Text + "'," + BusinessLayer.UserId_Static + ")";
                        ExecuteType = "Save";
                    }

                    int Result = objBL.Function_ExecuteNonQuery();
                    if (Result > 0)
                    {
                        if (ExecuteType == "Save")
                            objRL.ShowMessage(7, 1);
                        else if (ExecuteType == "Update")
                            objRL.ShowMessage(8, 1);
                        else
                            objRL.ShowMessage(9, 1);

                        FillGrid();
                        ClearAll();
                    }
                }
                else
                {
                    objRL.ShowMessage(11, 4);
                    return;
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void FillGrid()
        {
            try
            {
                if(SearchFlag == false)
                    objBL.Query = "select U.ID, (SELECT COUNT(*) FROM UOM U2 WHERE U2.ID <= U.ID and U2.CancelTag=0 and U.CancelTag=0) AS SrNo,U.UnitOfMessurement as [Unit of Messurement] from UOM U where U.CancelTag=0 order by U.ID";
                else
                    objBL.Query = "select U.ID, (SELECT COUNT(*) FROM UOM U2 WHERE U2.ID <= U.ID and U2.CancelTag=0 and U.CancelTag=0) AS SrNo,U.UnitOfMessurement as [Unit of Messurement] from UOM U where U.UnitOfMessurement like '%" + txtSearchUnitOfMessurement.Text + "%' and U.CancelTag=0 order by U.ID";

                DataSet ds = new DataSet();
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblTotalCount.Text = "Total Count-" + ds.Tables[0].Rows.Count;
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[2].Width = 660;
                }
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ClearAll();
                TableId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtUnitOfMessurement.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void txtUnitOfMessurement_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtUnitOfMessurement.Text != "")
            {
                if (e.KeyCode == Keys.Enter)
                    btnSave.Focus();
            }
        }

        private void txtSearchUnitOfMessurement_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchUnitOfMessurement.Text != "")
                SearchFlag = true;
            else
                SearchFlag = false;

            FillGrid();
        }
    }
}
