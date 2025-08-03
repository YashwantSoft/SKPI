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
    public partial class ItemOld : Form
    {
        public ItemOld()
        {
            InitializeComponent();
            objBL.Set_Design_All(this, lblHeader, btnSave, btnClear, btnDelete, btnExit, BusinessResources.FRM_ITEM);
        } 

        BusinessLayer objBL = new BusinessLayer();
        ErrorProvider objEP = new ErrorProvider();
        RedundancyLogics objRL = new RedundancyLogics();
        int TableId = 0;
        bool DeleteFlag = false;
        bool SearchFlag = false;
        string ExecuteType = "";

        private void btnAddUOM_Click(object sender, EventArgs e)
        {
            UnitOfMessurement objForm = new UnitOfMessurement();
            objForm.ShowDialog(this);
            objRL.Fill_UOM(cmbUOM);
            cmbUOM.Focus();
        }

        private void ItemMaster_Load(object sender, EventArgs e)
        {
            try
            {
                objRL.Fill_UOM(cmbUOM);
                
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
            txtItemName.Text = "";
            txtItemCode.Text = "";
            txtHSNCode.Text = "";
            txtDescription.Text = "";
            cmbUOM.SelectedIndex = -1;
            txtPrice.Text = "";
            txtSearchItemName.Text = "";
            txtItemName.Focus();
        }

        private bool Validation()
        {
            objEP.Clear();
            if (txtItemName.Text == "")
            {
                objEP.SetError(txtItemName, "Enter Item Name");
                txtItemName.Focus();
                return true;
            }
            else if (cmbUOM.SelectedIndex == -1)
            {
                objEP.SetError(cmbUOM, "Select unit of messurement");
                cmbUOM.Focus();
                return true;
            }
            else if (txtPrice.Text == "")
            {
                objEP.SetError(txtPrice, "Enter unit of messurement");
                txtPrice.Focus();
                return true;
            }
            else
                return false;
        }

        private bool AlreadyExist()
        {
            string ExistValue = txtItemCode.Text.Replace(" ", "");
            DataSet ds = new DataSet();
            objBL.Query = "select ID from Item where CancelTag=0 and Trim(ItemName)='" + txtItemName.Text + "' and ID <> " + TableId + "";
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
                            objBL.Query = "update Item set CancelTag=1 where ID=" + TableId + "";
                            ExecuteType = "Delete";
                        }
                        else
                        {
                            if (!AlreadyExist())
                            {
                                objBL.Query = "update Item set ItemName='" + txtItemName.Text + "',ItemCode='"+txtItemCode.Text+"',HSNCode='" + txtHSNCode.Text + "',Description='" + txtDescription.Text + "',UOMID=" + cmbUOM.SelectedValue + ",Price='" + txtPrice.Text + "',UserId=" + BusinessLayer.UserId_Static + " where ID=" + TableId + "";
                                ExecuteType = "Update";
                            }
                        }
                    }
                    else
                    {
                        objBL.Query = "insert into Item(ItemName,ItemCode,HSNCode,Description,UOMID,Price,UserId) values('" + txtItemName.Text + "','"+txtItemCode.Text+"','" + txtHSNCode.Text + "','" + txtDescription.Text + "'," + cmbUOM.SelectedValue + ",'" + txtPrice.Text + "'," + BusinessLayer.UserId_Static + ")";
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
                if (SearchFlag == false)
                    objBL.Query = "select I.ID, (SELECT COUNT(*) FROM Item I2 WHERE I2.ID <= I.ID and I.CancelTag=0 and I2.CancelTag=0) AS SrNo,I.ItemName as [Item Name],I.ItemCode,I.HSNCode,I.Description,I.UOMID,U.UnitOfMessurement as [UOM],I.Price from Item I inner join UOM U on U.ID=I.UOMID where I.CancelTag=0 and U.CancelTag=0 order by I.ID";
                else
                    objBL.Query = "select I.ID, (SELECT COUNT(*) FROM Item I2 WHERE I2.ID <= I.ID and I.CancelTag=0 and I2.CancelTag=0) AS SrNo,I.ItemName as [Item Name],I.ItemCode,I.HSNCode,I.Description,I.UOMID,U.UnitOfMessurement as [UOM],I.Price from Item I inner join UOM U on U.ID=I.UOMID where I.ItemName like '%" + txtSearchItemName.Text + "%' and I.CancelTag=0 and U.CancelTag=0 order by I.ID";
                
                DataSet ds = new DataSet();
                ds = objBL.ReturnDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblTotalCount.Text = "Total Count-" + ds.Tables[0].Rows.Count;
                    dataGridView1.DataSource = ds.Tables[0];
                    //dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[2].Width = 300;
                    dataGridView1.Columns[3].Width = 200;
                    dataGridView1.Columns[5].Width = 100;
                    dataGridView1.Columns[6].Width = 100;
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
                txtItemName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtItemCode.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtHSNCode.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                cmbUOM.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            catch (Exception ex1) { objRL.ErrorMessge(ex1.ToString()); }
            finally { GC.Collect(); }
        }

        private void txtSearchItemName_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchItemName.Text != "")
                SearchFlag = true;
            else
                SearchFlag = false;

            FillGrid();
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDescription.Focus();
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbUOM.Focus();
        }

        private void cmbUOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPrice.Focus();
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.Focus();
        }

        

        private void txtProfitMargin_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCost_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
